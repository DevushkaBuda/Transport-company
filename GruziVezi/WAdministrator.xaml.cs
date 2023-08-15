using Microsoft.SqlServer.Server;
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

namespace GruziVezi
{
    /// <summary>
    /// Логика взаимодействия для WAdministrator.xaml
    /// </summary>
    /// 

  

    public class CustomDriversTable
    {
        public string customCars { get; set; }
        public Drivers drivers { get; set; }
    }

    public class CustomCars
    {
        public string customCars { get; set; }
        public ModelCars modelCars { get; set; }
        public Cars cars { get; set; }

    }

    public partial class WAdministrator : Window
    {




        string oldLogin;

        public WAdministrator()
        {
            InitializeComponent();
            this.Title += WAuthorization.user.surname + " " + WAuthorization.user.name[0] + "." + WAuthorization.user.middlename[0] + ".";

            //Пользователи
            LoadCBRole();
            LoadDGUsers();

            //Роли
            LoadDGRoles();

            //Водители
            LoadDGDrivers();
            LoadCBCar();

            //Машины
            LoadDGCars();
            LoadCBModelCar();

            //Модели машин
            LoadDGModelCars();

        }
        
        List<Users> users;

        public void UpdateAll()
        {
            //Пользователи
            LoadCBRole();
            LoadDGUsers();

            //Роли
            LoadDGRoles();

            //Водители
            LoadDGDrivers();
            LoadCBCar();

            //Машины
            LoadDGCars();
            LoadCBModelCar();

            //Модели машин
            LoadDGModelCars();
        }

        /********************************************/
        /*             ПОЛЬЗОВАТЕЛИ                 */
        /********************************************/
        /*ГОТОВО*/

         private void LoadDGUsers()
        {
           GruziVeziEntities db = new GruziVeziEntities();

            dgUsers.ItemsSource = db.Users.ToList();
        }

        void LoadCBRole()
        {
            GruziVeziEntities db = new GruziVeziEntities();
            cbRole.ItemsSource = db.Roles.ToList();
            cbRole.SelectedIndex = 2;
        }

        void ClearTB()
        {
            tbLogin.Text = null;
            tbSurname.Text = null;
            tbName.Text = null;
            tbMiddlename.Text = null;
            pbPassword.Password = null;
            cbRole.SelectedIndex = 2;
        }



        private void FillTBUser(object sender, RoutedEventArgs e)
        {
            if ((dgUsers.SelectedItem as Users) != null)
            {
                tbLogin.Text = (dgUsers.SelectedItem as Users).login;
                tbSurname.Text = (dgUsers.SelectedItem as Users).surname;
                tbName.Text = (dgUsers.SelectedItem as Users).name;
                tbMiddlename.Text = (dgUsers.SelectedItem as Users).middlename;
                foreach (var item in cbRole.ItemsSource)
                {
                    if ((item as Roles).id == (dgUsers.SelectedItem as Users).id_Role)
                    {
                        cbRole.SelectedItem = item;
                        break;
                    }
                }
                oldLogin = (dgUsers.SelectedItem as Users).login;
            }

        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersTable.Add(tbLogin.Text, pbPassword.Password, tbSurname.Text, tbName.Text, tbMiddlename.Text, (cbRole.SelectedItem as Roles).id))
            {
                LoadDGUsers();
                ClearTB();
                UpdateAll();
            }
        }

        private void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if ((dgUsers.SelectedItem as Users) != null)
            {
                if (UsersTable.Update((dgUsers.SelectedItem as Users).id, tbLogin.Text, oldLogin, pbPassword.Password, tbSurname.Text, tbName.Text, tbMiddlename.Text, (cbRole.SelectedItem as Roles).id))
                {
                    LoadDGUsers();
                    ClearTB();
                    UpdateAll();
                }
            }
        }
        /********************************************/
        /*                 РОЛИ                     */
        /********************************************/
        /*ГОТОВО*/
        void LoadDGRoles()
        {
            GruziVeziEntities db = new GruziVeziEntities();
            dgRoles.ItemsSource = db.Roles.ToList();
        }

        private void FillTBRole(object sender, RoutedEventArgs e)
        {
            if ((dgRoles.SelectedItem as Roles) != null)
            {
                tbRoleName.Text = (dgRoles.SelectedItem as Roles).name;
            }

        }


        void ClearTBRoles()
        {
            tbRoleName.Text = null;
        }

        private void btnAddRole_Click(object sender, RoutedEventArgs e)
        {
            if (RolesTable.Add(tbRoleName.Text))
            {
                LoadDGRoles();
                ClearTBRoles();
                UpdateAll();
            }
        }

        private void btnUpdateRole_Click(object sender, RoutedEventArgs e)
        {
            if ((dgRoles.SelectedItem as Roles) != null)
            {
                if (RolesTable.Update((dgRoles.SelectedItem as Roles).id, tbRoleName.Text))
                {
                    LoadDGRoles();
                    ClearTBRoles();
                    UpdateAll();
                }
            }
        }

        /********************************************/
        /*                ВОДИТЕЛИ                  */
        /********************************************/
        /*ДОБАВЛЕНИЕ / РЕДАКТИРОВАНИЕ / ОЧИСТКА*/

        private void LoadDGDrivers()
        {
            GruziVeziEntities db = new GruziVeziEntities();

            List<CustomDriversTable> list = new List<CustomDriversTable>();

            foreach (Drivers item in db.Drivers.ToList())
            {
                CustomDriversTable custom = new CustomDriversTable();
                custom.customCars = item.Cars.ModelCars.name + " " + item.Cars.number;
                custom.drivers = item;

                list.Add(custom);
            }

            dgDrivers.ItemsSource = list;

        }
        private void LoadCBCar()
        {
            GruziVeziEntities db = new GruziVeziEntities();

            List<CustomCars> list = new List<CustomCars>();

            foreach (Cars item in db.Cars.ToList())
            {
                CustomCars custom = new CustomCars();
                custom.customCars = item.ModelCars.name + " " + item.number;
                custom.cars = item;

                list.Add(custom);
            }
            cbCar.ItemsSource = list;
        }

        void ClearTBDrivers()
        {
            tbsurnameDriver.Text = null;
            tbNameDriver.Text = null;
            tbMiddlenameDriver.Text = null;
        }

        private void FillTBDriver(object sender, RoutedEventArgs e)
        {
            if ((dgDrivers.SelectedItem as CustomDriversTable) != null)
            {

                tbsurnameDriver.Text = (dgDrivers.SelectedItem as CustomDriversTable).drivers.surname;
                tbNameDriver.Text = (dgDrivers.SelectedItem as CustomDriversTable).drivers.name;
                tbMiddlenameDriver.Text = (dgDrivers.SelectedItem as CustomDriversTable).drivers.middlename;

                GruziVeziEntities db = new GruziVeziEntities();


                foreach (CustomCars item in cbCar.ItemsSource)
                {
                    if (item.cars.id == (dgDrivers.SelectedItem as CustomDriversTable).drivers.id_Car)
                    {
                        cbCar.SelectedItem = item;
                        break;
                    }
                }


            }

        }

        private void BtnAddDriver_Click(object sender, RoutedEventArgs e)
        {
            if (DriversTable.Add(tbsurnameDriver.Text, tbNameDriver.Text, tbMiddlenameDriver.Text, (cbCar.SelectedItem as CustomCars) != null ? (cbCar.SelectedItem as CustomCars).cars.id : 0))
            {
                LoadDGDrivers();
                ClearTBDrivers();
                UpdateAll();
            }
        }

        private void BtnUpdateDriver_Click(object sender, RoutedEventArgs e)
        {

            if ((dgDrivers.SelectedItem as CustomDriversTable) != null)
            {
                if (DriversTable.Update((dgDrivers.SelectedItem as CustomDriversTable).drivers.id, tbsurnameDriver.Text, tbNameDriver.Text, tbMiddlenameDriver.Text, (cbCar.SelectedItem as CustomCars) != null ? (cbCar.SelectedItem as CustomCars).cars.id : 0))
                {
                    LoadDGDrivers();
                    ClearTBDrivers();
                    UpdateAll();
                }
            }
            else
            {
                MessageBox.Show("Запись не выбрана");
            }
        }
        /********************************************/
        /*                  МАШИНЫ                  */
        /********************************************/
        /*ДОБАВЛЕНИЕ / РЕДАКТИРОВАНИЕ */
        private void LoadDGCars()
        {
            GruziVeziEntities db = new GruziVeziEntities();

            dgCars.ItemsSource = db.Cars.ToList();

        }

        private void LoadCBModelCar()
        {
            GruziVeziEntities db = new GruziVeziEntities();

            cbModelCar.ItemsSource = db.ModelCars.ToList();
        }

        private void FillTBCar(object sender, RoutedEventArgs e)
        {
            if ((dgCars.SelectedItem as Cars) != null)
            {
                tbNumber.Text = (dgCars.SelectedItem as Cars).number;

                GruziVeziEntities db = new GruziVeziEntities();


                foreach (ModelCars item in cbModelCar.ItemsSource)
                {
                    if (item.id == (dgCars.SelectedItem as Cars).id_ModelCar)
                    {
                        cbModelCar.SelectedItem = item;
                        break;
                    }
                }


            }

        }

        void ClearTBCars()
        {
            tbNumber.Text = null;
        }

        private void BtnAddCar_Click(object sender, RoutedEventArgs e)
        {
            if (CarsTable.Add(tbNumber.Text, (cbModelCar.SelectedItem as ModelCars) != null ? (cbModelCar.SelectedItem as ModelCars).id : 0))
            {
                LoadDGCars();
                ClearTBCars();
                UpdateAll();
            }
        }

        private void BtnUpdateCar_Click(object sender, RoutedEventArgs e)
        {
            if ((dgCars.SelectedItem as Cars) != null)
            {
                if (CarsTable.Update((dgCars.SelectedItem as Cars).id, tbNumber.Text, (cbModelCar.SelectedItem as ModelCars) != null ? (cbModelCar.SelectedItem as ModelCars).id : 0))
                {
                    LoadDGCars();
                    ClearTBCars();
                    UpdateAll();
                }
            }
        }

        /********************************************/
        /*                МОДЕЛИ МАШИН              */
        /********************************************/
        /*ДОБАВЛЕНИЕ / РЕДАКТИРОВАНИЕ */

        private void LoadDGModelCars()
        {
            GruziVeziEntities db = new GruziVeziEntities();

            dgModelCars.ItemsSource = db.ModelCars.ToList();

        }



        private void FillTBModelCar(object sender, RoutedEventArgs e)
        {
            if ((dgModelCars.SelectedItem as ModelCars) != null)
            {

                tbModelCarName.Text = (dgModelCars.SelectedItem as ModelCars).name;

            }

        }

        void ClearTBModelCars()
        {
            tbModelCarName.Text = null;
        }

        private void btnAddModelCar_Click(object sender, RoutedEventArgs e)
        {
            if (ModelCarsTable.Add(tbModelCarName.Text))
            {
                LoadDGModelCars();
                ClearTBModelCars();
                UpdateAll();
            }
        }

        private void btnUpdateModelCar_Click(object sender, RoutedEventArgs e)
        {
            if ((dgModelCars.SelectedItem as ModelCars)!=null)
            {
                if (ModelCarsTable.Update((dgModelCars.SelectedItem as ModelCars).id, tbModelCarName.Text))
                {
                    LoadDGModelCars();
                    ClearTBModelCars();
                    UpdateAll();
                }
            }
        }

        private void dgUsers_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
         
        }

        private void FillTBRole(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((dgUsers.SelectedItem as Users) != null)
            {
                tbLogin.Text = (dgUsers.SelectedItem as Users).login;
                tbRoleName.Text = (dgUsers.SelectedItem as Users).Roles.name;
                tbSurname.Text = (dgUsers.SelectedItem as Users).surname;
                tbName.Text = (dgUsers.SelectedItem as Users).name;
                tbMiddlename.Text = (dgUsers.SelectedItem as Users).middlename;
                cbRole.Text = (dgUsers.SelectedItem as Users).Roles.name;

            }
        }









        /******************************************************/












    }
}
