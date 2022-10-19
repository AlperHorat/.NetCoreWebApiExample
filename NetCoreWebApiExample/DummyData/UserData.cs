using NetCoreWebApiExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiExample.DummyData
{
    public class UserData
    {
        public static List<User> UserList = new List<User>();

        static UserData()
        {
            UserList.Add(new User { Id = 1, Name = "Alper" });
            UserList.Add(new User { Id = 2, Name = "Test" });
            UserList.Add(new User { Id = 3, Name = "Kullanıcı" });
        }
    }
}
