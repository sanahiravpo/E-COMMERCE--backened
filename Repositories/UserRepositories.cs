using AutoMapper;
using E_COMMERCE_WEBSITE.Context;
using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace E_COMMERCE_WEBSITE.Repositories
{
    public class UserRepositories: IUser
    {
        private readonly UserDBContext _context;
        private readonly IMapper _mapper;
        public UserRepositories(UserDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
            public async Task<bool> RegisterUser(UserRegistrationDTO userdto)
        {
            var user=await _context.users.FirstOrDefaultAsync(U=>U.email== userdto.email);
            if(user!=null)
            {
                return false;

            }
              string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userdto.passwordHash, salt);
            userdto.passwordHash = passwordHash;
   var adduser = _mapper.Map<User>(userdto);

            _context.users.Add(adduser);
            await _context.SaveChangesAsync();
            return true;    
        }
        public async Task<User> AuthenticateUser(UserLoginDTO userlogndto)
        {
            var logined =await  _context.users.FirstOrDefaultAsync(a=>a.email == userlogndto.email);
            //var mapper = _mapper.Map<User>(userlogndto);

            return logined;
        }
        public async Task<List<UserDTOadmin>> GetAllUser()
        {
            var userS =await  _context.users.ToListAsync();
           var Mapper=_mapper.Map <List<UserDTOadmin>>(userS);
            return Mapper;

        }
        public async Task<UserDTOadmin> GetUserById(int id) {
        var finded = await _context.users.FirstOrDefaultAsync(u => u.id == id);
            var mapper=_mapper.Map<UserDTOadmin>(finded);
            return mapper;
        }
    }
}
