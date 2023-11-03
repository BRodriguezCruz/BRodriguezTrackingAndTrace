using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace BL
{
    public class Usuario
    {
        //sustituyen a ML ROL, ya que no se creo esa capa
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public Rol Rol { get; set; }

        //validaciones instead RESULT ya que no se creo ningun result.
        public bool Correct { get; set; }
        public List<object> Usuarios { get; set; } //SE CREA PARA USARSE EN LA VISTA DE PL, ES OPCIONAL USARSE PARA LA LOGICA AQUI BL


        //METDODOS 
        public static List<object> GetAllEF() //se devuelve una lista, pero bien se pudo regresar un tipo de dato"usuario" y trabajar sobre
                                              //la prop lista de usuarios creada arriba
        {
            Usuario usuario = new Usuario(); //se uso solo para las validaciones
            List<object> listaUsuarios = new List<object>();

            try
            {
                using (DL.BrodriguezTrackingAndTraceContext context = new DL.BrodriguezTrackingAndTraceContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll").ToList();

                    usuario.Usuarios = new List<object>();
                    

                    if ( query != null)
                    {
                        foreach( var registro in query)
                        {
                            Usuario usuario1 = new Usuario();

                            usuario1.IdUsuario = registro.IdUsuario;
                            usuario1.Nombre = registro.Nombre;
                            usuario1.ApellidoPaterno = registro.ApellidoPaterno;
                            usuario1.ApellidoMaterno = registro.ApellidoMaterno;
                            usuario1.Email = registro.Email;
                            usuario1.UserName = registro.UserName;
                            usuario1.Password = registro.Password;
                            usuario1.Rol = new Rol();
                            usuario1.Rol.IdRol = registro.IdRol;
                            usuario1.Rol.Tipo = registro.NombreRol;

                            listaUsuarios.Add(usuario1);
                        }
                        usuario.Correct = true;
                    }
                    else
                    {
                        usuario.Correct =false;
                    }
                }               
            }
            catch (Exception ex)
            {
                var exception = "HA OCURRIDO UN PROBLEMA: " + ex;
            }
            return listaUsuarios;
        }


        public static Usuario GetByIdEF(int idUsuario)
        {
            Usuario usuario = new Usuario();

            try
            {
                using (DL.BrodriguezTrackingAndTraceContext context = new DL.BrodriguezTrackingAndTraceContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetById {idUsuario}").AsEnumerable().SingleOrDefault();

                    if (query != null)
                    {
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.UserName = query.UserName;
                        usuario.Rol = new Rol();
                        usuario.Rol.IdRol = query.IdRol;
                        usuario.Rol.Tipo = query.NombreRol;

                        usuario.Correct = true;
                    }
                    else
                    {
                        usuario.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex;
            }
            return usuario;
        }

        public static bool AddEF(Usuario usuario)
        {
            bool success = false;
            try
            {
                using (DL.BrodriguezTrackingAndTraceContext context = new DL.BrodriguezTrackingAndTraceContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}', '{usuario.UserName}', @Password, @IdRol", new SqlParameter("Password", usuario.Password), new SqlParameter("IdRol", usuario.Rol.IdRol));

                    if (query > 0)
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
                var exception = ex;
            }
            return success;
        }

        public static bool UpdateEF(Usuario usuario)
        {
            bool success = false;
            try
            {
                using (DL.BrodriguezTrackingAndTraceContext context = new DL.BrodriguezTrackingAndTraceContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario},'{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}', '{usuario.UserName}', @Password, @IdRol", new SqlParameter("Password", usuario.Password), new SqlParameter("IdRol", usuario.Rol.IdRol));

                    if (query > 0)
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
                var exception = ex;
            }
            return success;
        }

        public static bool DeleteEF(int idUsuario)
        {
            bool success = false;
            try
            {
                using (DL.BrodriguezTrackingAndTraceContext context = new DL.BrodriguezTrackingAndTraceContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioDelete {idUsuario}");

                    if (query > 0)
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
                var exception = ex;
            }
            return success;
        }

        public static Usuario GetByEmail(string email)
        {
            Usuario usuario = new Usuario();

            try
            {
                using (DL.BrodriguezTrackingAndTraceContext context = new DL.BrodriguezTrackingAndTraceContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetByEmail '{email}'").AsEnumerable().SingleOrDefault();

                    if (query != null)
                    {

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.Rol = new Rol();
                        usuario.Rol.Tipo = query.NombreRol;
                        usuario.UserName = query.UserName;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;

                        usuario.Correct = true;
                    }
                    else
                    {
                        usuario.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex; 
            }
            return usuario;
        }

    }
}