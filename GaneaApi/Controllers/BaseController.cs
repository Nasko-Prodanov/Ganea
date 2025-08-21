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
    }
}
