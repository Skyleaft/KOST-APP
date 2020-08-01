using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;

namespace KOST_APP
{
    /// <summary>
    /// Interaction logic for Window_Login.xaml
    /// </summary>
    public partial class Window_Login : Window
    {
        koneksi k = new koneksi();
        public Window_Login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            String nip = "";
            String cekhak = "";
            k.sql = "select *from user inner join pemilik on user.id_relasi=pemilik.id_pemilik where username='" + txt_username.Text + "' and password='" + txt_password.Password + "'";
            k.setdt();
            int cekbaris = k.dt.Rows.Count;
            foreach (DataRow baris in k.dt.Rows)
            {
                nip = baris[2].ToString();
                cekhak = baris[3].ToString();
            }


            if (txt_username.Text == "" && txt_password.Password == "")
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Username dan Password Belum Di isi" }
                };
                DialogHost.Show(sampleMessageDialog, "LoginDialog");
            }
            else if (cekbaris == 0)
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Username atau Password Salah" }
                };
                DialogHost.Show(sampleMessageDialog, "LoginDialog");
                txt_username.Text = "";
                txt_password.Password = "";
            }
            else
            {
                //k.sql = "select * from session order by session_id asc";
                //k.setdt();
                //int cek = k.dt.Rows.Count;
                //String sesid, data;
                //int tambah;
                //DateTime dtime = DateTime.Now;

                //if (cek == 0)
                //{
                //    sesid = dtime.ToString("ddHHmm") + "01";
                //}
                //else
                //{
                //    data = k.dt.Rows[cekbaris - 1][0].ToString();
                //    tambah = Convert.ToInt32(data.Substring(data.Length - 2, 2)) + 1;
                //    if (tambah < 10)
                //    {
                //        sesid = dtime.ToString("ddHHmm") + "0" + tambah;
                //    }
                //    else
                //    {
                //        sesid = dtime.ToString("ddHHmm") + tambah;
                //    }
                //}
                MainWindow main = new MainWindow();
                main.Show();
                dissapear();

            }
        }

        private void dissapear()
        {
            Close();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_close_Click2(object sender, RoutedEventArgs e)
        {

        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            var keluar = new SampleMessageYesNo
            {
                Message = { Text = "Yakin Keluar Aplikasi?" }
            };
            DialogHost.Show(keluar, "LoginDialog");
        }

        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
