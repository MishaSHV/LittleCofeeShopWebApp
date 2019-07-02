using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleCofeeShopWebApp.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Recipe recipe, int quantity)
        {

        }
        public void RemoveLine(Recipe product)
        {

        }

        public decimal ComputeTotalValue()
        {
           // return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Recipe Recipe { get; set; }
        public int Quantity { get; set; }
    }
}
