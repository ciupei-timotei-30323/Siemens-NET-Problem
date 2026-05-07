namespace Siemens.Internship2026.GradeBook.Interfaces
{
    public interface IItemValidatorService
    {
        (bool IsValid, string Error) ValidateId(int id);
    }
}
