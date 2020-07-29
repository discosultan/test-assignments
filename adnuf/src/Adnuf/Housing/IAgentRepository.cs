using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adnuf.Housing
{
    public interface IAgentRepository
    {
        Task<List<Agent>> ListTopAgentsByProperties(
            string city,
            params string[] extras);
    }
}
