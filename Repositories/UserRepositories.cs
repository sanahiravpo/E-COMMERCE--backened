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
        public async Task RegisterUser(UserRegistrationDTO userdto)
        {
            
            var adduser = _mapper.Map<User>(userdto);
          
            _context.users.Add(adduser);
             await  _context.SaveChangesAsync();
        }
        public async Task <User > AuthenticateUser(UserLoginDTO userlogndto)
        {
            var logined =await  _context.users.FirstOrDefaultAsync(a=>a.email == userlogndto.email);


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
