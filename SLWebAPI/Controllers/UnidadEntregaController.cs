using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SLWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadEntregaController : ControllerBase
    {
        [Route("")]
        [HttpGet]

        public IActionResult GetAll()
        {
            BL.UnidadEntrega unidadEntrega = BL.UnidadEntrega.GetAllSP();

            if (unidadEntrega.Correct)
            {
                return Ok(unidadEntrega);
            }
            else
            {
                return BadRequest(unidadEntrega);
            }
        }

        [Route("{idUnidadEntrega}")]
        [HttpGet]
        public IActionResult GetById(int idUnidadEntrega)
        {
            BL.UnidadEntrega unidadEntrega = BL.UnidadEntrega.GetByIdSP(idUnidadEntrega);

            if (unidadEntrega.Correct)
            {
                return Ok(unidadEntrega);
            }
            else
            {
                return BadRequest(unidadEntrega);
            }
        }
 

        [Route("")]
        [HttpPost]
        public IActionResult Add(BL.UnidadEntrega unidadEntrega)
        {
            bool success = BL.UnidadEntrega.AddSP(unidadEntrega);

            if (success)
            {
                return Ok(success);
            }
            else
            {
                return BadRequest(success);
            }
        }


        [Route("{idUnidadEntrega}")]
        [HttpPut]
        public IActionResult Update(int idUnidadEntrega, [FromBody]BL.UnidadEntrega unidadEntrega )
        {
            unidadEntrega.IdUnidadEntrega = idUnidadEntrega;
            bool success = BL.UnidadEntrega.UpdateSP(unidadEntrega);

            if (success)
            {
                return Ok(success);
            }
            else
            {
                return BadRequest(success);
            }
        }

        [Route("{idUnidadEntrega}")]
        [HttpDelete]
        public IActionResult Delete(int idUnidadEntrega)
        {
            bool success = BL.UnidadEntrega.DeleteSP(idUnidadEntrega);

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
