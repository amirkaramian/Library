using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ExamLibrary.Models;
using ExamLibrary.Module;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ExamLibrary.Service
{
    public class RequestService : IRequestService
    {
        private HttpClient _client;
        private readonly ILogger<RequestService> _logger;

        public RequestService(HttpClient client, ILogger<RequestService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<List<BookModel>> GetBookModel(int categoryId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/books/{categoryId}")
                {
                    Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
                };
                return await _client.Exec<List<BookModel>>(request);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogDebug("error http request exception: " + ex);
                return new List<BookModel>();
            }
            catch (Exception exception)
            {
                _logger.LogDebug("error in get book data: " + exception);
                throw new Exception("library Error");
            }
        }

        public async Task<List<CategoryModel>> GetCategoryModel()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/categories")
                {
                    Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
                };
                return await _client.Exec<List<CategoryModel>>(request);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogDebug("error http request exception: " + ex);
                return new List<CategoryModel>();
            }
            catch (Exception exception)
            {
                _logger.LogDebug("error in get Category data: " + exception);
                throw new Exception("library Error");
            }
        }
    }
}