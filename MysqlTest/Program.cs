using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MysqlTest
{   

    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new Form1());
            // Application.Run(new Form2());
            Application.Run(new Form1());



            //using (SQLClass test = new SQLClass("show tables"))
            //{
            //    Console.WriteLine();
            //}
            //Program.test();
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //Console.WriteLine();
            //Console.WriteLine();
        }

       

    }
}
