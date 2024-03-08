using System.Collections.Generic;
using StarterEdit;

namespace StarterEdit
{
    abstract class Game
    {
        public static readonly long[] RomName = { 0x134, 0x135, 0x136, 0x137, 0x138, 0x139, 0x13A, 0x13B, 0x13C, 0x13D, 0x13E, 0x13F, 0x140, 0x141, 0x142, 0x143, 0x144 };
        public static readonly long[] FileIdentifierOffsets = { 0x14D, 0x14E, 0x14F };
        public abstract Version GetVersion();
        public abstract string GetFileIdentifier();
        public abstract Dictionary<Choice, long[]> GetStarterOffsets();
        public abstract Dictionary<Choice, long[]> GetRivalsChoice();
        public abstract Dictionary<DataType, long[]> GetFirstRivalBattle();
        public abstract long GetAutoScroll();

        public abstract Dictionary<DataType, long[]> GetBattlesForPlayersChoice(IPlayersChoice playersChoice, BattleName battleName);
        public abstract IPlayersChoice GetPlayersChoice(Choice choice);
    }
}