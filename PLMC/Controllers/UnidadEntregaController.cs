using Microsoft.AspNetCore.Mvc;
using NuGet.Versioning;

namespace PLMC.Controllers
{
    public class UnidadEntregaController : Controller
    {
        /* public IActionResult GetAll() CONSUMO DIRECTO EN BL
         {
             BL.UnidadEntrega unidadEntrega = BL.UnidadEntrega.GetAllSP();

             return View(unidadEntrega);
         }*/

        [HttpGet]
        public IActionResult GetAll()
        {
            BL.UnidadEntrega unidadEntrega = new BL.UnidadEntrega();
            unidadEntrega.ListUnidadEntrega = new List<object>();

            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("http://localhost:5063/api/");
                var respuesta = cliente.GetAsync("unidadEntrega");
                respuesta.Wait();

                var resultadoServicio = respuesta.Result;

                if(resultadoServicio.IsSuccessStatusCode)
                {
                    var leerTarea = resultadoServicio.Content.ReadAsAsync<BL.UnidadEntrega>();
                    leerTarea.Wait();

                    foreach (var resultUnidadesEntrega in leerTarea.Result.ListUnidadEntrega)
                    {
                        BL.UnidadEntrega resultItemsList = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.UnidadEntrega>(resultUnidadesEntrega.ToString());
                        unidadEntrega.ListUnidadEntrega.Add(resultItemsList);
                    }
                }
            }
            return View(unidadEntrega);
        }

        /* CONSUMO DIRECTO EN BL
        [HttpGet]
        public IActionResult Form(int? idUnidadEntrega)
        {
            BL.UnidadEntrega unidadEntrega = new BL.UnidadEntrega();

            if (idUnidadEntrega != null)
            {
                unidadEntrega = BL.UnidadEntrega.GetByIdSP(idUnidadEntrega.Value);

                if (unidadEntrega.Correct)
                {
                    //ddl para estatus entrega
                }
            }
            else
            {
                //ddl para estatus entrega
            }
            return View(unidadEntrega);
        }
        */

        [HttpGet]
        public IActionResult Form(int? idUnidadEntrega)
        {
            BL.UnidadEntrega unidadEntrega = new BL.UnidadEntrega();
            BL.EstatusUnidad estatusUnidad = BL.EstatusUnidad.GetAll();
            unidadEntrega.EstatusUnidad = new BL.EstatusUnidad();

            if (idUnidadEntrega != null)
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:5063/api/");
                    var respuesta = cliente.GetAsync("unidadEntrega/" + idUnidadEntrega);
                    respuesta.Wait();

                    var resultadoServicio = respuesta.Result;

                    if (resultadoServicio.IsSuccessStatusCode)
                    {
                        var leerTarea = resultadoServicio.Content.ReadAsAsync<BL.UnidadEntrega>();
                        leerTarea.Wait();

                        var objeto = leerTarea.Result;

                        unidadEntrega = objeto;
                    }
                }
                //DDL ESTATUS 
                unidadEntrega.EstatusUnidad.ListaEstatusUnidad = estatusUnidad.ListaEstatusUnidad;
            }
            else
            {
                //ddl para estatus entrega
                unidadEntrega.EstatusUnidad.ListaEstatusUnidad = estatusUnidad.ListaEstatusUnidad;
            }
            return View(unidadEntrega);
        }


        /* CONSUMO DIRECTO EN BL
        [HttpPost]
        public IActionResult Form(BL.UnidadEntrega unidadEntrega)
        {
            bool success;

            if (unidadEntrega.IdUnidadEntrega == 0)
            {
                success = BL.UnidadEntrega.AddSP(unidadEntrega);

                if(success)
                {
                    ViewBag.Message = "UNIDAD AGREGADA CON EXITO";
                }
                else
                {
                    ViewBag.Message = "ERROR, UNIDAD NO AGREGADA";
                }
            }
            else
            {
                success = BL.UnidadEntrega.UpdateSP(unidadEntrega);

                if (success)
                {
                    ViewBag.Message = "UNIDAD ACTUALIZADA CON EXITO";
                }
                else
                {
                    ViewBag.Message = "ERROR, UNIDAD NO ACTUALIZADA";
                }
            }
            return PartialView("Modal");
        }
        */

        [HttpPost]
        public IActionResult Form(BL.UnidadEntrega unidadEntrega)
        {

            if (unidadEntrega.IdUnidadEntrega == 0)
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:5063/api/");
                    //HTTP POST
                    var postTask = cliente.PostAsJsonAsync("unidadEntrega", unidadEntrega);
                    postTask.Wait();

                    var success = postTask.Result;

                    if (success.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "UNIDAD AGREGADA CON EXITO";
                    }
                    else
                    {
                        ViewBag.Message = "ERROR, UNIDAD NO AGREGADA";
                    }
                }
            }
            else
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:5063/api/");
                    //HTTP PUT
                    var putTask = cliente.PutAsJsonAsync("unidadEntrega/" + unidadEntrega.IdUnidadEntrega, unidadEntrega);
                    putTask.Wait();

                    var success = putTask.Result;

                    if (success.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "UNIDAD ACTUALIZADA CON EXITO";
                    }
                    else
                    {
                        ViewBag.Message = "ERROR, UNIDAD NO ACTUALIZADA";
                    }
                }
            }
            return PartialView("Modal");
        }

        /* CONUSMO DIRECTO DE BL
        [HttpDelete]
        public IActionResult Delete(int idUnidadEntrega)
        {
            bool success = BL.UnidadEntrega.DeleteSP(idUnidadEntrega);

            if (success)
            {
                ViewBag.Message = "UNIDAD ELIMINADA CON EXITO";
            }
            else
            {
                ViewBag.Message = "ERROR, UNIDAD NO ELIMINADA";
            }
            return PartialView("Modal");
        }
        */

        public IActionResult Delete(int idUnidadEntrega)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5063/api/");
                //HTTP DELETE
                var deleteTask = client.DeleteAsync("unidadEntrega/" + idUnidadEntrega);
                deleteTask.Wait();

                var success = deleteTask.Result;

                if (success.IsSuccessStatusCode)
                {
                    ViewBag.Message = "UNIDAD ELIMINADA CON EXITO";
                }
                else
                {
                    ViewBag.Message = "ERROR, UNIDAD NO ELIMINADA YA QUE SU ESTATUS SIGUE DADO DE ALTA, PRIMERO ELIMINALO";
                }
            }
            return PartialView("Modal");
        }
    }
}

