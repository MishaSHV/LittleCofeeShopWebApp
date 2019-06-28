using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace LittleCofeeShopWebApp.Domain.Entities
{
    public class Order
    {

        public int OrderId { get; set; }

        public int UserId { get; set; }
        public DateTime OrderTime { get; set; }

        [Flags]
        public enum OrderState
        {
            Default,
            Working,
            Successed,
            Canceled
        }

        public OrderState State { get; set; }

        public string RecipeJSON { get; set; }

        [NotMapped]
        public Recipe Recipe
        {
            get { return RecipeJSON == null ? null : JsonConvert.DeserializeObject<Recipe>(RecipeJSON); }
            set { RecipeJSON = JsonConvert.SerializeObject(value); }
        }

        public virtual User User { get; set; }
    }
}