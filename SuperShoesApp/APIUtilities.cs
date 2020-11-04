using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using System.Net;
using System.Configuration;
using System.IO;

using SuperShoes.Controllers;
using System.Net.Http;
using System.Threading.Tasks;

namespace SuperShoesApp
{
    public static class APIUtilities
    {
         /// <summary>
         /// Utility for API Get call
         /// </summary>
         /// <param name="controller"></param>
         /// <returns></returns>
        public async static Task<Response> Get(string controller)
        {
            Response response;
            string apiURL = ConfigurationManager.AppSettings.Get("APIURL");
            string result;

            if (string.IsNullOrEmpty(apiURL))
                throw new Exception("Missed configuration for APIs");

            var httpClient = new HttpClient();
            var webRequest = await httpClient.GetAsync(apiURL + controller);

            if (webRequest.StatusCode == HttpStatusCode.OK)
            {
                result = await webRequest.Content.ReadAsStringAsync();
                response = (Response)JsonConvert.DeserializeObject(result, (typeof(Response)));
            }
            else
            {
                try
                {
                    result = await webRequest.Content.ReadAsStringAsync();
                    response = (Response)JsonConvert.DeserializeObject(result, (typeof(Response)));
                }
                catch (Exception exc)
                {
                    response = new Response
                    {
                        ErrorCode = (int)webRequest.StatusCode,
                        ErrorMessage = exc.Message
                    };
                }
            }

            return response;
        }

        /// <summary>
        /// Inserts in entity
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async static Task<Response> Post(string controller, object data)
        {
            Response response;
            string apiURL = ConfigurationManager.AppSettings.Get("APIURL");
            string result;

            if (string.IsNullOrEmpty(apiURL))
                throw new Exception("Missed configuration for APIs");

            var httpClient = new HttpClient();
            var dataObject = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataObject, Encoding.UTF8, "application/json");
            var webRequest = await httpClient.PostAsync(apiURL + controller, content);

            if (webRequest.StatusCode == HttpStatusCode.OK)
            {
                result = await webRequest.Content.ReadAsStringAsync();

                response = (Response)JsonConvert.DeserializeObject(result, (typeof(Response)));
            }
            else
            {
                try
                {
                    result = await webRequest.Content.ReadAsStringAsync();
                    response = (Response)JsonConvert.DeserializeObject(result, (typeof(Response)));
                }
                catch (Exception exc)
                {
                    response = new Response
                    {
                        ErrorCode = (int)webRequest.StatusCode,
                        ErrorMessage = exc.Message
                    };
                }
            }

            return response;
        }

        /// <summary>
        /// Updates in entity
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async static Task<Response> Put(string controller, object data)
        {
            Response response;
            string apiURL = ConfigurationManager.AppSettings.Get("APIURL");
            string result;

            if (string.IsNullOrEmpty(apiURL))
                throw new Exception("Missed configuration for APIs");

            var httpClient = new HttpClient();
            var dataObject = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataObject, Encoding.UTF8, "application/json");
            var webRequest = await httpClient.PutAsync(apiURL + controller, content);

            if (webRequest.StatusCode == HttpStatusCode.OK)
            {
                result = await webRequest.Content.ReadAsStringAsync();

                response = (Response)JsonConvert.DeserializeObject(result, (typeof(Response)));
            }
            else
            {
                try
                {
                    result = await webRequest.Content.ReadAsStringAsync();
                    response = (Response)JsonConvert.DeserializeObject(result, (typeof(Response)));
                }
                catch (Exception exc)
                {
                    response = new Response
                    {
                        ErrorCode = (int)webRequest.StatusCode,
                        ErrorMessage = exc.Message
                    };
                }
            }

            return response;
        }
        
        /// <summary>
        /// Deletes in entity
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public async static Task<Response> Delete(string controller)
        {
            Response response;
            string apiURL = ConfigurationManager.AppSettings.Get("APIURL");
            string result;

            if (string.IsNullOrEmpty(apiURL))
                throw new Exception("Missed configuration for APIs");

            var httpClient = new HttpClient();
            var webRequest = await httpClient.DeleteAsync(apiURL + controller);

            if (webRequest.StatusCode == HttpStatusCode.OK)
            {
                result = await webRequest.Content.ReadAsStringAsync();

                response = (Response)JsonConvert.DeserializeObject(result, (typeof(Response)));
            }
            else
            {
                try
                {
                    result = await webRequest.Content.ReadAsStringAsync();
                    response = (Response)JsonConvert.DeserializeObject(result, (typeof(Response)));
                }
                catch (Exception exc)
                {
                    response = new Response
                    {
                        ErrorCode = (int)webRequest.StatusCode,
                        ErrorMessage = exc.Message
                    };
                }
            }

            return response;
        }
    }
}
