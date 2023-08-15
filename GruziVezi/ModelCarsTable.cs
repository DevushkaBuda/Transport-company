using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace GruziVezi
{
    public class ModelCarsTable
    {
        public static bool Add(string name)
        {

            GruziVeziEntities db = new GruziVeziEntities();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Вы не заполнили все поля", "Модели машин", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }



            ModelCars modelCars = new ModelCars();

            modelCars.name = name;

            db.ModelCars.Add(modelCars);
            db.SaveChanges();

            MessageBox.Show("Модель машины добавлена", "Модели машин", MessageBoxButton.OK, MessageBoxImage.Information);

            return true;


        }

        public static bool Update(int id, string name)
        {
            GruziVeziEntities db = new GruziVeziEntities();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Вы не заполнили все поля", "Модели машин", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }


            ModelCars modelCars = db.ModelCars.Where(mc => mc.id == id).FirstOrDefault();

            if (modelCars != null)
            {

                modelCars.name = name;


                db.SaveChanges();

                return true;
            }
            else
            {
                MessageBox.Show("Модель машины не найдена", "Модели машин", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
