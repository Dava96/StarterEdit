

namespace StarterEdit
{
    class PlayersChoiceCharmander
    {
        public long[] charmanderBattleRoute22Pkm;
        public long[] charmanderBattleRoute22Lvl;

        public long[] charmanderBattleCeruleanCityPkm;
        public long[] charmanderBattleCeruleanCityLvl;

        public long[] charmanderBattleSSAnnePkm;
        public long[] charmanderBattleSSAnneLvl;

        public long[] charmanderBattlePokemonTowerPkm;
        public long[] charmanderBattlePokemonTowerLvl;

        public long[] charmanderBattleSilphCoPkm;
        public long[] charmanderBattleSilphCoLvl;

        public long[] charmanderBattleRoute22Pkm2;
        public long[] charmanderBattleRoute22Lvl2;

        public long[] charmanderBattleIndigoPlateauPkm;
        public long[] charmanderBattleIndigoPlateauLvl;


        public PlayersChoiceCharmander()
        {
            charmanderBattleRoute22Pkm = new long[] { 0x3a1EF, 0x3a1F1, 0x0, 0x0, 0x0, 0x0 }; // first battle with the rival when the players choice is charmander. 
            charmanderBattleRoute22Lvl = new long[] { 0x3a1EE, 0x3a1F0, 0x0, 0x0, 0x0, 0x0 }; // level array

            charmanderBattleCeruleanCityPkm = new long[] { 0x3a201, 0x3a203, 0x3a205, 0x3a207, 0x0, 0x0 };
            charmanderBattleCeruleanCityLvl = new long[] { 0x3a200, 0x3a202, 0x3a204, 0x3a206, 0x0, 0x0 };

            charmanderBattleSSAnnePkm = new long[] { 0x3a403, 0x3a405, 0x3a407, 0x3a409, 0x0, 0x0 };
            charmanderBattleSSAnneLvl = new long[] { 0x3a402, 0x3a404, 0x3a406, 0x3a408, 0x0, 0x0 };

            charmanderBattlePokemonTowerPkm = new long[] { 0x3a421, 0x3a423, 0x3a425, 0x3a427, 0x3a429, 0x0 };
            charmanderBattlePokemonTowerLvl = new long[] { 0x3a420, 0x3a422, 0x3a424, 0x3a426, 0x3a428, 0x0 };

            charmanderBattleSilphCoPkm = new long[] { 0x3a445, 0x3a447, 0x3a449, 0x3a44B, 0x3a44D, 0x0 };
            charmanderBattleSilphCoLvl = new long[] { 0x3a444, 0x3a446, 0x3a448, 0x3a44A, 0x3a44C, 0x0 };

            charmanderBattleRoute22Pkm2 = new long[] { 0x3a469, 0x3a46B, 0x3a46D, 0x3a46F, 0x3a44D, 0x3a471 };
            charmanderBattleRoute22Lvl2 = new long[] { 0x3a468, 0x3a46A, 0x3a46C, 0x3a46E, 0x3a44C, 0x3a470 };

            charmanderBattleIndigoPlateauPkm = new long[] { 0x3a493, 0x3a495, 0x3a497, 0x3a499, 0x3a49B, 0x3a49D };
            charmanderBattleIndigoPlateauLvl = new long[] { 0x3a492, 0x3a494, 0x3a496, 0x3a498, 0x3a49A, 0x3a49C };
        }

        public void setOffsetsIfGreen(bool isGreen)
        {
            if (isGreen)
            {
                charmanderBattleRoute22Pkm = new long[] { 0x3A560, 0x3A562, 0x0, 0x0, 0x0, 0x0 }; // first battle with the rival when the players choice is charmander. 
                charmanderBattleRoute22Lvl = new long[] { 0x3A55F, 0x3A561, 0x0, 0x0, 0x0, 0x0 }; // level array

                charmanderBattleCeruleanCityPkm = new long[] { 0x3A572, 0x3A574, 0x3A576, 0x3A578, 0x0, 0x0 };
                charmanderBattleCeruleanCityLvl = new long[] { 0x3A571, 0x3A573, 0x3A575, 0x3A577, 0x0, 0x0 };

                charmanderBattleSSAnnePkm = new long[] { 0x3A774, 0x3A776, 0x3A778, 0x3A77A, 0x0, 0x0 };
                charmanderBattleSSAnneLvl = new long[] { 0x3A773, 0x3A775, 0x3A777, 0x3A779, 0x0, 0x0 };

                charmanderBattlePokemonTowerPkm = new long[] { 0x3A792, 0X3A794, 0x3A796, 0x3A798, 0x3A78A, 0x0 };
                charmanderBattlePokemonTowerLvl = new long[] { 0x3A791, 0x3A793, 0x3A795, 0x3A797, 0x3A799, 0x0 };

                charmanderBattleSilphCoPkm = new long[] { 0x3A7B6, 0x3A7B8, 0x3A7BA, 0x3A7BC, 0x3A7BE, 0x0 };
                charmanderBattleSilphCoLvl = new long[] { 0x3A7B5, 0x3A7B7, 0x3A7B9, 0x3A7BB, 0x3A7BD, 0x0 };

                charmanderBattleRoute22Pkm2 = new long[] { 0x3A7DA, 0x3A7DC, 0x3A7DE, 0x37AE0, 0x37AE2, 0x3A7E4 };
                charmanderBattleRoute22Lvl2 = new long[] { 0x3A7D9, 0x3A7DB, 0x3A7DD, 0x3A7DF, 0x3A7E1, 0x3A7E3 };

                charmanderBattleIndigoPlateauPkm = new long[] { 0x3A804, 0x3A806, 0x3A808, 0x3A80A, 0x3A80C, 0x3A80E };
                charmanderBattleIndigoPlateauLvl = new long[] { 0x3A803, 0x3A805, 0x3A807, 0x3A809, 0x3A80B, 0x3A80D };
            }
        }
    }
}
