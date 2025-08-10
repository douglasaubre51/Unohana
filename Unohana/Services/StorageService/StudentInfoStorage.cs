namespace Unohana.Services.StorageService
{
    // this class is not a model!
    // don't move it to Models folder!
    // this class is used to hold data!
    public class StudentInfoStorage
    {
        public double RegisterNumber { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
