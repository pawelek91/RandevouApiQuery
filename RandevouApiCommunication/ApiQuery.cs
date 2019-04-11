using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RandevouApiCommunication.Authentication;

namespace RandevouApiCommunication
{
    internal abstract class ApiQuery
    {
        private static string endpoint = ApiConfig.ApiEndpoint;
        protected AuthenticationHeaderValue auth = null;

        private  StringContent AsJson(object o)
             => new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");

        private TResult FromJson<TResult>(string queryResult)
            =>  JsonConvert.DeserializeObject<TResult>(queryResult);

        protected static string BuildAddress(string path, string id = "")
        {
            string result = endpoint + "/" + path;

            result = result.Replace("{id}", id);

            while (result.IndexOf("//","http://".Length+1) > -1)
                result = result.Replace("//", "/");

            result = "http://" + result;
            return result;
        }

        protected int Post<T>(string address, T dto, AuthenticationHeaderValue auth = null )
        {
            
            string endpoint = BuildAddress(address);
            using (HttpClient client = new HttpClient())
            {
                SetAuth(client, auth);
                var json = AsJson(dto);
                var postReult = client.PostAsync(endpoint, json).Result;
                if (!postReult.IsSuccessStatusCode)
                    throw new HttpRequestException(string.Format("Query on {0} not succeeded",endpoint));

                if(!int.TryParse(postReult.Content.ReadAsStringAsync().Result,out var id))
                    throw new HttpRequestException( "Not correct answer from API");

                return id;
            }
        }

        protected TResult PostSpecific<TResult, TQueryDto>(string address, TQueryDto dto, AuthenticationHeaderValue auth = null)
        {
            string endpoint = BuildAddress(address);
            using (HttpClient client = new HttpClient())
            {
                SetAuth(client, auth);
                var json = AsJson(dto);
                var postReult = client.PostAsync(endpoint, json).Result;

                if (!postReult.IsSuccessStatusCode)
                    throw new HttpRequestException(string.Format("Query on {0} not succeeded", endpoint));

                var resultToParse = postReult.Content.ReadAsStringAsync().Result;

                if(IsSimpleType(typeof(TResult)))
                {
                    return resultToParse is TResult t ? t: throw new HttpRequestException("wrong response which could not be parsed");
                }
                else return FromJson<TResult>(resultToParse);
            }
        }
        protected void Update<T>(string address, T dto, string id = "", AuthenticationHeaderValue auth = null)
        {
            string endpoint = BuildAddress(address, id);
            using (HttpClient client = new HttpClient())
            {
                SetAuth(client, auth);
                var json = AsJson(dto);
                var postReult = client.PatchAsync(endpoint, json).Result;
                if (!postReult.IsSuccessStatusCode)
                    throw new HttpRequestException(string.Format("Query on {0} not succeeded", endpoint));
            }
        }

        protected void Set<T>(string address, T dto, string id = "", AuthenticationHeaderValue auth = null)
        {
            string endpoint = BuildAddress(address, id);
            using (HttpClient client = new HttpClient())
            {
                SetAuth(client, auth);
                var json = AsJson(dto);
                var result = client.PutAsync(endpoint, json).Result;
                if(!result.IsSuccessStatusCode)
                    throw new HttpRequestException(string.Format("Query on {0} not succeeded", endpoint));
            }
        }

        protected void Delete(string address, int id, AuthenticationHeaderValue auth = null)
        {
            string endpoint = BuildAddress(address, id.ToString());
            using (HttpClient client = new HttpClient())
            {
                SetAuth(client, auth);
                var deleteResult = client.DeleteAsync(endpoint);
                if (!deleteResult.Result.IsSuccessStatusCode)
                    throw new HttpRequestException(string.Format("Query on {0} not succeeded", endpoint));
            }
        }

        protected async Task<T> Query<T>(string address, string id = null, AuthenticationHeaderValue auth = null) where T : class
        {
            using (HttpClient client = new HttpClient())
            {
                SetAuth(client, auth);
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

        public AuthenticationHeaderValue GetAuthentitaceUserKey(ApiAuthDto auth)
        {
            if (auth == null)
                return null;

            var authQuery = GetQueryProvider<IAuthenticationQuery>();
            var authKey = authQuery.GetLoginAuthKey(auth.UserName, auth.Password);
            return new AuthenticationHeaderValue("Basic", authKey);
        }

        protected T GetQueryProvider<T>()
            => ApiCommunicationProvider.GetInstance().GetQueryProvider<T>();

        private void SetAuth(HttpClient client, AuthenticationHeaderValue auth = null)
        {
            if (auth != null)
                client.DefaultRequestHeaders.Authorization = auth;
        }

        private bool IsSimpleType(Type type)
        {
            return type.IsPrimitive
              || type.Equals(typeof(string));
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
