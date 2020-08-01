using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void setdt()
        {
            dbkoneksi = new MySqlConnection(db);
            perintah = new MySqlCommand(sql, dbkoneksi);
            dt = new DataTable();
            da = new MySqlDataAdapter(perintah);
            da.Fill(dt);
        }

        public void setds()
        {
            dbkoneksi = new MySqlConnection(db);
            perintah = new MySqlCommand(sql, dbkoneksi);
            ds = new DataSet();
            da = new MySqlDataAdapter(perintah);
            da.Fill(ds, "tabel");

        }

        public void open()
        {
            dbkoneksi = new MySqlConnection(db);
            dbkoneksi.Open();
        }
        public void close()
        {
            dbkoneksi = new MySqlConnection(db);
            dbkoneksi.Close();
        }

        public void setparam()
        {
            dbkoneksi = new MySqlConnection(db);
            dbkoneksi.Open();
            perintah = new MySqlCommand(sql, dbkoneksi);
        }
    }
}

