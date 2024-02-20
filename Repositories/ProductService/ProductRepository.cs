using AutoMapper;
using E_COMMERCE_WEBSITE.Context;
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

        public ProductRepository(UserDBContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            HostUrl = _configuration["HostUrl:url"];
        }


        public async Task AddProduct(ProductClientDTO productsto,IFormFile image)
      {
            try
            {
                string productImage =null;
                if(image!=null && image.Length > 0)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Products", filename);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    productImage = "Images/Products" + filename;
                }
                else
                {
                    productImage = "Images/Default/empty.png";
                }
                var Addcateg = _mapper.Map<Product>(productsto);
                _dbContext.products.Add(Addcateg);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("error in adding  an image");
            }
     
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
        public async Task UpdateProduct(ProductClientDTO updateproducts, int id ,IFormFile image)
        {
            try
            {
                var item = await _dbContext.products.FirstOrDefaultAsync(e => e.Id == id);
                if (item != null)
                {
                    item.productName = updateproducts.productName;
                    item.productDescription = updateproducts.productDescription;
                    item.UnitPrice = updateproducts.UnitPrice;


                    if (image != null && image.Length > 0)
                    {
                        string filename = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Products", filename);
                        using (var stream = new FileStream(filepath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        item.productImage = "Images/Products" + filename;
                    }
                    else
                    {
                        item.productImage = HostUrl + updateproducts.productImage;
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

        public async Task<List<ProductClientDTO>> GetTotalproductspurchased(int userid)
        {
            var user=await _dbContext.users.Include(u=>u.orders).ThenInclude(u=>u.Products).FirstOrDefaultAsync(u=>u.id==userid);

            if(user != null)
            {
                var userdetail = user.orders.Select(u => new ProductClientDTO
                {
                   
                    productId=u.Products.Id,
                    productName=u.  Products.productName,

                    productDescription=u.Products.productDescription,
                    productImage= HostUrl+u.Products.productImage,
                   UnitPrice =u.Products.UnitPrice,
                  
                     


                }).ToList();

               
                return userdetail;

            }
            return new List<ProductClientDTO>();
        }

    }
}
