using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;


namespace GruziVezi
{
    public class SendersTable
    {
        public static bool Add(string surname, string name, string middlename, int idCompany)
        {
            try
            {
                GruziVeziEntities db = new GruziVeziEntities();


                if (string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Вы не заполнили все поля", "Заказчики", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show("Вы не написали фамилию", "Заказчики", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Вы не написали имя", "Заказчики", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (string.IsNullOrEmpty(middlename))
                {
                    MessageBox.Show("Вы не написали отчество", "Заказчики", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (matchSpecialSymbol.Count > 0)
                {
                    MessageBox.Show("В полях не допускаются спецсимволы.", "Заказчики", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (matchNumber.Count > 0)
                {
                    MessageBox.Show("В полях [Фамилия / Имя / Отчество] не допускаются числа.", "Заказчики", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (matchWordsFIO.Count > 0)
                {
                    MessageBox.Show("В полях не допускается латиница.", "Заказчики", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }


                Senders senders = new Senders();

                senders.surname = surname;
                senders.name = name;
                senders.middlename = middlename;
                senders.id_Company = idCompany;

                db.Senders.Add(senders);
                db.SaveChanges();

                MessageBox.Show("Заказчик добавлен", "Заказчики", MessageBoxButton.OK, MessageBoxImage.Information);

                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
            }
        }


        public static bool Update(int idSenders, string surname, string name, string middlename, int idCompany)
        {
            try
            {
                GruziVeziEntities db = new GruziVeziEntities();


                if (string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Вы не заполнили все поля", "Заказчики", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show("Вы не написали фамилию", "Заказчики", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Вы не написали имя", "Заказчики", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (string.IsNullOrEmpty(middlename))
                {
                    MessageBox.Show("Вы не написали отчество", "Заказчики", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (matchSpecialSymbol.Count > 0)
                {
                    MessageBox.Show("В полях не допускаются спецсимволы.", "Заказчики", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (matchNumber.Count > 0)
                {
                    MessageBox.Show("В полях [Фамилия / Имя / Отчество] не допускаются числа.", "Заказчики", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (matchWordsFIO.Count > 0)
                {
                    MessageBox.Show("В полях не допускается латиница.", "Заказчики", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }


                Senders senders = db.Senders.Where(s => s.id == idSenders).FirstOrDefault();

                if (senders != null)
                {
                    senders.surname = surname;
                    senders.name = name;
                    senders.middlename = middlename;
                    senders.id_Company = idCompany;

                    db.SaveChanges();

                    MessageBox.Show("Заказчик добавлен", "Заказчики", MessageBoxButton.OK, MessageBoxImage.Information);

                    return true;
                }
                else
                {
                    MessageBox.Show("Заказчик не найден", "Заказчики", MessageBoxButton.OK, MessageBoxImage.Information);
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
