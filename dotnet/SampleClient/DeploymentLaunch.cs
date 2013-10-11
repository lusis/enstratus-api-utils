using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Dell.CTO.Enstratius
{
    public class DeploymentLaunch
    {
        public string budget { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string label { get; set; }
        public string for_service_catalog { get; set; }
        public string storage_region_id { get; set; }
        public string backup_days_of_week { get; set; }
        public string backup_start_hour { get; set; }
        public string backup_start_minute { get; set; }
        public string backup_end_hour { get; set; }
        public string backup_end_minute { get; set; }
        public string maintenance_days_of_week { get; set; }
        public string maintenance_start_hour { get; set; }
        public string maintenance_start_minute { get; set; }
        public string maintenance_end_hour { get; set; }
        public string maintenance_end_minute { get; set; }
    }
}



