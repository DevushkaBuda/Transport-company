using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace GruziVezi
{
    public class RoutesTable
    {
        public static bool Add(int idCityStart, string adressStart, int  idCityEnd, string adressEnd, out Routes route)
        {
            route = null;

            try
            {
                GruziVeziEntities db = new GruziVeziEntities();
                
                                if (idCityStart == 0 || idCityEnd == 0 || string.IsNullOrEmpty(adressStart) || string.IsNullOrEmpty(adressStart))
                                {
                                    MessageBox.Show("Вы не заполнил все поля", "Маршруты", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return false;
                                }
                


                Regex SpecialSimbols = new Regex("([#]|[=]|[/]|[*]|[@]|[&]|[>]|[<]|[;]|[']|[$]|[№]|[!]|[№]|[;]|[{]|[}]|[[]|[]]|[~])");
                Regex searchNumber = new Regex(@"(\d)");
                Regex Words = new Regex("^[A-Za-z]");

                MatchCollection matchSpecialSymbol1;
                MatchCollection matchSpecialSymbol2;
                MatchCollection matchWords;

                matchSpecialSymbol1 = SpecialSimbols.Matches(adressStart);
                matchSpecialSymbol2 = SpecialSimbols.Matches(adressEnd);

                matchWords = Words.Matches(adressStart + adressEnd);

                MessageBox.Show(matchSpecialSymbol1.Count.ToString());
                MessageBox.Show(matchSpecialSymbol2.Count.ToString());

                if (matchSpecialSymbol1.Count > 0 && matchSpecialSymbol2.Count > 0)
                {
                    MessageBox.Show(adressStart + adressEnd);
                    MessageBox.Show("В полях не допускаются спецсимволы.", "Маршруты", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (matchWords.Count > 0)
                {
                    MessageBox.Show("В полях не допускается латиница.", "Маршруты", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                route = new Routes();

                route.id_CityStart = idCityStart;
                route.id_CityEnd = idCityEnd;
                route.adressStart = adressStart;
                route.adressEnd = adressEnd;


                db.Routes.Add(route);
                db.SaveChanges();

                MessageBox.Show("Маршрут добавлен", "Маршруты", MessageBoxButton.OK, MessageBoxImage.Information);

                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static bool Update(int idRoute,int idCityStart, string adressStart, int idCityEnd, string adressEnd, out Routes route)
        {
            route = null;

            try
            {
                GruziVeziEntities db = new GruziVeziEntities();

                if (idCityStart == 0 || idCityEnd == 0 || string.IsNullOrEmpty(adressStart) || string.IsNullOrEmpty(adressStart))
                {
                    MessageBox.Show("Вы не заполнил все поля", "Маршруты", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }



                Regex SpecialSimbols = new Regex("([#]|[=]|[/]|[*]|[@]|[&]|[>]|[<]|[;]|[']|[$]|[№]|[!]|[№]|[;]|[{]|[}]|[[]|[]]|[~])");
                Regex searchNumber = new Regex(@"(\d)");
                Regex Words = new Regex("^[A-Za-z]");

                MatchCollection matchSpecialSymbol1;
                MatchCollection matchSpecialSymbol2;
                MatchCollection matchWords;

                matchSpecialSymbol1 = SpecialSimbols.Matches(adressStart);
                matchSpecialSymbol2 = SpecialSimbols.Matches(adressEnd);

                matchWords = Words.Matches(adressStart + adressEnd);

                MessageBox.Show(matchSpecialSymbol1.Count.ToString());
                MessageBox.Show(matchSpecialSymbol2.Count.ToString());

                if (matchSpecialSymbol1.Count > 0 && matchSpecialSymbol2.Count > 0)
                {
                    MessageBox.Show(adressStart + adressEnd);
                    MessageBox.Show("В полях не допускаются спецсимволы.", "Маршруты", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (matchWords.Count > 0)
                {
                    MessageBox.Show("В полях не допускается латиница.", "Маршруты", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                route = db.Routes.Where(r => r.id == idRoute).FirstOrDefault();

                if (route != null)
                {

                    route.id_CityStart = idCityStart;
                    route.id_CityEnd = idCityEnd;
                    route.adressStart = adressStart;
                    route.adressEnd = adressEnd;



                    db.SaveChanges();

                    MessageBox.Show("Маршрут добавлен", "Маршруты", MessageBoxButton.OK, MessageBoxImage.Information);

                    return true;
                }
                else
                {
                    MessageBox.Show("Маршрут не найден", "Маршруты", MessageBoxButton.OK, MessageBoxImage.Information);

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
