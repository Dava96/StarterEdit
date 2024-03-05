using System.Collections.Generic;
using System;
using StarterEdit;

namespace StarterEdit
{
    class GreenCharmander : IPlayersChoice
    {
        Dictionary<BattleName, Dictionary<DataType, long[]>> battleData = new Dictionary<BattleName, Dictionary<DataType, long[]>>();

        public GreenCharmander()
        {
            battleData[BattleName.Route22_1] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A560, 0x3A562, 0x0, 0x0, 0x0, 0x0 } },
                {DataType.Level, new long[] { 0x3A55F, 0x3A561, 0x0, 0x0, 0x0, 0x0  } }
            };

            battleData[BattleName.CeruleanCity] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A572, 0x3A574, 0x3A576, 0x3A578, 0x0, 0x0 } },
                {DataType.Level, new long[] { 0x3A571, 0x3A573, 0x3A575, 0x3A577, 0x0, 0x0 } }
            };

            battleData[BattleName.SSAnne] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A774, 0x3A776, 0x3A778, 0x3A77A, 0x0, 0x0 } },
                {DataType.Level, new long[] { 0x3A773, 0x3A775, 0x3A777, 0x3A779, 0x0, 0x0 } }
            };

            battleData[BattleName.PokemonTower] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A792, 0X3A794, 0x3A796, 0x3A798, 0x3A78A, 0x0 } },
                {DataType.Level, new long[] { 0x3A791, 0x3A793, 0x3A795, 0x3A797, 0x3A799, 0x0 } }
            };

            battleData[BattleName.SilphCo] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A7B6, 0x3A7B8, 0x3A7BA, 0x3A7BC, 0x3A7BE, 0x0 } },
                {DataType.Level, new long[] { 0x3A7B5, 0x3A7B7, 0x3A7B9, 0x3A7BB, 0x3A7BD, 0x0 } }
            };

            battleData[BattleName.Route22_2] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A7DA, 0x3A7DC, 0x3A7DE, 0x37AE0, 0x37AE2, 0x3A7E4 } },
                {DataType.Level, new long[] { 0x3A7D9, 0x3A7DB, 0x3A7DD, 0x3A7DF, 0x3A7E1, 0x3A7E3 } }
            };

            battleData[BattleName.IndigoPlateau] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A804, 0x3A806, 0x3A808, 0x3A80A, 0x3A80C, 0x3A80E } },
                {DataType.Level, new long[] { 0x3A803, 0x3A805, 0x3A807, 0x3A809, 0x3A80B, 0x3A80D } }
            };
        }

        public Dictionary<DataType, long[]> getBattle(BattleName battleName)
        {
            return battleData[battleName];
        }
    }
}
