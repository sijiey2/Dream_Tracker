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
    public partial class frmSettingGoals : Form
    {
        SqlConnection cs = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Sijie\Desktop\DreamTracker\DreamTracker\DreamTracker_DB.mdf;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        string cmdText = "";

        double dblExpectTime = 0;
        double dblNumCheck = 0;

        public frmSettingGoals()
        {
            InitializeComponent();
        }


        //Load the SettingGoals form
        private void frmSettingGoals_Load(object sender, EventArgs e)
        {
            //Show saving data
            //User exp --- Show default Dream1 setting if it is set before
            cs.Open();
            cmdText = "SELECT *FROM tblSettingGoals WHERE DreamNum = 'Dream1'";
            SqlCommand ReadCommand = new SqlCommand(cmdText, cs);
            SqlDataReader dr = ReadCommand.ExecuteReader();
            //check if the Dream1 is set by the SettingGoals button
            if (dr.Read())
            {
                //Show the Dream1 setting
                txtKeyword.Text = dr["Keyword"].ToString();
                txtExpectTime.Text = dr["ExpectTime"].ToString();
                cboExpectTimeUnits.Text = "Hours";
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
            //Close the SettingGoals form
            this.Close();
        }


        //Save Button
        private void btnSave_Click(object sender, EventArgs e)
        {
            //Check if the blanks are filled correctly
            if (cboChooseDreams.Text == "Dream1" || cboChooseDreams.Text == "Dream2" || cboChooseDreams.Text == "Dream3" || cboChooseDreams.Text == "Dream4")
            {
                if (txtKeyword.Text != "")
                {
                    bool isNum = double.TryParse(txtExpectTime.Text, out dblNumCheck);
                    if (isNum)
                    {
                        if (cboExpectTimeUnits.Text == "Years" || cboExpectTimeUnits.Text == "Months" || cboExpectTimeUnits.Text == "Days" ||cboExpectTimeUnits.Text == "Hours")
                        {
                            switch (cboExpectTimeUnits.Text)
                            {
                                //
                                //Convert the ExpectTime to the unit of hours
                                //
                                case "Years":
                                    dblExpectTime = double.Parse(txtExpectTime.Text) * 365 * 24;
                                    break;

                                case "Months":
                                    dblExpectTime = double.Parse(txtExpectTime.Text) * 30 * 24;
                                    break;

                                case "Days":
                                    dblExpectTime = double.Parse(txtExpectTime.Text) * 24;
                                    break;

                                case "Hours":
                                    dblExpectTime = double.Parse(txtExpectTime.Text);
                                    break;

                                default:
                                    break;
                            }

                            //
                            // Save Data
                            //
                            //Read Data(update or add)                 
                            switch (cboChooseDreams.Text)
                            {
                                case "Dream1":
                                    cmdText = "SELECT DreamNum FROM tblSettingGoals WHERE DreamNum = 'Dream1'";
                                    break;

                                case "Dream2":
                                    cmdText = "SELECT DreamNum FROM tblSettingGoals WHERE DreamNum = 'Dream2'";
                                    break;

                                case "Dream3":
                                    cmdText = "SELECT DreamNum FROM tblSettingGoals WHERE DreamNum = 'Dream3'";
                                    break;

                                default:
                                    cmdText = "SELECT DreamNum FROM tblSettingGoals WHERE DreamNum = 'Dream4'";
                                    break;
                            }
                            cmd.CommandText = cmdText;
                            cmd.Connection = cs;

                            cs.Open();
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                            cs.Close();

                                //Update Data
                                cs.Open();
                                cmd.CommandText = "Update tblSettingGoals set Keyword = '"+txtKeyword.Text.ToUpper()+"', ExpectTime = '"+dblExpectTime+"' Where DreamNum = '"+cboChooseDreams.Text+"' ";
                                cmd.Connection = cs;
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Your Goal has been UPDATED!");
                                cs.Close();
                            }
                            else
                            {
                            cs.Close();

                                //Insert Data
                                cs.Open();
                                cmd.CommandText = "Insert Into tblSettingGoals (DreamNum, Keyword, ExpectTime) Values ('"+cboChooseDreams.Text+"', '"+txtKeyword.Text.ToUpper()+"', '"+dblExpectTime+"') ";
                                cmd.Connection = cs;
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Your Goal has been SAVED!");
                                cs.Close();
                            }

                        }
                        else
                        {
                            MessageBox.Show("Error. Please choose a unit from the Box");
                            cboExpectTimeUnits.Focus();

                        }
                    }

                    else
                    {
                        MessageBox.Show("Erro. Please give me a NUMBER of ExpectTime");
                        txtExpectTime.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Error. Please give me a keyword");
                    txtKeyword.Focus();
                }
            }
            else
            {
                MessageBox.Show("Error. Please choose one dream button from the Box");
                cboChooseDreams.Focus();
            }
        }


        //
        //User Experience
        //
        //cboChooseDream Change DreamNum ---> show previous data
        private void cboChooseDreams_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboChooseDreams.Text)
            {
                //Loading saving data
                case "Dream1":
                    cmdText = "SELECT *FROM tblSettingGoals WHERE DreamNum = 'Dream1'";
                    break;
                case "Dream2":
                    cmdText = "SELECT *FROM tblSettingGoals WHERE DreamNum = 'Dream2'";
                    break;
                case "Dream3":
                    cmdText = "SELECT *FROM tblSettingGoals WHERE DreamNum = 'Dream3'";
                    break;
                default:
                    cmdText = "SELECT *FROM tblSettingGoals WHERE DreamNum = 'Dream4'";
                    break;
            }

            cs.Open();
            SqlCommand ReadCommand = new SqlCommand(cmdText, cs);
            SqlDataReader dr = ReadCommand.ExecuteReader();
            //check if users has already set their goals 
            if (dr.Read())
            {
                //Show their previous setting 
                txtKeyword.Text = dr["Keyword"].ToString();
                txtExpectTime.Text = dr["ExpectTime"].ToString();
                cboExpectTimeUnits.Text = "Hours";
                cs.Close();
            }
            else
            {
                //Nothing shows out
                txtKeyword.Text = "";
                txtExpectTime.Text = "";
                cs.Close();
            }
        }


    }
}
