namespace Utils
{
    public class Settings : ISettings
    {
        public string DbConnectionString => resources.DbConnectionString;
    }
}