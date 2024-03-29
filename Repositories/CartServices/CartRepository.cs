﻿using AutoMapper;
using E_COMMERCE_WEBSITE.Context;
using E_COMMERCE_WEBSITE.JwtServise;
using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace E_COMMERCE_WEBSITE.Repositories.CartServices
{
    public class CartRepository : ICart
    {
        private readonly UserDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly string HostUrl;
        private readonly IJwtToken _jwtToken;

        public CartRepository(UserDBContext dbContext, IMapper mapper, IConfiguration configuration, IJwtToken jwtToken)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
            HostUrl = _configuration["HostUrl:url"];
            _jwtToken = jwtToken;
        }
        public async Task AddProductToCart(string token, int productid)
        {

            int userid = _jwtToken.GetUserIdFromToken(token);
                if (userid == null)
                {
                    throw new Exception("user is not found");
                }

                var user = await _dbContext.users.Include(u => u.carts).ThenInclude(u => u.cartItems).FirstOrDefaultAsync(u => u.id == userid);
                var product = await _dbContext.products.FirstOrDefaultAsync(p => p.Id == productid);
                if (product != null && user != null)
                {

                    if (user.carts == null)
                    {
                        user.carts = new Cart
                        {

                            userid = userid,

                            cartItems = new List<CartItem>()
                        };

                        _dbContext.carts.Add(user.carts);
                      await  _dbContext.SaveChangesAsync();
                   
          

                }
                var carteditem = user.carts.cartItems.FirstOrDefault(p => p.ProdId == productid);

                if (carteditem != null)
                {
                    carteditem.Quantity = carteditem.Quantity + 1;
                    //carteditem.price = 10;
                    //carteditem.Totalprice = carteditem.Quantity * carteditem.price;

                }
                else
                {

                    var newcartitem = new CartItem
                    {

                        cartid = user.carts.id,
                        ProdId = productid,
                        Quantity = 1


                    };
                   _dbContext.cartitems.Add(newcartitem);
                }
               await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<List<CartDTO>> Getcartdetail(string token)
        {
            
                int UserId = _jwtToken.GetUserIdFromToken(token);
                if (UserId == null)
                {
                    throw new Exception("user is not found");
                }


                var user = await _dbContext.carts
                    .Include(u => u.cartItems)
                    .ThenInclude(u => u.products)
                    .FirstOrDefaultAsync(u => u.userid== UserId);
                if (user != null)
                {
                    var cartitem = user.cartItems.Select(u => new CartDTO
                    {

                        ProdId = u.ProdId,
                        productName = u.products.productName,
                        productImage = HostUrl + u.products.productImage,
                        productDescription = u.products.productDescription,
                        Quantity = u.Quantity,
                        price = u.products.UnitPrice,
                        Totalprice = u.Quantity * u.products.UnitPrice,

                    }).ToList();
                    return cartitem;
                }
                return new List<CartDTO>();

        }

        public async Task Deletecartitem(string token, int productid)
        {

            int UserId = _jwtToken.GetUserIdFromToken(token);
            if (UserId == 0)
            {
                throw new Exception("user is not found");
            }
            var user =await  _dbContext.users.Include(u => u.carts).ThenInclude(u => u.cartItems).FirstOrDefaultAsync(u => u.id == UserId);
            var product =await  _dbContext.products.FirstOrDefaultAsync(P => P.Id == productid);
            if (user != null && product != null)
            {


                var item = user.carts.cartItems.FirstOrDefault(p => p.ProdId == productid);
                if (item != null)
                {
                    _dbContext.cartitems.Remove(item);
                    await _dbContext.SaveChangesAsync();
                }

            }
        }
        public async Task QuantityIncrease(string token, int productid)
        {
            int userid = _jwtToken.GetUserIdFromToken(token);
            if (userid == null)
            {
                throw new Exception("user is not found");
            }
            var user = await _dbContext.users.Include(u => u.carts).ThenInclude(U => U.cartItems).FirstOrDefaultAsync(u => u.id == userid);
            var product = await _dbContext.products.FirstOrDefaultAsync(p => p.Id == productid);

            if (user != null && product != null)
            {

                var item = user.carts.cartItems.FirstOrDefault(u => u.ProdId == productid);
                if (item != null)
                {
                    item.Quantity = item.Quantity + 1;
                   await  _dbContext.SaveChangesAsync();
                }
             
            }
        }

            public async Task QuantityDecrease(string token, int productid)
            {
            int userid=_jwtToken.GetUserIdFromToken(token);
            if (userid == null)
            {
                throw new Exception("user not found");
            }
                var user =await _dbContext.users.Include(u => u.carts).ThenInclude(U => U.cartItems).FirstOrDefaultAsync(u => u.id == userid);
                var product = await _dbContext.products.FirstOrDefaultAsync(p => p.Id == productid);

                if (user != null && product != null)
                {

                    var item = user.carts.cartItems.FirstOrDefault(u => u.ProdId == productid);
                    if (item != null)
                    {
                       item.Quantity= item.Quantity >=1 ? item.Quantity-1: item.Quantity;
                    if (item.Quantity == 0)
                    {
                         _dbContext.cartitems.Remove(item);
                       await _dbContext.SaveChangesAsync() ;

                    }
                    await _dbContext.SaveChangesAsync();
                }
                }


            }

    }
}
        
    

