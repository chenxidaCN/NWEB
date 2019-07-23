using SqlSugar;

namespace WebModels.Domain
{
    [SugarTable("test_form")]
    public class Form:BaseData
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}