using AutoMapper;
using E_COMMERCE_WEBSITE.Context;
using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace E_COMMERCE_WEBSITE.Repositories.OrderRepository
{
    public class OrderRepository:IOrder
    {
        private readonly UserDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private  string HostUrl {  get; set; }
        public OrderRepository(UserDBContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            HostUrl = _configuration["HostUrl:url"];
        }
        public async Task<bool> CreateOrder(int userid, int productId,int quantity)
        {
            var isProduct = await _dbContext.orders.FirstOrDefaultAsync(o => o.Productid == productId && o.Userid == userid);
            if (isProduct != null)
            {
                isProduct.Quantity += quantity;
                _dbContext.SaveChanges();
                return false;
            }
            var neworder = new Order
            {
                Userid = userid,
                Productid = productId,
                Quantity = quantity,
            };
            _dbContext.orders.Add(neworder);
           await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<OrderDTO>> GetAllOrders(int userid)
        {
            var user = await _dbContext.users.Include(u=>u.orders).ThenInclude(o => o.Products).FirstOrDefaultAsync(u => u.id == userid);
            if(user!=null)
            {
               var orderdto= user.orders.Select(u=>new OrderDTO
               {
                   Id= u.Id,
                   userid=userid,
                   productname = u.Products.productName,
                   productdescription = u.Products.productDescription,
                   productprice=u.Products.UnitPrice,
                   Quantity=u.Quantity,
                   TotalPrice= u.Products.UnitPrice*u.Quantity,
                   email =u.User.email,
                   phone=u.User.phone,
                   productImage=HostUrl+u.Products.productImage,

               }).ToList();
                return orderdto;
            }

            return new List<OrderDTO>();
            

        }
       public async Task<decimal> TotalRevenue()
        {
            var order = await _dbContext.users.Include(u=>u.orders).ThenInclude(p=>p.Products).ToListAsync();
            if(order!=null)
            {
                var items = order.SelectMany(o => o.orders).Select(o=>o.Products);
                var totalrevenue = items.Sum(i=>i.UnitPrice);

                return totalrevenue;
            }
            return decimal.Zero;
        }

        public async Task<List<OrderAdminDTO>> GetAlluserOrders(int userid)
        {
            var user = await _dbContext.users.Include(u => u.orders).ThenInclude(o => o.Products).FirstOrDefaultAsync(u => u.id == userid);
            if (user != null)
            {
                var orderdto = user.orders.Select(u => new OrderAdminDTO
                {
                    Id = u.Id,
                    userid = userid,
                    productname = u.Products.productName,
                    productImage=u.Products.productImage,
                    productdescription = u.Products.productDescription,
                    productprice = u.Products.UnitPrice,
                    Quantity = u.Quantity,
                    TotalPrice = u.Products.UnitPrice * u.Quantity,
                    email = u.User.email,
                    phone = u.User.phone

                }).ToList();
                return orderdto;
            }

            return new List<OrderAdminDTO>();


        }

    }
}
