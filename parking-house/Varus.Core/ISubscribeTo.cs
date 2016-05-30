namespace Varus.Core
{
    /// <summary>
    /// Implemented by anything that wishes to subscribe to an event emitted by
    /// an aggregate and successfully stored.
    /// </summary>
    /// <typeparam name="TEvent">Type of event to subscribe to.</typeparam>
    public interface ISubscribeTo<in TEvent> where TEvent : Event
    {
        void Handle(TEvent e);
    }
}
