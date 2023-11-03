using DL;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UnidadEntrega
    {
        public int IdUnidadEntrega { get; set; }
        public string? NumeroPlaca { get; set; }
        public string? Modelo { get; set;}
        public string? Marca { get; set; }
        public DateTime? AñoFabricacion { get; set; }
        public EstatusUnidad EstatusUnidad { get; set; }
        //lista para iterar en la vista (PL)
        public List<object>? ListUnidadEntrega { get; set; }
        //PROP para hacer validaciones
        public bool Correct { get; set; }

        // ------------------------ METODOS -------------
        public static UnidadEntrega GetAllSP()
        {
            UnidadEntrega unidadEntrega = new UnidadEntrega();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "UnidadEntregaGetAll";

                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //lee y extrae
                    DataTable tablaUnidadEntrega = new DataTable();
                    adapter.Fill(tablaUnidadEntrega);

                    unidadEntrega.ListUnidadEntrega = new List<object>();

                    if (tablaUnidadEntrega.Rows.Count > 0)
                    {
                        foreach (DataRow row in tablaUnidadEntrega.Rows)
                        {
                            UnidadEntrega unidadEntrega1 = new UnidadEntrega();
                            unidadEntrega1.IdUnidadEntrega = int.Parse(row[0].ToString());
                            unidadEntrega1.NumeroPlaca = row[1].ToString();
                            unidadEntrega1.Modelo = row[2].ToString();
                            unidadEntrega1.Marca = row[3].ToString();
                            unidadEntrega1.AñoFabricacion = DateTime.Parse(row[4].ToString());

                            //Instancia
                            unidadEntrega1.EstatusUnidad = new EstatusUnidad();
                            unidadEntrega1.EstatusUnidad.IdEstatusUnidad = int.Parse(row[5].ToString());
                            unidadEntrega1.EstatusUnidad.Estatus = row[6].ToString(); //el alias colocado en SQL no es necesario aqui

                            unidadEntrega.ListUnidadEntrega.Add(unidadEntrega1);
                        }
                        unidadEntrega.Correct = true;
                    }
                    else
                    {
                        unidadEntrega.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                var excepcion = ex;
            }
            return unidadEntrega;
        }

        public static UnidadEntrega GetByIdSP(int idUnidadEntrega)
        {
            UnidadEntrega unidadEntrega = new UnidadEntrega();  

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    var query = "UnidadEntregaGetById";

                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[1];    

                    collection[0] = new SqlParameter("@IdUnidadEntrega", SqlDbType.Int);
                    collection[0].Value = idUnidadEntrega;

                    cmd.Parameters.AddRange(collection);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //leer y extraer datos 
                    DataTable tablaUnidadEntrega = new DataTable(); //crear tabla
                    adapter.Fill(tablaUnidadEntrega); //Accedo al metodo llenar atraves del objeto adapter y lleno la tabla con lo leido por DataAdapter

                    if (tablaUnidadEntrega.Rows.Count > 0)
                    {
                        DataRow row = tablaUnidadEntrega.Rows[0];

                        unidadEntrega.IdUnidadEntrega = int.Parse(row[0].ToString());
                        unidadEntrega.NumeroPlaca = row[1].ToString();
                        unidadEntrega.Modelo = row[2].ToString();
                        unidadEntrega.Marca = row[3].ToString();
                        unidadEntrega.AñoFabricacion = DateTime.Parse(row[4].ToString());

                        unidadEntrega.EstatusUnidad = new EstatusUnidad();
                        unidadEntrega.EstatusUnidad.IdEstatusUnidad = int.Parse(row[5].ToString());
                        unidadEntrega.EstatusUnidad.Estatus = row[6].ToString();


                        unidadEntrega.Correct = true;
                    }
                    else
                    {
                        unidadEntrega.Correct = false;
                    }

                }
            }
            catch (Exception ex)
            {
                unidadEntrega.Correct = false;
                var excepcion = ex;
            }
            return unidadEntrega;
        }

        public static bool AddSP(UnidadEntrega unidadEntrega)
        {
            bool success;

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "UnidadEntregaAdd"; //nombre del SP

                    SqlCommand cmd = new SqlCommand(query, context); //command necesita la conexion y el query a realizar
                    cmd.CommandType = CommandType.StoredProcedure; //indicamos que es un comando de tipo SP

                    SqlParameter[] collection = new SqlParameter[5]; //indicamos que es una coleccion de 3 datos

                    collection[0] = new SqlParameter("@NumeroPlaca", SqlDbType.VarChar);
                    collection[0].Value = unidadEntrega.NumeroPlaca;

                    collection[1] = new SqlParameter("@Modelo", SqlDbType.VarChar);
                    collection[1].Value = unidadEntrega.Modelo;

                    collection[2] = new SqlParameter("@Marca", SqlDbType.VarChar);
                    collection[2].Value = unidadEntrega.Marca;

                    collection[3] = new SqlParameter("@AnioFabricacion", SqlDbType.DateTime);
                    collection[3].Value = unidadEntrega.AñoFabricacion;

                    collection[4] = new SqlParameter("@IdEstatusUnidad", SqlDbType.Int);
                    collection[4].Value = unidadEntrega.EstatusUnidad.IdEstatusUnidad;

                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();

                    int rowsAffected = cmd.ExecuteNonQuery(); // igualamos el resultado de la ejecucion a una variable INT para validar con ella

                    if (rowsAffected > 0)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                var excepcion = ex;
            }
            return success;
        }

        public static bool UpdateSP(UnidadEntrega unidadEntrega)
        {
            bool success;

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "UnidadEntregaUpdate"; //nombre del SP

                    SqlCommand cmd = new SqlCommand(query, context); //command necesita la conexion y el query a realizar
                    cmd.CommandType = CommandType.StoredProcedure; //indicamos que es un comando de tipo SP

                    SqlParameter[] collection = new SqlParameter[6]; //indicamos que es una coleccion de 6 datos

                    collection[0] = new SqlParameter("@IdUnidadEntrega", SqlDbType.Int);
                    collection[0].Value = unidadEntrega.IdUnidadEntrega;

                    collection[1] = new SqlParameter("@NumeroPlaca", SqlDbType.VarChar);
                    collection[1].Value = unidadEntrega.NumeroPlaca;

                    collection[2] = new SqlParameter("@Modelo", SqlDbType.VarChar);
                    collection[2].Value = unidadEntrega.Modelo;

                    collection[3] = new SqlParameter("@Marca", SqlDbType.VarChar);
                    collection[3].Value = unidadEntrega.Marca;

                    collection[4] = new SqlParameter("@AnioFabricacion", SqlDbType.DateTime);
                    collection[4].Value = unidadEntrega.AñoFabricacion;

                    collection[5] = new SqlParameter("@IdEstatusUnidad", SqlDbType.Int);
                    collection[5].Value = unidadEntrega.EstatusUnidad.IdEstatusUnidad;

                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();

                    int rowsAffected = cmd.ExecuteNonQuery(); // igualamos el resultado de la ejecucion a una variable INT para validar con ella

                    if (rowsAffected > 0)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                var excepcion = ex;
            }
            return success;
        }

        public static bool DeleteSP(int idUnidadEntrega)
        {
            bool success;

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "UnidadEntregaDelete"; //nombre del SP

                    SqlCommand cmd = new SqlCommand(query, context); //command necesita la conexion y el query a realizar
                    cmd.CommandType = CommandType.StoredProcedure; //indicamos que es un comando de tipo SP

                    SqlParameter[] collection = new SqlParameter[6]; //indicamos que es una coleccion de 6 datos

                    collection[0] = new SqlParameter("@IdUnidadEntrega", SqlDbType.Int);
                    collection[0].Value = idUnidadEntrega;

                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();

                    int rowsAffected = cmd.ExecuteNonQuery(); // igualamos el resultado de la ejecucion a una variable INT para validar con ella

                    if (rowsAffected > 0)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                var excepcion = ex;
            }
            return success;
        }
    }
}
