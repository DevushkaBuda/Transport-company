using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace GruziVezi
{
    public class RolesTable
    {
        public static bool Add(string name)
        {
         
                GruziVeziEntities db = new GruziVeziEntities();

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Вы не заполнили все поля", "Роли", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                Regex SpecialSimbols = new Regex("([#]|[=]|[/]|[*]|[@]|[&]|[>]|[<]|[;]|[']|[$]|[№]|[!]|[№]|[;]|[{]|[}]|[[]|[]]|[~])");

                MatchCollection matchSpecialSymbol;
                matchSpecialSymbol = SpecialSimbols.Matches(name);    


                if (matchSpecialSymbol.Count > 0)
                {
                    MessageBox.Show("В полях не допускаются спецсимволы.", "Роли", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }


                Roles role = new Roles();

                role.name = name;

                db.Roles.Add(role);
                db.SaveChanges();

                MessageBox.Show("Роль добавлена", "Роли", MessageBoxButton.OK, MessageBoxImage.Information);

                return true;
            

        }

        public static bool Update(int id, string name)
        {
            GruziVeziEntities db = new GruziVeziEntities();


            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Вы не заполнили все поля", "Роли", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            Regex SpecialSimbols = new Regex("([#]|[=]|[/]|[*]|[@]|[&]|[>]|[<]|[;]|[']|[$]|[№]|[!]|[№]|[;]|[{]|[}]|[[]|[]]|[~])");

            MatchCollection matchSpecialSymbol;
            matchSpecialSymbol = SpecialSimbols.Matches(name);


            if (matchSpecialSymbol.Count > 0)
            {
                MessageBox.Show("В полях не допускаются спецсимволы.", "Роли", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }



            Roles role = db.Roles.Where(r => r.id == id).FirstOrDefault();

            if (role != null)
            {

                role.name = name;


                db.SaveChanges();

                return true;
            }
            else
            {
                MessageBox.Show("Роль не найдена", "Роли", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
