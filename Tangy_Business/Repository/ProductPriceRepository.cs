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
    public class ProductPriceRepository : IProductPriceRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductPriceRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductPriceDTO> Create(ProductPriceDTO productPriceDTO)
        {
            var productPrice = _mapper.Map<ProductPriceDTO, ProductPrice>(productPriceDTO);

            _db.ProductPrice.Add(productPrice);
            await _db.SaveChangesAsync();

            return _mapper.Map<ProductPrice, ProductPriceDTO>(productPrice);
        }

        public async Task<int> Delete(long productPriceId)
        {
            var productPrice = await _db.ProductPrice.FirstOrDefaultAsync(p => p.Id == productPriceId);
            if (productPrice != null)
            {
                _db.ProductPrice.Remove(productPrice);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductPriceDTO> Get(long productPriceId)
        {
            var productPrice = await _db.ProductPrice.FirstOrDefaultAsync(p => p.Id == productPriceId);
            if (productPrice != null)
            {
                return _mapper.Map<ProductPrice, ProductPriceDTO>(productPrice);
            }
            return new ProductPriceDTO();
        }

        public async Task<IEnumerable<ProductPriceDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(await _db.ProductPrice.ToListAsync());
        }

        public async Task<ProductPriceDTO> Update(ProductPriceDTO productPriceDTO)
        {
            var productPrice = await _db.ProductPrice.FirstOrDefaultAsync(p => p.Id == productPriceDTO.Id);
            if (productPrice != null)
            {
                productPrice.ProductId = productPriceDTO.ProductId;
                productPrice.Size = productPriceDTO.Size;
                productPrice.Price = productPriceDTO.Price;
                _db.ProductPrice.Update(productPrice);
                await _db.SaveChangesAsync();
                return _mapper.Map<ProductPrice, ProductPriceDTO>(productPrice);
            }
            return productPriceDTO;
        }
    }
}
