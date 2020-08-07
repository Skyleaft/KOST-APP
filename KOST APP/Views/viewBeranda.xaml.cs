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

namespace KOST_APP.Views
{
    /// <summary>
    /// Interaction logic for viewBeranda.xaml
    /// </summary>
    public partial class viewBeranda : UserControl
    {
        koneksi k = new koneksi();
        public viewBeranda()
        {
            InitializeComponent();
            showdata();

        }
        public void showdata()
        {
            k.sql = "select count(*) from customer where status = 1";
            k.setdt();
            lb_jmlpenghuni.Text = k.dt.Rows[0][0].ToString();

            k.sql = "select count(*) from customer";
            k.setdt();
            lb_totalCust.Text = k.dt.Rows[0][0].ToString();

            k.sql = "select count(*) from kamar";
            k.setdt();
            lb_jmlkamar.Text = k.dt.Rows[0][0].ToString();

        }
    }
}
