using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Business.Repository.IRepository;
using Tangy_DataAccess;
using Tangy_DataAccess.Data;
using Tangy_Models;

namespace Tangy_Business.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Create(ProductDTO productDTO)
        {
            var product = _mapper.Map<ProductDTO, Product>(productDTO);

            _db.Product.Add(product);
            await _db.SaveChangesAsync();

            return _mapper.Map<Product, ProductDTO>(product);
        }

        public async Task<int> Delete(long productId)
        {
            var product = await _db.Product.FirstOrDefaultAsync(p => p.Id == productId);
            if (product != null)
            {
                _db.Product.Remove(product);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductDTO> Get(long productId)
        {
            var product = await _db.Product.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == productId);
            if (product != null)
            {
                return _mapper.Map<Product, ProductDTO>(product);
            }
            return new ProductDTO();
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(await _db.Product.Include(p => p.Category).ToListAsync());
        }

        public async Task<ProductDTO> Update(ProductDTO productDTO)
        {
            var product = await _db.Product.FirstOrDefaultAsync(p => p.Id == productDTO.Id);
            if (product != null)
            {
                product.Name = productDTO.Name;
                product.Description = productDTO.Description;
                product.ShopFavorites = productDTO.ShopFavorites;
                product.CustomerFavorites = productDTO.CustomerFavorites;
                product.Color = productDTO.Color;
                product.ImageUrl = productDTO.ImageUrl;
                product.CategoryId = productDTO.CategoryId;
                _db.Product.Update(product);
                await _db.SaveChangesAsync();
                return _mapper.Map<Product, ProductDTO>(product);
            }
            return productDTO;
        }
    }
}
