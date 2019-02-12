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
    public partial class Form1 : Form
    {
        string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=db;sslmode = none;";

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MySqlConnection databaseConnection = new MySqlConnection(MySQLConnectionString);
            MySqlCommand commandDatabase = new MySqlCommand("show tables", databaseConnection);
            commandDatabase.CommandTimeout = 60;
            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                if (myReader.HasRows)
                {
                    for (int Index = 0; myReader.Read(); Index++)
                    {
                        TabPage page = new TabPage();
                        page.Text = myReader.GetString(0);
                        tabControl1.TabPages.Add(page);
                        page.Location = new System.Drawing.Point(4, 38);
                        page.Padding = new System.Windows.Forms.Padding(3);
                        page.Size = new System.Drawing.Size(782, 798);
                        page.UseVisualStyleBackColor = true;

                        DataGridView data = new DataGridView();
                        page.Controls.Add(data);
                        data.Location = new System.Drawing.Point(3, 3);
                        data.RowTemplate.Height = 27;
                        data.Size = new System.Drawing.Size(776, 792);
                        data.Dock = System.Windows.Forms.DockStyle.Fill;
                        data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                        MySqlConnection dc = new MySqlConnection(MySQLConnectionString);
                        MySqlCommand title = new MySqlCommand("select column_name from INFORMATION_SCHEMA.COLUMNS where table_name='" + myReader.GetString(0) + "'", dc);
                        dc.Open();
                        MySqlDataReader titleReader = title.ExecuteReader();
                        for (int strindex = 0; titleReader.Read(); strindex++)
                        {
                            DataGridViewTextBoxColumn Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
                            Column.HeaderText = titleReader.GetString(0);
                            Column.Name = titleReader.GetString(0);
                            Column.ReadOnly = true;
                            Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
                            Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                            data.Columns.AddRange(Column);
                        }
                        dc.Close();
                        addrow(myReader.GetString(0), string.Empty, string.Empty, data);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Query error" + ex.Message);
            }
            searchListbox(tabControl1.SelectedIndex);
            databaseConnection.Close();
        }
        private void addrow(string tbName, string tbCol, string search, DataGridView data)
        {
            MySqlConnection dc_count = new MySqlConnection(MySQLConnectionString);
            MySqlCommand title_count = new MySqlCommand("select count(*) from INFORMATION_SCHEMA.COLUMNS where table_name='" + tbName + "'", dc_count);
            dc_count.Open();
            MySqlDataReader countReader = title_count.ExecuteReader();

            MySqlConnection table_dc = new MySqlConnection(MySQLConnectionString);

            string DbCommand = string.Empty;

            if (tbCol == string.Empty && search == string.Empty)
                DbCommand = "select * from " + tbName;
            else
                DbCommand = "select * from " + tbName + " where " + tbCol + " ='" + search + "'";

            MySqlCommand rows = new MySqlCommand(DbCommand, table_dc);
            table_dc.Open();
            try
            {
                MySqlDataReader rowReader = rows.ExecuteReader();
                while (countReader.Read())
                    while (rowReader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)data.Rows[0].Clone();
                        for (int index = 0; index < countReader.GetInt32(0); index++)
                            row.Cells[index].Value = rowReader.GetString(index);
                        data.Rows.Add(row);
                    }
            }
            catch (Exception e) { }
            dc_count.Close();
            table_dc.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            searchListbox(tabControl1.SelectedIndex);
        }
        private void searchListbox(int SelectedTab)
        {

            for (int i = 0; i < ((DataGridView)tabControl1.Controls[SelectedTab].Controls[0]).ColumnCount; i++)
            {
                listBox1.Items.Add(((DataGridView)tabControl1.Controls[SelectedTab].Controls[0]).Columns[i].HeaderText);
            }
            listBox1.Location = new Point(label1.Location.X, label1.Location.Y + label1.Height + listBox1.ItemHeight * 1);
            listBox1.Height = listBox1.ItemHeight * (listBox1.Items.Count + 1);

            label2.Location = new Point(listBox1.Location.X, listBox1.Location.Y + listBox1.Height + listBox1.ItemHeight * 1);

            textBox1.Location = new Point(label2.Location.X, label2.Location.Y + label2.Height + listBox1.ItemHeight * 1);

            button1.Location = new Point(textBox1.Location.X, textBox1.Location.Y + textBox1.Height + listBox1.ItemHeight * 1);

            button2.Location = new Point(button1.Location.X, button1.Location.Y + button1.Height + listBox1.ItemHeight * 1);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select something in listbox");
                return;
            }
            foreach (Control con in tabControl1.Controls)
            {
                string tbCol = listBox1.Items[listBox1.SelectedIndex].ToString();
                string tbName = ((TabPage)con).Text;
                string search = textBox1.Text;
                ((DataGridView)con.Controls[0]).Rows.Clear();
                addrow(tbName, tbCol, search, (DataGridView)con.Controls[0]);
            }
            timer1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control con in tabControl1.Controls)
            {
                ((DataGridView)con.Controls[0]).Rows.Clear();
                string tbName = ((TabPage)con).Text;
                addrow(tbName, string.Empty, string.Empty, (DataGridView)con.Controls[0]);
            }
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Control con in tabControl1.Controls)
            {
                ((DataGridView)con.Controls[0]).Rows.Clear();
                string tbName = ((TabPage)con).Text;
                addrow(tbName, string.Empty, string.Empty, (DataGridView)con.Controls[0]);
            }
        }
    }
}
//
//tabControl1.SelectedIndex = 0;程式碼控制頁面