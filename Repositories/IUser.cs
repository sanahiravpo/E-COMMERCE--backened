using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;

namespace E_COMMERCE_WEBSITE.Repositories
{
    public interface IUser
    {
        Task<bool> RegisterUser(UserRegistrationDTO userdto);
       Task<User> AuthenticateUser(UserLoginDTO userlogndto);
        public Task<List<UserDTOadmin>> GetAllUser();
        public Task<UserDTOadmin> GetUserById(int id);
  
    }
}
