using SqlSugar;

namespace WebModels.Domain
{
    [SugarTable("TestUser")]
    public class User:BaseData
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}