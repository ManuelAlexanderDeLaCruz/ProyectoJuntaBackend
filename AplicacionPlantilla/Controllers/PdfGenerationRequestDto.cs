namespace AplicacionPlantilla.Controllers
{
    public class PdfGenerationRequestDto
    {
        public string TemplateId { get; internal set; }
        public string JsonData { get; internal set; }
    }
}