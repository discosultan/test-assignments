using Akka.Actor;
using Gradilium.ShoppingBasket.Utils;

namespace Gradilium.ShoppingBasket.WebAPI.Utils
{
    // Named IActorRef instance for DI.
    public class NamedActorRef<T> where T : ActorBase
    {
        public NamedActorRef(ActorSystem actorSystem, Props props = null)
        {
            props = props ?? Props.Create<T>();
            Actor = actorSystem.ActorOf(props, typeof(T).Name.StripActor());
        }

        public IActorRef Actor { get; }
    }
}
