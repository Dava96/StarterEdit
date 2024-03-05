using System.Collections.Generic;
using System;
using StarterEdit;

namespace StarterEdit
{
    class Flareon : IPlayersChoice
    {
        Dictionary<BattleName, Dictionary<DataType, long[]>> battleData = new Dictionary<BattleName, Dictionary<DataType, long[]>>();
        
        public Flareon()
        {
            battleData[BattleName.PokemonTower] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A4B1, 0x3A4B3, 0x3A4B5, 0x3A4B7, 0x3A4B9, 0x0 } },
                {DataType.Level, new long[] { 0x3A4B0, 0x3A4B2, 0x3A4B4, 0x3A4B6, 0x3A4B8, 0x0 } }
            };

            battleData[BattleName.SilphCo] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A4D5, 0x3A4D7, 0x3A4D9, 0x3A4DB, 0x3A4DD, 0x0 } },
                {DataType.Level, new long[] { 0x3A4D4, 0x3A4D6, 0x3A4D8, 0x3A4DA, 0x3A4DC, 0x0 } }
            };

            battleData[BattleName.Route22_2] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A4FB, 0x3A4FD, 0x3A4FF, 0x3A501, 0x3A503, 0x3A505 } },
                {DataType.Level, new long[] { 0x3A4FA, 0x3A4FC, 0x3A4FE, 0x3A500, 0x3A502, 0x3A504 } }
            };

            battleData[BattleName.IndigoPlateau] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] {  0x3A525, 0x3A527, 0x3A529, 0x3A52B, 0x3A52D, 0x3A52F } },
                {DataType.Level, new long[] { 0x3A524, 0x3A526, 0x3A528, 0x3A52A, 0x3A52C, 0x3A52E } }
            };
    }

        public Dictionary<DataType, long[]> getBattle(BattleName battleName)
        {
            return battleData[battleName];
        }
    }
}
