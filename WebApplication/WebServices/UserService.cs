using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.Domain;

namespace WebServices
{
    interface UserService
    {
        User getUser(string id);
    }
}
