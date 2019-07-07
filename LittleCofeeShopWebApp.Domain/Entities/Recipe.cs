using LittleCofeeShopWebApp.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCofeeShopWebApp.Domain.Entities
{
    public class Recipe
    {
        public int CofeeId { get; set; }
        public string CofeeName { get; set; }
        public decimal CofeePriceCoef { get; set; }
        public decimal VolumeSize { get; set; }
        public bool IsCupCap { get; set; }
        public CofeeOptions Options { get; set; }

        public override bool Equals(object obj)
        {
            var recipe = obj as Recipe;
            if (recipe == null)
            {
                return false;
            }
            bool isEqualVolumes = (this.VolumeSize == recipe.VolumeSize) && (this.IsCupCap == recipe.IsCupCap);
            return isEqualVolumes && ((this.CofeeId == recipe.CofeeId) && (this.Options.Equals(recipe.Options)));
        }

        public override int GetHashCode()
        {
            return VolumeSize.GetHashCode() + IsCupCap.GetHashCode() + CofeeId.GetHashCode() + Options.GetHashCode();
        }

        public decimal Price()
        {
            return CofeePriceCoef * VolumeSize;
        }
    }

    /*
    public class PriceCalculator
    {
        public static decimal Calculate(Cofee cofee,VolumeOption volumeOption, MilkOption milkOption = null,SugarOption sugarOption = null)
        {
            return cofee.PriceCoeff * volumeOption.Size;
        }
    }
    */
    public class CofeeOptions
    {
        public int? MilkOptionId { get; set; }
        public string MilkUnitName { get; set; }
        public decimal? MilkSize { get; set; }
        public decimal? MilkPrice { get; set; }

        public int? SugarOptionId { get; set; }
        public string SugarUnitName { get; set; }
        public decimal? SugarSize { get; set; }
        public decimal? SugarPrice { get; set; }

        public override bool Equals(object obj)
        {
            var options = obj as CofeeOptions;
            if (options == null)
            {
                return false;
            }

            bool isEqualMilkOptions = (!this.MilkOptionId.HasValue && !options.MilkOptionId.HasValue) || ((this.MilkOptionId.HasValue && options.MilkOptionId.HasValue) && (this.MilkOptionId.Value == options.MilkOptionId.Value));
            bool isEqualSugarOptions = (!this.SugarOptionId.HasValue && !options.SugarOptionId.HasValue) || (this.SugarOptionId.HasValue && options.SugarOptionId.HasValue) && (this.SugarOptionId.Value == options.SugarOptionId.Value);
            return isEqualMilkOptions && isEqualSugarOptions;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + (MilkOptionId.HasValue ? 0 : MilkOptionId.GetHashCode());
            hash = hash * 23 + (SugarOptionId.HasValue ? 0 : SugarOptionId.GetHashCode());
            return hash;
        }
    }
}