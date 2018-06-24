using Akka.Actor;

namespace Gradilium.ShoppingBasket.Baskets
{
    public class BasketsManagerActor : UntypedActor
    {
        readonly IActorRef _productsManager;

        public BasketsManagerActor(IActorRef productsManager)
        {
            _productsManager = productsManager;
        }

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case BasketActor.UserCommand cmd:
                    var userIdStr = cmd.UserId.ToString();
                    var child = Context.Child(userIdStr);
                    if (child is Nobody)
                        child = Context.ActorOf(Props.Create<BasketActor>(_productsManager), userIdStr);
                    child.Forward(cmd);
                    break;
            }
        }
    }
}
