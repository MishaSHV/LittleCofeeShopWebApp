using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleCofeeShopWebApp.Domain.Concrete
{
    public class MatchParentAttribute: Attribute
    {
        public readonly string ParentPropertyName;
        public MatchParentAttribute(string parentPropertyName)
        {
            ParentPropertyName = parentPropertyName;
        }
    }
}
