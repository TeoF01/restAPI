using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;


namespace RestApiProject.DbConnection;

public class DbSetup
{   
    public Configuration CreateConnection()
    {
        var cfg = new Configuration();

        string dbServer = "localhost";
        string dbName = "localDb";
        string dbUser = "SA";
        string dbPassword = "Database#1";
            
        cfg.DataBaseIntegration(x =>
        {
            x.ConnectionString =  $"Server={dbServer}; database={dbName}; password={dbPassword}; user={dbUser}";
            x.Driver<SqlClientDriver>();
            x.Dialect<MsSql2012Dialect>();
        });
        return cfg;
    }
}