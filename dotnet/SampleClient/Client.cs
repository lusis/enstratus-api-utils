// Copyright (c) 2013 Dell. All rights reserved. Written by Doron Grinstein doron.grinstein@software.dell.com
using RestSharp;
using System.Collections.Generic;

namespace Dell.CTO.Enstratius
{
    public partial class Client
    {
        public CustomerList GetCustomerList(string id = "") // tested
        {
            CustomerList list = new CustomerList();
            DetailsEnum det = Details;
            Details = DetailsEnum.extended;
            IEnumerable<string> json = GetJson("/admin/Customer", 500, id);
            Details = det;
            foreach (string s in json)
            {
                var l = new JsonToList<CustomerList>().GetList(s);
                if (list.customers != null)
                    list.customers.AddRange(l.customers);
                else
                    list.customers = l.customers;
            }
            return list;
        }


        public BillingCodeList GetBillingCodeList(string id = "") //tested
        {
            BillingCodeList list = new BillingCodeList();
            DetailsEnum det = Details;
            Details = DetailsEnum.extended;
            IEnumerable<string> json = GetJson("/admin/BillingCode", 500, id);
            Details = det;
            foreach (string s in json)
            {
                var l = new JsonToList<BillingCodeList>().GetList(s);
                if (list.billingCodes != null)
                    list.billingCodes.AddRange(l.billingCodes);
                else
                    list.billingCodes = l.billingCodes;
            }
            return list;
        }



        public CloudList GetCloudList(string id = "") //tested
        {

            CloudList list = new CloudList();
            IEnumerable<string> json = GetJson("/geography/Cloud", 500, id);

            foreach (string s in json)
            {
                var l = new JsonToList<CloudList>().GetList(s);
                if (list.clouds != null)
                    list.clouds.AddRange(l.clouds);
                else
                    list.clouds = l.clouds;
            }
            return list;
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
            AddHeader("x-es-details", verbosity);
            AddHeader("Accept", "application/xml"); // for JSON use application/json
            string resource = api_root + "/admin/User";
            return invokeStringPost(resource, addUserXmlString);
        }


        public string GetJobsJson(string id = "")
        {
            clearHeaders();
            IList<Parameter> headers;
            string resource = api_root + "/admin/Job";
            if (id != "")
                resource += "/" + id;
            AddHeader("x-es-details", verbosity);
            return invokeCommand(Method.GET, resource, null, null, null, out headers);
        }

        public string CreateDeployment(DeploymentLaunch deployment)
        {
            string template = XmlTemplates.ResourceManager.GetString("create_deployment");
            string xml = SmartFormat.Smart.Format(template, deployment);
            clearHeaders();
            AddHeader("x-es-details", verbosity);
            AddHeader("Accept", "application/xml"); // for JSON use application/json
            string resource = api_root + "/automation/Deployment";
            return invokeStringPost(resource, xml);
        }



        public string LaunchDeployment(string id) //tested
        {
            string template = XmlTemplates.ResourceManager.GetString("launch_deployment");
            clearHeaders();
            AddHeader("Accept", "application/xml"); // for JSON use application/json
            string resource = api_root + "/automation/Deployment/" + id;
            return invokeStringPost(resource, template, true); // 3rd parameter true=PUT (not POST)
        }

        public string StopDeployment(string id) //tested
        {
            string template = XmlTemplates.ResourceManager.GetString("stop_deployment");
            clearHeaders();
            AddHeader("Accept", "application/xml");
            string resource = api_root + "/automation/Deployment/" + id;
            return invokeStringPost(resource, template, true); // 3rd parameter true=PUT (not POST)
        }



        public string LaunchServer(string budget, string name, string description, string machineImageId, string product, string dataCenterId) //tested
        {
            clearHeaders();
            AddHeader("x-es-details", verbosity);
            AddHeader("Accept", "application/xml"); // for JSON use application/json
            string resource = api_root + "/infrastructure/Server";
            launch l = new launch(budget, name, description, machineImageId, product, dataCenterId);
            var serializer = new EnstratiusSerializer<launch>();
            //return invokeCommand(Method.POST, resource, null, l, serializer);
            string xml = serializer.Serialize(l);
            string result = invokeStringPost(resource, xml);
            return result;
        }

        public string StopServer(string serverId) //tested
        {
            clearHeaders();
            AddHeader("x-es-details", verbosity);
            AddHeader("Accept", "application/xml");
            string resource = api_root + "/infrastructure/Server/" + serverId;
            stop s = new stop();
            s.force = true;
            s.server = "";
            var serializer = new EnstratiusSerializer<stop>();
            string xml = serializer.Serialize(s);
            return invokeStringPost(resource, xml, true);
        }

        public string TerminateServer(string serverId, string reason) //tested
        {
            clearHeaders();
            IList<Parameter> headers;
            AddHeader("x-es-details", verbosity);
            AddHeader("Accept", "application/xml");
            string resource = api_root + "/infrastructure/Server/" + serverId;
            return invokeCommand(Method.DELETE, resource, "reason=" + reason, null, null, out headers);
        }


 
        public DataCenterList GetDataCenterList(string regionId) //tested
        {
            DataCenterList list = new DataCenterList();
            IEnumerable<string> json = GetJson("/geography/DataCenter", 500, "", "regionId=" + regionId + "&activeOnly=true");
            foreach (string s in json)
            {
                var l = new JsonToList<DataCenterList>().GetList(s);
                if (list.dataCenters != null)
                    list.dataCenters.AddRange(l.dataCenters);
                else
                    list.dataCenters = l.dataCenters;
            }
            return list;

        }
            

        public RegionList GetRegionList(string regionId = "") //tested
        {
            RegionList list = new RegionList();
            string resource = "/geography/Region";
            if (regionId != "")
                regionId = "/" + regionId;
            resource += regionId;
            IEnumerable<string> json = GetJson(resource, 500, "", "");
            foreach (string s in json)
            {
                var l = new JsonToList<RegionList>().GetList(s);
                if (list.regions != null)
                    list.regions.AddRange(l.regions);
                else
                    list.regions = l.regions;
            }
            return list;
        }


        public AccountList GetAccountList(string id = "") //tested
        {
            AccountList list = new AccountList();
            IEnumerable<string> json = GetJson("/admin/Account", 500, id);
            foreach (string s in json)
            {
                var l = new JsonToList<AccountList>().GetList(s);
                if (list.accounts != null)
                    list.accounts.AddRange(l.accounts);
                else
                    list.accounts = l.accounts;
            }
            return list;            
        }



        public DeploymentList GetDeploymentList(string id = "") //tested
        {
            DeploymentList list = new DeploymentList();
            IEnumerable<string> json = GetJson("/automation/Deployment", 500, id);
            foreach (string s in json)
            {
                var l = new JsonToList<DeploymentList>().GetList(s);
                if (list.deployments != null)
                    list.deployments.AddRange(l.deployments);
                else
                    list.deployments = l.deployments;
            }
            return list;
        }


        public ServerList GetServerList() //tested
        {
            ServerList list = new ServerList();
            IEnumerable<string> serversJson = GetJson("/infrastructure/Server");
            foreach (string s in serversJson)
            {
                var l = new JsonToList<ServerList>().GetList(s);
                if (list.servers != null)
                    list.servers.AddRange(l.servers);
                else
                    list.servers = l.servers;
            }
            return list;
        }
    }
}
