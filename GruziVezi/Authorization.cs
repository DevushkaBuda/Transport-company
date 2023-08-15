using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace GruziVezi
{
    public class Authorization
    {
        public static Users SignIn(string login, string password)
        {
            GruziVeziEntities db = new GruziVeziEntities();
            if (login == "" || password == "")
            {
                MessageBox.Show("Вы не заполнили все поля", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            var authUser = db.Users.FirstOrDefault(user => user.login == login && user.password == password);
            if (authUser == null)
            {
                MessageBox.Show("Логин или пароль введены не верно", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return authUser;
        }
    }
}