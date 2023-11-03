using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BL
{
    public class EstatusUnidad
    {
        public int IdEstatusUnidad { get; set; }
        public string? Estatus { get; set; }

        //validaciones instead RESULT ya que no se creo ningun result.
        public bool Correct { get; set; }
        public List<object>? ListaEstatusUnidad { get; set; } // usada para iterar en PL y para trabajar aqui en BL


        public static EstatusUnidad GetAll()
        {
            EstatusUnidad estatusUnidad = new EstatusUnidad();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    var query = "EstatusUnidadGetAll";

                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablaEstatusUnidad = new DataTable();
                    adapter.Fill(tablaEstatusUnidad);

                    estatusUnidad.ListaEstatusUnidad = new List<object>();

                    if (tablaEstatusUnidad.Rows.Count > 0)
                    {
                        foreach (DataRow row in tablaEstatusUnidad.Rows)
                        {
                            EstatusUnidad estatusUnidad1 = new EstatusUnidad();
                            estatusUnidad1.IdEstatusUnidad = int.Parse(row[0].ToString());
                            estatusUnidad1.Estatus = row[1].ToString();

                            estatusUnidad.ListaEstatusUnidad.Add(estatusUnidad1);
                        }
                        estatusUnidad.Correct = true;
                    }
                    else
                    {
                        estatusUnidad.Correct = false;
                    }

                }
            }
            catch(Exception ex)
            {
                var excepcion = ex;
            }
            return estatusUnidad;
        }
    }
}
