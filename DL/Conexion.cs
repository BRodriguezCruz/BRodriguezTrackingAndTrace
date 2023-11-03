using Microsoft.Extensions.Configuration;


namespace DL
{
    public class Conexion
    {
        //PARA USAR ESTA INYECCION DE DEPENDENCIAS SE DEBEN INSTALAR DOS LIBRERIAS DESDE EL NUGET 
        private static readonly IConfiguration _configuration;
        static Conexion()
        {
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

                _configuration = builder.Build();

            /*var config =
                new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true)
                .AddEnvironmentVariables()
                .Build();
            */
        }
        public static string GetConnectionString()
        {
            /*
            string connectionString = "Data Source=.;Initial Catalog=BRodriguezTrackingAndTrace;User ID=sa;Password=pass@word1;TrustServerCertificate = True";
            return connectionString;
            */
           return _configuration.GetConnectionString("SqlClient");
        }
    }
}