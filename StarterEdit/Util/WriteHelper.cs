using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Controls;

namespace StarterEdit.Util
{

    class WriteHelper
    {
        StreamWriter writer;
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


        public void writeBattlePkm(long[] offsetArray, StreamWriter writer, int pkm1, int pkm2, int pkm3, int pkm4, int pkm5, int pkm6) // Pokemon 1,2 and 3 values from nameList
        {
            int[] pokemonArray = new int[] { pkm1, pkm2, pkm3, pkm4, pkm5, pkm6 }; // the game might default to bulbasurs position if this is changed
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


        public void writeBattleLvls(long[] offsetArray, StreamWriter writer, TextBox levelBox, TextBox levelBox2, TextBox levelBox3)
        {
            {

                TextBox[] levelBoxes = new TextBox[] { levelBox, levelBox2, levelBox3 };
                for (int i = 0; i < offsetArray.Length; i++)
                {
                    writer.BaseStream.Position = offsetArray[i];

                    writer.BaseStream.WriteByte((byte)Int32.Parse(levelBoxes[i].Text.ToString()));
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



        public void writeBattleLvls(long[] offsetArray, StreamWriter writer, TextBox levelBox, TextBox levelBox2, TextBox levelBox3, TextBox levelBox4, TextBox levelBox5, TextBox levelBox6)
        {
            TextBox[] levelBoxes = new TextBox[] { levelBox, levelBox2, levelBox3, levelBox4, levelBox5, levelBox6 };

            for (int i = 0; i < offsetArray.Length; i++)
            {
                if (!levelBoxes[i].Text.Equals("0"))
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

    }
}
