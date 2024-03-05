using System;

namespace StarterEdit
{
    class Offsets
    {
        // File identifier, these three bytes are different between pokemon blue and red
        public long[] fileIdentifier;
                                                                                   
        public long[] romName; 

        public Offsets()
        {
            this.romName = new long[] {0x134, 0x135, 0x136, 0x137, 0x138, 0x139, 0x13A, 0x13B, 0x13C, 0x13D, 0x13E, 0x13F, 0x140, 0x141, 0x142, 0x143, 0x144};
            this.fileIdentifier = new long[] { 0x14D, 0x14E, 0x14F };
        }

        public long[] getFileIdentifier()
        {
            return this.fileIdentifier;
        }

        public long[] getRomName()
        {
            return this.romName;
        }
    }
}
