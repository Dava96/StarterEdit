using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace StarterEdit.Util
{

    class WriteHelper
    {
        byte[] currentPokmon = new byte[3];

        public WriteHelper()
        {

        }


        public void writeStarterPokemon(long[] offsetArray, StreamWriter writer, int pokemonValue, int starterNumber)
        {
            for (int i = 0; i < offsetArray.Length; i++)
            {
                writer.BaseStream.Position = offsetArray[i];
                if (currentPokmon[starterNumber] != (byte)pokemonValue)
                {
                    writer.BaseStream.WriteByte((byte)pokemonValue);
                    writer.Flush();
                }
            }
        }

        public void writeBattlePkm(long[] offsetArray, StreamWriter writer, int pkm1, int pkm2, int pkm3) // Pokemon 1,2 and 3 values from nameList
        {
            int[] pokemonArray = new int[] { pkm1, pkm2, pkm3 }; // the game might default to bulbasurs position if this is changed
            for (int i = 0; i < offsetArray.Length; i++)
            {
                writer.BaseStream.Position = offsetArray[i];
                writer.BaseStream.WriteByte((byte)pokemonArray[i]);
                writer.Flush();
            }
        }

        public void writeBattlePkm(long[] offsetArray, StreamWriter writer, int pkm1) // Pokemon 1,2 and 3 values from nameList
        {
            // Overload for Pokemon Yellow
            int[] pokemonArray = new int[] { pkm1 }; 
            for (int i = 0; i < offsetArray.Length; i++)
            {
                writer.BaseStream.Position = offsetArray[i];
                writer.BaseStream.WriteByte((byte)pokemonArray[i]);
                writer.Flush();
            }
        }

        public void writeBattlePkm(long[] offsetArray, StreamWriter writer, int[] pokemonArray) // Pokemon 1,2 and 3 values from nameList
        {
            for (int i = 0; i < offsetArray.Length; i++)
            {
                if (pokemonArray[i] >= 0)
                {
                    writer.BaseStream.Position = offsetArray[i];
                    writer.BaseStream.WriteByte((byte)pokemonArray[i]);
                    writer.Flush();
                }
            }
        }

        public void writeBattleLvls(long[] offsetArray, StreamWriter writer, TextBox levelBox)
        {
            {
                // Overload for pokemon yellow
                TextBox[] levelBoxes = new TextBox[] { levelBox};
                for (int i = 0; i < offsetArray.Length; i++)
                {
                    writer.BaseStream.Position = offsetArray[i];

                    writer.BaseStream.WriteByte((byte)Int32.Parse(levelBoxes[i].Text.ToString()));
                    writer.Flush();
                }
            }
        }

        public void writeBattleLvls(long[] offsetArray, StreamWriter writer, TextBox[] levelBoxes)
        {

            for (int i = 0; i < offsetArray.Length; i++)
            {
                if (!levelBoxes[i].Text.Equals("0") && offsetArray[i] != 0)
                {
                    writer.BaseStream.Position = offsetArray[i];

                    writer.BaseStream.WriteByte((byte)Int32.Parse(levelBoxes[i].Text.ToString()));
                    writer.Flush();
                }
            }
        }

        public void writePatches(long offset, StreamWriter writer, CheckBox checkBox)
        {

                if (checkBox.IsChecked == false)
                {
                    writer.BaseStream.Position = offset;

                    writer.BaseStream.WriteByte(0xF0);
                    writer.Flush();
                }
                else if (checkBox.IsChecked == true)
                {
                writer.BaseStream.Position = offset;

                writer.BaseStream.WriteByte(0xc9);
                writer.Flush();
                }
        }

        public Boolean checkInputs(TextBox[] levelBoxes)
        {
            try
            {
                // checks if the inputs are under 0 or over 255
                int maxNumber = 254;
                for (int i = 0; i < levelBoxes.Length; i++)
                {
                    int number = Convert.ToInt32(levelBoxes[i].Text);
                    if (number > maxNumber)
                    {
                        levelBoxes[i].Text = maxNumber.ToString();
                    } else if (number < 0)
                    {
                        levelBoxes[i].Text = "0";
                    }
                    number = 0;
                }
                return true;
            }
            catch (System.FormatException args)
            {
                MessageBox.Show("Please Enter a number, i.e 25", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                Console.WriteLine(args);
                return false;
            }
        }

    }
}
