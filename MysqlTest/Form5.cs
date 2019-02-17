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
            string con = "datasource=127.0.0.1;port=3306;username=root;password=;database=db;sslmode = none;";
            string query = "select * from student_info";

            MySqlConnection connection = new MySqlConnection(con);
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter adap1 = new MySqlDataAdapter(cmd);
            DataTable ds1 = new DataTable();
            adap1.Fill(ds1);
            dataGridView1.DataSource = ds1;

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
