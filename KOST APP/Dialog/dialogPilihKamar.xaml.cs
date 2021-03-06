﻿using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for dialogPilihKamar.xaml
    /// </summary>
    public partial class dialogPilihKamar : UserControl
    {
        koneksi k = new koneksi();
        private string idKmr,namaKmr;
        public dialogPilihKamar()
        {
            InitializeComponent();
            showdata();
        }

        public event Action<string> Check;
        public event Action<string> Check2;

        private void lv_kamar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv_kamar.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)lv_kamar.SelectedItem;
                string index = dataRow.Row[0].ToString();

                k.sql = "select *from kamar  where id_kamar = '" + index + "'";
                k.setdt();

                idKmr = k.dt.Rows[0][0].ToString();
                namaKmr = k.dt.Rows[0][1].ToString();
            }
        }

        public void showdata()
        {
            k.sql = "select * from kamar where status = 0";
            k.setdt();
            lv_kamar.ItemsSource = k.dt.DefaultView;

        }

        private void txt_cari_TextChanged(object sender, TextChangedEventArgs e)
        {
            k.sql = "select * from kamar where nomor_kamar like'%" + txt_cari.Text + "%' or biaya like'%" + txt_cari.Text + "%'";
            k.setdt();
            lv_kamar.ItemsSource = k.dt.DefaultView;
        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            showdata();
        }

        private void btn_pilih_Click(object sender, RoutedEventArgs e)
        {
            Check(idKmr);
            Check2(namaKmr);
            DialogHost.CloseDialogCommand.Execute(null, this);
        }
    }
}
