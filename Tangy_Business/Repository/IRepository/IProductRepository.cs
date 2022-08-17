﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Models;

namespace Tangy_Business.Repository.IRepository
{
    public interface IProductRepository
    {
        public Task<ProductDTO> Create(ProductDTO productDTO);

        public Task<ProductDTO> Update(ProductDTO productDTO);

        public Task<int> Delete(long productId);

        public Task<ProductDTO> Get(long productId);

        public Task<IEnumerable<ProductDTO>> GetAll();
    }
}
