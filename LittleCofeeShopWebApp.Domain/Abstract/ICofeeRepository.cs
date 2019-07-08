using LittleCofeeShopWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LittleCofeeShopWebApp.Domain.Abstract
{
    public interface ICofeeRepository:IDisposable
    {
        IEnumerable<Cofee> Products { get; }
        void SaveProduct(Cofee product);

        Cofee DeleteProduct(int productID);
    }
}
