using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;

namespace E_COMMERCE_WEBSITE.Repositories.OrderRepository
{
    public interface IOrder
    {
        public Task<bool> CreateOrder(string token, OrderDTO orderdto);
        public Task<List<OrderuserDTO>> GetAllOrders(string token);
        //public Task <decimal> TotalRevenue();
        public  Task<OrderDetailViewUserDTO> GetAlluserOrders(int id);

public Task<List<OrderAdminDTO>> GetuserordersAdmin (int userid);
        public Task<List<OrderedUserDTO>> GetUserDetails(int userid);

    }
}
