using FootballEncyclopedia.ADO;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Authorization win = new Authorization();
            win.Show();
            this.Close();
        }

        private void btn_SignUP_Click(object sender, RoutedEventArgs e)
        {
            int flag = 0;
            if (!string.IsNullOrEmpty(tb_Login.Text) || !string.IsNullOrEmpty(tb_Password.Password) || !string.IsNullOrEmpty(tb_ConfirmPassword.Password))
            {
                var user = DBConnection.connection.User.Where(x => x.Login == tb_Login.Text).FirstOrDefault();
                if (user == null)
                {
                    if (tb_Password.Password != tb_ConfirmPassword.Password)
                    {
                        flag = 3;
                    }
                }
                else
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
                    User NewUser = new User()
                    {
                        Login = tb_Login.Text,
                        Password = tb_Password.Password,
                        ID_Role = 2
                    };
                    DBConnection.connection.User.Add(NewUser);
                    DBConnection.connection.SaveChanges();
                    Authorization win = new Authorization();
                    win.Show();
                    this.Close();
                    break;
                case 1:
                    MessageBox.Show("Введите все данные!");
                    break;
                case 2:
                    MessageBox.Show("Пользователь с таким логином уже существует!");
                    break;
                case 3:
                    MessageBox.Show("Пароль не совпадает с подтверждением пароля!");
                    break;
            }
        }
    }
}
