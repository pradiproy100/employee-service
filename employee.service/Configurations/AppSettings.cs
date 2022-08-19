
namespace employee.service.Configurations
{
    public class AppSettings
    {
        public string ApplicationName { get; set; }
        public string ConnectionString { get; set; }
        public bool IsProd { get; set; }
        public string AuthKeyName { get; set; }
        public string ApiKey { get; set; }

        public AppSettings() { }
    }
}
