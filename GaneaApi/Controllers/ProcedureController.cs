using Application.Common.Interfaces;
using Application.Common.Models.Procedure;
using Microsoft.AspNetCore.Http.HttpResults;
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
                var procedures = await procedureService.GetProceduresAsync(search, cancellationToken);
                return Ok(procedures);
           
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProcedure([FromBody] ProcedureDto procedureDto, CancellationToken cancellationToken)
        {
            try
            {
                int createdProcedure = await procedureService.CreateProcedureAsync(procedureDto, cancellationToken);
                CreatedAtAction(nameof(GetProcedures), new { id = createdProcedure }, createdProcedure);
                return StatusCode(StatusCodes.Status201Created, createdProcedure);
            }
            catch (ArgumentException ex)
            {
                // Log the exception (logging mechanism assumed to be in place)  
                return BadRequest($"Validation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism assumed to be in place)  
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ProcedureDto>> UpdateProcedure([FromRoute] int id, [FromBody] ProcedureDto procedureDto, CancellationToken cancellationToken)
        {
            try
            {
                await procedureService.UpdateProcedureAsync(id, procedureDto, cancellationToken);
                return NoContent();

            }
            catch (ArgumentException ex)
            {
                // Log the exception (logging mechanism assumed to be in place)  
                return BadRequest($"Validation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProcedureDto?>> DeleteProcedure([FromRoute] int id, CancellationToken cancellationToken)
        {
            try
            {
                await procedureService.DeleteProcedureAsync(id, cancellationToken);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Log the exception (logging mechanism assumed to be in place)  
                return NotFound($"Procedure with ID {id} not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism assumed to be in place)  
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}
