using Application.Common.Interfaces;
using Application.Common.Models.Procedure;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Services
{
    public class ProcedureService : IProcedureService
    {
        private readonly GaneaDbContext context;

        public ProcedureService(GaneaDbContext context)
        {
            this.context = context;
        }

        public async Task<List<ProcedureDto>> GetProceduresAsync(string? search, CancellationToken cancellationToken)
        {
            IQueryable<Procedure> procedure = context.Procedures;
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                procedure = procedure.Where(p => p.ProcedureName.ToLower().Contains(search));
            }

            List<ProcedureDto> result = await procedure
                .Select(p => new ProcedureDto
                {
                    ProcedureId = p.ProcedureId,
                    ProcedureName = p.ProcedureName,
                    Duration = p.Duration,
                    Price = p.Price,
                    ProcedureCategoryId = p.ProcedureCategoryId,
                    ProcedureCategory = p.ProcedureCategory != null ? p.ProcedureCategory.CategoryName : null,
                })
                .ToListAsync();

            return result;
        }

        public async Task<ProcedureDto> CreateProcedureAsync(ProcedureDto procedureDto, CancellationToken cancellationToken)
        {
            var procedure = context.Procedures
                .Add(new Infrastructure.Persistance.Entities.Procedure
                {
                    ProcedureName = procedureDto.ProcedureName,
                    Duration = procedureDto.Duration,
                    Price = procedureDto.Price,
                    ProcedureCategoryId = procedureDto.ProcedureCategoryId
                });

            await context.SaveChangesAsync(cancellationToken);
            
            return procedureDto;
        }
        public async Task<ProcedureDto> UpdateProcedureAsync(int id, ProcedureDto procedureDto, CancellationToken cancellationToken)
        {
            var procedure = await context.Procedures
                .FirstOrDefaultAsync(p => p.ProcedureId == id, cancellationToken);
            if (procedure == null)
            {
                throw new KeyNotFoundException($"Procedure with ID {id} not found.");
            }

            procedure.ProcedureName = procedureDto.ProcedureName;
            procedure.Duration = procedureDto.Duration;
            procedure.Price = procedureDto.Price;
            procedure.ProcedureCategoryId = procedureDto.ProcedureCategoryId;

            await context.SaveChangesAsync(cancellationToken);

            return procedureDto;
        }

        public async Task DeleteProcedureAsync(int id, CancellationToken cancellationToken)
        {
            var procedure = await context.Procedures
                .FirstOrDefaultAsync(p => p.ProcedureId == id, cancellationToken);
            if (procedure == null)
            {
                throw new KeyNotFoundException($"Procedure with ID {id} not found.");
            }

            context.Procedures.Remove(procedure);
            
            await context.SaveChangesAsync(cancellationToken);

        }

        Task<int> IProcedureService.CreateProcedureAsync(ProcedureDto procedureDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task IProcedureService.UpdateProcedureAsync(int id, ProcedureDto procedureDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
