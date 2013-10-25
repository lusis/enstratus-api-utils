// Copyright (c) 2013 Dell. All rights reserved. Written by Doron Grinstein doron.grinstein@software.dell.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Dell.CTO.Enstratius
{
    public class MaintenanceWindow
    {
        public int startMinute { get; set; }
        public int endMinute { get; set; }
        public int startHour { get; set; }
        public int endHour { get; set; }
        public List<string> daysOfWeek { get; set; }
    }

    public class Region
    {
        public string status { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public Cloud cloud { get; set; }
        public string providerId { get; set; }
        public string jurisdiction { get; set; }
        public Customer customer { get; set; }
        public int regionId { get; set; }
    }



    public class OwningGroup
    {
        public int groupId { get; set; }
    }

    public class OwningUser
    {
        public int userId { get; set; }
    }

    public class BackupWindow
    {
        public int startMinute { get; set; }
        public int endMinute { get; set; }
        public int startHour { get; set; }
        public int endHour { get; set; }
        public List<string> daysOfWeek { get; set; }
    }

    [DataContract]
    public class Deployment
    {
        [DataMember]
        public int budget { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public bool removable { get; set; }
        [DataMember]
        public MaintenanceWindow maintenanceWindow { get; set; }
        [DataMember]
        public int deploymentId { get; set; }
        [DataMember]
        public List<Region> regions { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public Customer customer { get; set; }
        [DataMember]
        public string launchTimestamp { get; set; }
        [DataMember]
        public List<OwningGroup> owningGroups { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string creationTimestamp { get; set; }
        [DataMember]
        public OwningUser owningUser { get; set; }
        [DataMember]
        public bool forServiceCatalog { get; set; }
        [DataMember]
        public BackupWindow backupWindow { get; set; }
        [DataMember]
        public string reasonNotRemovable { get; set; }
        [DataMember]
        public string label { get; set; }
    }

    public class DeploymentList
    {
        public List<Deployment> deployments { get; set; }
    }
}
