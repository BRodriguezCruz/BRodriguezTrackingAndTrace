using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SLWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepartidorController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public IActionResult GetAll()
        {
            BL.Repartidor repartidor = BL.Repartidor.GetAllLINQ();

            if (repartidor.Correct)
            {
                return Ok(repartidor);
            }
            else
            {
                return BadRequest(repartidor);
            }
        }
        
        [Route("{idRepartidor}")]
        [HttpGet]
        public IActionResult GetById(int idRepartidor)
        {
            BL.Repartidor repartidor = BL.Repartidor.GetByIdLINQ(idRepartidor);

            if (repartidor.Correct)
            {
                return Ok(repartidor);
            }
            else
            {
                return BadRequest(repartidor);
            }
        }

        
        [Route("")]
        [HttpPost]
        public IActionResult Add(BL.Repartidor repartidor)
        {
            bool success = BL.Repartidor.AddLINQ(repartidor);

            if (success == true)
            {
                return Ok(success);
            }
            else
            {
                return BadRequest(success);
            }
        }

        
        [Route("{idRepartidor}")]
        [HttpPut]
        public IActionResult Update(int idRepartidor,[FromBody]BL.Repartidor repartidor)
        {
            repartidor.IdRepartidor = idRepartidor;

            bool success = BL.Repartidor.UpdateLINQ(repartidor);

            if (success)
            {
                return Ok(success);
            }
            else
            {
                return BadRequest(success);
            }
        }

        
        [Route("{idRepartidor}")]
        [HttpDelete]
        public IActionResult Update(int idRepartidor)
        { 
            bool success = BL.Repartidor.DeleteLINQ(idRepartidor);

            if (success)
            {
                return Ok(success);
            }
            else
            {
                return BadRequest(success);
            }
        }
        
    }
}
