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
    }
}
