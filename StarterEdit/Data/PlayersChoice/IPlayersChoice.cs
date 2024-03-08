using System.Collections.Generic;

namespace StarterEdit
{
    public interface IPlayersChoice
    {
        public Dictionary<DataType, long[]> getBattle(BattleName battleName);
    }
}
