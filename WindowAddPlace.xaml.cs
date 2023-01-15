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
        public WindowAddPlace()
        {
            InitializeComponent();
        }

        public WindowAddPlace(MainPage mainPage)
        {
            InitializeComponent();
            this.mainPage = mainPage;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            int number = int.Parse(TextBoxNumber.Text);
            double marginUp = mainPage.MainGrid.Height / 2;
            double marginLeft = mainPage.MainGrid.Width / 2;
            WorkPlace addPlace = new WorkPlace(number, marginUp, marginLeft);
            //тут нужно добавлять место в базу
            mainPage.CreateImage(addPlace);
            this.DialogResult = true;
        }
    }
}
