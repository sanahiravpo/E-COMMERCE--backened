using AutoMapper;
using E_COMMERCE_WEBSITE.Context;
using E_COMMERCE_WEBSITE.JwtServise;
using E_COMMERCE_WEBSITE.Migrations;
using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;

using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace E_COMMERCE_WEBSITE.Repositories.OrderRepository
{
    public class OrderRepository : IOrder
    {
        private readonly UserDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private string HostUrl { get; set; }
        private readonly IJwtToken _jwtToken;
        private List<OrderDetailViewUserDTO> orderedtems;

        public OrderRepository(UserDBContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IJwtToken jwtToken)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            HostUrl = _configuration["HostUrl:url"];
            _jwtToken = jwtToken;
        }


        public async Task<bool> CreateOrder(string token, OrderDTO orderdto)
        {
            try
            {
                int userid = _jwtToken.GetUserIdFromToken(token);
                if (userid == 0)
                {
                    throw new Exception("user not found");
                }
                var findcart = await _dbContext.carts.Include(c => c.cartItems).ThenInclude(c => c.products).FirstOrDefaultAsync(c => c.userid == userid);
                if (findcart == null)
                {
                    return false;

                }
                var neworder = new Order
                {
                    Userid = userid,
                    CustomerEmail = orderdto.CustomerEmail,
                    CustomerName = orderdto.CustomerName,
                    CustomerPhone = orderdto.CustomerPhone,
                    CustomerCity = orderdto.CustomerCity,
                    HomeAddress = orderdto.HomeAddress,
                    TransactionId = orderdto.TransactionId,
                    orderdetail = findcart.cartItems.Select(o => new OrderDetail
                    {
                        Productid = o.products.Id,
                        Quantity = o.Quantity,
                        Totalprice = o.Quantity * o.products.UnitPrice,
                    }).ToList()

                };

                await _dbContext.orders.AddAsync(neworder);
                _dbContext.carts.Remove(findcart);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //if (orderdto.TransactionId == null)
            //{
            //    return false;
            //}


        }
        public async Task<List<OrderuserDTO>> GetAllOrders(string token)
        {
            int userid = _jwtToken.GetUserIdFromToken(token);
            if (userid == 0)
            {
                throw new Exception("user not found");
            }
            var user = await _dbContext.orders.Include(u => u.orderdetail).ThenInclude(u => u.Product).FirstOrDefaultAsync(u => u.Userid == userid);
            if (user != null)
            {
                var orderdto = user.orderdetail.Select(o => new OrderuserDTO
                {
                    ProductName = o.Product.productName,
                    ProductImage = HostUrl + o.Product.productImage,
                    TotalPrice = o.Quantity * o.Product.UnitPrice,
                    Quantity = o.Quantity,

                    OrderStatus = o.Order.OrderStatus
                }).ToList();
                return orderdto;
            }
            return new List<OrderuserDTO>();

        }

        public async Task<OrderDetailViewUserDTO> GetAlluserOrders(int userid)
        {


            var user = await _dbContext.orders.Include(u => u.orderdetail).ThenInclude(u => u.Product).FirstOrDefaultAsync(u => u.Userid == userid);
            if (user != null)
            {
                var orderdto = new OrderDetailViewUserDTO
                {
                    Id = user.Id,
                    OrderId = user.Id,
                    OrderStatus = user.OrderStatus,
                    customercity = user.CustomerCity,
                    customeremail = user.CustomerEmail,
                    customername = user.CustomerName,
                    customerphone = user.CustomerPhone,
                    Homeaddress = user.HomeAddress,
                    TransactionId = user.TransactionId,
                    orderedItems = user.orderdetail.Select(u => new CartViewDTO
                    {
                        ProdId = u.Id,
                        productImage = HostUrl + u.Product.productImage,
                        price = u.Product.UnitPrice,
                        productDescription = u.Product.productDescription,
                        productName = u.Product.productName,
                        Quantity = u.Quantity,
                        Totalprice = u.Totalprice,

                    }).ToList()

                };
                return orderdto;
            }

            return new OrderDetailViewUserDTO();
        }

        public async Task<List<OrderAdminDTO>> GetuserordersAdmin(int userid)
        {
            var order = await _dbContext.orders.Include(o => o.orderdetail).ThenInclude(o => o.Product).FirstOrDefaultAsync(o => o.Userid == userid);

            if (order != null)
            {


                var orderitems = order.orderdetail.Select(o => new OrderAdminDTO
                {
                    Id = o.Id,
                    productImage = HostUrl + o.Product.productImage,
                    productdescription = o.Product.productDescription,
                    productname = o.Product.productName,
                    productprice = o.Product.UnitPrice,
                    Quantity = o.Quantity,
                    TotalPrice = o.Totalprice,
                    orderid = o.orderid,
                }).ToList();

                return orderitems;

            }
            return new List<OrderAdminDTO>();
        }



        public async Task<List<OrderedUserDTO>> GetUserDetails(int userid)
        {
            var user = await _dbContext.orders.Include(o => o.orderdetail).Where(o => o.Userid == userid).ToListAsync();

            if (user != null)
            {
                var orders = user.Select(o => new OrderedUserDTO
                {
                    Id = o.Id,
                    customercity = o.CustomerCity,
                    customeremail = o.CustomerEmail,
                    customername = o.CustomerName,
                    customerphone = o.CustomerPhone,
                    Homeaddress = o.HomeAddress,
                    TransactionId = o.TransactionId,
                    OrderStatus = o.OrderStatus,

                }).ToList();

                return orders;
            };


            return new List<OrderedUserDTO>();


        }


    }
}
//public async Task<decimal> TotalRevenue()

// {

//     var order = await _dbContext.orders.Include(u => u.orderdetail).FirstOrDefaultAsync();
//     if(order!=null)
//     {
//         var items = order.SelectMany(o => o.orderdetail);
//         var totalrevenue = items.Sum(i=>i.UnitPrice);

//         return totalrevenue;
//     }
//     return decimal.Zero;
// }


    
//}
