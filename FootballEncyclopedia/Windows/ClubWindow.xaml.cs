using FootballEncyclopedia.ADO;
using FootballEncyclopedia.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace FootballEncyclopedia.Windows
{
    /// <summary>
    /// Логика взаимодействия для ClubWindow.xaml
    /// </summary>
    public partial class ClubWindow : Window
    {
        public ClubWindow()
        {
            InitializeComponent();
            this.Title = ClubService.club.Name;
            GetLogo();
            GetLeague();
            GetCountry();
            GetClubName();
            GetInfo();
            GetStadium();
            GenerateQR();
        }

        private void GenerateQR()
        {
            // Сначало необходимо уставновить NuGet: QRCoder
            // Ссылка на XL таблицу
            string soucer_xl = ClubService.club.Site; //внутри кавычек надо вставить ссылку
            // Создание переменной библиотеки QRCoder
            QRCoder.QRCodeGenerator qr = new QRCoder.QRCodeGenerator();
            // Присваеваем значиения
            QRCoder.QRCodeData data = qr.CreateQrCode(soucer_xl, QRCoder.QRCodeGenerator.ECCLevel.L);
            // переводим в Qr
            QRCoder.QRCode code = new QRCoder.QRCode(data);
            Bitmap bitmap = code.GetGraphic(100);
            /// Создание картинки
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                img_QR.Source = bitmapimage;
            }
        }

        private void GetInfo()
        {
            tb_Info.Text = ClubService.club.Description;
        }

        private void GetClubName()
        {
            lb_Name.Content = ClubService.club.Name;
        }

        private void GetLogo()
        {
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(ClubService.club.Logo);
            logo.EndInit();
            img_Logo.Source = logo;
        }

        private void GetLeague()
        {
            var league = DBConnection.connection.League.Where(x => x.League_ID == ClubService.club.ID_League).FirstOrDefault();
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(league.Logo);
            logo.EndInit();
            img_League.Source = logo;
        }

        private void GetCountry()
        {
            var city = DBConnection.connection.City.Where(x => x.City_ID == ClubService.club.ID_City).FirstOrDefault();
            var country = DBConnection.connection.Country.Where(x => x.Country_ID == city.ID_Country).FirstOrDefault();
            BitmapImage flag = new BitmapImage();
            flag.BeginInit();
            flag.UriSource = new Uri(country.Flag);
            flag.EndInit();
            img_Country.Source = flag;
        }

        private void GetStadium()
        {
            var stadium = DBConnection.connection.Stadium.Where(x => x.Stadium_ID == ClubService.club.ID_Stadium).FirstOrDefault();
            BitmapImage photo = new BitmapImage();
            photo.BeginInit();
            photo.UriSource = new Uri(stadium.Photo);
            photo.EndInit();
            img_Stadium.Source = photo;
            lb_StadiumName.Content = stadium.Name;
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindowUser win = new MainWindowUser();
            win.Show();
            this.Close();
        }
    }
}
