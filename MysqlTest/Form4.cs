using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MysqlTest//用DataGridView來顯示圖片
{


    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
       
        private void Form4_Load(object sender, EventArgs e)
        {

        #region 檢查有無Picture資料夾
            Console.WriteLine(Directory.GetCurrentDirectory());
            if (!Directory.Exists("Picture"))
                Directory.CreateDirectory("Picture");
        #endregion

            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add("die_gedanken_sind_frei.gif", "111");
            dataGridView1.Rows.Add("朝鮮愛國歌.jpg", "111");
            dataGridView1.Rows.Add("義勇軍進行曲.png", "111");
            dataGridView1.Rows.Add("233", "111");
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                if (File.Exists("Picture/" + dataGridView1.CurrentCell.Value.ToString()))//先看看圖片存不存在
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = Directory.GetCurrentDirectory() + "/Picture/" + dataGridView1.CurrentCell.Value.ToString();
                    process.StartInfo.Arguments = "rundll32.exe C://WINDOWS//system32//shimgvw.dll,ImageView_Fullscreen";
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    process.Start();//使用windows的圖片顯示器來顯示圖片
                    process.Close();
                }
                else
                {
                    MessageBox.Show(Directory.GetCurrentDirectory() + "/Picture/" + dataGridView1.CurrentCell.Value.ToString() + "\n圖片不存在", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
