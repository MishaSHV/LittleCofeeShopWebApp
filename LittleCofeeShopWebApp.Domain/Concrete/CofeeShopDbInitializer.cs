using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using LittleCofeeShopWebApp.Domain.Entities;

namespace LittleCofeeShopWebApp.Domain.Concrete
{
    public class CofeeShopDbInitializer : DropCreateDatabaseAlways<LittleCofeeShopDbContext>//DropCreateDatabaseIfModelChanges
    {
        protected override void Seed(LittleCofeeShopDbContext context)
        {
            this.ProcessSeed(context);
        }

        public void ProcessSeed(LittleCofeeShopDbContext context)
        {
            var notDefUnit = new Unit { Name = "Not defined", ShortName = "N/A" };
            Unit litreUnit = new Unit { Name = "Litre", ShortName = "L" };
            Unit teaspoonUnit = new Unit { Name = "Teaspoon", ShortName = "t" };
            Unit pieceUnit = new Unit { Name = "Piece", ShortName = "pcs" };
            context.Units.AddOrUpdate(notDefUnit);
            context.Units.AddOrUpdate(litreUnit);
            context.Units.AddOrUpdate(teaspoonUnit);
            context.Units.AddOrUpdate(pieceUnit);
            //context.Units.AddRange(units);

            IList<VolumeOption> defaultVolumeOptions = new List<VolumeOption>();

            defaultVolumeOptions.Add(new VolumeOption { Unit = litreUnit, Size = 0.133M, Name = "Small", IsCupCap = true });
            defaultVolumeOptions.Add(new VolumeOption { Unit = litreUnit, Size = 0.250M, Name = "Middle", IsCupCap = true });
            IList<VolumeOption> americanoVolumeOptions = new List<VolumeOption>(defaultVolumeOptions);
            americanoVolumeOptions.Add(new VolumeOption { Unit = litreUnit, Size = 0.500M, Name = "Large", IsCupCap = true });
            context.VolumeOptionRecords.AddRange(americanoVolumeOptions);

            IList<MilkOption> milkOptions = new List<MilkOption>();
            milkOptions.Add(new MilkOption { Unit = notDefUnit, Price = 0.1M, Size = 1.0M });
            context.MilkOptionRecords.AddRange(milkOptions);


            SugarOption sugarOption = new SugarOption { Unit = teaspoonUnit, Price = 0.1M, Size = 1.0M };
            context.SugarOptionRecords.AddOrUpdate(sugarOption);

            IList<SugarOption> sugarOptions = new List<SugarOption>();
            sugarOptions.Add(sugarOption);

            Cofee espressoCofee = new Cofee
            {
                Name = "Espresso",
                Description = "Good cofee",
                VolumeOptions = new List<VolumeOption>(defaultVolumeOptions),
                SugarOptions = new List<SugarOption>(sugarOptions),
                PriceCoeff = 3.0M
            };
            context.CofeeRecords.AddOrUpdate(espressoCofee);

            Cofee latteCofee = new Cofee
            {
                Name = "Latte",
                Description = "Very good cofee",
                VolumeOptions = new List<VolumeOption>(defaultVolumeOptions),
                SugarOptions = new List<SugarOption>(sugarOptions),
                PriceCoeff = 4.0M
            };
            context.CofeeRecords.AddOrUpdate(latteCofee);

            Cofee americanoCofee = new Cofee
            {
                Name = "Americano",
                Description = "Super cofee",
                MilkOptions = new List<MilkOption>(milkOptions),
                VolumeOptions = new List<VolumeOption>(americanoVolumeOptions),
                SugarOptions = new List<SugarOption>(sugarOptions),
                PriceCoeff = 3.0M
            };
            context.CofeeRecords.AddOrUpdate(americanoCofee);

            base.Seed(context);
        }
    }
}