namespace WebAPI.Entities
{
    public class User
    {
        public string GUID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}