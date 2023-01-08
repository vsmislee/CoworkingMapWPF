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
        int size = 8;
        WorkPlace[] Places; // думаю будут инициализироваться из базы
        User user;

        public MainPage(User user)// в параметры юзера
        {
            InitializeComponent();
            Places = new WorkPlace[size];
            this.user = user;
            for (int i = 0; i < size; i++)// пока без базы для проверки
            {
                Places[i] = new WorkPlace();
                Places[i].Number = i+1;
            }
            Places[0].Taked = true;
            Places[0].AddTakedDate(new CalendarDateRange(new DateTime(2023, 1, 10), new DateTime(2023, 1, 15)));
            Places[0].AddTakedDate(new CalendarDateRange(new DateTime(2023, 1, 20), new DateTime(2023, 1, 21)));
            Places[6].Taked = true;
        }

        private void main(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(user));
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
        public void TakePlace(WorkPlace place)
        {
            Window PlaceSelect = new WindowSelectPlace(place);
            PlaceSelect.Top = Mouse.GetPosition(this).Y;
            PlaceSelect.Left = Mouse.GetPosition(this).X;
            PlaceSelect.ShowDialog();
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            ImagePlace1.Source = Places[0].ChooseImage();
            ImagePlace2.Source = Places[1].ChooseImage();
            ImagePlace3.Source = Places[2].ChooseImage();
            ImagePlace4.Source = Places[3].ChooseImage();
            ImagePlace5.Source = Places[4].ChooseImage();
            ImagePlace6.Source = Places[5].ChooseImage();
            ImagePlace7.Source = Places[6].ChooseImage();
            ImagePlace8.Source = Places[7].ChooseImage();

        }

        private void ImagePlace_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image im = (Image)e.Source;
            int placeNumber = int.Parse(im.Name.Last().ToString());
            TakePlace(Places[placeNumber-1]);
        }
       
    }
}
