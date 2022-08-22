using Tangy_Models;

namespace TangyWeb_Client.ViewModels
{
    public class ShoppingCart
    {
        public long ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public long ProductPriceId { get; set; }
        public ProductPriceDTO ProductPrice { get; set; }
        public int Count { get; set; }
    }
}
