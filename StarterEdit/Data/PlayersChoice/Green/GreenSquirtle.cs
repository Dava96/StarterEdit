using System.Collections.Generic;
using System;
using StarterEdit;

namespace StarterEdit
{
    class GreenSquirtle : IPlayersChoice
    {
        Dictionary<BattleName, Dictionary<DataType, long[]>> battleData = new Dictionary<BattleName, Dictionary<DataType, long[]>>();

        public GreenSquirtle()
        {
            battleData[BattleName.Route22_1] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A566, 0x3A568, 0x0, 0x0, 0x0, 0x0 } },
                {DataType.Level, new long[] { 0x3A565, 0x3A567, 0x0, 0x0, 0x0, 0x0 } }
            };

            battleData[BattleName.CeruleanCity] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A57C, 0x3A57E, 0x3A580, 0x3A582, 0x0, 0x0 } },
                {DataType.Level, new long[] { 0x3A57B, 0x3A57D, 0x3A57F, 0x3A581, 0x0, 0x0 } }
            };

            battleData[BattleName.SSAnne] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A77E, 0x3A780, 0x3A782, 0x3A784, 0x0, 0x0 } },
                {DataType.Level, new long[] {0x3A77D, 0x3A77F, 0x3A781, 0x3A783, 0x0, 0x0 } }
            };

            battleData[BattleName.PokemonTower] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A79E, 0x3A7A0, 0x3A7A2, 0x3A7A4, 0x3A7A6, 0x0 } },
                {DataType.Level, new long[] { 0x3A79D, 0x3A79F, 0x3A7A1, 0x3A7A3, 0x3A7A5, 0x0 } }
            };

            battleData[BattleName.SilphCo] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A7C2, 0x3A7C4, 0x3A7C6, 0x3A7C8, 0x3A7CA, 0x0 } },
                {DataType.Level, new long[] { 0x3A7C1, 0x3A7C3, 0x3A7C5, 0x3A7C7, 0x3A7C9, 0x0 } }
            };

            battleData[BattleName.Route22_2] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A7E8, 0x3A7EA, 0x3A7EC, 0x3A7EE, 0x3A7F0, 0x3A7F2 } },
                {DataType.Level, new long[] { 0x3A7E7, 0x3A7E9, 0x3A7EB, 0x3A7ED, 0x3A7EF, 0x3A7F1 } }
            };

            battleData[BattleName.IndigoPlateau] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A812, 0x3A814, 0x3A816, 0x3A818, 0x3A81A, 0x3A81C } },
                {DataType.Level, new long[] { 0x3A811, 0x3A813, 0x3A815, 0x3A817, 0x3A819, 0x3A81B } }
            };
    }

        public void setOffsetsIfGreen(bool isGreen)
        {
            if (isGreen)
            {
                // squirtleBattleRoute22Pkm = new long[] { 0x3A566, 0x3A568, 0x0, 0x0, 0x0, 0x0 };  // first battle with the rival when the players choice is Squirtle. 
                // squirtleBattleRoute22Lvl = new long[] { 0x3A565, 0x3A567, 0x0, 0x0, 0x0, 0x0 }; // level array

                // squirtleBattleCeruleanCityPkm = new long[] { 0x3A57C, 0x3A57E, 0x3A580, 0x3A582, 0x0, 0x0 };
                // squirtleBattleCeruleanCityLvl = new long[] { 0x3A57B, 0x3A57D, 0x3A57F, 0x3A581, 0x0, 0x0 };

                // squirtleBattleSSAnnePkm = new long[] { 0x3A77E, 0x3A780, 0x3A782, 0x3A784, 0x0, 0x0 };
                // squirtleBattleSSAnneLvl = new long[] { 0x3A77D, 0x3A77F, 0x3A781, 0x3A783, 0x0, 0x0 };

                // squirtleBattlePokemonTowerPkm = new long[] { 0x3A79E, 0x3A7A0, 0x3A7A2, 0x3A7A4, 0x3A7A6, 0x0 };
                // squirtleBattlePokemonTowerLvl = new long[] { 0x3A79D, 0x3A79F, 0x3A7A1, 0x3A7A3, 0x3A7A5, 0x0 };

                // squirtleBattleSilphCoPkm = new long[] { 0x3A7C2, 0x3A7C4, 0x3A7C6, 0x3A7C8, 0x3A7CA, 0x0 };
                // squirtleBattleSilphCoLvl = new long[] { 0x3A7C1, 0x3A7C3, 0x3A7C5, 0x3A7C7, 0x3A7C9, 0x0 };

                // squirtleBattleRoute22Pkm2 = new long[] { 0x3A7E8, 0x3A7EA, 0x3A7EC, 0x3A7EE, 0x3A7F0, 0x3A7F2 }; // 2nd Route 22 battle
                // squirtleBattleRoute22Lvl2 = new long[] { 0x3A7E7, 0x3A7E9, 0x3A7EB, 0x3A7ED, 0x3A7EF, 0x3A7F1 };

                // squirtleBattleIndigoPlateauPkm = new long[] { 0x3A812, 0x3A814, 0x3A816, 0x3A818, 0x3A81A, 0x3A81C };
                // squirtleBattleIndigoPlateauLvl = new long[] { 0x3A811, 0x3A813, 0x3A815, 0x3A817, 0x3A819, 0x3A81B };
            }
        }

        public Dictionary<DataType, long[]> getBattle(BattleName battleName)
        {
            return battleData[battleName];
        }
    }
}
