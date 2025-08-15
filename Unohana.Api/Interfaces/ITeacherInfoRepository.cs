using Unohana.Shared.Models.SeedModels;

namespace Unohana.Api.Interfaces
{
    public interface ITeacherInfoRepository
    {
        Task<TeacherCSVModel> GetByEmployeeId(double employeeId);
    }
}
