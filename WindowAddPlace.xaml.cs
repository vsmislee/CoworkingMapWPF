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

namespace CoworkingMap
{
    /// <summary>
    /// Логика взаимодействия для WindowAddPlace.xaml
    /// </summary>
    public partial class WindowAddPlace : Window
    {
        MainPage mainPage;
        Point mousePosition;
        public WindowAddPlace()
        {
            InitializeComponent();
        }

        public WindowAddPlace(MainPage mainPage, Point mousePosition)
        {
            InitializeComponent();
            this.mainPage = mainPage;
            this.mousePosition = mousePosition;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            // место должно загружаться из базы, если нет места в базе, т е добавляем новое - по расположению мыши
            WorkPlace addPlace;
            int number;
            double marginUp;
            double marginLeft;
            if (false) //есть ли в базе
            {

            }
            else // если нет
            {
                number = int.Parse(TextBoxNumber.Text);
                marginUp = mousePosition.Y - 12;
                marginLeft = mousePosition.X - 14;
                addPlace = new WorkPlace(number, marginUp, marginLeft);
            }
            //тут нужно добавлять место в базу
            mainPage.CreateImage(addPlace);
            this.DialogResult = true;
        }
    }
}
