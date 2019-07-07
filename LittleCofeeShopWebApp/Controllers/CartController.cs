﻿using LittleCofeeShopWebApp.Domain.Abstract;
using LittleCofeeShopWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LittleCofeeShopWebApp.Controllers
{
    public class CartController : Controller
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;
        ICofeeRepository repository;

        public CartController(ICofeeRepository cofeeRepository)
        {
            this.repository = cofeeRepository;
        }

        public RedirectToRouteResult AddToCart(int quantity, int cofeeId, int volumeOptionId,bool isSugarOption,bool isMilkOption = false)
        {
            Cofee cofee = repository.Products
                .FirstOrDefault(c => c.CofeeId == cofeeId);
            VolumeOption volumeOption = cofee?.VolumeOptions
                .FirstOrDefault(vo => vo.VolumeOptionId == volumeOptionId);

            if ( (cofee != null)&&(volumeOption != null) )
            {
                Recipe recipe = new Recipe();
                recipe.CofeeId = cofee.CofeeId;
                recipe.CofeeName = cofee.Name;
                recipe.CofeePriceCoef = cofee.PriceCoeff;

                recipe.VolumeSize = volumeOption.Size;
                recipe.IsCupCap = volumeOption.IsCupCap;

                recipe.Options = new CofeeOptions();

                if (isSugarOption)
                {
                    SugarOption sugarOption = cofee.SugarOptions.FirstOrDefault();
                    if(sugarOption != null)
                    {
                        recipe.Options.SugarOptionId = sugarOption.SugarOptionId;
                        recipe.Options.SugarPrice = sugarOption.Price;
                        recipe.Options.SugarSize = sugarOption.Size;
                        recipe.Options.SugarUnitName = sugarOption.Unit.Name;
                    }
                }

                if(isMilkOption)
                {
                    MilkOption milkOption = cofee.MilkOptions.FirstOrDefault();
                    if(milkOption != null)
                    {
                        recipe.Options.MilkOptionId = milkOption.MilkOptionId;
                        recipe.Options.MilkPrice = milkOption.Price;
                        recipe.Options.MilkSize = milkOption.Size;
                        recipe.Options.MilkUnitName = milkOption.Unit.Name;
                    }
                }
                GetCart().AddItem(recipe, quantity);

                
            }
            return RedirectToAction("Index");
        }

        public RedirectToRouteResult RemoveFromCart(int cofeeId, int volumeOptionId, bool isSugarOption, bool isMilkOption = false)
        {
            Cofee cofee = repository.Products
                .FirstOrDefault(p => p.CofeeId == cofeeId);
            VolumeOption volumeOption = cofee?.VolumeOptions
                .FirstOrDefault(vo => vo.VolumeOptionId == volumeOptionId);

            if ((cofee != null) && (volumeOption != null))
            {
                int? sugarOptionId = isSugarOption ? cofee.SugarOptions.FirstOrDefault()?.SugarOptionId : null;

                int? milkoptionId = isMilkOption ? cofee.MilkOptions.FirstOrDefault()?.MilkOptionId : null;
                CofeeOptions cofeeOptions = new CofeeOptions { MilkOptionId = milkoptionId, SugarOptionId = sugarOptionId };
                Recipe recipe = new Recipe
                                {
                                    CofeeId = cofee.CofeeId,
                                    VolumeSize = volumeOption.Size,
                                    IsCupCap = volumeOption.IsCupCap,
                                    Options = cofeeOptions
                                };

                GetCart().RemoveLine(recipe);
            }

            return RedirectToAction("Index");
        }

        public ViewResult Index()
        {
            return View(GetCart());
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                repository.Dispose();
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
            base.Dispose(disposing);
        }
    }
}