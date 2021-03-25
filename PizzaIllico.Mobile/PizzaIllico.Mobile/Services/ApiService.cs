using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.Services
{
    public interface IApiService
    {
        Task<TResponse> Get<TResponse>(string url);
        Task<TResponse> Post<TResponse>(string url, string postcontent);
        Task<TResponse> Put<TResponse>(string url, string putcontent);

    }
    
    public class ApiService : IApiService
    {
	    private const string HOST = "https://pizza.julienmialon.ovh/";
        private readonly HttpClient _client = new HttpClient();
        
        public async Task<TResponse> Get<TResponse>(string url)
        {
	        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, HOST + url);

	        HttpResponseMessage response = await _client.SendAsync(request);

	        string content = await response.Content.ReadAsStringAsync();

	        return JsonConvert.DeserializeObject<TResponse>(content);
        }
        public async Task<TResponse> Post<TResponse>(string url, string postcontent)
        {
	        //requete post
	        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, HOST + url);
	        request.Headers.Add("Accept", "application/json");
	        request.Content = new StringContent(postcontent, System.Text.Encoding.UTF8, "application/json");
	        //réponse
	        HttpResponseMessage response = await _client.SendAsync(request);
	        string content = await response.Content.ReadAsStringAsync();
	        return JsonConvert.DeserializeObject<TResponse>(content);
        }
        public async Task<TResponse> Put<TResponse>(string url, string putcontent)
        {
	        //requete put
	        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, HOST + url);
	        request.Content = new StringContent(putcontent, System.Text.Encoding.UTF8, "application/json");
	        //réponse
	        HttpResponseMessage response = await _client.SendAsync(request);
	        string content = await response.Content.ReadAsStringAsync();
	        return JsonConvert.DeserializeObject<TResponse>(content);
        }
    }
}