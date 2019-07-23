using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.Domain;
using WebRepositories;

namespace WebServices.Impl
{
    public class FormServiceImpl:FormService
    {
        private FormDao FormDao;
        public Form getForm(string id)
        {
            return FormDao.Get(id);
        }
        public FormServiceImpl(FormDao formDao)
        {
            this.FormDao = formDao;
        }
    }
}
