using System;
using System.Collections.Generic;
using System.Text;

namespace StarterEdit
{
    class PokemonYellowOffsets
    {

        String[] yellowIdentifier = new string[] { "97", "04", "7C" }; // 3 Bytes used to identify the rom version

        public long[] yellowPikachuOffset = new long[] { 0x18F10, 0x18F1E }; // oak catches, level
        public long[] yellowFirstBattle = new long[] { 0x1CB41, 0x1CB61, 0x1CB66 }; // POKEMON, LEVEL, POKEMON, - USERS POKEMON
        public long[] yellowFirstBattleUserLvl = new long[] { 0x1CB61 }; // POKEMON, LEVEL, POKEMON, - USERS POKEMON

        public long[] yellowFirstBattleRival = new long[] { 0x3A28A }; // pokemon
        public long[] yellowFirstBattleRivalLvl = new long[] { 0x3A289 }; // level

        public long[] yellowRivalBattleRoute22PKM = new long[] { 0x3A28E, 0x3A290, 0x0, 0x0, 0x0, 0x0 }; // ROUTE 22
        public long[] yellowRivalBattleRoute22Lvl = new long[] { 0x3A28D, 0x3A28F, 0x0, 0x0, 0x0, 0x0 };

        public long[] yellowRivalBattleCeruleanCityPKM = new long[] { 0x3A294, 0x3A296, 0x3A298, 0x3A29A, 0x0, 0x0 }; // CERULEAN CITY
        public long[] yellowRivalBattleCeruleanCityLvl = new long[] { 0x3A293, 0x3A295, 0x3A297, 0x3A299, 0x0, 0x0 };

        public long[] yellowRivalBattleSSAnnePKM = new long[] {0x3A49B, 0x3A49D, 0x3A49F, 0x3A4A1, 0x0, 0x0 }; // S.S Anne
        public long[] yellowRivalBattleSSAnneLvl = new long[] { 0x3A49A, 0x3A49C, 0x3A49E, 0x3A4A0, 0x0, 0x0};

        public long[] yellowRivalBattlePokemonTowerC1PKM = new long[] { 0x3A4A5, 0x3A4A7, 0x3A4A9, 0x3A4AB, 0x3A4AD, 0x0 }; // Pokemon tower case 1 // fearow shellder vullpix sandshrew eveee // Jolteon
        public long[] yellowRivalBattlePokemonTowerC1Lvl = new long[] { 0x3A4A4, 0x3A4A6, 0x3A4A8, 0x3A4AA, 0x3A4AC, 0x0 };

        public long[] yellowRivalBattlePokemonTowerC2PKM = new long[] { 0x3A4B1, 0x3A4B3, 0x3A4B5, 0x3A4B7, 0x3A4B9, 0x0}; // Pokemon tower case 2 sandshrew  0x3A41B Flareon
        public long[] yellowRivalBattlePokemonTowerC2Lvl = new long[] { 0x3A4B0, 0x3A4B2, 0x3A4B4, 0x3A4B6, 0x3A4B8, 0x0}; 

        public long[] yellowRivalBattlePokemonTowerC3PKM = new long[] { 0x3A4BD, 0x3A4BF, 0x3A4C1, 0x3A4C3, 0x3A4C5, 0x0}; // Pokemon tower Case 3 // Vaporeon
        public long[] yellowRivalBattlePokemonTowerC3Lvl = new long[] { 0x3A4BC, 0x3A4BE, 0x3A4C0, 0x3A4C2, 0x3A4C4, 0x0};

        public long[] yellowRivalBattleSilphCoC1PKM = new long[] { 0x3A4C9, 0x3A4CB, 0x3A4CD, 0x3A4CF, 0x3A4D1, 0x0}; // Silph Co. Case 1 Jolteon
        public long[] yellowRivalBattleSilphCoC1Lvl = new long[] { 0x3A4C8, 0x3A4CA, 0x3A4CC, 0x3A4CE, 0x3A4D0, 0x0};

        public long[] yellowRivalBattleSilphCoC2PKM = new long[] { 0x3A4D5, 0x3A4D7, 0x3A4D9, 0x3A4DB, 0x3A4DD, 0x0}; // Silph Co. Case 2 Flareon
        public long[] yellowRivalBattleSilphCoC2Lvl = new long[] { 0x3A4D4, 0x3A4D6, 0x3A4D8, 0x3A4DA, 0x3A4DC, 0x0};

        public long[] yellowRivalBattleSilphCoC3PKM = new long[] { 0x3A4E1, 0x3A4E3, 0x3A4E5, 0x3A4E7, 0x3A4E9, 0x0 }; //Silph Co. Case 3 Vaporeon
        public long[] yellowRivalBattleSilphCoC3Lvl  = new long[] { 0x3A4E0, 0x3A4E2, 0x3A4E4, 0x3A4E6, 0x3A4E8, 0x0};

        public long[] yellowRivalBattleRoute22C1PKM = new long[] { 0x3A4ED, 0x3A4EF, 0x3A4F1, 0x3A4F3, 0x3A4F5, 0x3A4F7}; // ROUTE 22 (2) Case 1 Jolteon
        public long[] yellowRivalBattleRoute22C1Lvl = new long[] { 0x3A4EC, 0x3A4EE, 0x3A4F0, 0x3A4F2, 0x3A4F4, 0x3A4F6 };

        public long[] yellowRivalBattleRoute22C2PKM = new long[] { 0x3A4FB, 0x3A4FD, 0x3A4FF, 0x3A501, 0x3A503, 0x3A505}; // ROUTE 22 (2) Case 2 Flareon
        public long[] yellowRivalBattleRoute22C2Lvl = new long[] { 0x3A4FA, 0x3A4FC, 0x3A4FE, 0x3A500, 0x3A502, 0x3A504 };

        public long[] yellowRivalBattleRoute22C3PKM = new long[] { 0x3A509, 0x3A50B, 0x3A50D, 0x3A50F, 0x3A511, 0x3A513 }; // ROUTE 22 (2) Case 3 Vaporeon
        public long[] yellowRivalBattleRoute22C3Lvl = new long[] { 0x3A508, 0x3A50A, 0x3A50C, 0x3A50E, 0x3A510, 0x3A512};

        public long[] yellowRivalBattleIndigoPlateauC1PKM = new long[] { 0x3A517, 0x3A519, 0x3A51B, 0x3A51D, 0x3A51F, 0x3A521 }; // indingo plateau Case 1 Jolteon
        public long[] yellowRivalBattleIndigoPlateauC1Lvl = new long[] { 0x3A516, 0x3A518, 0x3A51A, 0x3A51C, 0x3A51E, 0x3A520};

        public long[] yellowRivalBattleIndigoPlateauC2PKM = new long[] {0x3A525, 0x3A527, 0x3A529, 0x3A52B, 0x3A52D, 0x3A52F }; // indingo plateau Case 2 Flareon
        public long[] yellowRivalBattleIndigoPlateauC2Lvl = new long[] { 0x3A524, 0x3A526, 0x3A528, 0x3A52A, 0x3A52C, 0x3A52E};

        public long[] yellowRivalBattleIndigoPlateauC3PKM = new long[] { 0x3A533, 0x3A535, 0x3A537, 0x3A539, 0x3A53B, 0x3A53D}; // indingo plateau Case 3 Vaporeon
        public long[] yellowRivalBattleIndigoPlateauC3Lvl = new long[] { 0x3A532, 0x3A534, 0x3A536, 0x3A538, 0x3A53A, 0x3A53C};

    }




}
