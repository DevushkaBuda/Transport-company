using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace GruziVezi
{
    public class UsersTable
    {
        public static bool Add(string login, string password, string surname, string name, string middlename, int idRole)
        {
            try
            { 
                GruziVeziEntities db = new GruziVeziEntities();

                var checkUser = db.Users.Where(u => u.login == login).FirstOrDefault();
                if (checkUser != null)
                {
                   MessageBox.Show("Такой логин уже есть в системе", "Пользователи", MessageBoxButton.OK, MessageBoxImage.Error);
                   return false;
                }

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Вы не заполнили все поля", "Пользователи", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

                Regex SpecialSimbols = new Regex("([#]|[=]|[/]|[*]|[@]|[&]|[>]|[<]|[;]|[']|[$]|[№]|[!]|[№]|[;]|[{]|[}]|[[]|[]]|[~])");
                Regex searchNumber = new Regex(@"(\d)");
                Regex Words = new Regex("^[A-Za-z]");
                MatchCollection matchNumber;
                MatchCollection matchSpecialSymbol;
                MatchCollection matchWordsLogPass;
                MatchCollection matchWordsFIO;
                matchSpecialSymbol = SpecialSimbols.Matches(login+password+surname+name+middlename);
                matchNumber = searchNumber.Matches(surname + name + middlename);
                matchWordsLogPass = Words.Matches(login + password);
                matchWordsFIO= Words.Matches(surname + name + middlename);

            if (string.IsNullOrEmpty(surname))
            {
                MessageBox.Show("Вы не написали фамилию", "Пользователи", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Вы не написали имя", "Пользователи", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (string.IsNullOrEmpty(middlename))
            {
                MessageBox.Show("Вы не написали отчество", "Пользователи", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (matchSpecialSymbol.Count > 0)
            {
                MessageBox.Show("В полях не допускаются спецсимволы.", "Пользователи", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (matchNumber.Count > 0)
            {
                MessageBox.Show("В полях [Фамилия / Имя / Отчество] не допускаются числа.", "Пользователи", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (matchWordsFIO.Count > 0)
            {
                MessageBox.Show("В полях не допускается латиница.", "Пользователи", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }


                Users user = new Users();


                user.login = login;
                user.password= password;
                user.surname= surname;
                user.name= name;
                user.middlename= middlename;
                user.id_Role = idRole;
               
                db.Users.Add(user);
                db.SaveChanges();

                MessageBox.Show("Пользователь добавлен", "Пользователи", MessageBoxButton.OK, MessageBoxImage.Information);

                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static bool Update(int id, string login,string oldLogin, string password, string surname, string name, string middlename, int idRole)
        {
            GruziVeziEntities db = new GruziVeziEntities();

            if (login != oldLogin)
            {
                var checkUser = db.Users.Where(u => u.login == login).FirstOrDefault();

                if (checkUser != null)
                {
                    MessageBox.Show("Такой логин уже есть в системе", "Пользователи", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Вы не заполнили все поля", "Пользователи", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            Regex SpecialSimbols = new Regex("([#]|[=]|[/]|[*]|[@]|[&]|[>]|[<]|[;]|[']|[$]|[№]|[!]|[№]|[;]|[{]|[}]|[[]|[]]|[~])");
            Regex searchNumber = new Regex(@"(\d)");
            Regex Words = new Regex("^[A-Za-z]");
            MatchCollection matchNumber;
            MatchCollection matchSpecialSymbol;
            MatchCollection matchWordsLogPass;
            MatchCollection matchWordsFIO;
            matchSpecialSymbol = SpecialSimbols.Matches(login + password + surname + name + middlename);
            matchNumber = searchNumber.Matches(surname + name + middlename);
            matchWordsLogPass = Words.Matches(login + password);
            matchWordsFIO = Words.Matches(surname + name + middlename);

            if (string.IsNullOrEmpty(surname))
            {
                MessageBox.Show("Вы не написали фамилию", "Пользователи", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Вы не написали имя", "Пользователи", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrEmpty(middlename))
            {
                MessageBox.Show("Вы не написали отчество", "Пользователи", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (matchSpecialSymbol.Count > 0)
            {
                MessageBox.Show("В полях не допускаются спецсимволы.", "Пользователи", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (matchNumber.Count > 0)
            {
                MessageBox.Show("В полях [Фамилия / Имя / Отчество] не допускаются числа.", "Пользователи", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (matchWordsFIO.Count > 0)
            {
                MessageBox.Show("В полях не допускается латиница.", "Пользователи", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }


            Users user =  db.Users.Where(u => u.id == id).FirstOrDefault();

            if (user != null)
            {

                user.login = login;
                user.password = password;
                user.surname = surname;
                user.name = name;
                user.middlename = middlename;
                user.id_Role = idRole;

                db.SaveChanges();

                return true;
            }
            else
            {
                MessageBox.Show("Пользователь не найден", "Пользователи", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
