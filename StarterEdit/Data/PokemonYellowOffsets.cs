using System;
using System.Collections.Generic;
using System.Text;

namespace StarterEdit.Data
{
    class PokemonYellowOffsets
    {

        String[] yellowIdentifier = new string[] { "97", "04", "7C" }; // 3 Bytes used to identify the rom version

        public long[] yellowPikachuOffset = new long[] { 0x18F10, 0x18F1E }; // oak catches, level
        public long[] yellowFirstBattle = new long[] { 0x1CB41, 0x1CB61, 0x1CB66 }; // POKEMON, LEVEL, POKEMON, - USERS POKEMON
        public long[] yellowFirstBattleRival = new long[] { 0x3A289, 0x3A28A }; // level, pokemon

        public long[] yellowRivalBattle1 = new long[] { 0x3A28E, 0x3A290 }; // ROUTE 22
        public long[] yellowRivalBattle1Lvls = new long[] { 0x3A28D, 0x3A28F };

        public long[] yellowRivalBattle2 = new long[] { 0x3A294, 0x3A296, 0x3A298, 0x3A29A }; // CERULEAN CITY
        public long[] yellowRivalBattle2Lvls = new long[] { 0x3A293, 0x3A295, 0x3A297, 0x3A299 };

        public long[] yellowRivalBattle3 = new long[] {0x3A49B, 0x3A49D, 0x3A49F, 0x3A4A1 }; // S.S Anne
        public long[] yellowRivalBattle3Lvls = new long[] { 0x3A49A, 0x3A49C, 0x3A49E, 0x3A4A0};

        public long[] yellowRivalBattle4 = new long[] { 0x3A4A5, 0x3A4A7, 0x3A4A9, 0x3A4AB, 0x3A4AD}; // Pokemon tower case 1
        public long[] yellowRivalBattle4Lvls = new long[] {0x3A4A4, 0x3A4A6, 0x3A4A8, 0x3A4AA, 0x3A4AC};

        public long[] yellowRivalBattle5 = new long[] { 0x3A41B, 0x3A4B3,  0x3A4B5, 0x3A4B7, 0x3A4B9}; // Pokemon tower case 2
        public long[] yellowRivalBattle5Lvls = new long[] { 0x3A4B0, 0x3A4B2, 0x3A4B4, 0x3A4B6, 0x3A4B8};

        public long[] yellowRivalBattle6 = new long[] { 0x3A4BD, 0x3A4BF, 0x3A4C1, 0x3A4C3, 0x3A4C5}; // Pokemon tower Case 3
        public long[] yellowRivalBattle6Lvls = new long[] { 0x3A4BC, 0x3A4BE, 0x3A4C0, 0x3A4C2, 0x3A4C4};

        public long[] yellowRivalBattle7 = new long[] { 0x3A4C9, 0x3A4CB, 0x3A4CD, 0x3A4CF, 0x3A4D1}; // Silph Co. Case 1
        public long[] yellowRivalBattle7Lvls = new long[] { 0x3A4C8, 0x3A4CA, 0x3A4CC, 0x3A4CE, 0x3A4D0};
        
    }




}
