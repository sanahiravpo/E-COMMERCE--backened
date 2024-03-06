using AutoMapper;
using E_COMMERCE_WEBSITE.Context;
using E_COMMERCE_WEBSITE.JwtServise;
using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;

using Microsoft.EntityFrameworkCore;

namespace E_COMMERCE_WEBSITE.Repositories.WishlistRepository
{
    public class WishListRepository:IWishlist
    {
        private readonly UserDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string HostUrl;
        private readonly IConfiguration _configuration;
        private readonly IJwtToken _jwtToken;

        public WishListRepository(UserDBContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IJwtToken jwtToken)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
           
            _configuration = configuration;
            HostUrl = _configuration["HostUrl:url"];
            _jwtToken = jwtToken;
        }
        public async Task<bool> AddToWishList(string token,int productid)
        {

            int userid = _jwtToken.GetUserIdFromToken(token);
            if (userid == 0)
            {
                throw new Exception("user not found");
            }
             var item=_dbContext.wishlists.Include(p=>p.products).FirstOrDefault(p=>p.Id== productid);    

                if (item==null) {
                    WishListAddDTO newwishlist = new WishListAddDTO
                    {
                       userId= userid,
                       ProductId= productid,
                    };

                    var mapper = _mapper.Map<WishList>(newwishlist);
                  _dbContext.wishlists.Add(mapper);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                _dbContext.wishlists.Remove(item);
               await _dbContext.SaveChangesAsync();
                return true;
            

           

        }

        public async Task<List<WishlistDTO>> GetAllWishlistDetails(string token) {



            int userid = _jwtToken.GetUserIdFromToken(token);
            if (userid == 0)
            {
                throw new Exception("user not found");
            }
            var user=_dbContext.wishlists.Include(u=>u.products).ThenInclude(u=>u.categ).Where(u=>u.userid == userid).ToList(); 
           
            if(user != null )
            {
               var wishlisted=user.Select(u=>new WishlistDTO
               {

                   Id = u.Id,
                   productName=u.products.productName,
                   productDescription=u.products.productDescription,
                   UnitPrice=u.products.UnitPrice,
                   productImage= HostUrl+u.products.productImage


               }).ToList();
                return wishlisted;
            }
            return new List<WishlistDTO>();


        }
        public async Task DeleteWishList(string token,int productid)
        {


            int userid = _jwtToken.GetUserIdFromToken(token);
            if (userid == 0)
            {
                throw new Exception("user not found");
            }

            var user=await _dbContext.wishlists.FirstOrDefaultAsync(u=>u.Id== userid);
            var product=await _dbContext.products.FirstOrDefaultAsync(u=>u.Id== productid);
            if(user != null && product!=null)
            {
                _dbContext.wishlists.Remove(user);
                await _dbContext.SaveChangesAsync();
            }

        }
    }
}
