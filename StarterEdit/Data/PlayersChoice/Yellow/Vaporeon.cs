using System.Collections.Generic;
using System;
using StarterEdit;

namespace StarterEdit
{
    class Vaporeon : IPlayersChoice
    {
        Dictionary<BattleName, Dictionary<DataType, long[]>> battleData = new Dictionary<BattleName, Dictionary<DataType, long[]>>();
        
        public Vaporeon()
        {
            battleData[BattleName.PokemonTower] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A4BD, 0x3A4BF, 0x3A4C1, 0x3A4C3, 0x3A4C5, 0x0 } },
                {DataType.Level, new long[] { 0x3A4BC, 0x3A4BE, 0x3A4C0, 0x3A4C2, 0x3A4C4, 0x0 } }
            };

            battleData[BattleName.SilphCo] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A4E1, 0x3A4E3, 0x3A4E5, 0x3A4E7, 0x3A4E9, 0x0 } },
                {DataType.Level, new long[] { 0x3A4E0, 0x3A4E2, 0x3A4E4, 0x3A4E6, 0x3A4E8, 0x0 } }
            };

            battleData[BattleName.Route22_2] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A509, 0x3A50B, 0x3A50D, 0x3A50F, 0x3A511, 0x3A513 } },
                {DataType.Level, new long[] { 0x3A508, 0x3A50A, 0x3A50C, 0x3A50E, 0x3A510, 0x3A512 } }
            };

            battleData[BattleName.IndigoPlateau] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] {  0x3A533, 0x3A535, 0x3A537, 0x3A539, 0x3A53B, 0x3A53D } },
                {DataType.Level, new long[] { 0x3A532, 0x3A534, 0x3A536, 0x3A538, 0x3A53A, 0x3A53C } }
            };
    }

        public Dictionary<DataType, long[]> getBattle(BattleName battleName)
        {
            return battleData[battleName];
        }
    }
}
