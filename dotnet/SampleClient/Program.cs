// Copyright (c) 2013 Dell. All rights reserved. Written by Doron Grinstein doron.grinstein@software.dell.com
using System;
using RestSharp;
using System.Text;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.IO;

namespace Dell.CTO.Enstratius
{
    // samples
    //string resource = "/api/enstratus/2013-01-29/geography/Region";
    //string resource = "/api/enstratus/2013-01-29/geography/DataCenter";
    //string resource = "/api/enstratus/2013-01-29/geography/Cloud";
    //string resource = "/api/enstratus/2013-01-29/admin/Job";
    
    //string resource = "/api/enstratus/2013-03-13/infrastructure/Server";
    //string resource = "/api/enstratus/2013-03-13/geography/Cloud/1";
    //string resource = "/api/enstratus/2013-01-29/infrastructure/Server";
    //string resource = "/api/enstratus/2013-01-29/geography/Region";
    //string resource = "/api/enstratus/2013-03-13/admin/Account";
    


    
    class Program
    {
        static void Main(string[] args)
        {
            Client c = new Client();
            //string resource = "/api/enstratus/2013-01-29/admin/Budget";

            

            // code commented below was tested successfuly. It documents what this assembly is capable of doing:

            //string result = c.StopServer("927");
            //string result = c.LauchServer("300", "antonio-1000", "started from an api call", "300", "1:512", "1");
            //string result = c.TerminateServer("927", "do not need it anymore");
            //string result = c.CreateUser("0", "Lisi", "Grinstein", "lisi@dell.com", "lisi@software.dell.com", "true", "true", "ALARM.ANY", "2", "0", "202");

            //string resource = "/api/enstratus/2013-03-13/infrastructure/MachineImage";
            

            //var method = Method.GET;
            //c.AddHeader("x-es-details", "basic");
            //Console.WriteLine(c.invokeCommand(method, resource, null, null, null));



            //Console.WriteLine(result);
            Console.WriteLine(c.LaunchDeployment("202"));

            Console.WriteLine("----all done----");
            Console.ReadKey();
        }
    }
}
