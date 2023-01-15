using System;
using System.Collections.Generic;
using System.Collections;
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
        WorkPlace[] Places; // временная штука для тестов
        ArrayList WorkPlaces; //думаю будет инициалиироваться из базы
        //User user;
        static User user;

        public MainPage()
        {
            InitializeComponent();
            WorkPlaces = new ArrayList();
            for (int i = 0; i < size; i++)
            {
                WorkPlace wp = new WorkPlace(i+1);
                WorkPlaces.Add(wp);
                
            }
            Places = new WorkPlace[size];
            for (int i = 0; i < size; i++)// пока без базы для проверки
            {
                Places[i] = new WorkPlace();
                Places[i].Number = i + 1;
            }
            Places[0].Take(new CalendarDateRange(new DateTime(2023, 1, 8), new DateTime(2023, 1, 12)), User);
            Places[0].Take(new CalendarDateRange(new DateTime(2023, 1, 20), new DateTime(2023, 1, 21)), User);
            Places[6].Take(new CalendarDateRange(new DateTime(2023, 1, 7), new DateTime(2023, 1, 18)), User);
        }

        public static User User
        {
            get { return user; }
            set { user = value; }
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
        public void TakePlace(WorkPlace place)
        {
            Window PlaceSelect = new WindowSelectPlace(place);
            PlaceSelect.Top = Mouse.GetPosition(this).Y;
            PlaceSelect.Left = Mouse.GetPosition(this).X;
            PlaceSelect.ShowDialog();
        }

        private void ImageMap_Loaded(object sender, RoutedEventArgs e) // тут нужно красиво сделать
        {
            ImagePlace1.Source = Places[0].ChooseImage();
            ImagePlace2.Source = Places[1].ChooseImage();
            ImagePlace3.Source = Places[2].ChooseImage();
            ImagePlace4.Source = Places[3].ChooseImage();
            ImagePlace5.Source = Places[4].ChooseImage();
            ImagePlace6.Source = Places[5].ChooseImage();
            ImagePlace7.Source = Places[6].ChooseImage();
            ImagePlace8.Source = Places[7].ChooseImage();

            //идqи по всем местам и создавать им соответствующий image 
            // тут должно быть так:
            /*foreach (WorkPlace item in WorkPlaces)
            {
                CreateImage(item);
            }*/
        }

        private void ImagePlace_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Image im = (Image)e.Source;
                int placeNumber = int.Parse(im.Name.Last().ToString());
                TakePlace(WorkPlaces[placeNumber - 1] as WorkPlace);
                im.Source = (WorkPlaces[placeNumber - 1] as WorkPlace).ChooseImage();
            }
            catch (ArgumentOutOfRangeException ex) 
            {
                MessageBox.Show(ex.Message + " " + ex.ParamName);
            }
            //MessageBox.Show(e.Source.ToString());
        }

        private void MenuMapItem_Click(object sender, RoutedEventArgs e)
        {
            Point MousePos = new Point();
            MousePos = Mouse.GetPosition(MainGrid);
            Window PlaceAdd = new WindowAddPlace(this, MousePos);
            PlaceAdd.ShowDialog();
        }

        public void CreateImage(WorkPlace place)
        {
            Image im = new Image();
            im.Margin = new Thickness(place.MarginLeft, place.MarginUp, 0, 0);
            im.Width = place.Width;
            im.Height = place.Height;
            im.HorizontalAlignment = HorizontalAlignment.Left;
            im.VerticalAlignment = VerticalAlignment.Top;
            im.Source = place.ChooseImage();
            im.Cursor = Cursors.Hand;
            im.Name = "ImagePlace" + place.Number.ToString();
            im.MouseLeftButtonUp += ImagePlace_MouseLeftButtonUp;
            MakeContextMenu(im);

            WorkPlaces.Add(place);
            MainGrid.Children.Add(im);
        }

        private void MakeContextMenu(Image im)
        {
            ContextMenu contextMenu = new ContextMenu();
            //изменить
            //переместить
            MenuItem menuItem1 = new MenuItem();
            menuItem1.Header = "Изменить";
            MenuItem menuItem2 = new MenuItem();
            menuItem2.Header = "Переместить";
            menuItem2.Click += ImagePlaceContextMenuPositionChangeClick;
            contextMenu.Items.Add(menuItem1);
            contextMenu.Items.Add(menuItem2);
            im.ContextMenu = contextMenu;
        }

        private void ImagePlaceContextMenuPositionChangeClick(object sender, RoutedEventArgs e)
        {
            ContextMenu cs = (e.Source as MenuItem).Parent as ContextMenu;
            Image im = cs.PlacementTarget as Image;
        }
    }
}
