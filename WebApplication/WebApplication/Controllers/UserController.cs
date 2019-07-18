using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebModels.Domain;
using WebServices;

namespace WebApplication.Controllers
{
    public class UserController : ApiController
    {
        private UserService UserService;
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public User Get(string id)
        {
            return UserService.getUser(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        public UserController(UserService userService) {
            this.UserService = userService;
        }
    }
}