using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AppMovilProyecto.Models;
using Newtonsoft.Json;

namespace AppMovilProyecto.Services
{
    public class APIService
    {
        public static string _baseUrl;
        public HttpClient _httpClient;

        public APIService()
        {

            _baseUrl = "https://apiusuarios20231208194835.azurewebsites.net";
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        //Usuario

        public async Task<List<User>> getUsuario()
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/User");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var usuario = JsonConvert.DeserializeObject<List<User>>(content);
                    return usuario;
                }
                else
                {
                    // Manejar el error aquí, lanzar una excepción o devolver una lista vacía
                    throw new Exception($"Error al obtener usuarios. StatusCode: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción, imprimir o registrar el error según sea necesario
                Debug.WriteLine($"Error en getUsuario: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Medico>> getMedicos()
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Medico");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var medicos = JsonConvert.DeserializeObject<List<Medico>>(content);
                    return medicos;
                }
                else
                {
                    throw new Exception($"Error al obtener médicos. StatusCode: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en getMedicos: {ex.Message}");
                return null;
            }
        }

        //CITAS

        public async Task<List<Cita>> getCita(string idUsuario)
        {
            var response = await _httpClient.GetAsync($"/api/User/{idUsuario}/citas");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var citas = JsonConvert.DeserializeObject<List<Cita>>(content);
                return citas;
            }
            return null;
        }


        public async Task<Cita> getCita(int IdCita)
        {
            var response = await _httpClient.GetAsync($"/api/Cita/{IdCita}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var cita = JsonConvert.DeserializeObject<Cita>(content);
                return cita;
            }
            return null;
        }


        public async Task<bool> addCita(Cita cita)
        {
            var jsonString = JsonConvert.SerializeObject(cita);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Cita/", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> updateCita(int IdCita, Cita cita)
        {

            var jsonString = JsonConvert.SerializeObject(cita);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/Cita/{IdCita}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> deleteCita(int IdCita)
        {
            var response = await _httpClient.DeleteAsync($"/api/Cita/{IdCita}");

            return response.IsSuccessStatusCode;
        }

        public async Task<User> IniciarSesion(User user)
        {
            var response = await _httpClient.PostAsync("/api/User/login", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var userContent = await response.Content.ReadAsStringAsync();
                var authenticatedUser = JsonConvert.DeserializeObject<User>(userContent);

                // Guarda el IdUsuario en las preferencias
                Preferences.Set("idusuario", authenticatedUser.IdUsuario);

                return authenticatedUser;
            }
            else
            {
                return null;
            }
        }





    }
}

