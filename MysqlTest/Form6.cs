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
    public partial class Form6 : Form
    {
        string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=db;sslmode = none;";

        public Form6()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            #region 檢查mysql有沒有打開
            MySqlConnection CheckDB = new MySqlConnection(MySQLConnectionString);
            try
            {
                CheckDB.Open();
                CheckDB.Close();
                MySqlConnection.ClearPool(CheckDB);
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message, "SQL錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                Environment.Exit(Environment.ExitCode);
            }
            #endregion

            using (SQLClass initialization = new SQLClass("show tables", MySQLConnectionString))
            {
                for (int Index = 0; initialization.Reader.Read(); Index++)
                {
                    TabPage page = new TabPage();
                    page.Text = initialization.Reader.GetString(0);//page name
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

                    using (SQLClass title = new SQLClass("select column_name from INFORMATION_SCHEMA.COLUMNS where table_name='" + initialization.Reader.GetString(0) + "'", MySQLConnectionString))
                    {
                        for (int strindex = 0; title.Reader.Read(); strindex++)
                        {
                            DataGridViewTextBoxColumn Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
                            Column.HeaderText = title.Reader.GetString(0);
                            Column.Name = title.Reader.GetString(0);
                            Column.ReadOnly = true;
                            Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
                            Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                            data.Columns.AddRange(Column);
                        }
                    }
                    addrow("select * from " + initialization.Reader.GetString(0), data);
                }
                searchListbox(tabControl1.SelectedIndex);
            }
        }
        private void addrow(string DbCommand, DataGridView data)
        {
            using (SQLClass rowData = new SQLClass(DbCommand, MySQLConnectionString))
            {
                while (rowData.Reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)data.Rows[0].Clone();
                    for (int index = 0; index < rowData.Reader.FieldCount; index++)
                        row.Cells[index].Value = rowData.Reader.GetString(index);
                    data.Rows.Add(row);
                }
            }
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
            string tbCol = listBox1.Items[listBox1.SelectedIndex].ToString();
            string search = textBox1.Text;
            foreach (Control con in tabControl1.Controls)
            {
                string tbName = ((TabPage)con).Text;
                ((DataGridView)con.Controls[0]).Rows.Clear();
                if (((DataGridView)con.Controls[0]).Columns.Contains(tbCol))//先確認page的Column裡面有沒有tbCol
                    addrow("select * from " + tbName + " where " + tbCol + " ='" + search + "'", (DataGridView)con.Controls[0]);
            }
            timer1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control con in tabControl1.Controls)
            {
                ((DataGridView)con.Controls[0]).Rows.Clear();
                string tbName = ((TabPage)con).Text;
                addrow("select * from " + tbName,(DataGridView)con.Controls[0]);
            }
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Control con in tabControl1.Controls)
            {
                ((DataGridView)con.Controls[0]).Rows.Clear();
                string tbName = ((TabPage)con).Text;
                addrow("select * from " + tbName, (DataGridView)con.Controls[0]);
            }
        }
    }
}
//
//tabControl1.SelectedIndex = 0;程式碼控制頁面