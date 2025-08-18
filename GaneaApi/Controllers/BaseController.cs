using Infrastructure.Persistance.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GaneaApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]

    public class BaseController : ControllerBase
    {
        /// <summary> Бърз достъп до токена за отнемане на заявка </summary>
        protected CancellationToken CancellationToken => HttpContext.RequestAborted;

        /// <summary> Връща 200ОК или 404 Not Found </summary>
        //protected ActionResult <Appointment> OkOrNotFound(Appointment? appointment)
        //{
        //    if (appointment is null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(appointment);
        //}
        /// <summary> Улеснява 201 Created + Location към именуван маршрут </summary>

        //protected IActionResult CreatedAtGet(string routeName, object routeValues, object? body = null)
        //    => CreatedAtRoute(routeName, routeValues, body ?? new { });

        /// <summary>Стандартизиране на грешка за internal server error </summary>
        //protected IActionResult InternalServerError(string message)
        //{
        //    return StatusCode(500, new { Error = message });
        //}

    }
}
