using AplicacionPlantilla.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using HandlebarsDotNet;
using iText.Html2pdf;                   
using iText.Kernel.Pdf;               
using System.IO;
using System.Collections.Generic;
using System;

using SelectPdf;                        
using HtmlToPdfConverter = SelectPdf.HtmlToPdf;  
using PdfDocumentSelect = SelectPdf.PdfDocument;  

[ApiController]
[Route("api/plantillas")]
public class PlantillaController : ControllerBase
{
    private readonly PlantillaRepository repository;

    public PlantillaController(PlantillaRepository repository)
    {
        this.repository = repository;
    }

    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await repository.GetAll());
    }

    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var plantilla = await repository.GetById(id);
        if (plantilla == null) return NotFound();
        return Ok(plantilla);
    }

    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Plantilla plantilla)
    {
        plantilla.FechaCreacion = DateTime.UtcNow;
        plantilla.FechaModificacion = DateTime.UtcNow;
        await repository.Add(plantilla);
        return CreatedAtAction(nameof(GetById), new { id = plantilla.Id }, plantilla);
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Plantilla plantilla)
    {
        if (id != plantilla.Id) return BadRequest();
        plantilla.FechaModificacion = DateTime.UtcNow;
        await repository.Update(plantilla);
        return NoContent();
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await repository.Delete(id);
        return NoContent();
    }

    
    [HttpPost("generate-pdf")]
    public async Task<IActionResult> GeneratePdf([FromBody] PdfGenerationRequest request)
    {
        var plantilla = await repository.GetById(request.TemplateId);
        if (plantilla == null) return NotFound("La plantilla no existe.");

        string processedHtml = ProcessTemplate(plantilla.Contenido, request.JsonData);

        using var memoryStream = new MemoryStream();
        var writer = new iText.Kernel.Pdf.PdfWriter(memoryStream);
        var pdfDoc = new iText.Kernel.Pdf.PdfDocument(writer);
        var properties = new ConverterProperties();

        HtmlConverter.ConvertToPdf(processedHtml, pdfDoc, properties);
        pdfDoc.Close();

        string base64Pdf = Convert.ToBase64String(memoryStream.ToArray());
        return Ok(new { PdfBase64 = base64Pdf });
    }

    
    [HttpPost("generate-pdf-selectpdf")]
    public async Task<IActionResult> GeneratePdfSelectPdf([FromBody] PdfGenerationRequest request)
    {
        var plantilla = await repository.GetById(request.TemplateId);
        if (plantilla == null) return NotFound("La plantilla no existe.");

        string processedHtml = ProcessTemplate(plantilla.Contenido, request.JsonData);

        var converter = new HtmlToPdfConverter();
        var doc = converter.ConvertHtmlString(processedHtml);
        byte[] pdfBytes = doc.Save();
        doc.Close();

        string base64Pdf = Convert.ToBase64String(pdfBytes);
        return Ok(new { PdfBase64 = base64Pdf });
    }
    
    [HttpPost("guardar-carta")]
    public async Task<IActionResult> GuardarCarta([FromBody] Plantilla nuevaPlantilla)
    {
        if (nuevaPlantilla == null || string.IsNullOrEmpty(nuevaPlantilla.Contenido))
            return BadRequest("El contenido de la carta es requerido.");

        nuevaPlantilla.FechaCreacion = DateTime.UtcNow;
        nuevaPlantilla.FechaModificacion = DateTime.UtcNow;

        await repository.Add(nuevaPlantilla);
        return Ok(new { mensaje = "Carta guardada exitosamente", plantilla = nuevaPlantilla });
    }

    
    private string ProcessTemplate(string htmlTemplate, string jsonData)
    {
        try
        {
            var template = Handlebars.Compile(htmlTemplate);
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonData);
            return template(data);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al procesar la plantilla: " + ex.Message);
            return $"<html><body><h1>Error: {ex.Message}</h1></body></html>";
        }
    }
}
