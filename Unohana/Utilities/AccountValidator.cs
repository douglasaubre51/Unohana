namespace Unohana.Utilities;

public static class AccountValidator
{
    public static bool ValidateStudent(string email, string password, IQueryable<Student> students)
    {
        Student? dbStudent = students.SingleOrDefault(e => e.Email == email);
        if (dbStudent is null)
            return false;

        if (dbStudent.Password != password)
            return false;

        return true;
    }
    public static bool ValidateTutor(string email, string password, IQueryable<Tutor> tutors)
    {
        Tutor? dbTutor = tutors.SingleOrDefault(e => e.Email == email);
        if (dbTutor is null)
            return false;

        if (dbTutor.Password != password)
            return false;

        return true;
    }
}
