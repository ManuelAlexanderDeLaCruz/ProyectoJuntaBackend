using AplicacionPlantilla.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AplicacionPlantilla.Data
{
    public class PlantillasDbContext(DbContextOptions<PlantillasDbContext> options) : DbContext(options)
    {
        public DbSet<Plantilla> Plantillas { get; set; }

        internal object Entry(Plantilla plantilla)
        {
            throw new NotImplementedException();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
