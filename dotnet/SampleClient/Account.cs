// Copyright (c) 2013 Dell. All rights reserved. Written by Doron Grinstein doron.grinstein@software.dell.com
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dell.CTO.Enstratius
{
    public class CloudSubscriptionId
    {
        public int cloudId { get; set; }
        public string accountNumber { get; set; }
        public int cloudSubscriptionId { get; set; }
        public object storageAccountNumber { get; set; }
    }

    public class AlertConfiguration
    {
        public bool allowIndividualSmsAlerts { get; set; }
        public int stopAlertsAfterMinutes { get; set; }
        public int globalSmsThreshold { get; set; }
        public int globalEmailThreshold { get; set; }
        public bool allowIndividualEmailAlerts { get; set; }
        public int alertIntervalInMinutes { get; set; }
    }


    [DataContract]
    public class Account
    {
        [DataMember]
        public int accountId { get; set; }
        [DataMember]
        public CloudSubscriptionId cloudSubscriptionId { get; set; }
        [DataMember]
        public bool configured { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public bool provisioned { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public AlertConfiguration alertConfiguration { get; set; }
        [DataMember]
        public int planId { get; set; }
        [DataMember]
        public Customer customer { get; set; }
        [DataMember]
        public string billingSystemId { get; set; }
        [DataMember]
        public bool subscribed { get; set; }
        [DataMember]
        public int defaultBudget { get; set; }
    }

    public class AccountList
    {
        public List<Account> accounts { get; set; }
    }
}

