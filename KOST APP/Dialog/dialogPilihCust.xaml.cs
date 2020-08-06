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

namespace KOST_APP.Dialog
{
    /// <summary>
    /// Interaction logic for dialogPilihCust.xaml
    /// </summary>
    public partial class dialogPilihCust : UserControl
    {
        private String idCust,namaCust;
        koneksi k = new koneksi();

        public event Action<string> Check;
        public event Action<string> Check2;
        public dialogPilihCust()
        {
            InitializeComponent();
            showdata();
        }

        public void showdata()
        {
            k.sql = "select * from customer";
            k.setdt();
            lv_customer.ItemsSource = k.dt.DefaultView;
        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            showdata();
        }

        private void lv_customer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv_customer.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)lv_customer.SelectedItem;
                string index = dataRow.Row[0].ToString();

                k.sql = "select *from customer  where id_customer = '" + index + "'";
                k.setdt();

                idCust = k.dt.Rows[0][0].ToString();
                namaCust = k.dt.Rows[0][2].ToString();
            }
        }

        private void txt_cari_TextChanged(object sender, TextChangedEventArgs e)
        {
            k.sql = "select * from customer where nama like'%" + txt_cari.Text + "%' or kota like'%" + txt_cari.Text + "%'";
            k.setdt();
            lv_customer.ItemsSource = k.dt.DefaultView;
        }

        private void btn_pilih_Click(object sender, RoutedEventArgs e)
        {
            Check(idCust);
            Check2(namaCust);
            DialogHost.CloseDialogCommand.Execute(null, this);
        }
    }
}
