using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI_Books.Model;
using WebAPI_Books.Model.Dto;

namespace WebAPI_Books.Service
{
    public class BookService
    {
        private readonly IHttpClientFactory _httpClient;
        private dynamic client;
        public BookService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            client = _httpClient.CreateClient("baseHttpClient");
        }
        public async Task<List<Books>> getBooks()
        {
            try
            {

                var response = await client.GetAsync("api/v1/Books");
                var statusCode = response.EnsureSuccessStatusCode();

                if (statusCode.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<List<Books>>(responseStream);
                }

                else
                {
                    throw new Exception("Error al obtener listado de libros");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<Books> getBooksById(int id)
        {
            try
            {

                var response = await client.GetAsync($"api/v1/Books/{id}");
                var statusCode = response.EnsureSuccessStatusCode();

                if (statusCode.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<Books>(responseStream);
                }

                else
                {
                    throw new Exception("Error al obtener listado de libros");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<Books> postBook(Books books)
        {
            try
            {
                var bookParams = JsonSerializer.Serialize(books);
                var requestContent = new StringContent(bookParams, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/v1/Books", requestContent);
                var statusCode = response.EnsureSuccessStatusCode();

                if (statusCode.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<Books>(content);
                }
                else
                {

                    throw new Exception("Error al crear registro");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Books> putBook(int id, BookDto books)
        {
            try
            {
                var _getBooksById = getBooksById(id).Result;
                if (_getBooksById==null)
                {
                    throw new Exception("Registro No Existe");
                }
                _getBooksById.title = books.title;
                _getBooksById.description = books.description;
                _getBooksById.publishDate = books.publishDate;
                _getBooksById.excerpt = books.excerpt;
                _getBooksById.publishDate = DateTime.Now;

                var bookParams = JsonSerializer.Serialize(_getBooksById);
                var requestContent = new StringContent(bookParams, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/v1/Books/{id}", requestContent);
                var statusCode = response.EnsureSuccessStatusCode();

                if (statusCode.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<Books>(content);
                }
                else
                {

                    throw new Exception("Error al actualizar registro");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task deleteBook(int id)
        {
            try
            {
                var _getBooksById = getBooksById(id).Result;
                if (_getBooksById == null)
                {
                    throw new Exception("Registro No Existe");
                }

                var response = await client.DeleteAsync($"api/v1/Books/{id}");
                var statusCode = response.EnsureSuccessStatusCode();
                if (statusCode.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    //return JsonSerializer.Deserialize<Books>(content);
                }

                throw new Exception("El registro no pudo ser eliminado, favor verificar!!"); ;

            }
            catch (Exception)
            {

                throw new Exception("Error al eliminar registro");
            }

        }
    }
}
