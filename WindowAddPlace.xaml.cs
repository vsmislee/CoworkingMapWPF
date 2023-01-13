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
        Grid grid;
        public WindowAddPlace()
        {
            InitializeComponent();
        }

        public WindowAddPlace(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("aboba");
            //тут добавляем место в базу
            int number = int.Parse(TextBoxNumber.Text);
            WorkPlace addPlace = new WorkPlace(number, 100, 100);
            addPlace.CreateImage(grid);
            this.DialogResult = true;
        }
    }
}
