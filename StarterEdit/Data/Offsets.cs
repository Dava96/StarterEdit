﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StarterEdit
{
    class Offsets
    {
        // Pokemon Red Offsets
        public long[] squirtleOffsets = new long[] { 0x1D104, 0x1D11F, 0x24BA5, 0x26FBC, 0x19591, 0x1CC88, 0x1CDC8, 0x50FAF, 0x510D9, 0x51CAF, 0x6060E, 0x61450, 0x75F9E};
        public long[] bulbasuarOffsets = new long[] { 0x1D115, 0x1D130, 0x24BA8, 0x26F87, 0x19599, 0x1CDD0, 0x39CF2, 0x50FB1, 0x510DB, 0x51CB7, 0x60616, 0x61458, 0x75FA6};
        public long[] charmanderOffsets = new long[] { 0x1D10E, 0x1D126, 0x24BA3, 0x26FF6, 0x1CC84, 0x39CF8, 0x50FB3, 0x510DD};

        // Pokemon Blue Offsets
        public long[] blueSquirtleOffsets = new long[] { 0x19591, 0x1CC88, 0x1CC88, 0x1CDC8, 0x1D104, 0x1D11F, 0x50FAF, 0x510D9, 0x51CAF, 0x6060E, 0x61450};
        public long[] blueBulbasuarOffsets = new long[] { 0x19599, 0x1CDD0, 0x1D115, 0x1D130, 0x39CF2, 0x50FB1, 0x510DB, 0x51CB7, 0x60616, 0x61458};
        public long[] blueCharmanderOffsets = new long[] { 0x1CC84, 0x1D10E, 0x1D126, 0x39CF8, 0x50FB3, 0x510DD};

        // Pokemon Green Offsets
        public long[] greenSquirtleOffsets = new long[] {0x19C66, 0x1C6C6, 0x1C806,  0x1CB9D, 0x1CBB8, 0x1CB9D, 0x5149D,  0x515C7, 0x52A1D, 0x606AD, 0x61F2D };
        public long[] greenBulbasuarOffsets = new long[] {0x19C6E, 0x1C80E, 0x1CBC9, 0x1CBAE, 0x1CBC9, 0x3A063, 0x5149F, 0x515C9, 0x52A25, 0x606B5, 0x61F35 };
        public long[] greenCharmanderOffsets = new long[] {0x1C6C2, 0x1CBA7, 0x1CBA7, 0x1CBBF,  0x3A069, 0x514A1, 0x515CB };

        // File identifier, these three bytes are different between pokemon blue and red
        public long[] fileIdentifier = new long[] { 0x14D, 0x14E, 0x14F };
        String[] blueIdentfier = new string[] { "D3", "9D", "0A" };
        String[] redIdentfier = new string[] { "20", "91", "E6" };
        String[] greenIdentfier = new string[] { "9C", "DD", "D5" };

        public long[] rivalsChoice1 = new long[] { 0x3A1E8 }; // rivals choice if you pick Squirtle, normally bulbasuar
        public long[] rivalsChoice2 = new long[] { 0x3A1EB }; // rivals choice if you pick charmander, normally squirtle
        public long[] rivalsChoice3 = new long[] { 0x3A1E5 }; // rivals choice if you pick bulbasuar, normally charmander

        public long[] greenRivalsChoice1 = new long[] { 0x3A559 }; // rivals choice if you pick Squirtle, normally bulbasuar
        public long[] greenRivalsChoice2 = new long[] { 0x3A55C }; // rivals choice if you pick charmander, normally squirtle
        public long[] greenRivalsChoice3 = new long[] { 0x3A556 }; // rivals choice if you pick bulbasuar, normally charmander

        public long[] greenFirstBattleLevels = new long[] { 0x3A555, 0x3A558, 0x3A55B };
        public long[] greenFirstBattlePokemon = new long[] { 0x3A556, 0x3A559, 0x3A55C };
        public long greenAutoScroll = 0x38AE;

        public long[] FirstBattleLevels = new long[] {0x3A1E7, 0x3A1EA, 0x3A1E4 };
        public long[] FirstBattlePokemon = new long[] {0x3A1E8, 0x3A1EB, 0x3A1E5 };
        public long autoScroll = 0x3865 ;

                                                                                   
        public long[] romName = new long[] {0x134, 0x135, 0x136, 0x137, 0x138, 0x139, 0x13A, 0x13B, 0x13C, 0x13D, 0x13E, 0x13F, 0x140, 0x141, 0x142, 0x143, 0x144}; 

        public Offsets()
        {

        }

        public long[] getSquirtleOffsets()
        {
            return squirtleOffsets;
        }

        public long[] getBulbasuarOffsets()
        {
            return bulbasuarOffsets;
        }

        public long[] getCharmanderOffsets()
        {
            return charmanderOffsets;
        }

        public long[] getRomName()
        {
            return romName;
        }

        public long[] getBlueCharmanderOffsets()
        {
            return blueCharmanderOffsets;
        }

        public long[] getBlueSquirtleOffsets()
        {
            return blueSquirtleOffsets;
        }

        public long[] getBlueBulbasaurOffsets()
        {
            return blueBulbasuarOffsets;
        }

        public long[] getFileIdentifier()
        {
            return fileIdentifier;
        }

        public String[] getRedIdenifer()
        {
            return redIdentfier;
        }

        public String[] getBlueIdentifer()
        {
            return blueIdentfier;
        }
    }
}
