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
            IList<VolumeOption> latteVolumeOptions = new List<VolumeOption>(defaultVolumeOptions);
            latteVolumeOptions.Add(new VolumeOption { Unit = litreUnit, Size = 0.500M, Name = "Large", IsCupCap = true });
            context.VolumeOptionRecords.AddRange(latteVolumeOptions);

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
                Description = @"Espresso is coffee of Italian origin, brewed by expressing or forcing a small amount of nearly boiling water under
                        pressure through finely ground coffee beans.Espresso is generally thicker than coffee brewed by other methods,
                        has a higher concentration of suspended and dissolved solids, and has crema on top(a foam with a creamy consistency).",
                VolumeOptions = new List<VolumeOption>(defaultVolumeOptions),
                SugarOptions = new List<SugarOption>(sugarOptions),
                PriceCoeff = 3.0M,
                ImagePath = "~/Static/Espresso.jpg"
            };
            context.CofeeRecords.AddOrUpdate(espressoCofee);

            Cofee latteCofee = new Cofee
            {
                Name = "Latte",
                Description = @"A latte is a coffee drink made with espresso and steamed milk. A cafe latte consists of 2 fluid ounces of espresso,
                                3 ounces of steamed milk, and typically a thin layer of foam on top.It can sometimes be referred to as a “Wet Cappuccino”.",
                MilkOptions = new List<MilkOption>(milkOptions),
                VolumeOptions = new List<VolumeOption>(latteVolumeOptions),
                SugarOptions = new List<SugarOption>(sugarOptions),
                PriceCoeff = 4.0M,
                ImagePath = "~/Static/Latte.jpg"
            };
            context.CofeeRecords.AddOrUpdate(latteCofee);

            Cofee americanoCofee = new Cofee
            {
                Name = "Americano",
                Description = @"Caffè Americano, or Americano (Italian: American coffee) is a style of coffee prepared by adding hot water to espresso,
                                giving a similar strength but different flavor from regular drip coffee. The strength of an Americano varies with the number
                                of shots of espresso and the amount of water added.",
                VolumeOptions = new List<VolumeOption>(defaultVolumeOptions),
                SugarOptions = new List<SugarOption>(sugarOptions),
                PriceCoeff = 3.0M,
                ImagePath = "~/Static/Americano.jpg"
            };
            context.CofeeRecords.AddOrUpdate(americanoCofee);

            base.Seed(context);
        }
    }
}