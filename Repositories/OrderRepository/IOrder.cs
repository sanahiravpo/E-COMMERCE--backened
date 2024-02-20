using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;

namespace E_COMMERCE_WEBSITE.Repositories.OrderRepository
{
    public interface IOrder
    {
        public Task<bool> CreateOrder( int userid, int productid,int Quantity);
        public Task<List<OrderDTO>> GetAllOrders(int userid);
        public Task <decimal> TotalRevenue();
        public  Task<List<OrderAdminDTO>> GetAlluserOrders(int userid);



    }
}
