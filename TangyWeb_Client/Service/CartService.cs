using Blazored.LocalStorage;
using Tangy_Common;
using TangyWeb_Client.Pages.CartDetails;
using TangyWeb_Client.Service.IService;
using TangyWeb_Client.ViewModels;

namespace TangyWeb_Client.Service
{
    public class CartService : ICartService
    {
        public event Action OnChange;

        private readonly ILocalStorageService _localStorageService;

        public CartService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task DecrementCart(ShoppingCart itemToSubtract)
        {
            var shoppingCart = await _localStorageService.GetItemAsync<IList<ShoppingCart>>(SD.ShoppingCart);
            if (shoppingCart != null)
            {
                var item = shoppingCart.FirstOrDefault(i => i.ProductId == itemToSubtract.ProductId && i.ProductPriceId == itemToSubtract.ProductPriceId);
                if (item != null)
                {
                    var diff = item.Count - itemToSubtract.Count;
                    if (diff > 0)
                    {
                        item.Count = diff;
                    }
                    else
                    {
                        shoppingCart.Remove(item);
                    }
                    await _localStorageService.SetItemAsync(SD.ShoppingCart, shoppingCart);
                }
            }
            OnChange.Invoke();
        }

        public async Task IncrementCart(ShoppingCart itemToAdd)
        {
            var shoppingCart = await _localStorageService.GetItemAsync<IList<ShoppingCart>>(SD.ShoppingCart);
            if (shoppingCart == null)
            {
                shoppingCart = new List<ShoppingCart>();
            }
            var item = shoppingCart.FirstOrDefault(i => i.ProductId == itemToAdd.ProductId && i.ProductPriceId == itemToAdd.ProductPriceId);
            if (item != null)
            {
                item.Count += itemToAdd.Count;
            }
            else
            {
                shoppingCart.Add(new ShoppingCart()
                {
                    ProductId = itemToAdd.ProductId,
                    ProductPriceId = itemToAdd.ProductPriceId,
                    Count = itemToAdd.Count
                });
            }
            await _localStorageService.SetItemAsync(SD.ShoppingCart, shoppingCart);
            OnChange.Invoke();
        }
    }
}
