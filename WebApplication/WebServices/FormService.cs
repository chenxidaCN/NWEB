using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.Domain;

namespace WebServices
{
    public interface FormService
    {
        Form getForm(string id);
    }
}
