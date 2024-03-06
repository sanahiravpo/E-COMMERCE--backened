namespace E_COMMERCE_WEBSITE.JwtServise
{
    public interface IJwtToken
    {
        int GetUserIdFromToken(string token);
    }
}
