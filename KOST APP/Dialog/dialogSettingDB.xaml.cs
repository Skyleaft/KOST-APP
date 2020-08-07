using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KOST_APP.Dialog
{
    /// <summary>
    /// Interaction logic for dialogTambahKamar.xaml
    /// </summary>
    public partial class dialogSettingDB : UserControl
    {
        koneksi k = new koneksi();
        public dialogSettingDB()
        {
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            k.setDB();
            txt_host.Text = k.res.Rows[0][0].ToString();
            txt_port.Text = k.res.Rows[0][1].ToString();
            txt_dbname.Text = k.res.Rows[0][2].ToString();
            txt_username.Text = k.res.Rows[0][3].ToString();
            txt_password.Text = k.res.Rows[0][4].ToString();
        }
        private void btn_simpan_Click(object sender, RoutedEventArgs e)
        {
            if(txt_host.Text == "" || txt_port.Text == "" || txt_dbname.Text == "")
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Lengkapi Dulu Data" }
                };
                DialogHost.Show(sampleMessageDialog, "SubDialogDB");
            }
            else
            {
                try
                {
                    k.setDB();
                    k.res.Rows[0][0] = txt_host.Text;
                    k.res.Rows[0][1] = txt_port.Text;
                    k.res.Rows[0][2] = txt_dbname.Text;
                    k.res.Rows[0][3] = txt_username.Text;
                    k.res.Rows[0][4] = txt_password.Text;
                    k.saveSetting();
                    DialogHost.CloseDialogCommand.Execute(null, this);
                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Pengaturan Koneksi Berhasil Diubah" }
                    };
                    DialogHost.Show(sampleMessageDialog, "LoginDialog");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("error " + ex);
                }

            }
            
        }

    }
}
