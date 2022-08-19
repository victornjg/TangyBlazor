﻿using Tangy_Models;

namespace TangyWeb_Client.Service.IService
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> GetAll();
        public Task<ProductDTO> Get(long productId);
    }
}
