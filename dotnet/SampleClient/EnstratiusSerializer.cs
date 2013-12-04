// Copyright (c) 2013 Dell. All rights reserved. Written by Doron Grinstein doron.grinstein@software.dell.com

using RestSharp.Serializers;
using System.IO;


namespace Dell.CTO.Enstratius
{
    public class EnstratiusSerializer<T> : ISerializer
    {
        public string ContentType
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public string DateFormat
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public string Namespace
        {
            get
            {
                return "";
            }
            set
            {

            }
        }

        public string RootElement
        {
            get
            {
                return null;
            }
            set
            {
            }
        }


        /// <summary>
        /// restSharp doesn't respect serialization attributes which the System.Xml.Serialization namespace does.
        /// in order to serialize the launch object the way that Enstratius accepts, I am overriding the 
        /// xml serialization. Notice that I strip out the xml <?xml.. first line because Enstratius doesn't like it..
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Serialize(object obj)
        {
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(T));
            StringWriter sb = new StringWriter();

            x.Serialize(sb, obj);
            string xml = sb.ToString();
            xml = xml.Remove(0, xml.IndexOf('\n'));

            return xml;
        }
    }

}
