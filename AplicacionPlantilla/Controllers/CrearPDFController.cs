using AplicacionPlantilla.Models;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;



namespace AplicacionPlantilla.Controllers
{
    public class CrearPDFController : Controller
    {
        public IActionResult Index()
        {
            var carta = new Carta()
            {
                Nombre = "",
                Destinatario = "",
                CargoDestinatario = "",
                EmpresaDestinatario = "",
                DireccionDestinatario = "",
                CiudadDestinatario = "",
                Remitente = "",
                DireccionRemitente = "",
                EmailRemitente = "",
                TelefonoRemitente = "",
                Fecha = DateTime.MinValue,
                Contenido = "",
                cartas = new List<Carta>()



            };

            return new ViewAsPdf("Index", carta)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.Legal,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                FileName = "Mi Carta.pdf"

            };
            
        }
     }
}
