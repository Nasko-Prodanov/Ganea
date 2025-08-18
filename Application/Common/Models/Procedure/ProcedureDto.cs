using Infrastructure.Persistance.Entities;

namespace Application.Common.Models.Procedure
{
    public class ProcedureDto
    {
        public int ProcedureId { get; set; }
        public string ProcedureName { get; set; } = string.Empty;
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public int? ProcedureCategoryId { get; set; }
        public string? ProcedureCategory { get; set; } = null!;
    }
}
