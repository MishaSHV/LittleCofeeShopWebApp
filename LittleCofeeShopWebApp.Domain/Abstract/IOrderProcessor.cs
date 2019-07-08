using LittleCofeeShopWebApp.Domain.Entities;

namespace LittleCofeeShopWebApp.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
