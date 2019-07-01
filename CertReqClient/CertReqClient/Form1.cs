using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CertReqClient
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            string commonName = textBox_commonName.Text;
            string SubjectAlternativeNames = textbox_alternativeNames.Text;
            string Organization = textbox_organization.Text;
            string Department = textBox_Department.Text;
            string City = textBox_City.Text;
            string State = textBox_State.Text;
            string Country = textBox_Country.Text;
            string Keysize = textBox_Keysize.Text;


        }
    }
}
