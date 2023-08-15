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

namespace GruziVezi
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class WAuthorization : Window
    {
        public WAuthorization()
        {
            InitializeComponent();

            /*

            tbLogin.Text = "oper";
            pbPassword.Password = "oper";
            btnAuthorization_Click(new object(),new RoutedEventArgs());*/
        }

        public static Users user;

        private void btnAuthorization_Click(object sender, RoutedEventArgs e)
        {
            user = Authorization.SignIn(tbLogin.Text,pbPassword.Password);

            if (user != null)
            {
                switch (user.id_Role)
                {
                    case 1:
                        Hide();
                        new WAdministrator().ShowDialog();                        
                        Application.Current.Shutdown();
                        break;
                    case 2:
                        Hide();
                        new WOperator().ShowDialog();
                        Application.Current.Shutdown();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
