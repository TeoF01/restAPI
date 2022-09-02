using System.Reflection;
using Microsoft.AspNetCore.Session;
using NHibernate;

namespace RestApiProject.DbConnection;

public class CreateDbConnection
{
    public static ISessionFactory Start()
    {
        var cfg = new DbSetup().CreateConnection();
        cfg.AddAssembly(Assembly.GetExecutingAssembly());

        var sefact = cfg.BuildSessionFactory();

        var session = sefact.OpenSession();
        {
            if (session.IsConnected)
                Console.WriteLine("DB localDb is connected!");
        }
        return sefact;
    }
}