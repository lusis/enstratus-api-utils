// Copyright (c) 2013 Dell. All rights reserved. Written by Doron Grinstein doron.grinstein@software.dell.com
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using System.Security.Cryptography;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Dell.CTO.Enstratius
{
    public enum DetailsEnum { none, basic, extended };

    public partial class Client
    {
        private string host_base;
        private string api_access_id;
        private string secret_key;
        private RestClient client;
        private string api_root;
        private Dictionary<string, string> additionalHeaders = null;

        /// <summary>
        /// The constructor for the DMCM Client initializes an object that can communicate with a DMCM server and submit REST API calls.
        /// </summary>
        /// <param name="hostBase">The URI endpoint for the DMCM API. For example: http://demo.enstratius.com:15000</param>
        /// <param name="apiAccessId">An API access ID that must be obtained from the DMCM web console. for example HUFVVXTGJWVZYFWRMAHU</param>
        /// <param name="apiSecretKey">The secret key corresponding to the API ID. It must be obtained from the DMCM web console</param>
        /// <param name="userAgent">Any string value that you wish to convey as the user-agent to the DMCM system</param>
        /// <param name="apiRoot">The portion of the URI after the host name. DMCM supports multiple versions of its API. For example: /api/enstratus/2013-03-13</param>

        public DetailsEnum Details { get; set; }

        public Client(string hostBase, string apiAccessId, string apiSecretKey, string userAgent, string apiRoot)
        {
            init(hostBase, apiAccessId, apiSecretKey, userAgent, apiRoot);
            Details = DetailsEnum.basic; // the default. can be overridden by accessing the Deails public property
        }

        private string verbosity
        {
            get
            {
                return Details.ToString();
            }
        }

        public void AddHeader(string name, string value)
        {
            if (additionalHeaders == null)
                additionalHeaders = new Dictionary<string, string>();
            additionalHeaders.Add(name, value);
        }

        public void clearHeaders()
        {
            additionalHeaders = null;
        }


        public delegate string GetStringDelegate();
        public delegate string GetStringDelegateWithOneParam(string param);

        public IEnumerable<string> GetJson(string resource, int requestLimit = 500, string id="", string queryString = "")
        {
            clearHeaders();
            AddHeader("x-es-details", verbosity);
            string _resource = api_root + resource;
            AddHeader("Accept", "application/json");
            if (id != "")
                _resource += "/" + id;
            
            string p = "requestLimit=" + requestLimit.ToString();
            if (queryString != "")
                p += "&" + queryString;
            return invokeCommandPaginated(Method.GET, _resource, p);
        }



        public class JsonToList<T>
        {
            public T GetList(GetStringDelegateWithOneParam d, string param)
            {
                string json = d(param);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
                {
                    return (T)ser.ReadObject(stream);
                }
            }

            public T GetList(string json)
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
                {
                    var x = ser.ReadObject(stream);
                    return (T)x;
                }

            }
        }


        public IEnumerable<string> invokeCommandPaginated(Method method, string resource, string parameters)
        {
            int currentPage = 1;
            IList<Parameter> headers = null;
            yield return invokeCommand(method, resource, parameters, null, null, out headers);
            HttpHeader header = null;
            if (headerExists(headers, "x-es-pagination", out header))
            {
                string paginationId = header.Value;
                while (!isLastPage(headers))
                    yield return invokeCommand(method, resource, "requestPaginationId=" + paginationId + "&requestPage=" + (++currentPage).ToString(), null, null, out headers);
            }
        }


        private bool isLastPage(IList<Parameter> headers)
        {
            HttpHeader header = null;
            if (headerExists(headers, "x-es-last-page", out header))
            {
                return (header.Value == "true");
            }
            else
                return true; // if we did not find the header x-es-last-page then surely this is the last page?
        }



        public string invokeCommand(RestSharp.Method method, string resource, string parameters, object obj, RestSharp.Serializers.ISerializer serializer, out IList<Parameter> headers)
        {
            string full_resource = resource;
            if (parameters != null)
                full_resource += "?" + parameters;

            RestRequest request = new RestRequest(full_resource, method);
            request.RequestFormat = DataFormat.Xml;
            if (serializer != null)
                request.XmlSerializer = serializer;
            if ((method == Method.POST) || (method == Method.PUT))
                request.AddBody(obj);

            long unixTimeStamp = GetCurrentUnixTimestampMillis();
            string toSign = api_access_id + ":" + method.ToString() + ":" + resource + ":" + unixTimeStamp.ToString() + ":" + client.UserAgent;
            var signature = sign(secret_key, toSign);

            request.AddHeader("x-esauth-access", api_access_id);
            request.AddHeader("x-esauth-signature", signature);
            request.AddHeader("x-esauth-timestamp", unixTimeStamp.ToString());
            if (additionalHeaders != null)
                foreach (var di in additionalHeaders)
                    request.AddHeader(di.Key, di.Value);

            //request.AddHeader("x-es-with-perms", "false");

            var response = client.Execute(request);
            headers = response.Headers;
            return response.Content; // no need to aggregate the responses. simply return the non-paginated content.
        }

        private bool headerExists(IList<Parameter> list, string p, out HttpHeader header)
        {
            foreach (var item in list)
            {
                if (item.Name == p && item.Type == ParameterType.HttpHeader)
                {
                    HttpHeader _header = new HttpHeader() { Name = item.Name, Value = item.Value.ToString() };
                    header = _header;
                    return true;
                }
            }
            header = null;
            return false;
        }



        public string invokeStringPost(string resource, string xml, Boolean put = false)
        {
            Method method = Method.POST;
            string methodString = "POST";
            if (put)
            {
                method = Method.PUT;
                methodString = "PUT";
            }

            RestRequest request = new RestRequest(resource, method);
            request.AddHeader("Accept", "application/xml");
            request.RequestFormat = DataFormat.Xml;
            request.AddParameter("text/xml", xml, ParameterType.RequestBody);
            long unixTimeStamp = GetCurrentUnixTimestampMillis();
            string toSign = api_access_id + ":" + methodString + ":" + resource + ":" + unixTimeStamp.ToString() + ":" + client.UserAgent;
            var signature = sign(secret_key, toSign);

            request.AddHeader("x-esauth-access", api_access_id);
            request.AddHeader("x-esauth-signature", signature);
            request.AddHeader("x-esauth-timestamp", unixTimeStamp.ToString());
            var response = client.Execute(request);
            return response.Content;
        }


        private void init(string hostBase, string apiAccessId, string apiSecretKey, string userAgent, string apiRoot)
        {
            this.host_base = hostBase;
            this.api_access_id = apiAccessId;
            this.secret_key = apiSecretKey;
            if (secret_key == null)
                secret_key = Environment.GetEnvironmentVariable("ES_SECRET_KEY");
            if (secret_key == null)
                throw new Exception("API secret key must be provided either in the constructor or the environment variable ES_SECRET_KEY must contain the secret key");
            client = new RestClient(host_base);
            client.UserAgent = userAgent;
            api_root = apiRoot;
        }


        private static string sign(String key, String stringToSign)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(key);
            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            return Convert.ToBase64String(hmacsha256.ComputeHash(encoding.GetBytes(stringToSign)));
        }



        private static readonly DateTime UnixEpoch =
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long GetCurrentUnixTimestampMillis()
        {
            DateTime tempTime = DateTime.UtcNow; //.Add(new TimeSpan(1, 0, 0));

            return (long)(tempTime - UnixEpoch).TotalMilliseconds;
        }
    }
}






