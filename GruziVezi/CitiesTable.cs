using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;
namespace GruziVezi
{
    public class CitiesTable
    {

        public static bool Add(string name)
        {

            GruziVeziEntities db = new GruziVeziEntities();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Вы не заполнили все поля", "Города", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            Regex SpecialSimbols = new Regex("([#]|[=]|[/]|[*]|[@]|[&]|[>]|[<]|[;]|[']|[$]|[№]|[!]|[№]|[;]|[{]|[}]|[[]|[]]|[~])");

            MatchCollection matchSpecialSymbol;
            matchSpecialSymbol = SpecialSimbols.Matches(name);


            if (matchSpecialSymbol.Count > 0)
            {
                MessageBox.Show("В полях не допускаются спецсимволы.", "Города", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }


            Cities cities = new Cities();

            cities.name = name;

            db.Cities.Add(cities);
            db.SaveChanges();

            MessageBox.Show("Город добавлен", "Города", MessageBoxButton.OK, MessageBoxImage.Information);

            return true;


        }

        public static bool Update(int id, string name)
        {
            GruziVeziEntities db = new GruziVeziEntities();


            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Вы не заполнили все поля", "Города", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            Regex SpecialSimbols = new Regex("([#]|[=]|[/]|[*]|[@]|[&]|[>]|[<]|[;]|[']|[$]|[№]|[!]|[№]|[;]|[{]|[}]|[[]|[]]|[~])");

            MatchCollection matchSpecialSymbol;
            matchSpecialSymbol = SpecialSimbols.Matches(name);


            if (matchSpecialSymbol.Count > 0)
            {
                MessageBox.Show("В полях не допускаются спецсимволы.", "Города", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }



            Cities cities = db.Cities.Where(c => c.id == id).FirstOrDefault();

            if (cities != null)
            {

                cities.name = name;


                db.SaveChanges();

                return true;
            }
            else
            {
                MessageBox.Show("Город не найден", "Города", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
