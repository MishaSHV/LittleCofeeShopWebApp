using System;
using System.Collections.Generic;
using LittleCofeeShopWebApp.Domain.Abstract;
using LittleCofeeShopWebApp.Domain.Entities;

namespace LittleCofeeShopWebApp.Domain.Concrete
{
    public class EFCofeRepository : ICofeeRepository, IDisposable
    {
        private LittleCofeeShopDbContext context = new LittleCofeeShopDbContext();

        public IEnumerable<Cofee> Products
        {
            get { return context.CofeeRecords.Include("VolumeOptions").Include("MilkOptions").Include("SugarOptions"); }
        }

        public Cofee DeleteProduct(int productID)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void SaveProduct(Cofee product)
        {
            throw new NotImplementedException();
        }
    }
}