using Application.Common.Interfaces;
using Application.Common.Models.ProcedureCategories;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Services
{
    public class ProcedureCategoriesService : IProcedureCategoriesService
    {
        private readonly GaneaDbContext context;

        public ProcedureCategoriesService(GaneaDbContext context)
        {
            this.context = context;
        }

        public async Task<List<ProcedureCategoryDto>> GetProcedureCategoriesAsync(string? search, CancellationToken cancellationToken)
        {
            IQueryable<ProcedureCategory> categories = context.ProcedureCategories;

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();

                categories = categories.Where(c => c.CategoryName.ToLower().Contains(search));
            }

            List<ProcedureCategoryDto> result = await categories
                .Select(c => new ProcedureCategoryDto
                {
                    Id = c.ProcedureCategoryID,
                    CategoryName = c.CategoryName
                })
                .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<int> CreateProcedureCategoryAsync(string CategoryName, CancellationToken cancellationToken)
        {
            var category = new ProcedureCategory
            {
                CategoryName = CategoryName
            };

            context.ProcedureCategories.Add(category);

            await context.SaveChangesAsync(cancellationToken);

            return category.ProcedureCategoryID;
        }

        public async Task UpdateProcedureCategoryAsync(int id, string categoryName, CancellationToken cancellationToken)
        {
            var category = await context.ProcedureCategories
                .FirstOrDefaultAsync(c => c.ProcedureCategoryID == id, cancellationToken);

            if (category == null)
            {
                throw new KeyNotFoundException($"Procedure Category with ID {id} not found.");
            }

            category.CategoryName = categoryName;

            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteProcedureCategoryAsync(int id, CancellationToken cancellationToken)
        {
            var category = await context.ProcedureCategories
                .FirstOrDefaultAsync(c => c.ProcedureCategoryID == id, cancellationToken);

            if (category == null)
            { 
                throw new KeyNotFoundException($"Procedure Category with ID {id} not found.");
            }

            context.ProcedureCategories.Remove(category);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
