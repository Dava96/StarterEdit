using System;
using System.Collections.Generic;
using StarterEdit;

namespace StarterEdit
{
    class Yellow : Game
    {
        public Version version;
        public string fileIdentifier;
        public Dictionary<Choice, long[]> rivalsChoice;
        public Dictionary<DataType, long[]> firstRivalBattle;
        public Dictionary<Choice, long[]> starterOffsets;
        public Dictionary<DataType, long[]> catchingPikachu;
        public Dictionary<BattleName, Dictionary<DataType, long[]>> eveeBattles = new Dictionary<BattleName, Dictionary<DataType, long[]>>();
        public long autoScrollOffset;
        public Jolteon jolteon;
        public Flareon flareon;
        public Vaporeon vaporeon;

        public Yellow()
        {
            this.fileIdentifier = "97047C";
            this.version = Version.Yellow;
            this.autoScrollOffset = 0x0;

            this.rivalsChoice = new Dictionary<Choice, long[]> {};

            this.starterOffsets = new Dictionary<Choice, long[]> {
            { Choice.Pikachu, new long[] { 0x18F10, 0x18F1E } },
        };

            this.catchingPikachu = new Dictionary<DataType, long[]> {
            { DataType.Level, new long[] {0x1CB61 }},
            { DataType.Pokemon, new long[] { 0x1CB41, 0x1CB66 }}
        };

            this.firstRivalBattle = new Dictionary<DataType, long[]> {
            { DataType.Level, new long[] { 0x3A289 }},
            { DataType.Pokemon, new long[] {0x3A28A }}
        };

            eveeBattles[BattleName.Route22_1] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A28E, 0x3A290, 0x0, 0x0, 0x0, 0x0 } },
                {DataType.Level, new long[] { 0x3A28D, 0x3A28F, 0x0, 0x0, 0x0, 0x0  } }
            };

            eveeBattles[BattleName.CeruleanCity] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] {0x3A294, 0x3A296, 0x3A298, 0x3A29A, 0x0, 0x0 } },
                {DataType.Level, new long[] {  0x3A293, 0x3A295, 0x3A297, 0x3A299, 0x0, 0x0  } }
            };

            eveeBattles[BattleName.SSAnne] = new Dictionary<DataType, long[]>
            {
                {DataType.Pokemon, new long[] { 0x3A49B, 0x3A49D, 0x3A49F, 0x3A4A1, 0x0, 0x0} },
                {DataType.Level, new long[] { 0x3A49A, 0x3A49C, 0x3A49E, 0x3A4A0, 0x0, 0x0  } }
            };

            this.jolteon = new Jolteon();
            this.flareon = new Flareon();
            this.vaporeon = new Vaporeon();
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
                case Choice.Vaporeon:
                    return vaporeon;
                case Choice.Jolteon:
                    return jolteon;
                case Choice.Flareon:
                    return flareon;
                default:
                    // Handle the case where choice doesn't match any of the enum values
                    throw new ArgumentException("Invalid choice", nameof(choice));
            }
        }

        public Dictionary<DataType, long[]> getCatchingPikachuBattle()
        {
            return this.catchingPikachu;
        }

        public Dictionary<BattleName, Dictionary<DataType, long[]>> getEveeBattles()
        {
            return this.eveeBattles;
        }
    }
}