using Microsoft.AspNetCore.Mvc;

namespace PLMC.Controllers
{
    public class RepartidorController : Controller
    {

        /* CONSUMO DIRECTO EN BL
        [HttpGet]
        public IActionResult GetAll()
        {
            BL.Repartidor repartidor = BL.Repartidor.GetAllLINQ();

            return View(repartidor);
        }
        */

        [HttpGet]
        public IActionResult GetAll()
        {
            BL.Repartidor repartidor = new BL.Repartidor();
            repartidor.Repartidores = new List<object>();

            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("http://localhost:5063/api/");
                var respuesta = cliente.GetAsync("repartidor");
                respuesta.Wait();

                var resultadoServicio = respuesta.Result;

                if (resultadoServicio.IsSuccessStatusCode)
                {
                    var leerTarea = resultadoServicio.Content.ReadAsAsync<BL.Repartidor>();
                    leerTarea.Wait();

                    foreach (var resultRepartidores in leerTarea.Result.Repartidores)
                    {
                        BL.Repartidor resultItemsList = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.Repartidor>(resultRepartidores.ToString());
                        repartidor.Repartidores.Add(resultItemsList);
                    }
                }
            }
            return View(repartidor);
        }


        /* CONSUMO DIRECTO DE BL
        [HttpGet]
        public IActionResult Form (int? idRepartidor)
        {
            BL.Repartidor repartidor = new BL.Repartidor();

            if(idRepartidor != null)
            {
                repartidor = BL.Repartidor.GetByIdLINQ(idRepartidor.Value);

                if(repartidor.Correct)
                {
                    //DDL DE UNIDADES
                }
            }
            else
            {
                //DDL DE UNIDADES
            }

            return View(repartidor);
        }
        */

        [HttpGet]
        public IActionResult Form(int? idRepartidor)
        {
            BL.Repartidor repartidor = new BL.Repartidor();

            if (idRepartidor != null)
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:5063/api/");
                    var respuesta = cliente.GetAsync("repartidor/" + idRepartidor);
                    respuesta.Wait();

                    var resultadoServicio = respuesta.Result;

                    if (resultadoServicio.IsSuccessStatusCode)
                    {
                        var leerTarea = resultadoServicio.Content.ReadAsAsync<BL.Repartidor>();
                        leerTarea.Wait();

                        var objeto = leerTarea.Result;

                        BL.Repartidor resultItem = objeto;
                        //BL.Repartidor resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.Repartidor>(leerTarea.Result.ToString());
                        repartidor = resultItem;
                    }
                }
                //DDL UNIDADES
            }
            else
            {
                //DDL DE UNIDADES
            }

            return View(repartidor);
        }

        /* CONSUMO DIRECTO DE BL
        [HttpPost]
        public IActionResult Form(BL.Repartidor repartidor)
        {
            bool success;
            if (repartidor.IdRepartidor == 0)
            {
                success = BL.Repartidor.AddLINQ(repartidor);

                if (success)
                {
                    ViewBag.Message = "SE AGREGO AL REPARTIDOR EXITOSAMENTE";
                }
                else
                {
                    ViewBag.Message = "ERROR, NO SE AGREGO AL REPARTIDOR";
                }
            }
            else
            {
                success = BL.Repartidor.UpdateLINQ(repartidor);

                if (success)
                {
                    ViewBag.Message = "SE ACTUALIZO AL REPARTIDOR EXITOSAMENTE";
                }
                else
                {
                    ViewBag.Message = "ERROR, NO SE ACTUALIZO AL REPARTIDOR";
                }
            }
            return PartialView("Modal");
        }
        */

        [HttpPost]
        public IActionResult Form(BL.Repartidor repartidor)
        {
            repartidor.Fotografia = "";
            repartidor.Repartidores = new List<object>();
            repartidor.Correct = false;
            repartidor.UnidadAsignada.NumeroPlaca = "";
            repartidor.UnidadAsignada.Modelo = "";
            repartidor.UnidadAsignada.Marca = "";
            repartidor.UnidadAsignada.AñoFabricacion = new DateTime();

            repartidor.UnidadAsignada.EstatusUnidad = new BL.EstatusUnidad();
            repartidor.UnidadAsignada.EstatusUnidad.Estatus = "";
            repartidor.UnidadAsignada.EstatusUnidad.Correct = true;
            repartidor.UnidadAsignada.EstatusUnidad.ListaEstatusUnidad = new List<object>();



            if (repartidor.IdRepartidor == 0)
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:5063/api/");

                    //HTTP POST
                    var postTask = cliente.PostAsJsonAsync("repartidor", repartidor);
                    postTask.Wait();

                    var success = postTask.Result;

                    if (success.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "SE AGREGO AL REPARTIDOR EXITOSAMENTE";
                    }
                    else
                    {
                        ViewBag.Message = "ERROR, NO SE AGREGO AL REPARTIDOR";
                    }
                }
            }
            else
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:5063/api/");

                    //HTTP PUT
                    var putTask = cliente.PutAsJsonAsync("repartidor/" + repartidor.IdRepartidor, repartidor);
                    putTask.Wait();

                    var success = putTask.Result;

                    if (success.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "SE ACTUALIZO AL REPARTIDOR EXITOSAMENTE";
                    }
                    else
                    {
                        ViewBag.Message = "ERROR, NO SE ACTUALIZO AL REPARTIDOR";
                    }
                }
            }
            return PartialView("Modal");
        }

        /*
        [HttpDelete]
        public IActionResult Delete(int idRepartidor)
        {
            bool succes = BL.Repartidor.DeleteLINQ(idRepartidor);

            if (succes)
            {
                ViewBag.Message = "SE ELIMINO AL REPARTIDOR EXITOSAMENTE";
            }
            else
            {
                ViewBag.Message = "ERROR, NO SE ELIMINO AL REPARTIDOR";
            }
            return PartialView("Modal");
        }*/


        [HttpDelete]
        public IActionResult Delete(int idRepartidor)
        {
            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("http://localhost:5063/api/");
                var deleteTask = cliente.DeleteAsync("repartidor/" + idRepartidor);
                deleteTask.Wait();

                var respuesta = deleteTask.Result;

                if(respuesta.IsSuccessStatusCode)
                {
                    ViewBag.Message = "SE ELIMINO AL REPARTIDOR EXITOSAMENTE";
                }
                else
                {
                    ViewBag.Message = "ERROR, NO SE ELIMINO AL REPARTIDOR";
                }
            }
                return PartialView("Modal");
        }
    }
}
