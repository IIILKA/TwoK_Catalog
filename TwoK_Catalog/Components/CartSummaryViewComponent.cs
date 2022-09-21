﻿using Microsoft.AspNetCore.Mvc;
using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart cart;
        public CartSummaryViewComponent(Cart cartService)
        {
            cart = cartService;
        }
        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
