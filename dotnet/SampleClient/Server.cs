using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Dell.CTO.Enstratius
{




    public class CmAccount
    {
        public int cmAccountId { get; set; }
    }


    [DataContract]
    public class Server
    {
        [DataMember]
        public Region region { get; set; }
        [DataMember]
        public int budget { get; set; }
        [DataMember]
        public string platform { get; set; }
        [DataMember]
        public string startDate { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public DataCenter dataCenter { get; set; }
        [DataMember]
        public string architecture { get; set; }
        [DataMember]
        public Customer customer { get; set; }
        [DataMember]
        public List<string> privateIpAddresses { get; set; }
        [DataMember]
        public MachineImage machineImage { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string providerId { get; set; }
        [DataMember]
        public Cloud cloud { get; set; }
        [DataMember]
        public int serverId { get; set; }
        [DataMember]
        public int agentVersion { get; set; }
        [DataMember]
        public string providerProductId { get; set; }
        [DataMember]
        public OwningUser owningUser { get; set; }
        [DataMember]
        public CmAccount cmAccount { get; set; }
        [DataMember]
        public List<OwningGroup> owningGroups { get; set; }
        [DataMember]
        public string label { get; set; }
    }

    public class ServerList
    {
        public List<Server> servers { get; set; }
    }
}
