using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dell.CTO.Enstratius
{
    public class BillingCode
    {
        public string budgetState { get; set; }
        public SoftQuota softQuota { get; set; }
        public string financeCode { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public HardQuota hardQuota { get; set; }
        public string name { get; set; }
        public int billingCodeId { get; set; }
        public ProjectedUsage projectedUsage { get; set; }
        public Customer customer { get; set; }
        public CurrentUsage currentUsage { get; set; }
    }
}
