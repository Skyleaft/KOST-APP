using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace KOST_APP
{
    class koneksi
    {
        public MySqlConnection dbkoneksi;
        public MySqlCommand perintah;
        public MySqlDataAdapter da;
        public DataTable dt;
        public DataSet ds;
        //192.168.1.112
        //public String db = @"Data Source=192.168.1.109,1433;
        //                    Network Library=DBMSSOCN;Initial Catalog=db_dmb;
        //                    User ID=milzan;Password=12345678";
        public String db = @"server=localhost;user=root;database=db_kostapp;port=3306;password=''";
        public String sql;

        public DataTable res;


        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        public void loadSetting()
        {
            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string fileName = Path.Combine(path, "settingKoneksi.csv");

            if (!File.Exists(fileName))
            {
                // Create the file.
                using (FileStream fs = File.Create(fileName))
                {
                    DataTable table = new DataTable();
                    //columns  
                    table.Columns.Add("Host", typeof(string));
                    table.Columns.Add("Port", typeof(string));
                    table.Columns.Add("Db_Name", typeof(string));
                    table.Columns.Add("user", typeof(string));
                    table.Columns.Add("pass", typeof(string));

                    table.Rows.Add("localhost", "3306", "db_kostapp","root","");

                    StringBuilder sb = new StringBuilder();

                    IEnumerable<string> columnNames = table.Columns.Cast<DataColumn>().
                                                      Select(column => column.ColumnName);
                    sb.AppendLine(string.Join(",", columnNames));

                    foreach (DataRow row in table.Rows)
                    {
                        IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                        sb.AppendLine(string.Join(",", fields));
                    }
                    fs.Close();
                    File.WriteAllText("settingKoneksi.csv", sb.ToString());
                    
                }
            }
            
        }

        public void backupDB()
        {
            setDB();
            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string fileName = Path.Combine(path, "BackupDatabase.sql");
            try
            {
                using (MySqlConnection conn = new MySqlConnection(db))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ExportToFile(fileName);
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error backup " + ex);

            }
        }

        public void restoreDB()
        {
            setDB();
            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string fileName = Path.Combine(path, "BackupDatabase.sql");
            try
            {
                using (MySqlConnection conn = new MySqlConnection(db))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ImportFromFile(fileName);
                            conn.Close();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("error restore " + ex);
                
            }
        }

        public void saveSetting()
        {
            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string fileName = Path.Combine(path, "settingKoneksi.csv");

            if (File.Exists(fileName))
            {
                using (StreamWriter writer = new StreamWriter(fileName, false))
                {
                    StringBuilder sb = new StringBuilder();

                    IEnumerable<string> columnNames = res.Columns.Cast<DataColumn>().
                                                      Select(column => column.ColumnName);
                    sb.AppendLine(string.Join(",", columnNames));

                    foreach (DataRow row in res.Rows)
                    {
                        IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                        sb.AppendLine(string.Join(",", fields));
                    }

                    writer.Write(sb.ToString());
                    writer.Close();
                }
            }




        }


        public void setDB()
        {
            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string fileName = Path.Combine(path, "settingKoneksi.csv");
            res = ConvertCSVtoDataTable(fileName);
            db = @"server="+res.Rows[0][0].ToString()+";" +
                "user="+res.Rows[0][3]+";" +
                "database="+res.Rows[0][2]+";" +
                "port="+res.Rows[0][1]+";" +
                "password="+res.Rows[0][4]+"";
        }

        public void setdt()
        {
            try
            {
                setDB();
                dbkoneksi = new MySqlConnection(db);
                perintah = new MySqlCommand(sql, dbkoneksi);
                dt = new DataTable();
                da = new MySqlDataAdapter(perintah);
                da.Fill(dt);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Koneksi :"+ex);
            }
        }

        public void setds()
        {
            setDB();
            dbkoneksi = new MySqlConnection(db);
            perintah = new MySqlCommand(sql, dbkoneksi);
            ds = new DataSet();
            da = new MySqlDataAdapter(perintah);
            da.Fill(ds, "tabel");

        }

        public void open()
        {
            setDB();
            dbkoneksi = new MySqlConnection(db);
            dbkoneksi.Open();
        }
        public void close()
        {
            setDB();
            dbkoneksi = new MySqlConnection(db);
            dbkoneksi.Close();
        }

        public void setparam()
        {
            setDB();
            dbkoneksi = new MySqlConnection(db);
            dbkoneksi.Open();
            perintah = new MySqlCommand(sql, dbkoneksi);
        }
    }
}

