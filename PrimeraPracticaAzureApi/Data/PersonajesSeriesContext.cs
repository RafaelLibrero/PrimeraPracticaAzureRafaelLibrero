using Microsoft.EntityFrameworkCore;
using PrimeraPracticaAzureApi.Models;

namespace PrimeraPracticaAzureApi.Data
{
    public class PersonajesSeriesContext: DbContext
    {
        public PersonajesSeriesContext(DbContextOptions<PersonajesSeriesContext> options) : base(options) { }

        public DbSet<PersonajeSerie> PersonajesSeries { get; set; }
    }
}
