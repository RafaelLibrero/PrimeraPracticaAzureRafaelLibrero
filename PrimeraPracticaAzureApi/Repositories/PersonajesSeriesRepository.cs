using Microsoft.EntityFrameworkCore;
using PrimeraPracticaAzureApi.Data;
using PrimeraPracticaAzureApi.Models;

namespace PrimeraPracticaAzureApi.Repositories
{
    public class PersonajesSeriesRepository
    {
        private PersonajesSeriesContext context;

        public PersonajesSeriesRepository(PersonajesSeriesContext context)
        {
            this.context = context;
        }

        public async Task<List<PersonajeSerie>> GetPersonajesAsync()
        {
            return await
                this.context.PersonajesSeries.ToListAsync();
        }

        public async Task<List<PersonajeSerie>> GetPersonajesFromSerieAsync(string serie)
        {
            return await
                this.context.PersonajesSeries
                .Where(x => x.Serie == serie).ToListAsync();
        }

        public async Task<PersonajeSerie> FindPersonajeAsync(int idpersonaje)
        {
            return await
                this.context.PersonajesSeries
                .FirstOrDefaultAsync(x => x.IdPersonaje == idpersonaje);
        }

        public async Task<List<string>> GetSeriesAsync()
        {
            return await
                this.context.PersonajesSeries
                .Select(x => x.Serie).Distinct().ToListAsync();
        }

        private async Task<int> GetMaxIdPersonajeAsync()
        {
            if (this.context.PersonajesSeries.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await
                    this.context.PersonajesSeries.MaxAsync(x => x.IdPersonaje) + 1;
            }
        }

        public async Task<PersonajeSerie> InsertPersonajeAsync(PersonajeSerie personaje)
        {
            PersonajeSerie personajeSerie = new PersonajeSerie();
            personajeSerie.IdPersonaje = await this.GetMaxIdPersonajeAsync();
            personajeSerie.Nombre = personaje.Nombre;
            personajeSerie.Imagen = personaje.Imagen;
            personajeSerie.Serie = personaje.Serie;

            this.context.PersonajesSeries.Add(personajeSerie);

            await this.context.SaveChangesAsync();

            return personajeSerie;
        }

        public async Task UpdatePersonajeAsync(PersonajeSerie personaje)
        {
            PersonajeSerie personajeSerie = await this.FindPersonajeAsync(personaje.IdPersonaje);
            personajeSerie.Nombre = personaje.Nombre;
            personajeSerie.Imagen = personaje.Imagen;
            personajeSerie.Serie = personaje.Serie;

            await this.context.SaveChangesAsync();
        }

        public async Task DeletePersonajeAsync(int idpersonaje)
        {
            PersonajeSerie personajeSerie = await this.FindPersonajeAsync(idpersonaje);
            this.context.PersonajesSeries.Remove(personajeSerie);

            await this.context.SaveChangesAsync();
        }
    }
}
