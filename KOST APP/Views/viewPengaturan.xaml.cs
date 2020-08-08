using MaterialDesignColors;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using MaterialDesignDemo.Domain;
using MaterialDesignColors.WpfExample.Domain;
using System.Windows.Shapes;
using System.ComponentModel;

namespace KOST_APP.Views
{
    /// <summary>
    /// Interaction logic for viewPengaturan.xaml
    /// </summary>
    public partial class viewPengaturan : UserControl
    {
        koneksi k = new koneksi();
        public viewPengaturan()
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

        private readonly PaletteHelper _paletteHelper = new PaletteHelper();


        private void ToggleBaseColour(bool isDark)
        {
            ITheme theme = _paletteHelper.GetTheme();
            IBaseTheme baseTheme = isDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();
            theme.SetBaseTheme(baseTheme);
            _paletteHelper.SetTheme(theme);
        }


        private void togleDark_Click(object sender, RoutedEventArgs e)
        {
            if (togleDark.IsChecked == true)
            {
                ToggleBaseColour(true);
            }
            else
            {
                ToggleBaseColour(false);
            }
        }

        private void btn_simpan_Click(object sender, RoutedEventArgs e)
        {
            if (txt_host.Text == "" || txt_port.Text == "" || txt_dbname.Text == "")
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
                    DialogHost.Show(sampleMessageDialog, "MainDialog");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error " + ex);
                }

            }
        }

        private void btn_backup_Click(object sender, RoutedEventArgs e)
        {
            k.backupDB();
            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = "Database Berhasil di simpan ke file BackupDatabase.sql" }
            };
            DialogHost.Show(sampleMessageDialog, "MainDialog");
        }

        private void btn_restore_Click(object sender, RoutedEventArgs e)
        {
            k.restoreDB();
            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = "Database Berhasil di Kembalikan" }
            };
            DialogHost.Show(sampleMessageDialog, "MainDialog");
        }
    }
}
