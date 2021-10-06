using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace FivemDocumentBlog.DataBaseConfig
{
    public static class DapperConfiguration
    {
        private static string ConnectionString = "";
        public static void AddDapper(this IServiceCollection service, string connectionString)
        {
            ConnectionString = connectionString;
            service.AddScoped<IDbConnection>(CreateNewConnection);
        }

        private static IDbConnection CreateNewConnection(IServiceProvider service)
        {
            return new SqlConnection(ConnectionString);
        }
    }
}