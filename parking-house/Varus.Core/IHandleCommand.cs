using System.Collections.Generic;

namespace Varus.Core
{
    public interface IHandleCommand<in TCommand> where TCommand : Command
    {
        IEnumerable<Event> Handle(TCommand command);
    }
}
