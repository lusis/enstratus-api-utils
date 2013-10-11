using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dell.CTO.Enstratius;
using RestSharp;


namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Client c = new Client("http://demo.enstratius.com:15000", "HUFVVXTGJWVZYFWRMAHU", "vImYFqGkUAIY10Rn0YUIlyQ+lgTCL+ctyQ9Zhgv9", "test", "/api/enstratus/2013-03-13");
            //string resource = "/api/enstratus/2013-01-29/automation/Deployment";

            //c.AddHeader("x-es-details", "basic");
            var list = c.GetServerList();
            if (list != null)
                MessageBox.Show("hi");

        }

     
    }
}
