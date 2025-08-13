namespace Infrastructure.Persistance.Entities
{
    public class ProcedureCategory
    {
        public int ProcedureCategoryID { get; set; }

        public string CategoryName { get; set; } = null!;
        public List<Procedure> Procedures { get; set; } = [];
    }
}
