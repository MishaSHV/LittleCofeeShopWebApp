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
            get { return context.CofeeRecords
                    .Include("VolumeOptions")
                    .Include("MilkOptions")
                    .Include("SugarOptions")
                    .Include("VolumeOptions.Unit")
                    .Include("MilkOptions.Unit")
                    .Include("SugarOptions.Unit"); }
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
            if (product.CofeeId == 0)
            {
                context.CofeeRecords.Add(product);
            }
            else
            {
                Cofee dbEntry = context.CofeeRecords.Find(product.CofeeId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.PriceCoeff = product.PriceCoeff;
                    dbEntry.ImagePath = product.ImagePath;
                }
            }
            context.SaveChanges();
        }
    }
}