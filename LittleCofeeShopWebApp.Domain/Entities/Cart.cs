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
            CartLine line = lineCollection
                .Where(p => p.Recipe.Equals(recipe))
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Recipe = recipe,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(Recipe recipe)
        {
            lineCollection.RemoveAll(l => l.Recipe == recipe);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Recipe.Price * e.Quantity);
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
