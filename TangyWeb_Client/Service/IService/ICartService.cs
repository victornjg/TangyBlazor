using TangyWeb_Client.ViewModels;

namespace TangyWeb_Client.Service.IService
{
    public interface ICartService
    {
        public event Action OnChange;

        public Task IncrementCart(ShoppingCart itemToAdd);

        public Task DecrementCart(ShoppingCart itemToSubtract);
    }
}
