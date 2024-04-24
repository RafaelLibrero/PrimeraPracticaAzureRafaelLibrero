using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeraPracticaAzureApi.Models;
using PrimeraPracticaAzureApi.Repositories;

namespace PrimeraPracticaAzureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajesSeriesController : ControllerBase
    {
        private PersonajesSeriesRepository repo;

        public PersonajesSeriesController(PersonajesSeriesRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonajeSerie>>> Get()
        {
            return await this.repo.GetPersonajesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonajeSerie>> Get(int id)
        {
            PersonajeSerie personaje = await this.repo.FindPersonajeAsync(id);
            if (personaje == null)
            {
                return NotFound();
            }
            return personaje;
        }

        [HttpPost]
        public async Task<ActionResult<PersonajeSerie>> Post(PersonajeSerie personaje)
        {
            await this.repo.InsertPersonajeAsync(personaje);
            return Ok(personaje);
        }

        [HttpPut]
        public async Task<ActionResult> Put(PersonajeSerie personaje)
        {
            PersonajeSerie personajeSerie = await this.repo.FindPersonajeAsync(personaje.IdPersonaje);

            if (personajeSerie == null)
            {
                return NotFound();
            }
            await this.repo.UpdatePersonajeAsync(personaje);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            PersonajeSerie personaje = await this.repo.FindPersonajeAsync(id);
            if (personaje == null)
            {
                return NotFound();
            }
            await this.repo.DeletePersonajeAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route("[action]/{serie}")]
        public async Task<ActionResult<List<PersonajeSerie>>> PersonajesSeries(string serie)
        {
            return await
                this.repo.GetPersonajesFromSerieAsync(serie);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<string>>> Series()
        {
            return await
                this.repo.GetSeriesAsync();
        }
    }
}
