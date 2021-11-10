using System;
using System.Collections.Generic;
using System.Text;

namespace StarterEdit
{
    class PlayersChoiceSquirtle
    {
        public long[] squirtleBattleRoute22Pkm;
        public long[] squirtleBattleRoute22Lvl;

        public long[] squirtleBattleCeruleanCityPkm;
        public long[] squirtleBattleCeruleanCityLvl;

        public long[] squirtleBattleSSAnnePkm;
        public long[] squirtleBattleSSAnneLvl;

        public long[] squirtleBattlePokemonTowerPkm;
        public long[] squirtleBattlePokemonTowerLvl;

        public long[] squirtleBattleSilphCoPkm;
        public long[] squirtleBattleSilphCoLvl;

        public long[] squirtleBattleRoute22Pkm2;
        public long[] squirtleBattleRoute22Lvl2;

        public long[] squirtleBattleIndigoPlateauPkm;
        public long[] squirtleBattleIndigoPlateauLvl;

        public PlayersChoiceSquirtle()
        {
        squirtleBattleRoute22Pkm = new long[] { 0x3a1F5, 0x3a1F7, 0x0, 0x0, 0x0, 0x0 };  // first battle with the rival when the players choice is Squirtle. 
        squirtleBattleRoute22Lvl = new long[] { 0x3a1F4, 0x3a1F6, 0x0, 0x0, 0x0, 0x0 }; // level array

        squirtleBattleCeruleanCityPkm = new long[] { 0x3a20B, 0x3a20D, 0x3a20F, 0x3a211, 0x0, 0x0 };
        squirtleBattleCeruleanCityLvl = new long[] { 0x3a20A, 0x3a20C, 0x3a20E, 0x3a210, 0x0, 0x0 };

        squirtleBattleSSAnnePkm = new long[] { 0x3a40D, 0x3a40F, 0x3a411, 0x3a413, 0x0, 0x0 };
        squirtleBattleSSAnneLvl = new long[] { 0x3a40C, 0x3a40E, 0x3a410, 0x3a412, 0x0, 0x0 };

        squirtleBattlePokemonTowerPkm = new long[] { 0x3a42D, 0x3a42F, 0x3a431, 0x3a433, 0x3a435, 0x0 };
        squirtleBattlePokemonTowerLvl = new long[] { 0x3a42C, 0x3a42E, 0x3a430, 0x3a432, 0x3a434, 0x0 };

        squirtleBattleSilphCoPkm = new long[] { 0x3a451, 0x3a453, 0x3a455, 0x3a457, 0x3a459, 0x0 };
        squirtleBattleSilphCoLvl = new long[] { 0x3a450, 0x3a452, 0x3a454, 0x3a456, 0x3a458, 0x0 };

        squirtleBattleRoute22Pkm2 = new long[] { 0x3a477, 0x3a479, 0x3a47B, 0x3a47D, 0x3a47F, 0x3a481 }; // 2nd Route 22 battle
        squirtleBattleRoute22Lvl2 = new long[] { 0x3a476, 0x3a478, 0x3a47A, 0x3a47C, 0x3a47E, 0x3a480 };

        squirtleBattleIndigoPlateauPkm = new long[] { 0x3a4A1, 0x3a4A3, 0x3a4A5, 0x3a4A7, 0x3a4A9, 0x3a4AB };
        squirtleBattleIndigoPlateauLvl = new long[] { 0x3a4A0, 0x3a4A2, 0x3a4A4, 0x3a4A6, 0x3a4A8, 0x3a4AA };
    }

        public void setOffsetsIfGreen(bool isGreen)
        {
            if (isGreen)
            {
                squirtleBattleRoute22Pkm = new long[] { 0x3A566, 0x3A568, 0x0, 0x0, 0x0, 0x0 };  // first battle with the rival when the players choice is Squirtle. 
                squirtleBattleRoute22Lvl = new long[] { 0x3A565, 0x3A567, 0x0, 0x0, 0x0, 0x0 }; // level array

                squirtleBattleCeruleanCityPkm = new long[] { 0x3A57C, 0x3A57E, 0x3A580, 0x3A582, 0x0, 0x0 };
                squirtleBattleCeruleanCityLvl = new long[] { 0x3A57B, 0x3A57D, 0x3A57F, 0x3A581, 0x0, 0x0 };

                squirtleBattleSSAnnePkm = new long[] { 0x3A77E, 0x3A780, 0x3A782, 0x3A784, 0x0, 0x0 };
                squirtleBattleSSAnneLvl = new long[] { 0x3A77D, 0x3A77F, 0x3A781, 0x3A783, 0x0, 0x0 };

                squirtleBattlePokemonTowerPkm = new long[] { 0x3A79E, 0x3A7A0, 0x3A7A2, 0x3A7A4, 0x3A7A6, 0x0 };
                squirtleBattlePokemonTowerLvl = new long[] { 0x3A79D, 0x3A79F, 0x3A7A1, 0x3A7A3, 0x3A7A5, 0x0 };

                squirtleBattleSilphCoPkm = new long[] { 0x3A7C2, 0x3A7C4, 0x3A7C6, 0x3A7C8, 0x3A7CA, 0x0 };
                squirtleBattleSilphCoLvl = new long[] { 0x3A7C1, 0x3A7C3, 0x3A7C5, 0x3A7C7, 0x3A7C9, 0x0 };

                squirtleBattleRoute22Pkm2 = new long[] { 0x3A7E8, 0x3A7EA, 0x3A7EC, 0x3A7EE, 0x3A7F0, 0x3A7F2 }; // 2nd Route 22 battle
                squirtleBattleRoute22Lvl2 = new long[] { 0x3A7E7, 0x3A7E9, 0x3A7EB, 0x3A7ED, 0x3A7EF, 0x3A7F1 };

                squirtleBattleIndigoPlateauPkm = new long[] { 0x3A812, 0x3A814, 0x3A816, 0x3A818, 0x3A81A, 0x3A81C };
                squirtleBattleIndigoPlateauLvl = new long[] { 0x3A811, 0x3A813, 0x3A815, 0x3A817, 0x3A819, 0x3A81B };
            }
        }

    }
}
