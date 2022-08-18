using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Models;

namespace Tangy_Business.Repository.IRepository
{
    public interface IProductPriceRepository
    {
        public Task<ProductPriceDTO> Create(ProductPriceDTO productPriceDTO);

        public Task<ProductPriceDTO> Update(ProductPriceDTO productPriceDTO);

        public Task<int> Delete(long productPriceId);

        public Task<ProductPriceDTO> Get(long productPriceId);

        public Task<IEnumerable<ProductPriceDTO>> GetAll();
    }
}
