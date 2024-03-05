using System.Collections.Generic;
using System;
using StarterEdit;

namespace StarterEdit
{
    class Charmander : IPlayersChoice
    {
        Dictionary<BattleName, Dictionary<DataType, long[]>> battleData = new Dictionary<BattleName, Dictionary<DataType, long[]>>();

        public Charmander()
        {
            battleData[BattleName.Route22_1] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3a1EF, 0x3a1F1, 0x0, 0x0, 0x0, 0x0 } },
                {DataType.Level, new long[] { 0x3a1EE, 0x3a1F0, 0x0, 0x0, 0x0, 0x0 } }
            };

            battleData[BattleName.CeruleanCity] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3a201, 0x3a203, 0x3a205, 0x3a207, 0x0, 0x0 } },
                {DataType.Level, new long[] { 0x3a200, 0x3a202, 0x3a204, 0x3a206, 0x0, 0x0 } }
            };

            battleData[BattleName.SSAnne] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3a403, 0x3a405, 0x3a407, 0x3a409, 0x0, 0x0 } },
                {DataType.Level, new long[] { 0x3a402, 0x3a404, 0x3a406, 0x3a408, 0x0, 0x0 } }
            };

            battleData[BattleName.PokemonTower] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3a421, 0x3a423, 0x3a425, 0x3a427, 0x3a429, 0x0 } },
                {DataType.Level, new long[] { 0x3a420, 0x3a422, 0x3a424, 0x3a426, 0x3a428, 0x0 } }
            };

            battleData[BattleName.SilphCo] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3a445, 0x3a447, 0x3a449, 0x3a44B, 0x3a44D, 0x0 } },
                {DataType.Level, new long[] { 0x3a444, 0x3a446, 0x3a448, 0x3a44A, 0x3a44C, 0x0 } }
            };

            battleData[BattleName.Route22_2] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] {0x3a469, 0x3a46B, 0x3a46D, 0x3a46F, 0x3a44D, 0x3a471 } },
                {DataType.Level, new long[] { 0x3a468, 0x3a46A, 0x3a46C, 0x3a46E, 0x3a44C, 0x3a470 } }
            };

            battleData[BattleName.IndigoPlateau] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3a493, 0x3a495, 0x3a497, 0x3a499, 0x3a49B, 0x3a49D } },
                {DataType.Level, new long[] { 0x3a492, 0x3a494, 0x3a496, 0x3a498, 0x3a49A, 0x3a49C } }
            };
        }

         public Dictionary<DataType, long[]> getBattle(BattleName battleName)
        {
            return battleData[battleName];
        }
    }
}
