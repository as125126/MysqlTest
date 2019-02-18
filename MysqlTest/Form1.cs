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

        int ScanFaceInfoIndex = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void ScanFaceInfoWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            addrow("select * from scan_face_info where Face_Record >" + ScanFaceInfoIndex, dataGridView1);
            //把大於Face_Record的row都加進去
            using (SQLClass CurrentCount = new SQLClass("select count(*) from scan_face_info", MySQLConnectionString))
            {
                CurrentCount.Reader.Read();
                ScanFaceInfoIndex = CurrentCount.Reader.GetInt32(0);
            }
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
                this.Close();
                Environment.Exit(Environment.ExitCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                Environment.Exit(Environment.ExitCode);
            }
            #endregion

            dataGridView1.Location = new System.Drawing.Point(3, 3);
            dataGridView1.RowTemplate.Height = 27;
            dataGridView1.Size = new System.Drawing.Size(776, 792);
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            using (SQLClass title = new SQLClass("select column_name from INFORMATION_SCHEMA.COLUMNS where table_name='scan_face_info'", MySQLConnectionString))
            {
                for (int strindex = 0; title.Reader.Read(); strindex++)
                {
                    DataGridViewTextBoxColumn Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
                    Column.HeaderText = title.Reader.GetString(0);
                    Column.Name = title.Reader.GetString(0);
                    Column.ReadOnly = true;
                    Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
                    Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView1.Columns.AddRange(Column);
                }
            }
            addrow("select * from scan_face_info", dataGridView1);//把資料放入欄位


            using (SQLClass ScanFaceInfoCount = new SQLClass("select count(*) from scan_face_info", MySQLConnectionString))
            {
                ScanFaceInfoCount.Reader.Read();
                ScanFaceInfoIndex = ScanFaceInfoCount.Reader.GetInt32(0);
            }
        }


        private void addrow(string DbCommand, DataGridView data)
        //DbCommand 指令
        //data 放在哪一個表
        {
            using (SQLClass rowData = new SQLClass(DbCommand, MySQLConnectionString))
            {
                while (rowData.Reader.Read())
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(data);
                    for (int index = 0; index < rowData.Reader.FieldCount; index++)
                        row.Cells[index].Value = rowData.Reader.GetString(index);
                    data.Rows.Add(row);
                }
            }
        }
    }
}
