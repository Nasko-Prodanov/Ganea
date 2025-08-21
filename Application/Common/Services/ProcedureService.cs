using Application.Common.Interfaces;
using Application.Common.Models.Procedure;
using Application.Common.Validators;
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

        public async Task<int> CreateProcedureAsync(ProcedureDto procedureDto, CancellationToken cancellationToken)
        {
            ProcedureValidator.ProcedureNameEmptyValidator(procedureDto.ProcedureName);

            ProcedureValidator.ProcedureDurationValidator(procedureDto.Duration);

            ProcedureValidator.ProcedurePriceValidator(procedureDto.Price);

            ProcedureValidator.ProcedureCategoryIdValidator(procedureDto.ProcedureCategoryId);

            await ProcedureValidator.ProcedureDuplicateNameValidator(procedureDto.ProcedureName, context);

            var procedure = context.Procedures
                .Add(new Procedure
                {
                    ProcedureName = procedureDto.ProcedureName,
                    Duration = procedureDto.Duration,
                    Price = procedureDto.Price,
                    ProcedureCategoryId = procedureDto.ProcedureCategoryId
                });

            await context.SaveChangesAsync(cancellationToken);

            return procedureDto.ProcedureId;
        }
        public async Task<ProcedureDto> UpdateProcedureAsync(int id, ProcedureDto procedureDto, CancellationToken cancellationToken)
        {
            var procedure = await context.Procedures
                .FirstOrDefaultAsync(p => p.ProcedureId == id, cancellationToken);

            ProcedureValidator.ProcedureIdValidator(procedure.ProcedureId);

            procedure.ProcedureName = procedureDto.ProcedureName;
            procedure.Duration = procedureDto.Duration;
            procedure.Price = procedureDto.Price;
            procedure.ProcedureCategoryId = procedureDto.ProcedureCategoryId;

            ProcedureValidator.ProcedureNameEmptyValidator(procedure.ProcedureName);

            ProcedureValidator.ProcedureDurationValidator(procedure.Duration);

            ProcedureValidator.ProcedurePriceValidator(procedure.Price);

            ProcedureValidator.ProcedureCategoryIdValidator(procedure.ProcedureCategoryId);

            await ProcedureValidator.ProcedureDuplicateNameValidator(procedure.ProcedureName, context);

            await context.SaveChangesAsync(cancellationToken);

            return procedureDto;
        }

        public async Task DeleteProcedureAsync(int id, CancellationToken cancellationToken)
        {
            var procedure = await context.Procedures
                .FirstOrDefaultAsync(p => p.ProcedureId == id, cancellationToken);

            ProcedureValidator.ProcedureIdValidator(procedure.ProcedureId);

            context.Procedures.Remove(procedure);

            await context.SaveChangesAsync(cancellationToken);

        }
    }
}
