using System;
using System.Linq;
using MahApps.Metro.IconPacks;

namespace KOST_APP.ViewModels
{
    internal class ShellViewModel : ViewModelBase
    {
        public ShellViewModel()
        {
            // Build the menus
            this.Menu.Add(new MenuItem() {Icon = new PackIconEntypo() {Kind = PackIconEntypoKind.Home}, Text = "Beranda", NavigationDestination = new Uri("Views/viewBeranda.xaml", UriKind.RelativeOrAbsolute)});
            this.Menu.Add(new MenuItem() {Icon = new PackIconMaterial() {Kind = PackIconMaterialKind.HomeCityOutline}, Text = "Data Kostan", NavigationDestination = new Uri("Views/viewDataKost.xaml", UriKind.RelativeOrAbsolute)});
            this.Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.HouseUserSolid}, Text = "Data Kamar", NavigationDestination = new Uri("Views/viewDataKamar.xaml", UriKind.RelativeOrAbsolute) });
            this.Menu.Add(new MenuItem() { Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.AccountGroup }, Text = "Data Customer", NavigationDestination = new Uri("Views/viewCustomer.xaml", UriKind.RelativeOrAbsolute) });
            


            //menu opsi
            this.OptionsMenu.Add(new MenuItem() {Icon = new PackIconFontAwesome() {Kind = PackIconFontAwesomeKind.CogsSolid}, Text = "Pengaturan", NavigationDestination = new Uri("Views/PageSetting.xaml", UriKind.RelativeOrAbsolute)});
            this.OptionsMenu.Add(new MenuItem() {Icon = new PackIconFontAwesome() {Kind = PackIconFontAwesomeKind.InfoCircleSolid}, Text = "Tentang Aplikasi", NavigationDestination = new Uri("Views/AboutPage.xaml", UriKind.RelativeOrAbsolute)});
            this.OptionsMenu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.SignOutAltSolid }, Text = "Log out", NavigationDestination = new Uri("Views/PageLogout.xaml", UriKind.RelativeOrAbsolute) });
        }

        public object GetItem(object uri)
        {
            return null == uri ? null : this.Menu.FirstOrDefault(m => m.NavigationDestination.Equals(uri));
        }

        public object GetOptionsItem(object uri)
        {
            return null == uri ? null : this.OptionsMenu.FirstOrDefault(m => m.NavigationDestination.Equals(uri));
        }
    }
}