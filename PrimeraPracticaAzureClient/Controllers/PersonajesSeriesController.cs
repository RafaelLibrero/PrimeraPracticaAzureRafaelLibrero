using Microsoft.AspNetCore.Mvc;
using PrimeraPracticaAzureClient.Models;
using PrimeraPracticaAzureClient.Services;

namespace PrimeraPracticaAzureClient.Controllers
{
    public class PersonajesSeriesController : Controller
    {
        private ServiceApiPracticaAzure service;

        public PersonajesSeriesController(ServiceApiPracticaAzure service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<PersonajeSerie> personajes = await this.service.GetPersonajeSeriesAsync();
            return View(personajes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonajeSerie personajeSerie)
        {
            await this.service.InsertPersonajeSerieAsync(personajeSerie);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            PersonajeSerie personaje = await this.service.FindPersonajeSeriesAsync(id);
            return View(personaje);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PersonajeSerie personajeSerie)
        {
            await this.service.UpdatePersonajeSerieAsync(personajeSerie);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersonajeSerieAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> PersonajesSerie()
        {
            ViewData["SERIES"] = await this.service.GetSeriesAsync();
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PersonajesSerie(string serie)
        {
            ViewData["SERIES"] = await this.service.GetSeriesAsync();
            List<PersonajeSerie> personajes = await this.service.GetPersonajesFromSerieAsync(serie);
            return View(personajes);
        }

    }
}
