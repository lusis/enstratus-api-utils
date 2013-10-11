// Copyright (c) 2013 Dell. All rights reserved. Written by Doron Grinstein doron.grinstein@software.dell.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Dell.CTO.Enstratius
{
    public class launch
    {

        public launch(){}
        

        public launch(string budget, string name, string description, string machineImageId, string product, string dataCenterId)
        {
            servers = new Servers();
            servers.server = new ServerToLaunch();
            servers.server.budget = budget;
            servers.server.name = name;
            servers.server.description = description;
            servers.server.machineImage.machineImageId = machineImageId;
            string _product = product;
            servers.server.product = _product;
            servers.server.dataCenter.dataCenterId = dataCenterId;
        }

        public Servers servers { get; set; }
    }


    public class Servers
    {
        public ServerToLaunch server { get; set; }
    }

    public class ServerToLaunch
    {
        [XmlAttribute]
        public string budget { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public MachineImage machineImage { get; set; }
        public string product { get; set; }
        public DataCenter dataCenter { get; set; }

        public ServerToLaunch()
        {
            machineImage = new MachineImage();
            dataCenter = new DataCenter();
        }
    }

    public class MachineImage
    {
        [XmlAttribute]
        public string machineImageId { get; set; }
    }

    public class DataCenter
    {
        [XmlAttribute]
        public string dataCenterId { get; set; }
    }

}
