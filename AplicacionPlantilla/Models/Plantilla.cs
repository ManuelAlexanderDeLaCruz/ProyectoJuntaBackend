namespace AplicacionPlantilla.Models
{
    public class Plantilla
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string? Descripcion { get; set; }
        public string? Tipo { get; set; }
        public required string Version { get; set; }
    }
}
