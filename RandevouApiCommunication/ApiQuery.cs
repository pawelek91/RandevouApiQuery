﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RandevouApiCommunication
{
    internal abstract class ApiQuery
    {
        private static string endpoint = ApiConfig.ApiEndpoint;

        private  StringContent AsJson(object o)
             => new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");

        private TResult FromJson<TResult>(string queryResult)
            => JsonConvert.DeserializeObject<TResult>(queryResult);

        protected static string BuildAddress(string path, string id = "")
        {
            string result = endpoint + "/" + path;

            result = result.Replace("{id}", id);

            while (result.IndexOf("//","http://".Length+1) > -1)
                result = result.Replace("//", "/");

            result = "http://" + result;
            return result;
        }

        protected int Post<T>(string address, T dto)
        {
            string endpoint = BuildAddress(address);
            using (HttpClient client = new HttpClient())
            {
                var json = AsJson(dto);
                var postReult = client.PostAsync(endpoint, json).Result;
                if (!postReult.IsSuccessStatusCode)
                    throw new HttpRequestException(string.Format("Query on {0} not succeeded",endpoint));

                if(!int.TryParse(postReult.Content.ReadAsStringAsync().Result,out var id))
                    throw new HttpRequestException( "Not correct answer from API");

                return id;
            }
        }

        protected TResult PostSpecific<TResult, TQueryDto>(string address, TQueryDto dto)
        {
            string endpoint = BuildAddress(address);
            using (HttpClient client = new HttpClient())
            {
                var json = AsJson(dto);
                var postReult = client.PostAsync(endpoint, json).Result;

                if (!postReult.IsSuccessStatusCode)
                    throw new HttpRequestException(string.Format("Query on {0} not succeeded", endpoint));

                var resultToParse = postReult.Content.ReadAsStringAsync().Result;
                var result = FromJson<TResult>(resultToParse);
                return result;
            }
        }
        protected void Update<T>(string address, T dto, string id = "")
        {
            string endpoint = BuildAddress(address, id);
            using (HttpClient client = new HttpClient())
            {
                var json = AsJson(dto);
                var postReult = client.PatchAsync(endpoint, json).Result;
                if (!postReult.IsSuccessStatusCode)
                    throw new HttpRequestException(string.Format("Query on {0} not succeeded", endpoint));
            }
        }

        protected void Set<T>(string address, T dto, string id = "")
        {
            string endpoint = BuildAddress(address, id);
            using (HttpClient client = new HttpClient())
            {
                var json = AsJson(dto);
                var result = client.PutAsync(endpoint, json).Result;
                if(!result.IsSuccessStatusCode)
                    throw new HttpRequestException(string.Format("Query on {0} not succeeded", endpoint));
            }
        }

        protected void Delete(string address, int id)
        {
            string endpoint = BuildAddress(address, id.ToString());
            using (HttpClient client = new HttpClient())
            {
                var deleteResult = client.DeleteAsync(endpoint);
                if (!deleteResult.Result.IsSuccessStatusCode)
                    throw new HttpRequestException(string.Format("Query on {0} not succeeded", endpoint));
            }
        }

        protected async Task<T> Query<T>(string address, string id = null) where T : class
        {
            using (HttpClient client = new HttpClient())
            {
                string endpoint = BuildAddress(address, id);
                var response = await client.GetAsync(endpoint);

                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null;
                }

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException(string.Format("Query on {0} not succeeded", endpoint));

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }

     
    }

    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string endpoint, HttpContent iContent)
        {
            var method = new HttpMethod("PATCH");
            Uri requestUri = new Uri(endpoint);
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = iContent
            };

            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.SendAsync(request);
            }
            catch (TaskCanceledException e)
            {
                throw new HttpRequestException(e.Message);
            }

            return response;
        }
    }
}
