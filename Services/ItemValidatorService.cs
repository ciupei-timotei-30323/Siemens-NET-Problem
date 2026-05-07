using Siemens.Internship2026.GradeBook.Interfaces;

namespace Siemens.Internship2026.GradeBook.Services
{
    public class ItemValidatorService : IItemValidatorService
    {

        public (bool IsValid, string Error) ValidateId(int id)
        {
            if (id <= 0)
                return (false, "Id must be a positive integer.");

            return (true, string.Empty);
        }

    }
}
