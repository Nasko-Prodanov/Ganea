using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Validators
{
    public class ProcedureValidator
    {


        public static void ProcedureNameEmptyValidator(string? procedureName)
        {
            if (string.IsNullOrWhiteSpace(procedureName))
            {
                throw new ArgumentException("Procedure name cannot be empty.", nameof(procedureName));
            }
        }

        public static void ProcedureDurationValidator(int duration)
        {
            if (duration <= 0)
            {
                throw new ArgumentException("Duration must be positive.", nameof(duration));
            }
        }

        public static void ProcedurePriceValidator(decimal price)
        {
            if (price <= 0)
            {
                throw new ArgumentException("Price must be positive.", nameof(price));
            }
        }

        public static void ProcedureCategoryIdValidator(int? procedureCategoryId)
        {
            if (procedureCategoryId.HasValue && procedureCategoryId <= 0)
            {
                throw new ArgumentException("Invalid procedure category ID.", nameof(procedureCategoryId));
            }
        }

        public static async Task ProcedureDuplicateNameValidator(string name, GaneaDbContext context)
        {
            bool procedureExist = await context.Procedures
                .AnyAsync(p => p.ProcedureName.ToLower() == name.ToLower());
            if (procedureExist)
            {
                throw new ArgumentException("Procedure name already exist.", nameof(procedureExist));
            }
        }

        public static void ProcedureIdValidator(int id)
        {
            if (id <= 0)
            {
                throw new KeyNotFoundException($"Procedure with ID {id} not found.");
            }
        }
    }
}
