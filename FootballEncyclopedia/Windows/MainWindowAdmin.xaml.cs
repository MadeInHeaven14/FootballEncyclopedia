using FootballEncyclopedia.Services;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для MainWindowAdmin.xaml
    /// </summary>
    public partial class MainWindowAdmin : Window
    {
        public MainWindowAdmin()
        {
            InitializeComponent();
            lb_AdminLogin.Content = UserService.user.Login;
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Authorization win = new Authorization();
            win.Show();
            this.Close();
        }

        private void btn_AddClub_Click(object sender, RoutedEventArgs e)
        {
            AddClubWindow win = new AddClubWindow();
            win.Show();
            this.Close();
        }
    }
}
