using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;
namespace GruziVezi
{
    public class CarsTable
    {
        public static bool Add(string number, int idModelCar)
        {
            try
            {
                GruziVeziEntities db = new GruziVeziEntities();

                if (string.IsNullOrEmpty(number))
                {
                    MessageBox.Show("Вы не написали фамилию", "Машины", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (number.Length > 11)
                {
                    MessageBox.Show("Длина номера превышает допустимые значения", "Машины", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
               

                Cars car = new Cars();


                car.number = number;
                car.id_ModelCar = idModelCar;

                db.Cars.Add(car);
                db.SaveChanges();

                MessageBox.Show("Машина добавлена", "Машины", MessageBoxButton.OK, MessageBoxImage.Information);

                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
            }
        }


        public static bool Update(int idCar,string number, int idModelCar)
        {
            try
            {
                GruziVeziEntities db = new GruziVeziEntities();

                if (string.IsNullOrEmpty(number))
                {
                    MessageBox.Show("Вы не написали фамилию", "Машины", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (number.Length > 11)
                {
                    MessageBox.Show("Длин номера превышает допустимые значения", "Машины", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }


                Cars car = db.Cars.Where(c => c.id == idCar).FirstOrDefault();

                if (car!=null)
                {
                    car.number = number;
                    car.id_ModelCar = idModelCar;

                    db.SaveChanges();

                    MessageBox.Show("Машина обновлена", "Машины", MessageBoxButton.OK, MessageBoxImage.Information);

                    return true;
                }
                else
                {
                    MessageBox.Show("Машина не найдена", "Машины", MessageBoxButton.OK, MessageBoxImage.Information);
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
