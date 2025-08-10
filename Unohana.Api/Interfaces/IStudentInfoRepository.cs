using Unohana.Shared.Models.SeedModels;

namespace Unohana.Api.Interfaces
{
    public interface IStudentInfoRepository
    {
        Task<StudentCSVModel> GetByRegisterNumber(double registerNumber);
    }
}
