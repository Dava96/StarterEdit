using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Controls;

namespace StarterEdit.Util
{
    class ReaderHelper
    {
        private string redIdentfier;
        private string blueIdentfier;
        private string yellowIdentfier;
        private string greenIdentfier;

        public ReaderHelper()
        {
            this.yellowIdentfier = "9747C";
            this.greenIdentfier = "9CDDD5";
            this.redIdentfier ="2091E6";
            this.blueIdentfier = "D39DA";
        }

        public string getNameOfRomLoaded(BinaryReader reader, long[] romName)
        {
            string rName = "";
            char[] name = new char[17];
            for (int i = 0; i < romName.Length; i++)
            {
                reader.BaseStream.Position = romName[i];
                string hexVal = string.Format("{0:X}", reader.ReadByte());
                int decVal = Convert.ToInt32(hexVal, 16);

                name[i] = Convert.ToChar(decVal);
                rName = string.Concat(name);
            }
            return rName.Trim();
        }

        public Version getRomVersion(BinaryReader reader, long[] fileIdentifer)
        {
            String hexVal = "";

            for (int i = 0; i < fileIdentifer.Length; i++)
            {
                reader.BaseStream.Position = fileIdentifer[i];
               hexVal += string.Format("{0:X}", reader.ReadByte());
            }

            if (hexVal.Equals(this.redIdentfier))
            {
                return Version.Red;
            } 
            else if (hexVal.Equals(this.blueIdentfier))
            {
                return Version.Blue;
            } else if (hexVal.Equals(this.yellowIdentfier))
            {
                return Version.Yellow;
            } else
            {
                return Version.Green;
            }
        }

        public void readBattleLvls(long[] levelArray, BinaryReader reader, TextBox[] battleBoxes)
        {
            int pokemonLevel = 0;
            for (int i = 0; i < levelArray.Length; i++)
            {
                if (levelArray[i] != 0x0)
                {
                    reader.BaseStream.Position = levelArray[i];
                    string hexVal = string.Format("{0:X}", reader.ReadByte());
                    pokemonLevel = Convert.ToInt32(hexVal, 16);
                    battleBoxes[i].Text = pokemonLevel.ToString();
                }
            }
        }

        public bool readPatches(long offset, BinaryReader reader)
        {
            reader.BaseStream.Position = offset;
            if (reader.ReadByte() == 0xF0)
            {
                return false;
            }
            return true;
        }

        public void readBattlePokemon(long[] offsetArray, BinaryReader reader, ComboBox[] pokemon)
        {
            int decVal = 0;
            for (int i = 0; i < offsetArray.Length; i++)
            {
                if (offsetArray[i] != 0x0)
                {
                    reader.BaseStream.Position = offsetArray[i];
                    string hexVal = string.Format("{0:X}", reader.ReadByte());
                    decVal = Convert.ToInt32(hexVal, 16);
                    pokemon[i].SelectedIndex = decVal;
                }
                else if (offsetArray[i] == 0x0)
                {
                    pokemon[i].SelectedIndex = -1;
                }
            }
        }

    }

}
