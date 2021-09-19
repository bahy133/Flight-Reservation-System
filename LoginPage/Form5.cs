using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace LoginPage
{
    public partial class Form5 : Form
    {
        OracleConnection con;
        CrystalReport2 cr;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            con = new OracleConnection("Data Source = ORCL ; User Id=scott; Password = tiger;");
            con.Open();
            cr = new CrystalReport2();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "select F_ID from ticket where PASS_SSN = :ssn";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("ssn", global.passuser);
            OracleDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            dr.Close();
            con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please Choose a Ticket");
            }
            else
            {
                cr.SetParameterValue(0, comboBox1.Text);
                crystalReportViewer1.ReportSource = cr;
            }
        }
    }
}
