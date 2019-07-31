using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.Domain;
using WebRepositories;
using WebCommons.Cacher;

namespace WebServices.Impl
{
    public class UserServiceImpl:UserService
    {
        private UserDao UserDao {get;set;}

        [Cacheable(CacheName = "User", Key = "#p0")]
        [CachePut(CacheName = "User", Key = "#result.Name")]
        public User getUser(string id)
        {
            return UserDao.Get(id);
        }
        [CachePut(CacheName = "User", Key = "#result.Id")]
        public User saveUser(User user)
        {
            var f = UserDao.Save(user);
            if (f)
                return user;
            else
                return null;
        }
        [CachePut(CacheName = "User", Key = "#result.Id")]
        public User updateUser(Dictionary<string,object> dic) 
        {
            if (!dic.ContainsKey("Id") || dic["Id"] == null|| string.IsNullOrEmpty(dic["Id"].ToString()))
                throw new Exception("没id");
            var f = UserDao.Update(dic);
            if (f)
                return UserDao.Get(dic["Id"].ToString());
            else
                return null;
        }
        public UserServiceImpl(UserDao userDao)
        {
            this.UserDao = userDao;
        }
    }
}
