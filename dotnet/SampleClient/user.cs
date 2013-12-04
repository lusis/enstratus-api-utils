// Copyright (c) 2013 Dell. All rights reserved. Written by Doron Grinstein doron.grinstein@software.dell.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Dell.CTO.Enstratius
{
    public class user
    {
        public string accountId { get; set; }
        public string givenName { get; set; }
        public string familyName { get; set; }
        public string email { get; set; }
        public string emailTarget { get; set; }
        public string notifyViaEmail { get; set; }
        public string notifyViaScreen { get; set; }
        public string eventType { get; set; }
        public string severity { get; set; }
        public string billingCode { get; set; }
        public string groupId { get; set; }

    }
}
