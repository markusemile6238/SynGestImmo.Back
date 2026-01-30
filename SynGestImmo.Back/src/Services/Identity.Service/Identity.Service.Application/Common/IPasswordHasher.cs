namespace Identity.Service.Application.Common
{
    public interface IPasswordHasher
    {

        // hash password with randm sel
        string HashPassword(string password);
        

        // verification of password hash with the hashed stocked password
        bool VerifyPassword(string password, string hashedPassword);

    }
}
