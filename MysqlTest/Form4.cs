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


namespace MysqlTest
{
   

    public partial class Form4 : Form
    {
        string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=db;sslmode = none;";
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            MySqlConnection databaseConnection = new MySqlConnection(MySQLConnectionString);
            databaseConnection.Open();

            MySqlCommand commandDatabase = new MySqlCommand("show tables", databaseConnection);
            
            MySqlDataReader myReader = commandDatabase.ExecuteReader();

            MySqlCommand commandDatabase_1 = new MySqlCommand("show tables", databaseConnection);
          



            if (myReader.HasRows)
                for (int Index = 0; myReader.Read(); Index++)
                    Console.WriteLine(myReader.GetString(0));
            databaseConnection.Close();

            
            MySqlDataReader myReader_1 = commandDatabase_1.ExecuteReader();
            if (myReader_1.HasRows)
                for (int Index = 0; myReader_1.Read(); Index++)
                    Console.WriteLine(myReader_1.GetString(0));
            databaseConnection.Close();


        }
    }
}
