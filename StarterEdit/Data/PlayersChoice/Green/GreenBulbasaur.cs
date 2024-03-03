using System.Collections.Generic;
using System;
using StarterEdit;

namespace StarterEdit
{
    class GreenBulbasaur : IPlayersChoice
    {
        Dictionary<BattleName, Dictionary<DataType, long[]>> battleData = new Dictionary<BattleName, Dictionary<DataType, long[]>>();

        public GreenBulbasaur()
        {
            battleData[BattleName.Route22_1] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A56C, 0x3A56E, 0x0, 0x0, 0x0, 0x0 } },
                {DataType.Level, new long[] { 0x3A56B, 0x3A56D, 0x0, 0x0, 0x0, 0x0  } }
            };

            battleData[BattleName.CeruleanCity] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A586, 0x3A588, 0x3A58A, 0x3A58C, 0x0, 0x0 } },
                {DataType.Level, new long[] { 0x3A585, 0x3A587, 0x3A589, 0x3A58B, 0x0, 0x0 } }
            };

            battleData[BattleName.SSAnne] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A788, 0x3A78A, 0x3A78C, 0x3A78E, 0x0, 0x0 } },
                {DataType.Level, new long[] { 0x3A787, 0x3A789, 0x3A78B, 0x3A78D, 0x0, 0x0 } }
            };

            battleData[BattleName.PokemonTower] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A7AA, 0x3A7AC, 0X3A7AE, 0x3A7B0, 0x3A7B2, 0x0 } },
                {DataType.Level, new long[] { 0x3A7A9, 0x3A7AB, 0x3A7AD, 0x3A7AF, 0x3A7B1, 0x0 } }
            };

            battleData[BattleName.SilphCo] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x37ACE, 0x37AD0, 0x37AD2, 0x3A7D4, 0x3A7D6, 0x0 } },
                {DataType.Level, new long[] { 0x37ACD, 0x37ACF, 0x37AD1, 0x3A7D3, 0x3A7D5, 0x0 } }
            };

            battleData[BattleName.Route22_2] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A7F6, 0x3A7F8, 0x3A7FA, 0x3A7FC, 0x3A7FE, 0x3A800 } },
                {DataType.Level, new long[] { 0x3A7F5, 0x3A7F7, 0x3A7F9, 0x3A7FB, 0x3A7FD, 0x3A7FF } }
            };

            battleData[BattleName.IndigoPlateau] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A820, 0x3A822, 0x3A824, 0x3A826, 0x3A828, 0x3A82A } },
                {DataType.Level, new long[] { 0x3A81F, 0x3A821, 0x3A823, 0x3A825, 0x3A827, 0x3A829 } }
            };
        }

        public void setOffsetsIfGreen(bool isGreen)
        {
            if (isGreen)
            {
                // bulbasaurBattleRoute22Pkm = new long[] { 0x3A56C, 0x3A56E, 0x0, 0x0, 0x0, 0x0 }; // first battle with the rival when the players choice is bulbasuar
                // bulbasaurBattleRoute22Lvl = new long[] { 0x3A56B, 0x3A56D, 0x0, 0x0, 0x0, 0x0 }; // level array

                // bulbasaurBattleCeruleanCityPkm = new long[] { 0x3A586, 0x3A588, 0x3A58A, 0x3A58C, 0x0, 0x0 };
                // bulbasaurBattleCeruleanCityLvl = new long[] { 0x3A585, 0x3A587, 0x3A589, 0x3A58B, 0x0, 0x0 };

                // bulbasaurBattleSSAnnePkm = new long[] { 0x3A788, 0x3A78A, 0x3A78C, 0x3A78E, 0x0, 0x0 };
                // bulbasaurBattleSSAnneLvl = new long[] { 0x3A787, 0x3A789, 0x3A78B, 0x3A78D, 0x0, 0x0 };

                // bulbasaurBattlePokemonTowerPkm = new long[] { 0x3A7AA, 0x3A7AC, 0X3A7AE, 0x3A7B0, 0x3A7B2, 0x0 };
                // bulbasaurBattlePokemonTowerLvl = new long[] { 0x3A7A9, 0x3A7AB, 0x3A7AD, 0x3A7AF, 0x3A7B1, 0x0 };

                // bulbasaurBattleSilphCoPkm = new long[] { 0x37ACE, 0x37AD0, 0x37AD2, 0x3A7D4, 0x3A7D6, 0x0 };
                // bulbasaurBattleSilphCoLvl = new long[] { 0x37ACD, 0x37ACF, 0x37AD1, 0x3A7D3, 0x3A7D5, 0x0 };

                // bulbasaurBattleRoute22Pkm2 = new long[] { 0x3A7F6, 0x3A7F8, 0x3A7FA, 0x3A7FC, 0x3A7FE, 0x3A800 };
                // bulbasaurBattleRoute22Lvl2 = new long[] { 0x3A7F5, 0x3A7F7, 0x3A7F9, 0x3A7FB, 0x3A7FD, 0x3A7FF };

                // bulbasaurBattleIndigoPlateauPkm = new long[] { 0x3A820, 0x3A822, 0x3A824, 0x3A826, 0x3A828, 0x3A82A };
                // bulbasaurBattleIndigoPlateauLvl = new long[] { 0x3A81F, 0x3A821, 0x3A823, 0x3A825, 0x3A827, 0x3A829 };
            }
        }

        public Dictionary<DataType, long[]> getBattle(BattleName battleName)
        {
            return battleData[battleName];
        }
    }
}
