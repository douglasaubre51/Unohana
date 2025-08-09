using Unohana.Api.Models.SeedModels;

namespace Unohana.Api.Interfaces
{
    public interface IStudentInfoRepository
    {
        Task<StudentCSVModel> GetByRegisterNumber(double registerNumber);
    }
}
