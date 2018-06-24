using Akka.Actor;
using Gradilium.ShoppingBasket.Baskets;
using Gradilium.ShoppingBasket.WebAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Gradilium.ShoppingBasket.Baskets.BasketActor;

namespace Gradilium.ShoppingBasket.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        readonly IActorRef _basketsManager;

        public BasketController(NamedActorRef<BasketsManagerActor> basketsManager)
        {
            _basketsManager = basketsManager.Actor;
        }

        /// <summary>
        /// Get the contents of a user's shopping basket.
        /// </summary>
        [HttpGet("{userId}")]
        public Task<Basket> Get(Guid userId)
        {
            return _basketsManager.Ask<Basket>(new GetBasket { UserId = userId });
        }

        /// <summary>
        /// Add an item to user's shopping basket.
        /// </summary>
        [HttpPost("{userId}")]
        public Task Post(Guid userId, [FromBody] Item item)
        {
            return _basketsManager.Ask(new AddToBasket
            {
                UserId = userId,
                Item = item
            });
        }

        /// <summary>
        /// Remove an item from user's shopping basket.
        /// </summary>
        [HttpDelete("{userId}")]
        public Task Delete(Guid userId, [FromBody] Item item)
        {
            return _basketsManager.Ask(new RemoveFromBasket
            {
                UserId = userId,
                Item = item
            });
        }
    }
}
