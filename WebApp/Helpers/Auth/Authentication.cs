
class Authentication : IAuth
{
    public bool Validate(string email, string password)
    {
        return true;
    }
}