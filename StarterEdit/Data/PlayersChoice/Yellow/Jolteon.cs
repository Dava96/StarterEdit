using System.Collections.Generic;
using System;
using StarterEdit;

namespace StarterEdit
{
    class Jolteon : IPlayersChoice
    {
        Dictionary<BattleName, Dictionary<DataType, long[]>> battleData = new Dictionary<BattleName, Dictionary<DataType, long[]>>();
        
        public Jolteon()
        {
            battleData[BattleName.PokemonTower] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A4A5, 0x3A4A7, 0x3A4A9, 0x3A4AB, 0x3A4AD, 0x0 } },
                {DataType.Level, new long[] { 0x3A4A4, 0x3A4A6, 0x3A4A8, 0x3A4AA, 0x3A4AC, 0x0 } }
            };

            battleData[BattleName.SilphCo] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A4C9, 0x3A4CB, 0x3A4CD, 0x3A4CF, 0x3A4D1, 0x0 } },
                {DataType.Level, new long[] { 0x3A4C8, 0x3A4CA, 0x3A4CC, 0x3A4CE, 0x3A4D0, 0x0 } }
            };

            battleData[BattleName.Route22_2] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A4ED, 0x3A4EF, 0x3A4F1, 0x3A4F3, 0x3A4F5, 0x3A4F7 } },
                {DataType.Level, new long[] { 0x3A4EC, 0x3A4EE, 0x3A4F0, 0x3A4F2, 0x3A4F4, 0x3A4F6 } }
            };

            battleData[BattleName.IndigoPlateau] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] {  0x3A517, 0x3A519, 0x3A51B, 0x3A51D, 0x3A51F, 0x3A521 } },
                {DataType.Level, new long[] { 0x3A516, 0x3A518, 0x3A51A, 0x3A51C, 0x3A51E, 0x3A520 } }
            };
    }

        public Dictionary<DataType, long[]> getBattle(BattleName battleName)
        {
            return battleData[battleName];
        }
    }
}
