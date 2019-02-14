using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MysqlTest
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            string MyConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=db;sslmode = none;";
 
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd1 = connection.CreateCommand();
                cmd1.CommandText = "select * from student_info ";
                MySqlDataAdapter adap1 = new MySqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                adap1.Fill(ds1);
                dataGridView1.DataSource = ds1.Tables[0].DefaultView;
                
                MySqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "SHOW TABLES";
                MySqlDataAdapter adap2 = new MySqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                adap2.Fill(ds2);
                dataGridView1.DataSource = ds2.Tables[0].DefaultView;
            }
            catch (System.Exception ex)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
