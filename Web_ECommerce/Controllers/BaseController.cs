using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Web_Locadora.Controllers
{
    // ─────────────────────────────────────────────────────────────
    //  BaseController
    //  Herdado por todos os controllers do projeto.
    //  Centraliza a comunicação com a Swagger/API via HttpClient.
    //  Quando as outras camadas estiverem prontas, basta o Startup.cs
    //  registrar o HttpClient apontando para a URL correta.
    // ─────────────────────────────────────────────────────────────
    public abstract class BaseController : Controller
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        // Nome do cliente HTTP registrado no Startup.cs / Program.cs
        protected const string API_CLIENT_NAME = "LocadoraApi";

        protected BaseController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // ── Helpers genéricos ────────────────────────────────────

        protected HttpClient CreateClient() => _httpClientFactory.CreateClient(API_CLIENT_NAME);

        /// <summary>GET /api/{entity} → retorna lista</summary>
        protected async Task<List<T>> ApiGetList<T>(string endpoint)
        {
            try
            {
                var client = CreateClient();
                var result = await client.GetFromJsonAsync<List<T>>(endpoint);
                return result ?? new List<T>();
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro ao buscar dados: {ex.Message}";
                return new List<T>();
            }
        }

        /// <summary>GET /api/{entity}/{id} → retorna entidade</summary>
        protected async Task<T?> ApiGetById<T>(string endpoint, int id) where T : class
        {
            try
            {
                var client = CreateClient();
                return await client.GetFromJsonAsync<T>($"{endpoint}/{id}");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro ao buscar registro: {ex.Message}";
                return null;
            }
        }

        /// <summary>POST /api/{entity} → cria novo registro</summary>
        protected async Task<bool> ApiPost<T>(string endpoint, T entity)
        {
            try
            {
                var client = CreateClient();
                var response = await client.PostAsJsonAsync(endpoint, entity);
                if (!response.IsSuccessStatusCode)
                    TempData["Error"] = $"Erro ao salvar: {response.ReasonPhrase}";
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro ao salvar: {ex.Message}";
                return false;
            }
        }

        /// <summary>PUT /api/{entity}/{id} → atualiza registro</summary>
        protected async Task<bool> ApiPut<T>(string endpoint, int id, T entity)
        {
            try
            {
                var client = CreateClient();
                var response = await client.PutAsJsonAsync($"{endpoint}/{id}", entity);
                if (!response.IsSuccessStatusCode)
                    TempData["Error"] = $"Erro ao atualizar: {response.ReasonPhrase}";
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro ao atualizar: {ex.Message}";
                return false;
            }
        }

        /// <summary>DELETE /api/{entity}/{id} → remove registro</summary>
        protected async Task<bool> ApiDelete(string endpoint, int id)
        {
            try
            {
                var client = CreateClient();
                var response = await client.DeleteAsync($"{endpoint}/{id}");
                if (!response.IsSuccessStatusCode)
                    TempData["Error"] = $"Erro ao excluir: {response.ReasonPhrase}";
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro ao excluir: {ex.Message}";
                return false;
            }
        }
    }
}