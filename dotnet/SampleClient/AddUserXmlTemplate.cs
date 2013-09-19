using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dell.CTO.Enstratius
{
    public class AddUserXmlTemplate
    {
        public static string getXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<addUser>");
            sb.Append("<users>");
            sb.Append("<user>");
            sb.Append("<account accountId=\"{accountId}\" />");
            sb.Append("<givenName>{givenName}</givenName>");
            sb.Append("<familyName>{familyName}</familyName>");
            sb.Append("<email>{email}</email>");
            sb.Append("<notificationsTargets>");
            sb.Append("<emailTarget>{emailTarget}</emailTarget>");
            sb.Append("</notificationsTargets>");
            sb.Append("<notificationsSettings>");
            sb.Append("<notificationsSetting>");
            sb.Append("<notifyViaEmail>{notifyViaEmail}</notifyViaEmail>");
            sb.Append("<notifyViaScreen>{notifyViaScreen}</notifyViaScreen>");
            sb.Append("<eventType>{eventType}</eventType>");
            sb.Append("<severity>{severity}</severity>");
            sb.Append("</notificationsSetting>");
            sb.Append("</notificationsSettings>");
            sb.Append("<billingCodes>");
            sb.Append("<billingCode billingCodeId=\"{billingCode}\" />");
            sb.Append("</billingCodes>");
            sb.Append("<groups>");
            sb.Append("<group groupId=\"{groupId}\" />");
            sb.Append("</groups>");
            sb.Append("</user>");
            sb.Append("</users>");
            sb.Append("</addUser>");
            return sb.ToString();
        }
    }
}
