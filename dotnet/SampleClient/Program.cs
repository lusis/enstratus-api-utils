// Copyright (c) 2013 Dell. All rights reserver. Written by Doron Grinstein doron.grinstein@software.dell.com
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
    //string resource = "/api/enstratus/2013-01-29/admin/Budget";
    //string resource = "/api/enstratus/2013-01-29/admin/Job";
    //string resource = "/api/enstratus/2013-03-13/infrastructure/MachineImage";
    //string resource = "/api/enstratus/2013-03-13/infrastructure/Server";
    //string resource = "/api/enstratus/2013-03-13/geography/Cloud/1";
    //string resource = "/api/enstratus/2013-01-29/infrastructure/Server";
    //string resource = "/api/enstratus/2013-01-29/geography/Region";
    //string resource = "/api/enstratus/2013-03-13/admin/Account";
    //var method = Method.GET;
    //Console.WriteLine(r.invokeCommand(method, resource, null, l));


    
    class Program
    {
        static void Main(string[] args)
        {
            Client r = new Client();

            // code commented below was tested successfuly. It documents what this assembly is capable of doing:

            //string result = r.StopServer("608");
            //string result = r.LauchServer("200", "doron-100", "started from an api call", "300", 1, 512, "1");
            string result = r.TerminateServer("910", "do not need it anymore");
            
            Console.WriteLine(result);
            Console.WriteLine("----all done----");
            Console.ReadKey();
        }
    }
}
