using System;
using System.Collections.Generic;
using StarterEdit;

namespace StarterEdit
{

    class Red : Game
    {
        public Version version;
        public string fileIdentifier;
        public Dictionary<Choice, long[]> rivalsChoice;
        public Dictionary<DataType, long[]> firstRivalBattle;
        public Dictionary<Choice, long[]> starterOffsets;
        public long autoScrollOffset;
        public Squirtle squirtle;
        public Charmander charmander;
        public Bulbasaur bulbasaur;

        public Red()
        {
            this.fileIdentifier = "2091E6";
            this.version = Version.Red;
            this.autoScrollOffset = 0x3865;

            this.rivalsChoice = new Dictionary<Choice, long[]> {
            { Choice.Bulbasaur, new long[] { 0x3A1E8 }},
            { Choice.Squirtle, new long[] { 0x3A1EB }},
            { Choice.Charmander, new long[] { 0x3A1E5 }}
        };

            this.starterOffsets = new Dictionary<Choice, long[]> {
            { Choice.Squirtle, new long[] { 0x1D104, 0x1D11F, 0x24BA5, 0x26FBC, 0x19591, 0x1CC88, 0x1CDC8, 0x50FAF, 0x510D9, 0x51CAF, 0x6060E, 0x61450, 0x75F9E } },
            { Choice.Bulbasaur, new long[] { 0x1D115, 0x1D130, 0x24BA8, 0x26F87, 0x19599, 0x1CDD0, 0x39CF2, 0x50FB1, 0x510DB, 0x51CB7, 0x60616, 0x61458, 0x75FA6 }},
            { Choice.Charmander, new long[] { 0x1D10E, 0x1D126, 0x24BA3, 0x26FF6, 0x1CC84, 0x39CF8, 0x50FB3, 0x510DD }}
        };

            this.firstRivalBattle = new Dictionary<DataType, long[]> {
            { DataType.Level, new long[] { 0x3A1E7, 0x3A1EA, 0x3A1E4 }},
            { DataType.Pokemon, new long[] {0x3A1E8, 0x3A1EB, 0x3A1E5 }}
        };

            this.squirtle = new Squirtle();
            this.bulbasaur = new Bulbasaur();
            this.charmander = new Charmander();
        }

        public override string GetFileIdentifier()
        {
            return this.fileIdentifier;
        }

        public override Dictionary<Choice, long[]> GetRivalsChoice()
        {
            return this.rivalsChoice;
        }

        public override Dictionary<DataType, long[]> GetFirstRivalBattle()
        {
            return this.firstRivalBattle;
        }

        public override Dictionary<Choice, long[]> GetStarterOffsets()
        {
            return this.starterOffsets;
        }

        public override Version GetVersion()
        {
            return this.version;
        }

        public override long GetAutoScroll()
        {
            return this.autoScrollOffset;
        }

        public override Dictionary<DataType, long[]> GetBattlesForPlayersChoice(IPlayersChoice playersChoice, BattleName battleName)
        {
            return playersChoice.getBattle(battleName);
        }

        public override IPlayersChoice GetPlayersChoice(Choice choice)
        {
            switch (choice)
            {
                case Choice.Squirtle:
                    return squirtle;
                case Choice.Bulbasaur:
                    return bulbasaur;
                case Choice.Charmander:
                    return charmander;
                default:
                    // Handle the case where choice doesn't match any of the enum values
                    throw new ArgumentException("Invalid choice", nameof(choice));
            }
        }
    }
}