using System;
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

        // File identifier, these three bytes are different between pokemon blue and red
        public long[] fileIdentifier = new long[] { 0x14D, 0x14E, 0x14F };
        String[] blueIdentfier = new string[] { "D3", "9D", "0A" };
        String[] redIdentfier = new string[] { "20", "91", "E6" };

        public long[] rivalsChoice1 = new long[] { 0x3A1E8 }; // rivals choice if you pick Squirtle, normally bulbasuar
        public long[] rivalsChoice2 = new long[] { 0x3A1EB }; // rivals choice if you pick charmander, normally squirtle
        public long[] rivalsChoice3 = new long[] { 0x3A1E5 }; // rivals choice if you pick bulbasuar, normally charmander

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
