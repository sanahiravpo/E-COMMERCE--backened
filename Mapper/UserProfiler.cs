using AutoMapper;
using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;

namespace E_COMMERCE_WEBSITE.Mapper
{
    public class UserProfiler:Profile
    {
        
        public UserProfiler()
        {
            CreateMap<User, UserRegistrationDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();
            CreateMap<User, UserDTOadmin>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product,ProductClientDTO>().ReverseMap();
            CreateMap<Cart, CartDTO>().ReverseMap();
            CreateMap<WishList, WishlistDTO>().ReverseMap();
            CreateMap<WishList, WishListAddDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, OrderuserDTO>().ReverseMap();

        }
    }
}
