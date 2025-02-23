using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GestionTache.Models;

namespace GestionTache.Services
{
    public class TaskService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:3306/api/tasks"; // Assure-toi que ton API tourne sur ce port

        public TaskService()
        {
            _httpClient = new HttpClient();
        }

        // 🔹 Récupérer toutes les tâches
        public async Task<List<TaskItem>> GetTasksAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TaskItem>>(_baseUrl);
        }

        // 🔹 Ajouter une nouvelle tâche
        public async Task<bool> AddTaskAsync(TaskItem task)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, task);
            return response.IsSuccessStatusCode;
        }

        // 🔹 Supprimer une tâche
        public async Task<bool> DeleteTaskAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
