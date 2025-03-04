

namespace AplicacionPlantilla.Models
{
    public class Carta
    {
        public required string Nombre { get; set; }
        public required string Destinatario { get; set; }
        public required string CargoDestinatario { get; set; }
        public required string EmpresaDestinatario { get; set; }
        public required string DireccionDestinatario { get; set; }
        public required string CiudadDestinatario { get; set; }
        public required string Remitente { get; set; }
        public required string DireccionRemitente { get; set; }
        public required string EmailRemitente { get; set; }
        public required string TelefonoRemitente { get; set; }
        public DateTime Fecha { get; set; }
        public required string Contenido { get; set; }

        public required List<Carta> cartas { get; set; }

        public Carta(
           string nombre,
           string destinatario,
           string cargoDestinatario,
           string empresaDestinatario,
           string direccionDestinatario,
           string ciudadDestinatario,
           string remitente,
           string direccionRemitente,
           string emailRemitente,
           string telefonoRemitente,
           DateTime fecha,
           string contenido,
           List<Carta> cartasList = null)
        {
            Nombre = nombre;
            Destinatario = destinatario;
            CargoDestinatario = cargoDestinatario;
            EmpresaDestinatario = empresaDestinatario;
            DireccionDestinatario = direccionDestinatario;
            CiudadDestinatario = ciudadDestinatario;
            Remitente = remitente;
            DireccionRemitente = direccionRemitente;
            EmailRemitente = emailRemitente;
            TelefonoRemitente = telefonoRemitente;
            Fecha = fecha;
            Contenido = contenido;
            cartas = cartasList;

        }

        public Carta()
        {
        }
    }
}
