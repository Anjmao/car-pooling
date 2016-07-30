using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarPooling.Utils
{
    public abstract class ClientBase : IDisposable
    {
        private bool disposed;

        protected ClientBase(Uri uri)
        {
            this.InitializeClient(uri);
        }

        public HttpClient HttpClient { get; private set; }

        public virtual Task<T> GetAsync<T>(string url) where T : class
        {
            return this.ExecuteAsync<T>(new HttpRequestMessage(HttpMethod.Get, url));
        }

        public virtual Task<T> ExecuteAsync<T>(HttpRequestMessage request) where T : class
        {
            return this.ExecuteAsync(request).ContinueWith(task => this.DeserializeObject<T>(task.Result));
        }
        
        public Task<HttpResponseMessage> ExecuteAsync(HttpRequestMessage request)
        {
            return this.HttpClient.SendAsync(request).ContinueWith(task => CheckResponseForErrors(task.Result));
        }
        
        protected abstract JsonSerializerSettings GetJsonSerializerSettings();
        
        public virtual Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return this.HttpClient.DeleteAsync(url).ContinueWith(task => CheckResponseForErrors(task.Result));
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.HttpClient.Dispose();
            }

            this.disposed = true;
        }

        private static HttpResponseMessage CheckResponseForErrors(HttpResponseMessage clientRsp)
        {
            if (!clientRsp.IsSuccessStatusCode)
            {
                var jsonResponse = clientRsp.Content.ReadAsStringAsync().Result;

                if (clientRsp.StatusCode == HttpStatusCode.NotFound)
                {
                    return clientRsp;
                }

                throw new Exception("Exception requesting API", new Exception(string.Format("Response ({0}): {1}", clientRsp.StatusCode, jsonResponse)));
            }

            return clientRsp;
        }

        private T DeserializeObject<T>(HttpResponseMessage response) where T : class
        {
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            string content = null;
            try
            {
                content = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(content, this.GetJsonSerializerSettings());
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Exception has been thrown trying to deserialize JSON: {0}", content), ex);
            }
        }

        private void InitializeClient(Uri uri)
        {
            this.HttpClient = new HttpClient(new HttpClientHandler { UseCookies = false })
            {
                BaseAddress = uri,
                DefaultRequestHeaders =
                                          {
                                              Accept =
                                                  {
                                                      new MediaTypeWithQualityHeaderValue("application/json")
                                                  }
                                          }
            };
        }
    }
}
