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
using System.Windows.Shapes;
using KOST_APP.Views;
using MahApps.Metro.Controls;

namespace KOST_APP
{
    /// <summary>
    /// Interaction logic for MainWindowV2.xaml
    /// </summary>
    public partial class MainWindowV2 : MetroWindow
    {
        public MainWindowV2()
        {
            InitializeComponent();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            content.Children.Clear();
            UserControl usc = null;

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    usc = new viewBeranda();
                    content.Children.Add(usc);
                    break;
                case "ItemDataKost":
                    usc = new viewDataKost();
                    content.Children.Add(usc);
                    break;
                case "ItemDataKamar":
                    usc = new viewDataKamar();
                    content.Children.Add(usc);
                    break;
                case "ItemDataCustomer":
                    usc = new viewCustomer();
                    content.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }

        private void MenuToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuToggleButton.IsChecked == true)
            {
                Storyboard sb = this.FindResource("menu_open") as Storyboard;
                sb.Begin();
            }
            else
            {
                Storyboard sb = this.FindResource("menu_close") as Storyboard;
                sb.Begin();
            }
        }
    }
}
