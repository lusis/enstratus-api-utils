// Copyright (c) 2013 Dell. All rights reserved. Written by Doron Grinstein doron.grinstein@software.dell.com
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dell.CTO.Enstratius;


namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        readonly int KNOWN_CUSTOMER = 200;
        readonly string KNOWN_ACCOUNT = "200";
        readonly string KNOWN_REGION = "200";
        readonly string KNOWN_CLOUD = "1";
        readonly string KNOWN_DEPLOYMENT = "403";
        readonly string KNOWN_BUDGET = "700"; // same as BillingCode. These terms are used interchangeably by Enstratius
        readonly string KNOWN_MACHINE_IMAGE = "300";
        readonly string KNOWN_DATA_CENTER = "1";

        static Client c;
        static string secretKey = Environment.GetEnvironmentVariable("ES_SECRET_KEY");

        [TestInitialize]
        public void init()
        {
            if (c == null)
                c = new Client("http://demo.enstratius.com:15000", "HUFVVXTGJWVZYFWRMAHU", secretKey, "test", "/api/enstratus/2013-03-13");
        }

  

        [TestMethod]
        public void TestGetCustomerList()
        {
            CustomerList list = c.GetCustomerList();
            Assert.IsTrue(list.customers.Count > 0);
            bool knownCustomerFound = list.customers.Exists(p => p.customerId == KNOWN_CUSTOMER);
            Assert.IsTrue(knownCustomerFound);
            list = c.GetCustomerList(KNOWN_CUSTOMER.ToString());
            Assert.IsTrue(list.customers.Count == 1);
        }


        [TestMethod]
        public void TestGetAccountList()
        {
            AccountList list = c.GetAccountList();
            Assert.IsTrue(list.accounts.Count > 0);

            list = c.GetAccountList(KNOWN_ACCOUNT);
            Assert.IsTrue(list.accounts.Count == 1);
        }



        [TestMethod]
        public void TestGetDataCenterList()
        {
            DataCenterList list = c.GetDataCenterList(KNOWN_REGION);
            Assert.IsTrue(list.dataCenters.Count > 0);
        }



        [TestMethod]
        public void TestGetRegionList()
        {
            RegionList list = c.GetRegionList();
            Assert.IsTrue(list.regions.Count > 0);
            list = c.GetRegionList(KNOWN_REGION);
            Assert.IsTrue(list.regions.Count == 1);
        }



        [TestMethod]
        public void TestGetCloudList()
        {
            CloudList list = c.GetCloudList();
            Assert.IsTrue(list.clouds.Count > 1);
            list = c.GetCloudList(KNOWN_CLOUD);
            Assert.IsTrue(list.clouds.Count == 1);
        }

        //[TestMethod]
        //public void TestLaunchDeployment()
        //{
        //    string result = c.LaunchDeployment(KNOWN_DEPLOYMENT);
        //    Assert.IsTrue(result.Contains("<job jobId=") || result.Contains("LAUNCHING"));
        //}

        //[TestMethod]
        //public void TestStopDeployment()
        //{
        //    string result = c.StopDeployment(KNOWN_DEPLOYMENT);
        //    Assert.IsTrue(result.Contains("<job jobId=") || result.Contains("RUNNING"));
        //}

        //[TestMethod]
        //public void TestLaunchServer()
        //{
        //    string result = c.LaunchServer(KNOWN_BUDGET, "doron-500", "started from unit test", KNOWN_MACHINE_IMAGE, "1:512", KNOWN_DATA_CENTER);
        //    Assert.IsTrue(result.Contains("<job jobId=") || result.Contains("LAUNCHING"));
        //}

        [TestMethod]
        public void TestStopServer()
        {
            string result = c.StopServer("2150");
            Assert.IsTrue(result.Contains("<job jobId=") || result.Contains("STOPPING"));
        }

        [TestMethod]
        public void TestTerminateServer()
        {
            string result = c.TerminateServer("2150", "it is no longer needed called from unit test");
            Assert.IsTrue(result.Contains("<job jobId=") || result.Contains("TERMINATING"));
        }



        [TestMethod]
        public void TestGetDeploymentsList()
        {
            DeploymentList list = c.GetDeploymentList();
            Assert.IsTrue(list.deployments.Count > 0);

            list = c.GetDeploymentList(KNOWN_DEPLOYMENT);
            Assert.IsTrue(list.deployments.Count == 1);
        }



        [TestMethod]
        public void TestGetServerList()
        {
            ServerList list = c.GetServerList();
            Assert.IsTrue(list.servers.Count > 0);
        }



        [TestMethod]
        public void TestGetBillingCodeList()
        {
            BillingCodeList list = c.GetBillingCodeList();
            Assert.IsTrue(list.billingCodes.Count > 0);

            list = c.GetBillingCodeList(KNOWN_BUDGET);
            Assert.IsTrue(list.billingCodes.Count == 1);

        }

    }
}
