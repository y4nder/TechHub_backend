namespace infrastructure.services.passwordService;

public interface IPasswordHasherService
{
    public string HashPassword(string password);
    bool VerifyHashedPassword(string hashedPassword, string providedPassword);
}