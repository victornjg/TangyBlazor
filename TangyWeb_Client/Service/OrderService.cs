using Newtonsoft.Json;
using System.Collections.Generic;
using Tangy_Models;
using TangyWeb_Client.Service.IService;

namespace TangyWeb_Client.Service
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OrderDTO> Get(int orderId)
        {
            var response = await _httpClient.GetAsync($"/api/order/{orderId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                
                var order = JsonConvert.DeserializeObject<OrderDTO>(content);
                return order;
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(error.ErrorMessage);
            }
        }

        public async Task<IEnumerable<OrderDTO>> GetAll(string? userId)
        {
            var orders = new List<OrderDTO>();
            var response = await _httpClient.GetAsync("/api/order/");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<OrderDTO>>(content);
                if (result != null)
                    orders = result.ToList();
            }
            return orders;
        }
    }
}
