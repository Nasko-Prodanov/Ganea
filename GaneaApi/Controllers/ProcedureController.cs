using Application.Common.Interfaces;
using Application.Common.Models.Procedure;
using Microsoft.AspNetCore.Mvc;

namespace GaneaApi.Controllers
{
    public class ProcedureController : BaseController
    {
        private readonly IProcedureService procedureService;
        
        public ProcedureController(IProcedureService procedureService)
        {
            this.procedureService = procedureService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProcedureDto>>> GetProcedures([FromQuery] string? search, CancellationToken cancellationToken)
        {
            return await procedureService.GetProceduresAsync(search, cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProcedure([FromBody] ProcedureDto procedureDto, CancellationToken cancellationToken)
        {
            int createdProcedure = await procedureService.CreateProcedureAsync(procedureDto, cancellationToken);
            return createdProcedure;
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<ProcedureDto>> UpdateProcedure([FromRoute] int id, [FromBody] ProcedureDto procedureDto, CancellationToken cancellationToken)
        {
            await procedureService.UpdateProcedureAsync(id, procedureDto, cancellationToken);

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProcedureDto?>> DeleteProcedure([FromRoute]int id, CancellationToken cancellationToken)
        {
            await procedureService.DeleteProcedureAsync(id, cancellationToken);
            
            return NoContent();
        }
    }
}
