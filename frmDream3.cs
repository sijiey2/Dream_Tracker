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
    public partial class frmDream3 : Form
    {
        SqlConnection cs = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Sijie\Desktop\DreamTracker\DreamTracker\DreamTracker_DB.mdf;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        double dblAddHours = 0;

        public frmDream3()
        {
            InitializeComponent();
        }

        //Load the Dream3 form
        private void frmDream3_Load(object sender, EventArgs e)
        {
            //
            //Show Previous Records
            //
            //First --- Show Expected Hours
            cs.Open();
            cmd.CommandText = "Select * from tblSettingGoals Where DreamNum = 'Dream3'";
            cmd.Connection = cs;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lblExpectTime.Text = dr["ExpectTime"].ToString();
                this.Text = dr["Keyword"].ToString();
                cs.Close();
            }
            else
            {
                cs.Close();
            }
            //Second --- Show CountDays
            cs.Open();
            cmd.CommandText = "SELECT TOP 1 * FROM tblDream3 ORDER BY CountDays DESC";
            cmd.Connection = cs;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtCountDays.Text = dr["CountDays"].ToString();
                cs.Close();
            }
            else
            {
                cs.Close();
            }
            //Third --- Show SumOfHours
            cs.Open();
            cmd.CommandText = "SELECT SumOfHours FROM tblDream3 WHERE CountDays = (SELECT MAX(CountDays) FROM tblDream3)";
            cmd.Connection = cs;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtSumOfHours.Text = dr["SumOfHours"].ToString();
                cs.Close();
            }
            else
            {
                cs.Close();
            }
            //Fourth --- Show AverageHours
            cs.Open();
            cmd.CommandText = "SELECT AverageHours FROM tblDream3 WHERE CountDays = (SELECT MAX(CountDays) FROM tblDream3)";
            cmd.Connection = cs;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtAverageHours.Text = dr["AverageHours"].ToString();
                cs.Close();
            }
            else
            {
                cs.Close();
            }
            //Fifth --- Show Percentage
            cs.Open();
            cmd.CommandText = "SELECT Percentage FROM tblDream3 WHERE CountDays = (SELECT MAX(CountDays) FROM tblDream3)";
            cmd.Connection = cs;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtPercentage.Text = dr["Percentage"].ToString();
                cs.Close();
            }
            else
            {
                cs.Close();
            }
            //Sixth --- Show Progress Bar
            this.timer.Start();
            //Seventh --- Show Chart
            for (int dblCountDays = 1; dblCountDays <= double.Parse(txtCountDays.Text); dblCountDays++) {
                cs.Open();
                cmd.CommandText = "Select AddHours From tblDream3 Where CountDays = '" + dblCountDays + "' ";
                cmd.Connection = cs;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    this.chtHours.Series["Hours"].Points.AddXY(dblCountDays, dr["AddHours"]);
                    cs.Close();
                }
                else
                {
                    cs.Close();
                }
            }

        }


        //Quit Button
        private void btnQuit_Click(object sender, EventArgs e)
        {
            //Close the Dream3 form
            this.Close();
        }

        //Confirm Quit
        private void frmDream3_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Show Warrning Message
            if (MessageBox.Show("Are you sure that you want to close this form ?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                e.Cancel = true;
            }
                
        }


        //AddHours Button
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //if txt boxes are empty
            if (txtAddHoursH.Text == "")
            {
                txtAddHoursH.Text = "0";
            }
            if (txtAddHoursM.Text == "")
            {
                txtAddHoursM.Text = "0";

            }


            //Warning Message (0-24h and 0-60min)     
            if (double.Parse(txtAddHoursH.Text) <= 24 && double.Parse(txtAddHoursH.Text) >= 0)
            {
                if (double.Parse(txtAddHoursM.Text) <= 60 && double.Parse(txtAddHoursM.Text) >= 0)
                {

                    //
                    //Update the data in the form
                    //
                    //First --- Update all text boxes
                    dblAddHours = double.Parse(txtAddHoursH.Text) + double.Parse(txtAddHoursM.Text) / 60;
                    txtSumOfHours.Text = double.Parse(txtSumOfHours.Text) + dblAddHours + "";
                    if (double.Parse(lblExpectTime.Text) == 0)
                    {
                        //NaN situation
                        txtPercentage.Text = "0";
                    }
                    else
                    {
                        txtPercentage.Text = double.Parse(txtSumOfHours.Text) / double.Parse(lblExpectTime.Text) * 100 + "";
                    }
                    txtCountDays.Text = double.Parse(txtCountDays.Text) + 1 + "";
                    if (double.Parse(txtCountDays.Text) == 0)
                    {
                        //0 day situation
                        txtAverageHours.Text = "0";
                    }
                    else
                    {
                        txtAverageHours.Text = double.Parse(txtSumOfHours.Text) / double.Parse(txtCountDays.Text) + "";
                    }
                    //Second --- Refresh the chart
                    this.chtHours.Series["Hours"].Points.AddXY(double.Parse(txtCountDays.Text), dblAddHours);
                    //Third --- Refresh the progressbar
                    this.timer.Start();

                    //
                    //If 100 Percent
                    //
                    //Congratulation MessageBox
                    if (double.Parse(txtPercentage.Text) >= 100)
                    {
                        MessageBox.Show("CONGRATULATIONS! You have successully achieved your goal! Your Dream is going to COME TRUE!");
                    }

                }
                else
                {
                    MessageBox.Show("Minuites are between 0 and 60!");
                    txtAddHoursM.Focus();
                }
            }
            else
            {
                MessageBox.Show("Hours are between 1 and 24!");
                txtAddHoursH.Focus();
            }
        }


        //Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            //Insert Data
            cs.Open();
            cmd.CommandText = "Insert into tblDream3 (CountDays, SumOfHours, AverageHours, Percentage, AddHours) Values ( '" + txtCountDays.Text + "', '" + txtSumOfHours.Text + "', '" + txtAverageHours.Text + "', '" + txtPercentage.Text + "', '" + dblAddHours + "') ";
            cmd.Connection = cs;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Your Hours have been Saved!");
            cs.Close();

        }


        //Timer
        private void timer_Tick(object sender, EventArgs e)
        {
            int pgbPercentageNum = (int)double.Parse(txtPercentage.Text);

            if (this.pgbPercentage.Value < pgbPercentageNum)
            {
                this.pgbPercentage.Increment(1);
            }
            else
            {
                this.timer.Stop();
            }
        }

        //txtAddHours MouseDown
        //First --- Hours
        private void txtAddHoursH_MouseDown(object sender, MouseEventArgs e)
        {
            txtAddHoursH.Text = "";
        }
        //Second --- Mins
        private void txtAddHoursM_MouseDown(object sender, MouseEventArgs e)
        {
            txtAddHoursM.Text = "";
        }

        




    }
}
