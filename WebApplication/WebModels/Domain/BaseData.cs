using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModels.Domain
{
    public class BaseData
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string Id { get; set; }
        [SugarColumn(ColumnName = "created_on")]
        public DateTime? CreatedOn { get; set; }
        [SugarColumn(ColumnName = "modified_on")]
        public DateTime? ModifiedOn { get; set; }
    }
}
