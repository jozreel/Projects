using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using emails;
namespace emailJobs
{
   public class notification:ApplicationContext
    {
        private NotifyIcon ni = new NotifyIcon();
        public notification()
        {

            ContextMenuStrip cms = new ContextMenuStrip();
            ToolStripMenuItem exitToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem startTool = new ToolStripMenuItem();
            ToolStripMenuItem addEv = new ToolStripMenuItem();
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            exitToolStripMenuItem.Text = "&Exit";
            exitToolStripMenuItem.Click += new System.EventHandler(exitToolStripMenuItem_Click);


            startTool.Name = "startTool";
            startTool.Size = new System.Drawing.Size(152, 22);
            startTool.Text = "&Configure Outgoing Server";
            startTool.Click += new System.EventHandler(startTool_Click);


            addEv.Name = "AddEv";
            addEv.Size = new System.Drawing.Size(152, 22);
            addEv.Text = "&Add Schedule";
            addEv.Click += new System.EventHandler(addEv_Click);



            cms.Items.Add(exitToolStripMenuItem);
            cms.Items.Add(startTool);
            cms.Items.Add(addEv);

            cms.Size = new System.Drawing.Size(153, 48);
            ni.ContextMenuStrip = cms;
            ni.Icon = Properties.Resources.taxi;

            ni.Visible = true;

        
        }

      public  void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ni.Visible = false;
            Application.Exit();


        }

        public void startTool_Click(object sender, EventArgs e)
        {
            //email.startService();
            configuration conf = new configuration();
            conf.Show();


        }

       public void addEv_Click(object sender, EventArgs e)
        {

            tskSchedule.task_scheduler tsk = new tskSchedule.task_scheduler();
            tsk.showForm();


        }
    
    
       


    }
}
