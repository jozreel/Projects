using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
namespace emails
{
    public class db
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        public db()
        {
            initializeDB();
        }
        private void initializeDB()
        {

            server = "localhost";
            database = "taxi360";
            uid = "root";
            password = "password";
            string connectionString;
            try
            {
                connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

                connection = new MySqlConnection(connectionString);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
               
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                       Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        //Select statement
        public List<taxiJob>Select()
        {
            string query = "SELECT * FROM asg_req where req_status = \"assigned\"";

            //Create a list to store the result
            List<taxiJob> list = new List<taxiJob>();
           

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list.Add(new taxiJob(dataReader["req_pickup"] + "", dataReader["asg_driveremail"] + "",  dataReader["req_whom"] + "", dataReader["location_from"] + "",dataReader["location_to"] + ""));
                   
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }
    
    }

    public class taxiJob
    {
        public taxiJob(string reqP, string asgDriver,string reqWh,string locFrom,string locTo)
        {
            reqPickup = reqP;
            asg_driveremail = asgDriver;
            req_whom = reqWh;
            location_to = locTo;
            location_from = locFrom;
        }
        public string reqPickup{get;set;}
       public string asg_driveremail { get; set; }
        public string req_whom { get; set; }
        public string location_from{ get; set; }
        public string location_to { get; set; }

        
    }
}
