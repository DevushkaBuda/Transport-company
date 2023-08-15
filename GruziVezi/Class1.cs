using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruziVezi
{
    public class AuthClass
    {
        public static GruziVeziEntities db = new GruziVeziEntities();
        public static string Auto(string login, string password)
        {
            var currentUser = db.Users.FirstOrDefault(p => p.login == login && p.password == password);
            if (currentUser != null)
            {
                switch (currentUser.id_Role)
                {
                    case 1: return "Администратор";
                    case 2: return "Оператор";

                }
            }
            return "Такого пользователя нет";
        }
    }
}
