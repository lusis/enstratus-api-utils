// Copyright (c) 2013 Dell. All rights reserved. Written by Doron Grinstein doron.grinstein@software.dell.com

using System.Xml.Serialization;

namespace Dell.CTO.Enstratius
{
    public class stop
    {
        [XmlAttribute]
        public bool force { get; set; }
        public string server { get; set; }
    }
}
