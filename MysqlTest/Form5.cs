using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=db;sslmode = none;";
            MySqlConnection dbcon = new MySqlConnection(MySQLConnectionString);
            MySqlCommand commad = new MySqlCommand("show tables", dbcon);
            dbcon.Open();
            MySqlDataReader countReader = commad.ExecuteReader();
            Console.WriteLine(countReader.FieldCount);
            dbcon.Close();
            commad = new MySqlCommand("select * from face_info", dbcon);
            dbcon.Open();
            MySqlDataReader countReader2 = commad.ExecuteReader();
            Console.WriteLine(countReader2.FieldCount);
            dbcon.Close();



        }
    }
}
