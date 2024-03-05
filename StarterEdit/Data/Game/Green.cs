using System;
using System.Collections.Generic;
using StarterEdit;

namespace StarterEdit
{
    class Green : Game
    {
        public Version version;
        public string fileIdentifier;
        public Dictionary<Choice, long[]> rivalsChoice;
        public Dictionary<DataType, long[]> firstRivalBattle;
        public Dictionary<Choice, long[]> starterOffsets;
        public long autoScrollOffset;
        public GreenSquirtle squirtle;
        public GreenCharmander charmander;
        public GreenBulbasaur bulbasaur;

        public Green()
        {
            this.fileIdentifier = "9CDDD5";
            this.version = Version.Green;
            this.autoScrollOffset = 0x38AE;

            this.rivalsChoice = new Dictionary<Choice, long[]> {
            { Choice.Bulbasaur, new long[] { 0x3A559 }},
            { Choice.Squirtle, new long[] { 0x3A55C }},
            { Choice.Charmander, new long[] { 0x3A556 }}
        };

            this.starterOffsets = new Dictionary<Choice, long[]> {
            { Choice.Squirtle, new long[] { 0x19C66, 0x1C6C6, 0x1C806,  0x1CB9D, 0x1CBB8, 0x1CB9D, 0x5149D,  0x515C7, 0x52A1D, 0x606AD, 0x61F2D } },
            { Choice.Bulbasaur, new long[] { 0x19C6E, 0x1C80E, 0x1CBC9, 0x1CBAE, 0x1CBC9, 0x3A063, 0x5149F, 0x515C9, 0x52A25, 0x606B5, 0x61F35 }},
            { Choice.Charmander, new long[] { 0x1C6C2, 0x1CBA7, 0x1CBA7, 0x1CBBF,  0x3A069, 0x514A1, 0x515CB }}
        };

            this.firstRivalBattle = new Dictionary<DataType, long[]> {
            { DataType.Level, new long[] {  0x3A555, 0x3A558, 0x3A55B }},
            { DataType.Pokemon, new long[] { 0x3A556, 0x3A559, 0x3A55C }}
        };

            this.squirtle = new GreenSquirtle();
            this.bulbasaur = new GreenBulbasaur();
            this.charmander = new GreenCharmander();
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