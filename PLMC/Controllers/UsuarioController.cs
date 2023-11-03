using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace PLMC.Controllers
{
    public class UsuarioController : Controller
    {

        [HttpGet]
        public IActionResult GetAll()
        {
            BL.Usuario usuario = new BL.Usuario();
            usuario.Usuarios = BL.Usuario.GetAllEF();

            return View(usuario);
        }

        [HttpGet]
        public IActionResult Form(int? idUsuario)
        {
            BL.Usuario usuario = new BL.Usuario();
            usuario.Rol = new BL.Rol();

            BL.Rol listaRoles = BL.Rol.GetAllEF();

            if (idUsuario.HasValue)
            {
                usuario = BL.Usuario.GetByIdEF(idUsuario.Value);

                if (usuario.Correct)
                {
                    usuario.Rol.Roles = listaRoles.Roles;
                }
            }
            else
            {
                usuario.Rol.Roles = listaRoles.Roles;
            }
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Form(BL.Usuario usuario, string password)
        {
            byte[] convertHexadecimal = Encoding.UTF8.GetBytes(password);
            usuario.Password = convertHexadecimal;

            if (usuario.IdUsuario == 0)
            {
                bool succes = BL.Usuario.AddEF(usuario);

                if (succes == true)
                {
                    ViewBag.Message = "USUARIO INGRESADO CORRECTAMENTE";
                }
                else
                {
                    ViewBag.Message = "ERROR, EL USUARIO NO SE AGREGO";
                }
            }
            else
            {
                bool succes = BL.Usuario.UpdateEF(usuario);

                if (succes == true)
                {
                    ViewBag.Message = "USUARIO ACTUALIZADO CORRECTAMENTE";
                }
                else
                {
                    ViewBag.Message = "ERROR, EL USUARIO NO SE ACTUALIZO";
                }

            }
            return PartialView("Modal");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        public IActionResult Login(string email, string password)
        {
            //Convert the string into an array of bytes.
            byte[] messageBytes = Encoding.UTF8.GetBytes(password);

            //Create the hash value from the array of bytes.
            //byte[] hashValue = SHA256.HashData(messageBytes); convierte la corriente de bytes en byte20, y no coincide con el byte11 de la BD aunque
            //sea la contraseña correcta, no esta mal, solo es diferente formato el hasheo


            /* 
             FORMA 2 DE ENCRIPTAR ---> (Si se usa esta forma, el hasValue y messageBytes de arriba se deben comentar)
             var bcrypt = new Rfc2898DeriveBytes(password, new byte[0], 10000, HashAlgorithmName.SHA256);
             var passwordHash = bcrypt.GetBytes(20); este convierte la corrientes de bytes en tamaño de byte20
            */

            BL.Usuario usuario = BL.Usuario.GetByEmail(email);

            if (usuario.Correct)
            {
                HttpContext.Session.SetString("NombreRol", usuario.Rol.Tipo);

                if ((usuario.Password.SequenceEqual(messageBytes)))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Login = true;
                    ViewBag.Message = "CONTRASEÑA INCORRECTA, INTENTA DE NUEVO";
                    return PartialView("Modal");
                }
            }
            else
            {
                ViewBag.Login = true;
                ViewBag.Message = "USUARIO NO EXISTENTE";
                return PartialView("Modal");
            }
        }
    }
}
