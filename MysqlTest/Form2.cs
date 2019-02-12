using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using System.IO;

namespace MysqlTest
{
    public partial class Form2 : Form
    {
        string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=db;CharSet=utf8; sslmode = none;";

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (string f in Directory.GetDirectories(Directory.GetCurrentDirectory()))
                if (f.Contains(textBox1.Text))
                {
                    MessageBox.Show("檔名重複", string.Empty, MessageBoxButtons.OK,
                          MessageBoxIcon.Information);
                    return;
                }

            MySqlConnection face_count = new MySqlConnection(MySQLConnectionString);
            face_count.Open();
            MySqlCommand cmd_face_count = new MySqlCommand("select count(*) from face_info;", face_count);
            MySqlDataReader count_reader = cmd_face_count.ExecuteReader();
            count_reader.Read();
            MySqlConnection databaseConnection = new MySqlConnection(MySQLConnectionString);
            databaseConnection.Open();
            string adddata = string.Format("insert into face_info value('{0:000000}','{1}\\{2}','{3}')", Convert.ToInt16(count_reader.GetString(0))+1, Directory.GetCurrentDirectory(), textBox1.Text, comboBox1.Text).Replace("\\", "\\\\");
            MySqlCommand commandDatabase = new MySqlCommand(adddata, databaseConnection);
            commandDatabase.Connection = databaseConnection;
            commandDatabase.ExecuteNonQuery();
            Console.WriteLine(adddata);
            databaseConnection.Close();
            face_count.Close();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            MySqlConnection databaseConnection = new MySqlConnection(MySQLConnectionString);
            databaseConnection.Open();
            MySqlCommand commandDatabase = new MySqlCommand("select * from student_info", databaseConnection);
            MySqlDataReader myReader = commandDatabase.ExecuteReader();
            while (myReader.Read())
                comboBox1.Items.Add(myReader.GetString(0));
            databaseConnection.Close();
        }
    }
}
