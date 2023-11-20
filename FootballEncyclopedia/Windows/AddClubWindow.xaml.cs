using FootballEncyclopedia.ADO;
using FootballEncyclopedia.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Логика взаимодействия для AddClubWindow.xaml
    /// </summary>
    public partial class AddClubWindow : Window
    {
        string LogoPath = "";
        string StadiumPath = "";
        bool isLogoLoaded = false;
        bool isStadiumLoaded = false;

        public AddClubWindow()
        {
            InitializeComponent();
            GetLeagues();
        }

        private void btn_LoadLogo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.gif)|*.png;*.jpeg;*.jpg;*.gif|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                // Получаем путь к выбранному файлу
                string selectedFilePath = openFileDialog.FileName;

                // Определяем путь, куда нужно сохранить выбранный файл (например, на рабочий стол)
                string savePath = "";
                if (cb_Leagues.Text == "Английская Премьер Лига")
                {
                    savePath = $"C:\\Users\\Артур\\source\\repos\\FootballEncyclopedia\\FootballEncyclopedia\\Images\\Clubs\\England";
                }
                else if (cb_Leagues.Text == "Испанская Ла Лига")
                {
                    savePath = $"C:\\Users\\Артур\\source\\repos\\FootballEncyclopedia\\FootballEncyclopedia\\Images\\Clubs\\Spain";
                }
                else if (cb_Leagues.Text == "Итальянская Серия А")
                {
                    savePath = $"C:\\Users\\Артур\\source\\repos\\FootballEncyclopedia\\FootballEncyclopedia\\Images\\Clubs\\Italy";
                }
                

                try
                {
                    // Копируем выбранный файл в указанное место
                    string fileName = System.IO.Path.GetFileName(selectedFilePath);
                    string destinationPath = System.IO.Path.Combine(savePath, fileName);
                    System.IO.File.Copy(selectedFilePath, destinationPath, true);
                    LogoPath = destinationPath;
                    lb_LogoName.Content = fileName;
                    isLogoLoaded = true;

                    // Добавьте здесь логику сохранения файла в нужном месте
                    // Например, для сохранения в определенной папке используйте System.IO.File.Copy
                }
                catch (Exception ex)
                {
                    // Обработка ошибок сохранения файла
                    MessageBox.Show("Ошибка при сохранении файла: " + ex.Message);
                }
            }
        }

        private void GetLeagues()
        {
            foreach (var item in DBConnection.connection.League.ToList())
            {
                cb_Leagues.Items.Add(item.Name);
            }
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindowAdmin win = new MainWindowAdmin();
            win.Show();
            this.Close();
        }

        private void cb_Leagues_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_LoadLogo.IsEnabled = true;
            btn_LoadStadium.IsEnabled = true;
        }

        private void btn_LoadStadium_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.gif)|*.png;*.jpeg;*.jpg;*.gif|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                // Получаем путь к выбранному файлу
                string selectedFilePath = openFileDialog.FileName;

                // Определяем путь, куда нужно сохранить выбранный файл (например, на рабочий стол)
                string savePath = "";
                if (cb_Leagues.Text == "Английская Премьер Лига")
                {
                    savePath = $"C:\\Users\\Артур\\source\\repos\\FootballEncyclopedia\\FootballEncyclopedia\\Images\\Stadiums\\England";
                }
                else if (cb_Leagues.Text == "Испанская Ла Лига")
                {
                    savePath = $"C:\\Users\\Артур\\source\\repos\\FootballEncyclopedia\\FootballEncyclopedia\\Images\\Stadiums\\Spain";
                }
                else if (cb_Leagues.Text == "Итальянская Серия А")
                {
                    savePath = $"C:\\Users\\Артур\\source\\repos\\FootballEncyclopedia\\FootballEncyclopedia\\Images\\Stadiums\\Italy";
                }
                

                try
                {
                    // Копируем выбранный файл в указанное место
                    string fileName = System.IO.Path.GetFileName(selectedFilePath);
                    string destinationPath = System.IO.Path.Combine(savePath, fileName);
                    System.IO.File.Copy(selectedFilePath, destinationPath, true);
                    StadiumPath = destinationPath;
                    lb_StadiumName.Content = fileName;
                    isStadiumLoaded = true;
                    // Добавьте здесь логику сохранения файла в нужном месте
                    // Например, для сохранения в определенной папке используйте System.IO.File.Copy
                }
                catch (Exception ex)
                {
                    // Обработка ошибок сохранения файла
                    MessageBox.Show("Ошибка при сохранении файла: " + ex.Message);
                }
            }
        }

        private void btn_AddClub_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_ClubName.Text) || string.IsNullOrEmpty(tb_StadiumName.Text) || string.IsNullOrEmpty(tb_Description.Text)
                || string.IsNullOrEmpty(tb_Site.Text) || string.IsNullOrEmpty(cb_Leagues.Text) || isLogoLoaded == false || isStadiumLoaded == false)
            {
                MessageBox.Show("Введите все данные!");
            }
            else
            {
                var stadium = DBConnection.connection.Stadium.Where(x => x.Name == tb_StadiumName.Text).FirstOrDefault();
                var club = DBConnection.connection.Club.Where(x => x.Name == tb_ClubName.Text).FirstOrDefault();
                if (stadium != null || club != null)
                {
                    MessageBox.Show("Клуб или стадион с таким названием уже есть!");
                }
                else
                {
                    Stadium NewStadium = new Stadium()
                    {
                        Name = tb_StadiumName.Text,
                        Photo = StadiumPath
                    };

                    var league = DBConnection.connection.League.Where(x => x.Name == cb_Leagues.Text).FirstOrDefault();

                    Club NewClub = new Club()
                    {
                        Name = tb_ClubName.Text,
                        Description = tb_Description.Text,
                        Logo = LogoPath,
                        Site = tb_Site.Text,
                        ID_League = league.League_ID,
                        ID_EuroLeague = 1,
                        ID_City = 1,
                        ID_Manager = 1,
                        ID_Stadium = NewStadium.Stadium_ID
                    };

                    DBConnection.connection.Stadium.Add(NewStadium);
                    DBConnection.connection.Club.Add(NewClub);
                    DBConnection.connection.SaveChanges();

                    MessageBox.Show("Клуб успешно добавлен!");

                    MainWindowAdmin win = new MainWindowAdmin();
                    win.Show();
                    this.Close();
                }
            }
        }
    }
}
