# ProyectoJuntaBackend

# Proyecto ASP.NET Core - Generación de PDFs con Handlebars, iText y SelectPdf

Este proyecto en **ASP.NET Core** proporciona una API que genera archivos PDF a partir de plantillas HTML. El sistema utiliza el motor de plantillas **Handlebars** para procesar HTML dinámico y luego convierte el HTML a PDF utilizando las librerías **iText 7** (con `HtmlConverter`) y **SelectPdf**.

## Descripción

Este proyecto permite generar documentos PDF a partir de plantillas HTML. La API proporciona varias rutas para crear, actualizar y generar PDFs de plantillas dinámicas.

### Funcionalidades
- Cargar plantillas desde la base de datos o archivos.
- Procesar plantillas HTML con datos dinámicos utilizando **Handlebars**.
- Generar PDFs desde plantillas HTML con dos métodos: usando **iText 7** y **SelectPdf**.
- Guardar y administrar plantillas de cartas o documentos.

## Requisitos

- .NET 6 o superior
- Paquetes NuGet:
  - `Handlebars.Net`
  - `itext7`
  - `itext7.pdfhtml`
  - `SelectPdf`

## Instalación

1. Clona o descarga el repositorio:

    ```bash
    git clone https://github.com/ManuelAlexanderDeLaCruz/Proyect-Junta.git
    ```

2. Navega hasta la carpeta del proyecto y restaura los paquetes NuGet:

    ```bash
    cd DataBase
    dotnet restore
    ```

3. Ejecuta la aplicación:

    ```bash
    dotnet run
    ```

## Rutas de la API

### 1. **GET /api/plantillas**

Obtiene todas las plantillas disponibles en la base de datos.

**Ejemplo de petición:**

```http
GET http://localhost:5199/api/plantillas

Respuesta: 
[
    {
        "id": 1,
        "contenido": "<html><body><h1>Hola, {{nombre}}!</h1></body></html>",
        "fechaCreacion": "2025-03-06T00:00:00",
        "fechaModificacion": "2025-03-06T00:00:00"
    }
]

2. POST /api/plantillas/generate-pdf
Genera un archivo PDF a partir de una plantilla HTML y datos JSON proporcionados.

Ejemplo de petición: 

POST http://localhost:5199/api/plantillas/generate-pdf
Content-Type: application/json

{
  "templateId": 1,
  "jsonData": "{\"nombre\":\"Juan\"}"
}

Respuesta:
{
    "PdfBase64": "JVBERi0xLjQKJcKlVqVYmVB0F5T9oKyzM6Ww=="
}

3. POST /api/plantillas/generate-pdf-selectpdf
Genera un archivo PDF usando SelectPdf a partir de una plantilla HTML y datos JSON proporcionados.

Ejemplo de petición:

POST http://localhost:5199/api/plantillas/generate-pdf-selectpdf
Content-Type: application/json

{
  "templateId": 1,
  "jsonData": "{\"nombre\":\"Ana\", \"edad\": 25}"
}

Respuesta:
{
    "PdfBase64": "JVBERi0xLjQKJcKlVqVYmVB0F5T9oKyzM6Ww=="
}

Guarda una nueva plantilla de carta o documento en la base de datos.

Ejemplo de petición:

POST http://localhost:5199/api/plantillas/guardar-carta
Content-Type: application/json

{
  "contenido": "<html><body><h1>Querido {{nombre}}</h1></body></html>"
}


Respuesta:

{
    "mensaje": "Carta guardada exitosamente",
    "plantilla": {
        "id": 1,
        "contenido": "<html><body><h1>Querido {{nombre}}</h1></body></html>",
        "fechaCreacion": "2025-03-06T00:00:00",
        "fechaModificacion": "2025-03-06T00:00:00"
    }
}

Lógica de Generación de PDF
Procesamiento de Plantillas
En el proyecto, las plantillas HTML son procesadas usando el motor de plantillas Handlebars. Esto permite insertar datos dinámicos en las plantillas, como nombres, fechas, etc.


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


Generación de PDF con iText 7
iText 7 se utiliza para convertir el HTML procesado en un archivo PDF. Se utiliza la clase HtmlConverter de iText7.pdfhtml para esta conversión.
using (var memoryStream = new MemoryStream())
{
    var writer = new PdfWriter(memoryStream);
    var pdfDoc = new PdfDocument(writer);
    var properties = new ConverterProperties();
    
    HtmlConverter.ConvertToPdf(processedHtml, pdfDoc, properties);
    pdfDoc.Close();

    string base64Pdf = Convert.ToBase64String(memoryStream.ToArray());
    return Ok(new { PdfBase64 = base64Pdf });
}

  Generación de PDF con SelectPdf
También se utiliza SelectPdf como opción alternativa para convertir el HTML en PDF. Esta librería proporciona un método sencillo para convertir una cadena HTML a un archivo PDF.

    var converter = new HtmlToPdfConverter();
var doc = converter.ConvertHtmlString(processedHtml);
byte[] pdfBytes = doc.Save();
doc.Close();

string base64Pdf = Convert.ToBase64String(pdfBytes);
return Ok(new { PdfBase64 = base64Pdf });

Configuración
appsettings.json
Este archivo contiene la cadena de conexión a la base de datos SQL Server:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=DESKTOP-BV5JCDI;Database=PlantillasDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

launchSettings.json
Este archivo contiene las configuraciones de inicio para el proyecto, definiendo las URL de acceso a la aplicación y los perfiles de ejecución:


{
  "$schema": "http://json.schemastore.org/launchsettings.json",
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "https://localhost:7186;http://localhost:54495",
      "sslPort": 44360
    }
  },
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5199;https://localhost:7186",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5199",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}



Contribuciones
Si deseas contribuir, por favor realiza un fork del repositorio, haz tus cambios y abre un pull request. Asegúrate de escribir pruebas para nuevas funcionalidades.

  
### Cambios y mejoras realizadas:
1. **Detalles de la API**: Añadí explicaciones detalladas sobre las rutas de la API y ejemplos de peticiones y respuestas.
2. **Lógica de la generación de PDF**: Describí cómo se procesa el HTML con Handlebars y se convierte en PDF utilizando tanto **iText 7** como **SelectPdf**.
3. **Configuración**: Incluí los archivos `appsettings.json` y `launchSettings.json` para que los usuarios comprendan cómo configurar la aplicación.
   
Este README ahora proporciona una visión más completa del proyecto y cómo utilizar la API de forma efectiva.

