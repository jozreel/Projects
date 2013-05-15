using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Xml.Serialization;
using System.IO;
using System.Security.Cryptography;
namespace emails
{
    public class email:ApplicationContext
    {

        private string emaila = "";
        private string passwd = "";
        private NetworkCredential loginInfo = null;
        private MailMessage msg = null;
        private SmtpClient smtpClient = null;
        private static Timer timer = null;


        public email()
        {
           
            Data rx=null;
            XmlSerializer reader = new XmlSerializer(typeof(Data));
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            using (FileStream input = File.OpenRead(appPath+@"\data.xml"))
            {
                if(input.Length !=0)
                   rx = reader.Deserialize(input) as  Data;
            }




            if (rx != null)
            {
                try
                {
                    emaila = rx.userName;
                    passwd = UnprotectPassword(rx.passwd);
                    loginInfo = new NetworkCredential(emaila, passwd);
                    msg = new MailMessage();
                    smtpClient = new SmtpClient(rx.outGoing, rx.port);
                    smtpClient.EnableSsl = rx.ssl;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = loginInfo;
                    this.createMessage();
                    Environment.Exit(0);
                }
                catch (SmtpException sysex)
                {




                    MessageBox.Show("Taxi Notification App Has Encountered a Problem " +sysex.Message + " Please Check Your Configuration Settings", "Taxi Notification Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else Environment.Exit(0);
           

        }

        public static string UnprotectPassword(string protectedPassword)
        {
            byte[] bytes = Convert.FromBase64String(protectedPassword);
            byte[] password = ProtectedData.Unprotect(bytes, null, DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(password);
        }
       


        private void createMessage()
        {
           db dd = new db();
           List<taxiJob> list = dd.Select();

            //for each record in DB where jobs not less than current date do the following 
           foreach (taxiJob ls in list)
           {
               string toAddr = ls.asg_driveremail;
               DateTime pickUp = Convert.ToDateTime(ls.reqPickup);
               string picUpConv = pickUp.ToString("f");
               string subject = "Taxi Service Job Alert";
           
               string message = "<p style=\"font-weight:bold;\">Job Reminder Alert</p>";
              message+= "<table><tr><td>Passenger: </td> <td>" + ls.req_whom+"</td></tr>";
              message += "<tr><td width=\"80\">From: </td><td>" + ls.location_from + "</td>";
              message += "<tr><td width=\"80\">To:</td><td>" + ls.location_to + "</td></tr>";
              message += "<tr><td width=\"80\">Time:</td><td> " + picUpConv + "<td></tr></table>";
               sendMessg(toAddr, subject, message);
           }
        }
        public void sendMessg(string address, string subject, string message)
        {
            try
            {  msg.To.Clear();
                msg.From = new MailAddress(emaila);
                msg.To.Add(new MailAddress(address));
                msg.Subject = subject;
                msg.Body = message;
                msg.IsBodyHtml = true;
                smtpClient.Send(msg);
            }
            catch (SmtpException sysex)
            {
                MessageBox.Show(sysex.Message + " Please Check Your Configuration Settings");
            }

        }

      
    }
}
