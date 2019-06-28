using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCafeeShopTest.Domain.Entities
{
    public class User
    {
        public User()
        {
            this.UserOrders = new HashSet<Order>();
        }
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Order> UserOrders { get; set; }
    }
}