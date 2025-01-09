using Unohana.Models;

namespace Unohana.ViewModels
{
    public class CreateUserAccountVM
    {
        public UserDetails UserDetails;
        public List<Department> departments;
        public List<Semester> semesters;
        public string? SqlErrorMessages;
    }
}