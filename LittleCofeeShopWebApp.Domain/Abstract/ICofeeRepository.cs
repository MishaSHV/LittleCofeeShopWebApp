using System;
using System.Collections.Generic;


namespace LittleCofeeShopWebApp.Domain.Abstract
{
    public interface ICofeeRepository:IDisposable
    {
        IEnumerable<Cofee> Products { get; }
        void SaveProduct(Cofee product);

        Cofee DeleteProduct(int productID);
    }
}
