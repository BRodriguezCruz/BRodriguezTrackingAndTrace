using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string Tipo { get; set; }
        //validaciones instead RESULT ya que no se creo ningun result.
        public bool Correct { get; set; }
        public List<object> Roles { get; set; } // usada para iterar en PL 


        //METDODOS 
        public static Rol GetAllEF() 
        {
            Rol rol = new Rol(); 
            try
            {
                using (DL.BrodriguezTrackingAndTraceContext context = new DL.BrodriguezTrackingAndTraceContext())
                {
                    var query = context.Rols.FromSqlRaw($"RolGetAll").ToList();

                    rol.Roles = new List<object>();

                    if (query != null)
                    {
                        foreach (var registro in query)
                        {
                            Rol rol1 = new Rol();

                            rol1.IdRol = registro.IdRol;
                            rol1.Tipo = registro.Tipo;

                            rol.Roles.Add(rol1);
                        }
                        rol.Correct = true;
                    }
                    else
                    {
                        rol.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = "HA OCURRIDO UN PROBLEMA: " + ex;
            }
            return rol;
        }
    }
}
