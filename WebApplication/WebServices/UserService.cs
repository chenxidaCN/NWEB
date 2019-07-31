using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.Domain;

namespace WebServices
{
    public interface UserService
    {
        User getUser(string id);

        User updateUser(Dictionary<string, object> dic);
    }
}
