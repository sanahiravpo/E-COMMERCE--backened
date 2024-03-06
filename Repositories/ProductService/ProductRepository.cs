using AutoMapper;
using E_COMMERCE_WEBSITE.Context;
using E_COMMERCE_WEBSITE.JwtServise;
using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;
using E_COMMERCE_WEBSITE.Repositories.categories;

using Microsoft.EntityFrameworkCore;

namespace E_COMMERCE_WEBSITE.Repositories.ProductService
{
    public class ProductRepository : IProduct
    {
        private readonly UserDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string HostUrl;
        private readonly IConfiguration _configuration;
        private readonly IJwtToken _jwtToken;

        public ProductRepository(UserDBContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IJwtToken jwtToken)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            HostUrl = _configuration["HostUrl:url"];
            _jwtToken = jwtToken;
        }


        public async Task AddProduct(ProductDTO productsto,IFormFile image)
      {
           
                string productImage = null;
                if(image!=null && image.Length > 0)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Products", filename);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    productImage = "/Images/Products/" + filename;
                }
                else
                {
                    productImage = "/Images/Default/empty.png";
                }
                var Addcateg = _mapper.Map<Product>(productsto);
                Addcateg.productImage=productImage;
                await  _dbContext.products.AddAsync(Addcateg);
                await _dbContext.SaveChangesAsync();
          
     
        }

        public async Task<List<ProductClientDTO>> GetAllProducts()
        {

            var listedproduct = await _dbContext.products.Include(p=>p.categ).ToListAsync();
            if (listedproduct.Count > 0)
            {
                var newitem = listedproduct.Select(u=>new ProductClientDTO
                {
                    productId=u.Id,
                    productDescription=u.productDescription,
                    productName=u.productName,
                    productImage= HostUrl+u.productImage,
                    UnitPrice=u.UnitPrice,
                    category=u.categ.name
                       } ).ToList();
                return newitem;
            }
            return new List<ProductClientDTO>();
        }

        public async Task<ProductClientDTO> GetProductById(int id)
        {

            var item =await  _dbContext.products.Include(p=>p.categ).FirstOrDefaultAsync(e => e.Id == id);
            if(item != null)
            {
                ProductClientDTO product = new ProductClientDTO
                {
                    productId = item.Id,
                    productDescription = item.productDescription,
                    productName = item.productName,
                    productImage = HostUrl+item.productImage,
                    UnitPrice = item.UnitPrice,
                    category=item.categ.name
                };
                return product;
            }
            return new ProductClientDTO();
           
        }
        public async Task UpdateProduct(ProductDTO updateproducts, int id ,IFormFile image)
        {
            try
            {
                var item = await _dbContext.products.FirstOrDefaultAsync(e => e.Id == id);
                if (item != null)
                {
                    item.productName = updateproducts.productName;
                    item.productDescription = updateproducts.productDescription;
                    item.UnitPrice = updateproducts.UnitPrice;
                    item.categid= updateproducts.categid;


                    if (image != null && image.Length > 0)
                    {
                        string filename = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Products", filename);


                        using (var stream = new FileStream(filepath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        item.productImage = "/Images/Products/" + filename;

                        await _dbContext.SaveChangesAsync();

                    }
                    else
                    {
                        throw new InvalidOperationException($"Product with ID  not found.");
                    }

                    await _dbContext.SaveChangesAsync();
                }
            }
            catch
            {
                throw new Exception("error in updating  the image in the id");
            }
           

        }
        public async Task DeleteProduct(int id)
        {
            var item =await _dbContext.products.FirstOrDefaultAsync(e => e.Id == id);
            if (item != null)
            {
                _dbContext.products.Remove(item);
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task<List<ProductClientDTO>> GetAllProductsByCategory(int id)
        {

            return _dbContext.products.Include(p => p.categ).Where(p => p.categid == id).Select(p => new
            ProductClientDTO
            {
                productId = p.Id,
                productName = p.productName,
                productDescription = p.productDescription,
                UnitPrice= p.UnitPrice,
                productImage= HostUrl+ p.productImage,
                category = p.categ.name
            }
            ).ToList();

        }

        public async Task<List<ProductClientDTO>> GetTotalproductspurchased(string token)
        {


            int userid = _jwtToken.GetUserIdFromToken(token);
            if (userid == 0)
            {
                throw new Exception("user not found");
            }
            var user=await _dbContext.orders.Include(u=>u.orderdetail).ThenInclude(u => u.Product).FirstOrDefaultAsync(u=>u.Userid==userid);

            if(user != null)
            {
                var userdetail = user.orderdetail.Select(u => new ProductClientDTO
                {
                   
                    productId=u.Productid,
                    productName=u.Product.productName,
                    productImage= HostUrl+u.Product.productImage,
                   UnitPrice =u.Product.UnitPrice,
                  
                     


                }).ToList();

               
                return userdetail;

            }
            return new List<ProductClientDTO>();
        }
        public async Task<List<ProductClientDTO>>Searcheditems(string search)
        {
            try
            {
                var searchitems = await _dbContext.products.Where(o => o.productName.Contains(search)).ToListAsync();

                if (searchitems!=null)
                {
                    var items = searchitems.Select(o => new ProductClientDTO
                    {
                        productId = o.Id,
                        productDescription = o.productDescription,
                        productImage = HostUrl + o.productImage,
                        productName = o.productName,
                        UnitPrice = o.UnitPrice,
                       

                    }).ToList();

                    return items;
                }
                
                return new List<ProductClientDTO>();
            }
          catch(Exception ex)
            {
                throw new Exception("error in searching the product ", ex);
            }
        }
    }
}
