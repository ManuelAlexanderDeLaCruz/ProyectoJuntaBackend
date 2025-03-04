using AplicacionPlantilla.Models;
using AplicacionPlantilla.Data;
using Microsoft.EntityFrameworkCore;


public class PlantillaRepository
{
    private readonly PlantillasDbContext _context;
    public PlantillaRepository(PlantillasDbContext context)
    {
        _context = context;
    }

    public async Task<List<Plantilla>> GetAll()
    {
        return await _context.Plantillas.ToListAsync();
    }

    public async Task<Plantilla> GetById(int id)
    {
        return await _context.Plantillas.FindAsync(id);
    }

    public async Task Add(Plantilla plantilla)
    {

        if (plantilla == null)
        {
            throw new ArgumentNullException(nameof(plantilla), "The plantilla cannot be null.");
        }
        await _context.Plantillas.AddAsync(plantilla);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Plantilla plantilla)
    {
        if (plantilla == null)
        {
            throw new ArgumentNullException(nameof(plantilla), "The plantilla cannot be null.");
        }

        var existingPlantilla = await _context.Plantillas.FindAsync(plantilla.Id);

        if (existingPlantilla == null)
        {
            throw new KeyNotFoundException($"Plantilla with ID {plantilla.Id} not found.");
        }

        // Update properties of existingPlantilla with values from plantilla
        plantilla.Nombre = plantilla.Nombre;
        plantilla.Contenido = plantilla.Contenido;
        plantilla.FechaCreacion = plantilla.FechaCreacion;
        plantilla.FechaModificacion = plantilla.FechaModificacion;
        plantilla.Descripcion = plantilla.Descripcion;
        plantilla.Tipo = plantilla.Tipo;
        plantilla.Version = plantilla.Version;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var plantilla = await _context.Plantillas.FindAsync(id);
        if (plantilla != null)
        {
            _context.Plantillas.Remove(plantilla);
            await _context.SaveChangesAsync();
        }
        else
        {
            
            throw new KeyNotFoundException($"Plantilla with ID {id} not found.");
        }
    }

    
}