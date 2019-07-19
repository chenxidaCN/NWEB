using SqlSugar;

namespace WebModels.Domain
{
    [SugarTable("test_user")]
    public class User:BaseData
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}