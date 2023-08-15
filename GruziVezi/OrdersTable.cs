using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace GruziVezi
{
    public class OrdersTable
    {

        public static bool Add(int  idSender, int idDriver, int idRoute, string description, int idStatus, int idUser)
        {

            GruziVeziEntities db = new GruziVeziEntities();

            if (idSender == 0)
            {
                MessageBox.Show("Вы не заполнили все поля Sender", "Заказы", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (idDriver == 0)
            {
                MessageBox.Show("Вы не заполнили все поля Driver", "Заказы", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (idRoute == 0  )
            {
                MessageBox.Show("Вы не заполнили все поля Route", "Заказы", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }


            Orders order = new Orders();

            order.id_Sender = idSender;
            order.id_Driver = idDriver;
            order.id_Route = idRoute;
            order.description = description;
            order.id_Status = idStatus;
            order.id_User = idUser;


            db.Orders.Add(order);
            db.SaveChanges();

            MessageBox.Show("Заказ добавлен", "Заказы", MessageBoxButton.OK, MessageBoxImage.Information);

            return true;


        }


        public static bool Update(int idOrder,int idSender, int idDriver, int idRoute, string description, int idStatus, int idUser)
        {

            GruziVeziEntities db = new GruziVeziEntities();

            if (idSender == 0)
            {
                MessageBox.Show("Вы не заполнили все поля Sender", "Заказы", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (idDriver == 0)
            {
                MessageBox.Show("Вы не заполнили все поля Driver", "Заказы", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (idRoute == 0)
            {
                MessageBox.Show("Вы не заполнили все поля Route", "Заказы", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }


            Orders order = db.Orders.Where(o => o.id == idOrder).FirstOrDefault();
            if (order != null)
            {
                order.id_Sender = idSender;
                order.id_Driver = idDriver;
                order.id_Route = idRoute;
                order.description = description;
                order.id_Status = idStatus;
                order.id_User = idUser;

                db.SaveChanges();

                MessageBox.Show("Заказ добавлен", "Заказы", MessageBoxButton.OK, MessageBoxImage.Information);

                return true;
            }
            else {
                MessageBox.Show("Заказ не найден", "Заказ", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }


        }

    }
}
