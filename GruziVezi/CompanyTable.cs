using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;


namespace GruziVezi
{
    public class CompanyTable
    {
        public static bool Add(string name)
        {
            
            GruziVeziEntities db = new GruziVeziEntities();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Вы не заполнили все поля", "Компании", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }



            Company company = new Company();

            company.name = name;

            db.Company.Add(company);
            db.SaveChanges();

            MessageBox.Show("Компания добавлена", "Компании", MessageBoxButton.OK, MessageBoxImage.Information);

            return true;


        }

        public static bool Update(int id, string name)
        {
            GruziVeziEntities db = new GruziVeziEntities();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Вы не заполнили все поля", "Компании", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }


            Company company = db.Company.Where(c => c.id == id).FirstOrDefault();

            if (company != null)
            {

                company.name = name;


                db.SaveChanges();

                return true;
            }
            else
            {
                MessageBox.Show("Компания не найдена", "Компании", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
