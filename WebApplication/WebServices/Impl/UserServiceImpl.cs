using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.Domain;
using WebRepositories;

namespace WebServices.Impl
{
    public class UserServiceImpl:UserService
    {
        private UserDao UserDao;
        public User getUser(string id)
        {
            return UserDao.Get(id);
        }
        public UserServiceImpl(UserDao userDao)
        {
            this.UserDao = userDao;
        }
    }
}
