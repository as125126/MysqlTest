using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace MysqlTest
{
    class SQLClass : IDisposable
    {
        public readonly MySqlDataReader Reader;
        MySqlConnection databaseConnection;
        MySqlCommand commandDatabase;

        public SQLClass(string command, string SQLConnectionString)
        {
            databaseConnection = new MySqlConnection(SQLConnectionString);
            commandDatabase = new MySqlCommand(command, databaseConnection);
            try
            {
                databaseConnection.Open();
                Reader = commandDatabase.ExecuteReader();
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message, "SQL錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                databaseConnection.Close();
                MySqlConnection.ClearPool(databaseConnection);
            }
        }

        #region 關閉DB連線
        private bool disposedValue = false; // 偵測多餘的呼叫
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    databaseConnection.Close();
                    MySqlConnection.ClearPool(databaseConnection);
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
