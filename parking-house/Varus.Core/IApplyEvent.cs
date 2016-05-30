namespace Varus.Core
{
    /// <summary>
    /// Implemented by an aggregate once for each event type it can apply.
    /// </summary>
    /// <typeparam name="TEvent">Type of event it can apply.</typeparam>
    public interface IApplyEvent<in TEvent> where TEvent : Event
    {
        void Apply(TEvent e);
    }
}
