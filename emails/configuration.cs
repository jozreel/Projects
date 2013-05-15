using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Xml.Serialization;
using System.IO;
using System.Security.Cryptography;


namespace emails
{
    public partial class configuration : DevComponents.DotNetBar.Metro.MetroForm
    {
        private bool issl;
        public configuration()
        {
            
            InitializeComponent();
        }

        private void labelX2_Click(object sender, EventArgs e)
        {

        }

        private void labelX4_Click(object sender, EventArgs e)
        {

        }

        private void labelX5_Click(object sender, EventArgs e)
        {

        }

        private void configuration_Load(object sender, EventArgs e)
        {
            Data rx = null;
            XmlSerializer reader = new XmlSerializer(typeof(Data));
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            using (FileStream input = File.OpenRead(appPath + @"\data.xml"))
            {
                if (input.Length != 0)
                    rx = reader.Deserialize(input) as Data;
            }




            if (rx != null)
            {
                try
                {
                    textBoxX1.Text = rx.outGoing;
                    textBoxX2.Text = rx.userName;
                    textBoxX3.Text = rx.passwd;
                    integerInput1.Value = rx.port;
                    if(rx.ssl)
                        checkBoxX3.Checked =true;
                    else
                        checkBoxX2.Checked =true;
                   
                }
                catch (Exception sysex)
                {
                    throw sysex;
                }
            }

        }

        private void labelX7_Click(object sender, EventArgs e)
        {

        }

        private void labelX8_Click(object sender, EventArgs e)
        {

        }

        private void labelX1_Click(object sender, EventArgs e)
        {

        }

        private void groupPanel1_Click(object sender, EventArgs e)
        {

        }

        private void labelX3_Click(object sender, EventArgs e)
        {

        }

        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {

        }

        private void integerInput1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxX3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxX3.Checked)
            {
                checkBoxX2.Checked = false;
                issl = true;
            }
        }

        private void checkBoxX2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxX2.Checked)
            {
                checkBoxX3.Checked = false;
                issl = false;

            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            Data tx = new Data();
            tx.outGoing = textBoxX1.Text;
            tx.userName = textBoxX2.Text;
            tx.passwd = ProtectPassword(textBoxX3.Text);
            tx.port = integerInput1.Value;
            tx.ssl = issl;


            // Write to XML
            XmlSerializer writer = new XmlSerializer(typeof(Data));
            using (FileStream file = File.OpenWrite("data.xml"))
            {
                writer.Serialize(file, tx);
            }

        }


        public static string ProtectPassword(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] protectedPassword = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(protectedPassword);
        }

     

        private void buttonX2_Click(object sender, EventArgs e)
        {
            
        }
    }

    public class Data
    {
        public string outGoing { get; set; }
        public string userName { set; get; }
        public string passwd { get; set; }
        public int port{get;set;}
        public bool ssl { get; set; }

    }
}