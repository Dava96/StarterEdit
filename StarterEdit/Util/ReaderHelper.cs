using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Controls;

namespace StarterEdit.Util
{
    class ReaderHelper
    {

        public ReaderHelper()
        {

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

        public String getRomVersion(BinaryReader reader, long[] fileIdentifer)
        {
            String hexVal = "";
            String blueIdentfier = "D39DA";
            String redIdentfier = "2091E6";
            String yellowIdentfier = "9747C";
            String greenIdentfier = "9CDDD5";

            for (int i = 0; i < fileIdentifer.Length; i++)
            {
                reader.BaseStream.Position = fileIdentifer[i];
               hexVal += string.Format("{0:X}", reader.ReadByte());
            }

            if (hexVal.Equals(redIdentfier))
            {
                return "red";
            } 
            else if (hexVal.Equals(blueIdentfier))
            {
                return "blue";
            } else if (hexVal.Equals(yellowIdentfier))
            {
                return "yellow";
            } else
            {
                return "green";
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


        // public void readBattleLvls(long[] offsetArray, BinaryReader reader, TextBox levelBox, TextBox levelBox2, TextBox levelBox3)
        // {

        //     // This deals with the first battle, not entirely sure why I've written this twice to do the same thing

        //     TextBox[] levelBoxes = new TextBox[] { levelBox, levelBox2, levelBox3 };
        //     int pokemonLevel = 0;
        //     for (int i = 0; i < offsetArray.Length; i++)
        //     {
        //         reader.BaseStream.Position = offsetArray[i];
        //         string hexVal = string.Format("{0:X}", reader.ReadByte());
        //         pokemonLevel = Convert.ToInt32(hexVal, 16);
        //         levelBoxes[i].Text = pokemonLevel.ToString();
        //     }
        // }

        public void readBattleLvls(long[] offsetArray, BinaryReader reader, TextBox levelBox)
        {

            // This deals with the first battle for pokemon yellow as there is only one pokemon at the start

            TextBox[] levelBoxes = new TextBox[] { levelBox };
            int pokemonLevel = 0;
            for (int i = 0; i < offsetArray.Length; i++)
            {
                reader.BaseStream.Position = offsetArray[i];
                string hexVal = string.Format("{0:X}", reader.ReadByte());
                pokemonLevel = Convert.ToInt32(hexVal, 16);
                levelBoxes[i].Text = pokemonLevel.ToString();
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
