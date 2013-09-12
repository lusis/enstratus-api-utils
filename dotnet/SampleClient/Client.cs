// Copyright (c) 2013 Dell. All rights reserved. Written by Doron Grinstein doron.grinstein@software.dell.com
using System;
using System.Collections.Generic;
using RestSharp;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Dell.CTO.Enstratius
{
    class Client
    {
        public string host_base;
        public string api_access_id;
        public string secret_key;
        public RestClient client;
        Dictionary<string, string> additionalHeaders = null;


        public Client()
        {
            init();
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


        public string LauchServer(string budget, string name, string description, string machineImageId, int cpus, int ram, string dataCenterId)
        {
            clearHeaders();
            AddHeader("x-es-details", "basic");
            AddHeader("Accept", "application/xml"); // for JSON use application/json
            string resource = "/api/enstratus/2013-03-13/infrastructure/Server";
            launch l = new launch(budget, name, description, machineImageId, cpus, ram, dataCenterId);
            var serializer = new EnstratiusSerializer<launch>();
            return invokeCommand(Method.POST, resource , null, l, serializer);
        }

        public string StopServer(string serverId)
        {
            clearHeaders();
            AddHeader("x-es-details", "basic");
            AddHeader("Accept", "application/xml"); // for JSON use application/json
            string resource = "/api/enstratus/2013-03-13/infrastructure/Server/" + serverId;
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
            string resource = "/api/enstratus/2013-03-13/infrastructure/Server/" + serverId;
            return invokeCommand(Method.DELETE, resource, "reason=" + reason, null, null);
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


        private void init()
        {
            host_base = Properties.Settings.Default["host_name"].ToString();
            api_access_id = Properties.Settings.Default["api_access_id"].ToString();
            secret_key = Properties.Settings.Default["secret_key"].ToString();
            client = new RestClient(host_base);
            client.UserAgent = Properties.Settings.Default["user_agent"].ToString();
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
            return (long)(DateTime.UtcNow - UnixEpoch).TotalMilliseconds;
        }
    }


}
