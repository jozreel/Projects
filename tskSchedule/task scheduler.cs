using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using System.IO;
using System.Reflection;
using emails;
namespace tskSchedule
{
    public partial class task_scheduler :  DevComponents.DotNetBar.Metro.MetroForm
    {
        private bool taskDefined = false;
        public task_scheduler()
        {
            InitializeComponent();
        }

        private void task_scheduler_Load(object sender, EventArgs e)
        {
            using (TaskService ts = new TaskService())
            {
                Task t = ts.GetTask("taxiService");
               if(t != null) taskDefined = true;
            }
        }

        private void dateTimeInput1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimeInput2_Click(object sender, EventArgs e)
        {

        }

        private void labelX1_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (taskDefined == false)
            {
                startSchedule(createSchedule(dateTimeInput1.Value));
                taskDefined = true;
            }
            else
                modifyTask(createSchedule(dateTimeInput1.Value));
        }

        public void showForm()
        {
            this.Show();
        }
        public DailyTrigger createSchedule(DateTime t)
        {
            DailyTrigger dt = new DailyTrigger();
            dt.StartBoundary = t;
            return dt;

        }

        static void startSchedule(DailyTrigger dt)
        {
            using (TaskService ts = new TaskService())
            {
             
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Start Taxi Service";

                

                td.Triggers.Add(dt);
            

                string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            
                td.Actions.Add(new ExecAction(appPath+@"\emails.exe", "c:\\test.log", null));
                
                
                string d = dt.StartBoundary.TimeOfDay.ToString();
                //string nd= d.Replace(':', 'T');
                ts.RootFolder.RegisterTaskDefinition(@"taxiService", td);

                // Remove the task we just created
               // ts.RootFolder.DeleteTask("Test");
            }
        }

        public static void modifyTask(DailyTrigger dt)
        {
            using (TaskService ts = new TaskService())
            {
                Task t = ts.GetTask("taxiService");
               TaskDefinition td = t.Definition;
               td.Triggers.Add(dt);
               ts.RootFolder.RegisterTaskDefinition(@"taxiService", td);
            }
        }
    }
}
