using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCofeeShopWebApp.Domain.Entities
{
    public class Cofee
    {
        public Cofee()
        {
            this.VolumeOptions = new HashSet<VolumeOption>();
            this.MilkOptions = new HashSet<MilkOption>();
            this.SugarOptions = new HashSet<SugarOption>();
        }
        public int CofeeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PriceCoeff { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<VolumeOption> VolumeOptions { get; set; }
        public virtual ICollection<MilkOption> MilkOptions { get; set; }
        public virtual ICollection<SugarOption> SugarOptions { get; set; }
    }

    public class VolumeOption
    {
        public VolumeOption()
        {
            this.CofeeTypes = new HashSet<Cofee>();
        }
        public int VolumeOptionId { get; set; }
        public string Name { get; set; }
        public int UnitId { get; set; }

        public decimal Size { get; set; }
        public bool IsCupCap { get; set; }
        public decimal CupPrice { get; set; }

        public virtual ICollection<Cofee> CofeeTypes { get; set; }
        public virtual Unit Unit { get; set; }
    }

    public class MilkOption
    {
        public MilkOption()
        {
            this.CofeeTypes = new HashSet<Cofee>();
        }
        public int MilkOptionId { get; set; }
        public int UnitId { get; set; }
        public decimal Size { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Cofee> CofeeTypes { get; set; }
        public virtual Unit Unit { get; set; }
    }

    public class SugarOption
    {
        public SugarOption()
        {
            this.CofeeTypes = new HashSet<Cofee>();
        }
        public int SugarOptionId { get; set; }
        public int UnitId { get; set; }
        public decimal Size { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Cofee> CofeeTypes { get; set; }
        public virtual Unit Unit { get; set; }
    }
}