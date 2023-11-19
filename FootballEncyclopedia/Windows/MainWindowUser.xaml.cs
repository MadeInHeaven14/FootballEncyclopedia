using FootballEncyclopedia.ADO;
using FootballEncyclopedia.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FootballEncyclopedia.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindowUser.xaml
    /// </summary>
    public partial class MainWindowUser : Window
    {
        public MainWindowUser()
        {
            InitializeComponent();
            lv_Clubs.ItemsSource = DBConnection.connection.Club.ToList();
            tb_Login.Text = UserService.user.Login;
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Authorization win = new Authorization();
            win.Show();
            this.Close();
        }

        private void lv_Clubs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedClub = lv_Clubs.SelectedItem as Club;
            var club = DBConnection.connection.Club.Where(x => x.Name == selectedClub.Name).FirstOrDefault();
            ClubService.club = club;
            ClubWindow win = new ClubWindow();
            win.Show();
            this.Close();
        }
    }
}
