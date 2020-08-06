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
    /// Interaction logic for dialogTambahSewa.xaml
    /// </summary>
    public partial class dialogTambahSewa : UserControl
    {
        public dialogTambahSewa()
        {
            InitializeComponent();
        }

        private void btn_caricust_Click(object sender, RoutedEventArgs e)
        {
            var showdialog = new dialogPilihCust();
            DialogHost.Show(showdialog, "SubDialogSewa", ClosingEventHandler);
        }
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            //showdata();
        }

        private void btn_simpan_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_carikamar_Click(object sender, RoutedEventArgs e)
        {
            var showdialog = new dialogPilihKamar();
            DialogHost.Show(showdialog, "SubDialogSewa", ClosingEventHandler);
        }
    }
}
