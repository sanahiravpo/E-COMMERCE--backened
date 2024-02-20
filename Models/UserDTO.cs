namespace E_COMMERCE_WEBSITE.Models
{
    public class UserRegistrationDTO
    {
         public string username { get; set; }
        public string passwordHash { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
    public class UserLoginDTO
    {
      
        public string password { get; set; }
        public string email { get; set; }
    }
    public class UserRegisterviewDTO
    {
        public string username { get; set; }
    
        public string email { get; set; }
        public string phone { get; set; }
    }

}
