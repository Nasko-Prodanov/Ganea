using Application.Common.Models.ProcedureCategories;

namespace Application.Common.Interfaces
{
    public interface IProcedureCategoriesService
    {
        Task<List<ProcedureCategoryDto>> GetProcedureCategoriesAsync(string? search, CancellationToken cancellationToken);

        Task <int> CreateProcedureCategoryAsync(string CategoryName, CancellationToken cancellationToken);

        Task UpdateProcedureCategoryAsync(int id, string CategoryName, CancellationToken cancellationToken);

        Task DeleteProcedureCategoryAsync(int id, CancellationToken cancellationToken);
    }
}
