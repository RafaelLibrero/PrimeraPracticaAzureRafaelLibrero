using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrimeraPracticaAzureClient.Models;
using System.Net.Http.Headers;
using System.Text;

namespace PrimeraPracticaAzureClient.Services
{
    public class ServiceApiPracticaAzure
    {
        private string UrlApiPracticaAzure;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiPracticaAzure(IConfiguration configuration)
        {
            this.UrlApiPracticaAzure =
                configuration.GetValue<string>("ApiUrls:ApiPracticaAzure");
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiPracticaAzure);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<PersonajeSerie>> GetPersonajeSeriesAsync()
        {
            string request = "api/personajesseries";
            List<PersonajeSerie> personajes = await
                this.CallApiAsync<List<PersonajeSerie>>(request);
            return personajes;
        }

        public async Task<List<PersonajeSerie>> GetPersonajesFromSerieAsync(string serie)
        {
            string request = "api/personajesseries/personajesseries/" + serie;
            List<PersonajeSerie> personajes = await
                this.CallApiAsync<List<PersonajeSerie>>(request);
            return personajes;
        }

        public async Task<List<string>> GetSeriesAsync()
        {
            string request = "api/personajesseries/series";
            List<string> series = await
                this.CallApiAsync<List<string>>(request);
            return series;
        }

        public async Task<PersonajeSerie> FindPersonajeSeriesAsync(int id)
        {
            string request = "api/personajesseries/" + id;
            PersonajeSerie personaje = await
                this.CallApiAsync<PersonajeSerie>(request);
            return personaje;
        }

        public async Task<PersonajeSerie> InsertPersonajeSerieAsync(PersonajeSerie personaje)
        {
            string request = "api/personajesseries";
            string jsonContent = JsonConvert.SerializeObject(personaje);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            using (HttpClient client =  new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiPracticaAzure);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    PersonajeSerie personajeSerie = await content.ReadAsAsync<PersonajeSerie>();
                    return personajeSerie;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task UpdatePersonajeSerieAsync(PersonajeSerie personaje)
        {
            string request = "api/personajesseries";
            string jsonContent = JsonConvert.SerializeObject(personaje);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiPracticaAzure);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.PutAsync(request, content);
            }
        }

        public async Task DeletePersonajeSerieAsync(int id)
        {
            string request = "api/personajesseries/" + id;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiPracticaAzure);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = 
                    await client.DeleteAsync(request);
            }
        }
    }
}

