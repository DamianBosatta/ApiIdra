using Microsoft.Extensions.Configuration;

namespace EsMasBarato.Negocios.Contexto
{
    public static class EFStringConnection
    {
        public static IConfiguration? Configuration { get; set; }

        public static string? StringConnection { get; set; }


        public static string GetStringConnection()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(System.AppContext.BaseDirectory)
                                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            string usarStringConnection = Configuration["ConnectionStringEsMasBarato"];
            StringConnection = Configuration.GetConnectionString(usarStringConnection);
            return StringConnection;
        }
    }
}
