using KOST_APP.Dialog;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KOST_APP.Views
{
    /// <summary>
    /// Interaction logic for viewBeranda.xaml
    /// </summary>
    public partial class viewDataKamar : UserControl
    {
        koneksi k = new koneksi();
        public viewDataKamar()
        {
            InitializeComponent();

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("id-ID");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("id-ID");

            showdata();
        }

        
        public void showdata()
        {
            k.sql = "select * from kamar";
            k.setdt();
            lv_kamar.ItemsSource = k.dt.DefaultView;

        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            showdata();
            cmb_filter.SelectedItem = null;
        }

        private void lv_kamar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv_kamar.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)lv_kamar.SelectedItem;
                string index = dataRow.Row[0].ToString();

                k.sql = "select *from kamar  where id_kamar = '" + index + "'";
                k.setdt();

                String idkmr = k.dt.Rows[0][0].ToString();

                var showdialog = new dialogOpenKamar(idkmr);
                DialogHost.Show(showdialog, "MainDialog", ClosingEventHandler);

            }
        }

        //private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        //{
        //    showdata();
        //}

        private void btn_tambah_Click(object sender, RoutedEventArgs e)
        {
            var showdialog = new dialogTambahKamar();
            DialogHost.Show(showdialog, "MainDialog", ClosingEventHandler);
        }
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            showdata();
        }

        private void txt_cari_TextChanged(object sender, TextChangedEventArgs e)
        {
            k.sql = "select * from kamar where nomor_kamar like'" + txt_cari.Text + "%' or biaya like'%" + txt_cari.Text + "%'";
            k.setdt();
            lv_kamar.ItemsSource = k.dt.DefaultView;
        }

        private void cmb_filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cmb_filter.SelectedIndex)
            {
                case 0:
                    k.sql = "select * from kamar where status=0";
                    k.setdt();
                    lv_kamar.ItemsSource = k.dt.DefaultView;
                    break;
                case 1:
                    k.sql = "select * from kamar where status=1";
                    k.setdt();
                    lv_kamar.ItemsSource = k.dt.DefaultView;
                    break;
                case 2:
                    k.sql = "select * from kamar where status=2";
                    k.setdt();
                    lv_kamar.ItemsSource = k.dt.DefaultView;
                    break;
                case 3:
                    k.sql = "select * from kamar order by biaya asc";
                    k.setdt();
                    lv_kamar.ItemsSource = k.dt.DefaultView;
                    break;
                case 4:
                    k.sql = "select * from kamar order by biaya desc";
                    k.setdt();
                    lv_kamar.ItemsSource = k.dt.DefaultView;
                    break;
            }
        }
    }
}
