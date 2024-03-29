﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [HiddenInput(DisplayValue = false)]
        public int CofeeId { get; set; }

        [Required(ErrorMessage = "Please enter a cofee name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal PriceCoeff { get; set; }

        [Required(ErrorMessage = "Please specify an image")]
        public string ImagePath { get; set; }

        public byte[] ImageData { get; set; }

        [MaxLength(50)]
        public string ImageMimeType { get; set; }

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