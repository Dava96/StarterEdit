using System.Collections.Generic;
using StarterEdit;

namespace StarterEdit
{
    abstract class Game
    {
        public abstract Version GetVersion();
        public abstract string GetFileIdentifier();
        public abstract Dictionary<Choice, long[]> GetStarterOffsets();
        public abstract Dictionary<Choice, long[]> GetRivalsChoice();
        public abstract Dictionary<DataType, long[]> GetFirstRivalBattle();
        public abstract long GetAutoScroll();

        public abstract Dictionary<DataType, long[]> GetBattlesForPlayersChoice(IPlayersChoice playersChoice, BattleName battleName);
    }
}