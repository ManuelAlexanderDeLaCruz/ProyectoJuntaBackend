using Microsoft.AspNetCore.Mvc;
using SelectPdf;                        
using System.Text.Json;
using iText.Html2pdf;                  
using iText.Kernel.Pdf;                
using System.IO;
using System;
using System.Text;
using AplicacionPlantilla.Models;      

namespace AplicacionPlantilla.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PdfGeneratorController : ControllerBase
    {
        
        [HttpPost("generate")]
        public IActionResult GeneratePdf([FromBody] PdfGenerationRequestDto request)
        {
            try
            {
                if(request?.TemplateId == null)
                {
                    return BadRequest("TemplaId es requerido");
                }
                
                string htmlTemplate = LoadHtmlTemplate(int.Parse(request.TemplateId));

                
                string processedHtml = ProcessTemplate(htmlTemplate, request.JsonData);

                
                using var memoryStream = new MemoryStream();
                var writer = new iText.Kernel.Pdf.PdfWriter(memoryStream);
                var pdfDoc = new iText.Kernel.Pdf.PdfDocument(writer);
                var properties = new ConverterProperties();

                HtmlConverter.ConvertToPdf(processedHtml, pdfDoc, properties);
                pdfDoc.Close();

                
                string base64Pdf = Convert.ToBase64String(memoryStream.ToArray());
                return Ok(new { PdfBase64 = base64Pdf });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 🔹 Cargar la plantilla HTML
        private string LoadHtmlTemplate(int templateId)
        {
            // Implementa la lógica para cargar la plantilla HTML desde un archivo, base de datos, etc.
            // Ejemplo simple:
            return $"<html><body><h1>Template ID: {templateId}</h1><p>{{data}}</p></body></html>";
        }

        // 🔹 Procesar plantilla con JSON
        private string ProcessTemplate(string htmlTemplate, string jsonData)
        {
            try
            {
                // Analiza la cadena JSON
                JsonDocument jsonDocument = JsonDocument.Parse(jsonData);
                JsonElement root = jsonDocument.RootElement;

                // Accede a los valores de las propiedades JSON
                if (root.TryGetProperty("nombre", out JsonElement nombreElement))
                {
                    string nombre = nombreElement.GetString();
                    htmlTemplate = htmlTemplate.Replace("{{nombre}}", nombre);
                }

                if (root.TryGetProperty("edad", out JsonElement edadElement))
                {
                    int edad = edadElement.GetInt32();
                    htmlTemplate = htmlTemplate.Replace("{{edad}}", edad.ToString());
                }

                if (root.TryGetProperty("ciudad", out JsonElement ciudadElement))
                {
                    string ciudad = ciudadElement.GetString();
                    htmlTemplate = htmlTemplate.Replace("{{ciudad}}", ciudad);
                }

                return htmlTemplate;
            }
            catch (Exception ex)
            {
                return $"<html><body><h1>Error: {ex.Message}</h1></body></html>";
            }
        }
    }
}
