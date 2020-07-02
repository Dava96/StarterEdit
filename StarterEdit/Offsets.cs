using System;
using System.Collections.Generic;
using System.Text;

namespace StarterEdit
{
    class Offsets
    {

        public long[] squirtleOffsets = new long[] { 0x1D104, 0x1D11F, 0x24BA5, 0x26FBC};
        public long[] bulbasuarOffsets = new long[] { 0x1D115, 0x1D130, 0x24BA8, 0x26F87 };
        public long[] charmanderOffsets = new long[] { 0x1D10E, 0x1D126, 0x24BA3, 0x26FF6};

        public long[] FirstBattleLevels = new long[] { 0x3A1E4, 0x3A1E7, 0x3A1EA };
        public long[] FirstBattlePokemon = new long[] {0x3A1E5, 0x3A1E8, 0x3A1EB };
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

        public long[] getRomName ()
        {
            return romName;
        }
    }
}
