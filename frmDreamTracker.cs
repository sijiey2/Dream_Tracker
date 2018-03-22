using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DreamTracker
{
    public partial class frmDreamTracker : Form
    {
        SqlConnection cs = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Sijie\Desktop\DreamTracker\DreamTracker\DreamTracker_DB.mdf;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        public frmDreamTracker()
        {
            InitializeComponent();
        }

        //Load form
        private void frmDreamTracker_Load(object sender, EventArgs e)
        {
            //
            //Load the texts of the 4 Buttons
            //
            //First --- Load Dream1 Button
            cs.Open();
            cmd.CommandText = "Select Keyword from tblSettingGoals Where DreamNum = 'Dream1' ";
            cmd.Connection = cs;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                btnDream1.Text = dr["Keyword"].ToString();
                cs.Close();
            }
            else
            {
                cs.Close();
            }
            //Second --- Load Dream2 Button
            cs.Open();
            cmd.CommandText = "Select Keyword from tblSettingGoals Where DreamNum = 'Dream2' ";
            cmd.Connection = cs;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                btnDream2.Text = dr["Keyword"].ToString();
                cs.Close();
            }
            else
            {
                cs.Close();
            }
            //Third ---Load Dream3 Button
            cs.Open();
            cmd.CommandText = "Select Keyword from tblSettingGoals Where DreamNum = 'Dream3' ";
            cmd.Connection = cs;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                btnDream3.Text = dr["Keyword"].ToString();
                cs.Close();
            }
            else
            {
                cs.Close();
            }
            //Fourth ---Load Dream4 Button
            cs.Open();
            cmd.CommandText = "Select Keyword from tblSettingGoals Where DreamNum = 'Dream4' ";
            cmd.Connection = cs;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                btnDream4.Text = dr["Keyword"].ToString();
                cs.Close();
            }
            else
            {
                cs.Close();
            }


        }

        //Quit Button
        private void btnQuit_Click(object sender, EventArgs e)
        {
            //Close the main form
            this.Close();
        }


        //SettingGoals Button
        private void btnSettingGoals_Click(object sender, EventArgs e)
        {
            //Show SettingGoals form
            frmSettingGoals frmSettingGoalsDialog = new frmSettingGoals();
            frmSettingGoalsDialog.Show();

        }


        //Dream1 Button
        private void btnDream1_Click(object sender, EventArgs e)
        {
            //Show Dream1 form
            frmDream1 frmDream1Dialog = new frmDream1();
            frmDream1Dialog.Show();
        }


        //Dream2 Button
        private void btnDream2_Click(object sender, EventArgs e)
        {
            //Show Dream2 form
            frmDream2 frmDream2Dialog = new frmDream2();
            frmDream2Dialog.Show();
        }


        //Dream3 Button
        private void btnDream3_Click(object sender, EventArgs e)
        {
            //Show Dream3 form
            frmDream3 frmDream3Dialog = new frmDream3();
            frmDream3Dialog.Show();
        }


        //Dream4 Button
        private void btnDream4_Click(object sender, EventArgs e)
        {
            //Show Dream4 form
            frmDream4 frmDream4Dialog = new frmDream4();
            frmDream4Dialog.Show();
        }


        //Refresh Button
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //
            //Refresh the texts of the 4 Buttons
            //
            //First --- Refresh Dream1 Button
            cs.Open();
            cmd.CommandText = "Select Keyword from tblSettingGoals Where DreamNum = 'Dream1' ";
            cmd.Connection = cs;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                btnDream1.Text = dr["Keyword"].ToString();
                cs.Close();
            }
            else
            {
                cs.Close();
            }
            //Second --- Refresh Dream2 Button
            cs.Open();
            cmd.CommandText = "Select Keyword from tblSettingGoals Where DreamNum = 'Dream2' ";
            cmd.Connection = cs;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                btnDream2.Text = dr["Keyword"].ToString();
                cs.Close();
            }
            else
            {
                cs.Close();
            }
            //Third --- Refresh Dream3 Button
            cs.Open();
            cmd.CommandText = "Select Keyword from tblSettingGoals Where DreamNum = 'Dream3' ";
            cmd.Connection = cs;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                btnDream3.Text = dr["Keyword"].ToString();
                cs.Close();
            }
            else
            {
                cs.Close();
            }
            //Fourth --- Refresh Dream4 Button
            cs.Open();
            cmd.CommandText = "Select Keyword from tblSettingGoals Where DreamNum = 'Dream4' ";
            cmd.Connection = cs;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                btnDream4.Text = dr["Keyword"].ToString();
                cs.Close();
            }
            else
            {
                cs.Close();
            }
    
          
        }


        //Confirm Quit
        private void frmDreamTracker_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Show Question Message
            if (MessageBox.Show(" Are you sure that you want to close this form ?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }




    }
}
