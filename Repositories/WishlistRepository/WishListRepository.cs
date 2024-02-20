using AutoMapper;
using E_COMMERCE_WEBSITE.Context;
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

        public WishListRepository(UserDBContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
           
            _configuration = configuration;
            HostUrl = _configuration["HostUrl:url"];
        }
        public async Task AddToWishList(int userid,int productid)
        {
            var user = await _dbContext.users.Include(u => u.WishLists).FirstOrDefaultAsync(u =>u.id == userid);
            var product=await _dbContext.products.FirstOrDefaultAsync(p=>p.Id == productid);
            if (user != null && product!=null) {
            
                var item=user.WishLists.FirstOrDefault(P=>P.productid == productid);
                if (item==null) {
                    var newwishlist = new WishList
                    {
                        userid = userid,
                        productid = productid
                    };
                  _dbContext.wishlists.Add(newwishlist);
                }
               await _dbContext.SaveChangesAsync();
            }
            await _dbContext.SaveChangesAsync();

        }

        public async Task<List<WishlistDTO>> GetAllWishlistDetails(int userid) {


      var user= _dbContext.wishlists.Include(u=>u.products).ThenInclude(u=>u.categ).Where(u=>u.userid == userid).ToList(); 
           
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
        public async Task DeleteWishList(int userid,int productid)
        {

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
