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

namespace CoworkingMap
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void main(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void Maintoroom(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PersonalRoom());
        }

        private void Stats(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Stats());
        }

        private void History(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new History());
        }

        private void contacts(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Contacts());
        }

        private void TakePlace(int placeNumber)
        {
            Window PlaceSelect = new WindowSelectPlace(placeNumber);
            PlaceSelect.Top = Mouse.GetPosition(this).Y;
            PlaceSelect.Left = Mouse.GetPosition(this).X;
            PlaceSelect.ShowDialog();
        }

        BitmapImage ChooseSource(int placeNumber)
        {
            BitmapImage bit = new BitmapImage();
            string source;
            if (true)//проверка занято ли место
            {
                source = "images/places/" + placeNumber.ToString() + ".PNG";
                bit.BeginInit();
                bit.UriSource = new Uri(source, UriKind.Relative);
                bit.EndInit();
            }
            else
            {
                source = "images/places/" + placeNumber.ToString() + "blue.PNG";
                bit.BeginInit();
                bit.UriSource = new Uri(source, UriKind.Relative);
                bit.EndInit();
            }
            return bit;
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {

            ImagePlace1.Source = ChooseSource(1);
        }

        private void ImagePlace1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TakePlace(1);
        }

        private void ImagePlace2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TakePlace(2);
        }

        private void ImagePlace3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TakePlace(3);
        }

        private void ImagePlace4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TakePlace(4);
        }

        private void ImagePlace5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TakePlace(5);
        }

        private void ImagePlace6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TakePlace(6);
        }

        private void ImagePlace7_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TakePlace(7);
        }

        private void ImagePlace8_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TakePlace(8);
        }
    }
}
