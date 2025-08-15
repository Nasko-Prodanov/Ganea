namespace Infrastructure.Persistance.Entities
{
    public class Procedure
    {
        public int ProcedureId { get; set; }
        public string ProcedureName { get; set; } = string.Empty;
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public int? ProcedureCategoryId { get; set; }
        public ProcedureCategory? ProcedureCategory { get; set; } = null!;
        public List<AppointmentProcedure> AppointmentProcedures { get; set; } = [];
    }
}
