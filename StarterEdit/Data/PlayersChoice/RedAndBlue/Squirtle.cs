﻿using System.Collections.Generic;
using System;
using StarterEdit;

namespace StarterEdit
{
    class Squirtle : IPlayersChoice
    {
        Dictionary<BattleName, Dictionary<DataType, long[]>> battleData = new Dictionary<BattleName, Dictionary<DataType, long[]>>();
        
        public Squirtle()
        {
            battleData[BattleName.Route22_1] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3a1F5, 0x3a1F7, 0x0, 0x0, 0x0, 0x0 } },
                {DataType.Level, new long[] { 0x3a1F4, 0x3a1F6, 0x0, 0x0, 0x0, 0x0 } }
            };

            battleData[BattleName.CeruleanCity] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3a20B, 0x3a20D, 0x3a20F, 0x3a211, 0x0, 0x0 } },
                {DataType.Level, new long[] { 0x3a20A, 0x3a20C, 0x3a20E, 0x3a210, 0x0, 0x0 } }
            };

            battleData[BattleName.SSAnne] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3a40D, 0x3a40F, 0x3a411, 0x3a413, 0x0, 0x0 } },
                {DataType.Level, new long[] { 0x3a40C, 0x3a40E, 0x3a410, 0x3a412, 0x0, 0x0 } }
            };

            battleData[BattleName.PokemonTower] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3a42D, 0x3a42F, 0x3a431, 0x3a433, 0x3a435, 0x0 } },
                {DataType.Level, new long[] { 0x3a42C, 0x3a42E, 0x3a430, 0x3a432, 0x3a434, 0x0 } }
            };

            battleData[BattleName.SilphCo] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3a451, 0x3a453, 0x3a455, 0x3a457, 0x3a459, 0x0 } },
                {DataType.Level, new long[] { 0x3a450, 0x3a452, 0x3a454, 0x3a456, 0x3a458, 0x0 } }
            };

            battleData[BattleName.Route22_2] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3a477, 0x3a479, 0x3a47B, 0x3a47D, 0x3a47F, 0x3a481 } },
                {DataType.Level, new long[] { 0x3a476, 0x3a478, 0x3a47A, 0x3a47C, 0x3a47E, 0x3a480 } }
            };

            battleData[BattleName.IndigoPlateau] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3a4A1, 0x3a4A3, 0x3a4A5, 0x3a4A7, 0x3a4A9, 0x3a4AB } },
                {DataType.Level, new long[] { 0x3a4A0, 0x3a4A2, 0x3a4A4, 0x3a4A6, 0x3a4A8, 0x3a4AA } }
            };
    }
    
        public Dictionary<DataType, long[]> getBattle(BattleName battleName)
        {
            return battleData[battleName];
        }
    }
}
