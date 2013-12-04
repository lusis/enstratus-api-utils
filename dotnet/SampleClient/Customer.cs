// Copyright (c) 2013 Dell. All rights reserved. Written by Doron Grinstein doron.grinstein@software.dell.com
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;


namespace Dell.CTO.Enstratius
{
    [DataContract]
    public class Customer
    {
        [DataMember]
        public string businessName { get; set; }
        [DataMember]
        public List<Account> accounts { get; set; }
        [DataMember]
        public int customerId { get; set; }
        [DataMember]
        public string createdTimestamp { get; set; }
        [DataMember]
        public string created { get; set; }
        [DataMember]
        public string accountingCurrency { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string timeZone { get; set; }
        [DataMember]
        public bool automatedExchangeRates { get; set; }
    }

    [DataContract]
    public class CustomerList
    {
        [DataMember]
        public List<Customer> customers { get; set; }
    }
}
