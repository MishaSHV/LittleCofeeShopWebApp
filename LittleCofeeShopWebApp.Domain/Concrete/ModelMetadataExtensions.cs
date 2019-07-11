using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LittleCofeeShopWebApp.Domain.Concrete
{
    public static class ModelMetadataExtensions
    {
        public static ModelMetadata GetMetadata<TModel>(this TModel model)
        {
            return ModelMetadataProviders.Current.GetMetadataForType(null, typeof(TModel));
        }
    }
}
