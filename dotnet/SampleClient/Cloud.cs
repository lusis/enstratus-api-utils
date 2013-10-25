// Copyright (c) 2013 Dell. All rights reserved. Written by Doron Grinstein doron.grinstein@software.dell.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Dell.CTO.Enstratius
{


    public class PrivateCloudCustomer
    {
        public int customerId { get; set; }
    }

    [DataContract]
    public class Cloud
    {
        [DataMember]
        public string computeDelegate { get; set; }
        [DataMember]
        public int cloudId { get; set; }
        [DataMember]
        public string computeX509KeyLabel { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string computeEndpoint { get; set; }
        [DataMember]
        public bool privateCloud { get; set; }
        [DataMember]
        public string computeSecretKeyLabel { get; set; }
        [DataMember]
        public string computeX509CertLabel { get; set; }
        [DataMember]
        public string computeAccountNumberLabel { get; set; }
        [DataMember]
        public string documentationLabel { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string cloudProviderName { get; set; }
        [DataMember]
        public string cloudProviderConsoleURL { get; set; }
        [DataMember]
        public string computeAccessKeyLabel { get; set; }
        [DataMember]
        public string cloudProviderLogoURL { get; set; }
        [DataMember]
        public string storageX509KeyLabel { get; set; }
        [DataMember]
        public string storageX509CertLabel { get; set; }
        [DataMember]
        public string storageEndpoint { get; set; }
        [DataMember]
        public string storageDelegate { get; set; }
        [DataMember]
        public string storageAccountNumberLabel { get; set; }
        [DataMember]
        public string storageSecretKeyLabel { get; set; }
        [DataMember]
        public string storageAccessKeyLabel { get; set; }
        [DataMember]
        public PrivateCloudCustomer privateCloudCustomer { get; set; }
    }

    public class CloudList
    {
        public List<Cloud> clouds { get; set; }
    }

}
