// Copyright (c) 2013 Dell. All rights reserved. Written by Doron Grinstein doron.grinstein@software.dell.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Dell.CTO.Enstratius
{
    public class DataCenter
    {
        public Region region { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public string providerId { get; set; }
        [XmlAttribute]
        public int dataCenterId { get; set; }
    }

    public class DataCenterList
    {
        public List<DataCenter> dataCenters {get; set;}
    }
}
