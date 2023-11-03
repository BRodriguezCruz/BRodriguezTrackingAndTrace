using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Repartidor
    {
        public int IdRepartidor { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Telefono { get; set; }
        public string Fotografia { get; set; }
        public UnidadEntrega UnidadAsignada { get; set; }
        //validaciones instead RESULT ya que no se creo ningun result.
        public bool Correct { get; set; }
        public List<object> Repartidores { get; set; }//SE CREA PARA USARSE EN LA VISTA DE PL, ES OPCIONAL USARSE PARA LA LOGICA AQUI BL



        //METODOS
        public static Repartidor GetAllLINQ() //se devuelve una lista de tipo "usuario", pero bien se pudo regresar solo un tipo "lista" y trabajar sobre ella
        {
            Repartidor repartidor = new Repartidor(); 

            try
            {
                using (DL.BrodriguezTrackingAndTraceContext context = new DL.BrodriguezTrackingAndTraceContext())
                {
                    var query = (from aliasRepartidor in context.Repartidors
                                 join aliasUnidadAsignada in context.UnidadEntregas on aliasRepartidor.IdUnidadAsignada equals aliasUnidadAsignada.IdUnidadEntrega
                                 select new
                                 {
                                     idRepartidorAlias = aliasRepartidor.IdRepartidor,
                                     nombreRepartidorAlias = aliasRepartidor.Nombre,
                                     apellidoPaternoAlias = aliasRepartidor.ApellidoPaterno,
                                     apellidoMaternoAlias = aliasRepartidor.ApellidoMaterno,
                                     telefonoAlias = aliasRepartidor.Telefono,
                                     fechaIngresoAlias = aliasRepartidor.FechaIngreso,
                                     fotografiaAlias = aliasRepartidor.Fotografia,
                                     idUnidadEntregaAlias = aliasUnidadAsignada.IdUnidadEntrega
                                 }).ToList();

                    /* 
                     var query = (from aliasEmpleado in context.Empleadoes
                                 join aliasEntidad in context.CatEntidadFederativas on aliasEmpleado.IdCatEntidadFederativa equals aliasEntidad.IdCatEntidadFederativa
                                 select new
                                 {
                                     idEmpleadoAlias = aliasEmpleado.IdEmpleado,
                                     numeroNominaAlias = aliasEmpleado.NumeroNomina,
                                     nombreAlias = aliasEmpleado.Nombre,
                                     apellidoPaternoAlias = aliasEmpleado.ApellidoPaterno,
                                     apellidoMaternoAlias = aliasEmpleado.ApellidoMaterno,
                                     idEntidadAlias = aliasEntidad.IdCatEntidadFederativa,
                                     nombreEntidadAlias = aliasEntidad.Estado
                                 }).ToList();*/

                    repartidor.Repartidores = new List<object>();

                    if (query != null)
                    {
                        foreach (var registro in query)
                        {
                            Repartidor repartidor1 = new Repartidor();

                            repartidor1.IdRepartidor = registro.idRepartidorAlias;
                            repartidor1.Nombre = registro.nombreRepartidorAlias;
                            repartidor1.ApellidoPaterno = registro.apellidoPaternoAlias;
                            repartidor1.ApellidoMaterno = registro.apellidoMaternoAlias;
                            repartidor1.Telefono = registro.telefonoAlias;
                            repartidor1.FechaIngreso = registro.fechaIngresoAlias;
                            repartidor1.Fotografia = registro.fotografiaAlias;
                            repartidor1.UnidadAsignada = new UnidadEntrega();
                            repartidor1.UnidadAsignada.IdUnidadEntrega = registro.idUnidadEntregaAlias;

                            repartidor.Repartidores.Add(repartidor1);
                        }
                        repartidor.Correct = true;
                    }
                    else
                    {
                        repartidor.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = "HA OCURRIDO UN PROBLEMA: " + ex;
            }
            return repartidor;
        }

        public static Repartidor GetByIdLINQ(int idRepartidor) 
        {
            Repartidor repartidor = new Repartidor();

            try
            {
                using (DL.BrodriguezTrackingAndTraceContext context = new DL.BrodriguezTrackingAndTraceContext())
                {
                    var query = (from aliasRepartidor in context.Repartidors
                                 join aliasUnidadAsignada in context.UnidadEntregas on aliasRepartidor.IdUnidadAsignada equals aliasUnidadAsignada.IdUnidadEntrega
                                 where aliasRepartidor.IdRepartidor == idRepartidor
                                 select new
                                 {
                                     aliasIdRepartidor = aliasRepartidor.IdRepartidor,
                                     aliasNombre = aliasRepartidor.Nombre,
                                     aliasApellidoPaterno = aliasRepartidor.ApellidoPaterno,
                                     aliasApellidoMaterno = aliasRepartidor.ApellidoMaterno,
                                     aliasTelefono = aliasRepartidor.Telefono,
                                     aliasFechaIngreso = aliasRepartidor.FechaIngreso,
                                     aliasFotografia = aliasRepartidor.Fotografia,
                                     aliasIdUnidadAsignada = aliasUnidadAsignada.IdUnidadEntrega
                                 }).SingleOrDefault();

                    if (query != null)
                    {

                            repartidor.IdRepartidor = query.aliasIdRepartidor;
                            repartidor.Nombre = query.aliasNombre;
                            repartidor.ApellidoPaterno = query.aliasApellidoPaterno;
                            repartidor.ApellidoMaterno = query.aliasApellidoMaterno;
                            repartidor.Telefono = query.aliasTelefono;
                            repartidor.FechaIngreso = query.aliasFechaIngreso;
                            repartidor.Fotografia = query.aliasFotografia;
                            repartidor.UnidadAsignada = new UnidadEntrega();
                            repartidor.UnidadAsignada.IdUnidadEntrega = query.aliasIdUnidadAsignada;

                        repartidor.Correct = true;
                    }
                    else
                    {
                        repartidor.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = "HA OCURRIDO UN PROBLEMA: " + ex;
            }
            return repartidor;
        }

        public static bool AddLINQ(Repartidor repartidor)
        {
            bool success = false;
            try
            {
                using (DL.BrodriguezTrackingAndTraceContext context = new DL.BrodriguezTrackingAndTraceContext())
                {
                   DL.Repartidor repartidorEntity = new DL.Repartidor(); //objeto que accede al modelo creado de entity para pasar los datos. 

                    repartidorEntity.Nombre = repartidor.Nombre;
                    repartidorEntity.ApellidoPaterno = repartidor.ApellidoPaterno;
                    repartidorEntity.ApellidoMaterno = repartidor.ApellidoMaterno;
                    repartidorEntity.Telefono = repartidor.Telefono;
                    repartidorEntity.FechaIngreso = repartidor.FechaIngreso;
                    repartidorEntity.Fotografia = repartidor.Fotografia;
                    repartidorEntity.IdUnidadAsignada = repartidor.UnidadAsignada.IdUnidadEntrega;

                    context.Repartidors.Add(repartidorEntity); //CONTEXT conoce todo de su propio modelo creado por ENTITY, es por eso que se guarda
                                                   //  "repartidor1" que tambien es un objeto de su propia clase "repartidor" y no la mia
                    context.SaveChanges();//guarda cambios OUES ANTES DE ESTE METODO estan guardados temporalmente, y devuelve un INT

                    success = true;
                }
            }
            catch (Exception ex)
            {
                var exception = "HA OCURRIDO UN PROBLEMA: " + ex;
                success = false;
            }
            return success;
        }
        public static bool UpdateLINQ(Repartidor repartidor)
        {
            bool success = false;
            try
            {
                using (DL.BrodriguezTrackingAndTraceContext context = new DL.BrodriguezTrackingAndTraceContext())
                {
                    var query = (from aliasRepartidor in context.Repartidors
                                 where aliasRepartidor.IdRepartidor == repartidor.IdRepartidor
                                 select aliasRepartidor).SingleOrDefault();

                    if (query != null)
                    {
                        query.Nombre = repartidor.Nombre;
                        query.ApellidoPaterno = repartidor.ApellidoPaterno;
                        query.ApellidoMaterno = repartidor.ApellidoMaterno;
                        query.Telefono = repartidor.Telefono;
                        query.FechaIngreso = repartidor.FechaIngreso;
                        query.Fotografia = repartidor.Fotografia;
                        query.IdUnidadAsignada = repartidor.UnidadAsignada.IdUnidadEntrega;

                        context.SaveChanges();

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
                var exception = "HA OCURRIDO UN PROBLEMA: " + ex;
                success = false;
            }
            return success;
        }

        public static bool DeleteLINQ(int idRepartidor)
        {
            bool success = false;
            try
            {
                using (DL.BrodriguezTrackingAndTraceContext context = new DL.BrodriguezTrackingAndTraceContext())
                {
                    var query = (from aliasRepartidor in context.Repartidors
                                 where aliasRepartidor.IdRepartidor == idRepartidor
                                 select aliasRepartidor).First();

                    context.Repartidors.Remove(query);

                    int rowsAffected = context.SaveChanges();

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
                var exception = "HA OCURRIDO UN PROBLEMA: " + ex;
                success = false;
            }
            return success;
        }

    }
}
