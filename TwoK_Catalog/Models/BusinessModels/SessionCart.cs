using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using TwoK_Catalog.Infrastructure;

namespace TwoK_Catalog.Models.BusinessModels
{
    public class SessionCart : Cart
    {
        [JsonIgnore]
        public ISession Session { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionCart cart = session.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }
        public override void AddItem(Product product, int quantity = 1)
        {
            base.AddItem(product, quantity);
            Session.SetJson("Cart", this);
        }
        public override void RemoveItem(Product product, int quantity = 1)
        {
            base.RemoveItem(product, quantity);
            Session.SetJson("Cart", this);
        }
        public override void UpdateCart()
        {
            Session.SetJson("Cart", this);
        }
        public override void Clear()
        {
            base.Clear();
            Session.SetJson("Cart", this);
        }
    }
}
