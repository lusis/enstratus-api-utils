// Copyright (c) 2013 Dell. All rights reserved. Written by Doron Grinstein doron.grinstein@software.dell.com
using System;
using System.Collections.Generic;
using RestSharp;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;




namespace Dell.CTO.Enstratius
{
    public class Client
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
        public Client(string hostBase, string apiAccessId, string apiSecretKey, string userAgent, string apiRoot)
        {
            init(hostBase, apiAccessId, apiSecretKey, userAgent, apiRoot);
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


        public CustomerList GetCustomerList()
        {
            return new JsonToList<CustomerList>().GetList(GetCustomersJson);
        }

        public string GetCustomersJson()
        {
            clearHeaders();
            string resource = api_root + "/admin/Customer";
            var method = Method.GET;
            AddHeader("x-es-details", "extended");
            return invokeCommand(method, resource, null, null, null);
        }

        public string GetAccountJson(string id)
        {
            clearHeaders();
            string resource = api_root + "/admin/Account/" + id;
            var method = Method.GET;
            AddHeader("x-es-details", "basic");
            return invokeCommand(method, resource, null, null, null);
        }

        public string GetCloudsJson()
        {
            clearHeaders();
            string resource = api_root + "/geography/Cloud";
            AddHeader("x-es-details", "basic");
            return invokeCommand(Method.GET, resource, null, null, null);
        }

        public CloudList GetCloudList()
        {
            return new JsonToList<CloudList>().GetList(GetCloudsJson);
        }


        public Account GetAccount(string id)
        {
            string json = GetAccountJson(id);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AccountList));
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                Account account = ((ser.ReadObject(stream) as AccountList).accounts[0]);
                return account;
            }
        }



        public string CreateUser(string accountId,
                              string givenName,
                              string familyName,
                              string email,
                              string emailTarget,
                              string notifyViaEmail,
                              string notifyViaScreen,
                              string eventType,
                              string severity,
                              string billingCode,
                              string groupId)
        {
            string xml = XmlTemplates.ResourceManager.GetString("add_user");
            user u = new user();
            u.accountId = accountId;
            u.givenName = givenName;
            u.familyName = familyName;
            u.email = email;
            u.emailTarget = emailTarget;
            u.notifyViaEmail = notifyViaEmail;
            u.notifyViaScreen = notifyViaScreen;
            u.eventType = eventType;
            u.severity = severity;
            u.billingCode = billingCode;
            u.groupId = groupId;
            string addUserXmlString = SmartFormat.Smart.Format(xml, u);


            clearHeaders();
            AddHeader("x-es-details", "basic");
            AddHeader("Accept", "application/xml"); // for JSON use application/json
            string resource = api_root + "/admin/User";
            return invokeStringPost(resource, addUserXmlString);
        }


        public string CreateDeployment(DeploymentLaunch deployment)
        {
            string template = XmlTemplates.ResourceManager.GetString("create_deployment");
            string xml = SmartFormat.Smart.Format(template, deployment);
            clearHeaders();
            AddHeader("x-es-details", "basic");
            AddHeader("Accept", "application/xml"); // for JSON use application/json
            string resource = api_root + "/automation/Deployment";
            return invokeStringPost(resource, xml);
        }

        public string LaunchDeployment(string id)
        {
            string template = XmlTemplates.ResourceManager.GetString("launch_deployment");
            clearHeaders();
            AddHeader("Accept", "application/xml"); // for JSON use application/json
            string resource = api_root + "/automation/Deployment/" + id;
            return invokeStringPost(resource, template, true); // 3rd parameter true=PUT (not POST)
        }



        public string LaunchServer(string budget, string name, string description, string machineImageId, string product, string dataCenterId)
        {
            clearHeaders();
            AddHeader("x-es-details", "basic");
            AddHeader("Accept", "application/xml"); // for JSON use application/json
            string resource = api_root + "/infrastructure/Server";
            launch l = new launch(budget, name, description, machineImageId, product, dataCenterId);
            var serializer = new EnstratiusSerializer<launch>();
            return invokeCommand(Method.POST, resource, null, l, serializer);
        }

        public string StopServer(string serverId)
        {
            clearHeaders();
            AddHeader("x-es-details", "basic");
            AddHeader("Accept", "application/xml"); // for JSON use application/json
            string resource = api_root + "/infrastructure/Server/" + serverId;
            stop s = new stop();
            s.force = true;
            s.server = "";
            var serializer = new EnstratiusSerializer<stop>();
            return invokeCommand(Method.PUT, resource, null, s, serializer);
        }

        public string TerminateServer(string serverId, string reason)
        {
            clearHeaders();
            AddHeader("x-es-details", "basic");
            AddHeader("Accept", "application/xml"); // for JSON use application/json
            string resource = api_root + "/infrastructure/Server/" + serverId;
            return invokeCommand(Method.DELETE, resource, "reason=" + reason, null, null);
        }


        public string GetAccountsJson()
        {
            clearHeaders();
            AddHeader("x-es-details", "basic");
            AddHeader("Accept", "application/json"); // for JSON use application/json
            return invokeCommand(RestSharp.Method.GET, api_root + "/admin/Account", null, null, null);
        }

        public AccountList GetAccountList()
        {
            return new JsonToList<AccountList>().GetList(GetAccountsJson);
        }

        public string GetDeploymentsJson()
        {
            clearHeaders();
            AddHeader("x-es-details", "basic");
            AddHeader("Accept", "application/json"); // for JSON use application/json
            return invokeCommand(Method.GET, api_root + "/automation/Deployment", null, null, null);
        }

        public DeploymentList GetDeploymentList()
        {
            return new JsonToList<DeploymentList>().GetList(GetDeploymentsJson);
        }


        public string GetServersJson()
        {
            clearHeaders();
            AddHeader("x-es-details", "basic");
            string resource = api_root + "/infrastructure/Server";
            return invokeCommand(Method.GET, resource, null, null, null);
        }

        public ServerList GetServerList()
        {
            return new JsonToList<ServerList>().GetList(GetServersJson);
        }


        public delegate string GetStringDelegate();

        public class JsonToList<T>
        {
            public T GetList(GetStringDelegate d)
            {
                string json = d();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
                {
                    return (T)ser.ReadObject(stream);
                }
            }
        }

        public string invokeCommand(RestSharp.Method method, string resource, string parameters, object obj, RestSharp.Serializers.ISerializer serializer)
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
            return response.Content;
        }

        public string invokeStringPost(string resource, string xml, Boolean put=false)
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
