using FootballEncyclopedia.ADO;
using FootballEncyclopedia.Services;
using FootballEncyclopedia.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FootballEncyclopedia
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Registration win = new Registration();
            win.Show();
            this.Close();
        }

        private void btn_SignIn_Click(object sender, RoutedEventArgs e)
        {
            int flag = 0;
            if (!string.IsNullOrEmpty(tb_Login.Text) || !string.IsNullOrEmpty(tb_Password.Password))
            {
                var user = DBConnection.connection.User.Where(x => x.Login == tb_Login.Text).FirstOrDefault();
                if (user == null || user.Password != tb_Password.Password)
                {
                    flag = 2;
                }
            }
            else
            {
               flag = 1;
            }

            switch (flag)
            {
                case 0:
                    MessageBox.Show("ОК");
                    break;
                case 1:
                    MessageBox.Show("Введите все данные!");
                    break;
                case 2:
                    MessageBox.Show("Неправильно введены логин или пароль!");
                    break;
            }
        }
    }
}
