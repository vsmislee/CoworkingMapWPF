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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        AppContext db;
        public Authorization()
        {
            InitializeComponent();
            db = new AppContext();
        }
        private void AuthClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = LoginBox.Text.Trim();
                string password = PasswordBox.Password.Trim();
                User AuthUser = null;
                using (AppContext db = new AppContext())
                {
                    AuthUser = db.Users.Where(b => b.Login == login && b.Password == password).FirstOrDefault();
                }
                if (AuthUser != null)
                {
                    MainPage.User = AuthUser;
                    NavigationService.Navigate(new MainPage());
                }
                else
                    throw new Exception("Неверный логин или пароль.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

