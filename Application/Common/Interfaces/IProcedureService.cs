using Application.Common.Models.Procedure;
using Infrastructure.Persistance.Entities;

namespace Application.Common.Interfaces
{
    public interface IProcedureService
    {
        Task<List<ProcedureDto>>GetProceduresAsync(string? search, CancellationToken cancellationToken);

        Task<int> CreateProcedureAsync(ProcedureDto procedureDto, CancellationToken cancellationToken);

        Task UpdateProcedureAsync(int id, ProcedureDto procedureDto, CancellationToken cancellationToken);

        Task DeleteProcedureAsync(int id, CancellationToken cancellationToken);
    }
}
