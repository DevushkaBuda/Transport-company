using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace GruziVezi
{
    public class DriversTable
    {
        public static bool Add(string surname, string name, string middlename, int idCar)
        {
            try
            {
                GruziVeziEntities db = new GruziVeziEntities();

                if (string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Вы не заполнили все поля", "Водители", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (idCar ==0)
                {
                    MessageBox.Show("Вы не назначили машину", "Водители", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                Regex SpecialSimbols = new Regex("([#]|[=]|[/]|[*]|[@]|[&]|[>]|[<]|[;]|[']|[$]|[№]|[!]|[№]|[;]|[{]|[}]|[[]|[]]|[~])");
                Regex searchNumber = new Regex(@"(\d)");
                Regex Words = new Regex("^[A-Za-z]");
                MatchCollection matchNumber;
                MatchCollection matchSpecialSymbol;
                MatchCollection matchWordsFIO;
                matchSpecialSymbol = SpecialSimbols.Matches(surname + name + middlename);
                matchNumber = searchNumber.Matches(surname + name + middlename);
                matchWordsFIO = Words.Matches(surname + name + middlename);

                if (string.IsNullOrEmpty(surname))
                {
                    MessageBox.Show("Вы не написали фамилию", "Водители", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Вы не написали имя", "Водители", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }


                if (matchSpecialSymbol.Count > 0)
                {
                    MessageBox.Show("В полях не допускаются спецсимволы.", "Водители", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (matchNumber.Count > 0)
                {
                    MessageBox.Show("В полях [Фамилия / Имя / Отчество] не допускаются числа.", "Водители", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (matchWordsFIO.Count > 0)
                {
                    MessageBox.Show("В полях не допускается латиница.", "Водители", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }


                Drivers driver = new Drivers();


                driver.surname = surname;
                driver.name = name;
                driver.middlename = middlename;
                driver.id_Car = idCar;

                db.Drivers.Add(driver);
                db.SaveChanges();

                MessageBox.Show("Водитель добавлен", "Водители", MessageBoxButton.OK, MessageBoxImage.Information);

                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static bool Update(int idDriver,string surname, string name, string middlename, int idCar)
        {
            try
            {
                GruziVeziEntities db = new GruziVeziEntities();

                if (string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Вы не заполнили все поля", "Водители", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                Regex SpecialSimbols = new Regex("([#]|[=]|[/]|[*]|[@]|[&]|[>]|[<]|[;]|[']|[$]|[№]|[!]|[№]|[;]|[{]|[}]|[[]|[]]|[~])");
                Regex searchNumber = new Regex(@"(\d)");
                Regex Words = new Regex("^[A-Za-z]");
                MatchCollection matchNumber;
                MatchCollection matchSpecialSymbol;
                MatchCollection matchWordsFIO;
                matchSpecialSymbol = SpecialSimbols.Matches(surname + name + middlename);
                matchNumber = searchNumber.Matches(surname + name + middlename);
                matchWordsFIO = Words.Matches(surname + name + middlename);

                if (string.IsNullOrEmpty(surname))
                {
                    MessageBox.Show("Вы не написали фамилию", "Водители", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Вы не написали имя", "Водители", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }


                if (matchSpecialSymbol.Count > 0)
                {
                    MessageBox.Show("В полях не допускаются спецсимволы.", "Водители", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (matchNumber.Count > 0)
                {
                    MessageBox.Show("В полях [Фамилия / Имя / Отчество] не допускаются числа.", "Водители", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (matchWordsFIO.Count > 0)
                {
                    MessageBox.Show("В полях не допускается латиница.", "Водители", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }


                Drivers driver = db.Drivers.Where(w => w.id == idDriver).FirstOrDefault();

                if (driver != null)
                {

                    driver.surname = surname;
                    driver.name = name;
                    driver.middlename = middlename;
                    driver.id_Car = idCar;
                    db.SaveChanges();

                    MessageBox.Show("Водитель обновлен", "Водители", MessageBoxButton.OK, MessageBoxImage.Information);

                    return true;
                }
                else
                {
                    MessageBox.Show("Водитель не найден", "Водители", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
            }
        }
    }
}
