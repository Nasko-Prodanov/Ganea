using Application.Common.Interfaces;
using Application.Common.Models.ProcedureCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GaneaApi.Controllers
{
    public class ProcedureCategoriesController : BaseController
    {
        private readonly IProcedureCategoriesService procedureCategoriesService;

        public ProcedureCategoriesController(IProcedureCategoriesService procedureCategoriesService)
        {
            this.procedureCategoriesService = procedureCategoriesService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ProcedureCategoryDto>>> GetCategories([FromQuery] string? search, CancellationToken cancellationToken)
        {
            return await procedureCategoriesService.GetProcedureCategoriesAsync(search, CancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCategory([FromBody] string categoryName, CancellationToken cancellationToken)
        {
            int Id = await procedureCategoriesService.CreateProcedureCategoryAsync(categoryName, cancellationToken);
            return Id;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory([FromRoute] int id, [FromBody] string categoryName, CancellationToken cancellationToken)
        {
            await procedureCategoriesService.UpdateProcedureCategoryAsync(id, categoryName, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory([FromRoute]int id, CancellationToken cancellationToken)
        {
            await procedureCategoriesService.DeleteProcedureCategoryAsync(id, cancellationToken);
            
            return NoContent();
        }
    }
}
