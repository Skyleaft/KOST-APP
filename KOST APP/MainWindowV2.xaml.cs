using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

            Storyboard sb = this.FindResource("menu_open") as Storyboard;
            sb.Begin();
            MenuToggleButton.IsChecked = true;
        }

        private async void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                await Task.Delay(100);
                ListViewOption.UnselectAll();

                if (ListViewMenu.SelectedItem != null)
                {
                    
                    content.Children.Clear();
                    UserControl usc;
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
                        case "ItemSewa":
                            usc = new viewSewa();
                            content.Children.Add(usc);
                            break;
                        default:
                            break;
                    }
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("error di "+ex);
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

        private void ItemHome_Selected(object sender, RoutedEventArgs e)
        {

        }

        private async void ListViewOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                await Task.Delay(100);
                ListViewMenu.UnselectAll();

                if (ListViewOption.SelectedItem != null)
                {
                    content.Children.Clear();
                    UserControl usc;
                    switch (((ListViewItem) ((ListView) sender).SelectedItem).Name)
                    {
                        case "ItemPengaturan":
                            usc = new viewPengaturan();
                            content.Children.Add(usc);
                            break;
                        case "ItemLogout":
                            var login = new Window_Login();
                            this.Close();
                            login.Show();
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error di " + ex);
            }
        }
    }
}
