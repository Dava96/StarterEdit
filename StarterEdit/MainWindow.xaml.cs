using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace StarterEdit
{
    public partial class MainWindow : Window
    {
        public readonly Regex numbersOnly = new Regex("[^0-9.-]+"); // Used force text input on the text boxes
        int maxNumber = 254;
        Dictionary<int, string> pokemonNames = new Dictionary<int, string>();
        PokemonData pokemonData = new PokemonData();
        Offsets offsets = new Offsets();
        static OpenFileDialog openDialog = new OpenFileDialog();
        string[] pkmNames;
        long[] sqrtlOffsets;
        long[] bulbOffsets;
        long[] charmOffsets;
        long[] romName;
        byte[] currentPokmon = new byte[3];


        public MainWindow()
        {
            InitializeComponent();

            NameList.ItemsSource = pokemonData.getPokemonNames();
            NameList2.ItemsSource = pokemonData.getPokemonNames();
            NameList3.ItemsSource = pokemonData.getPokemonNames();


           
            pkmNames = pokemonData.getPokemonNames();
            bulbOffsets = offsets.getBulbasuarOffsets();
            charmOffsets = offsets.getCharmanderOffsets();
            sqrtlOffsets = offsets.getSquirtleOffsets();
            romName = offsets.getRomName();
            //offsets.FirstBattleOffsets;
           
        }

        private void menuOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                openDialog.Filter = "PKM R/B (*.gb)|*.gb";
                openDialog.ShowDialog();
                BinaryReader reader = new BinaryReader(File.OpenRead(openDialog.FileName));

                readStarterPokemon(sqrtlOffsets, reader, Starter1, NameList, 0);
                readStarterPokemon(bulbOffsets, reader, Starter2, NameList2, 1);
                readStarterPokemon(charmOffsets, reader, Starter3, NameList3, 2);
                StarterEditWindow.Title = "Starter Edit | " + getRomLoaded(reader, romName);
                readBattleLvls(offsets.FirstBattleLevels, reader, LevelBox, LevelBox2, LevelBox3);
                reader.Close();
            }
            catch (Exception args)
            {
                if (args is FileNotFoundException)
                {
                    MessageBox.Show("File Not Found", "File Not found", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Console.WriteLine(args);
                }
                else if (args is ArgumentException) {
                    MessageBox.Show("Please Select a Pokemon Red/Blue rom.", "No File selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Console.WriteLine(args);
                }
            }
        }

        public string getRomLoaded(BinaryReader reader, long[] romName)
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

        public void readStarterPokemon(long[] offsetArray, BinaryReader reader, Label Starter, ComboBox List, int starterNumber)
        {
            int decVal = 0;
            for (int i = 0; i < offsetArray.Length; i++)
            {
                reader.BaseStream.Position = offsetArray[i];
                string hexVal = string.Format("{0:X}",  reader.ReadByte());
                decVal = Convert.ToInt32(hexVal, 16);
            }
            Starter.Content = pkmNames[decVal];
            List.SelectedIndex = decVal;
            currentPokmon[starterNumber] = (byte)decVal;
        }

        private void menuSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamWriter writer = new StreamWriter(File.OpenWrite(openDialog.FileName));
                writeStarterPokemon(sqrtlOffsets, writer, NameList.SelectedIndex, 0);
                writeStarterPokemon(bulbOffsets, writer, NameList2.SelectedIndex, 1);
                writeStarterPokemon(charmOffsets, writer, NameList3.SelectedIndex, 2);

                isNumbersUnder(LevelBox, LevelBox2, LevelBox3); // if the number inputted is > 254 it will be set to 254 as that's the max number the games can handle, otherwise it glitches out
                writeBattleLvls(offsets.FirstBattleLevels, writer, LevelBox, LevelBox2, LevelBox3);
                writeBattlePkm(offsets.FirstBattlePokemon, writer, NameList.SelectedIndex, NameList2.SelectedIndex, NameList3.SelectedIndex);

                MessageBox.Show("Changes saved succesfully", "Changes saved");
                writer.Close();
            }
            catch (ArgumentException args)
            {
                MessageBox.Show("Please Open a file before trying to save.", "No File Selected Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                Console.WriteLine(args);
                //this.Close();
            }
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

        public void readBattleLvls(long[] offsetArray, BinaryReader reader, TextBox levelBox, TextBox levelBox2, TextBox levelBox3)
        {
            TextBox[] levelBoxes = new TextBox[] { levelBox, levelBox2, levelBox3 };
            int pokemonLevel = 0;
            for (int i = 0; i < offsetArray.Length; i++)
            {
                reader.BaseStream.Position = offsetArray[i];
                string hexVal = string.Format("{0:X}", reader.ReadByte());
                pokemonLevel = Convert.ToInt32(hexVal, 16);
                levelBoxes[i].Text = pokemonLevel.ToString();
            }
        }

        public void writeBattleLvls(long[] offsetArray, StreamWriter writer, TextBox levelBox, TextBox levelBox2, TextBox levelBox3)
        {
            TextBox[] levelBoxes = new TextBox[] { levelBox, levelBox2, levelBox3 };
            for (int i = 0; i < offsetArray.Length; i++)
            {
                writer.BaseStream.Position = offsetArray[i];

                writer.BaseStream.WriteByte((byte)Int32.Parse(levelBoxes[i].Text.ToString()));
                writer.Flush();
            }
        }

        public void isNumbersUnder(TextBox levelBox, TextBox levelBox2, TextBox levelBox3)
        {
            TextBox[] levelBoxes = new TextBox[] { levelBox, levelBox2, levelBox3 };
            for (int i = 0; i < levelBoxes.Length; i++)
            {
                int number = Convert.ToInt32(levelBoxes[i].Text);
                if (number > maxNumber)
                {
                    levelBoxes[i].Text = maxNumber.ToString();
                }
                number = 0;
            }
        }
        
        public bool isTextAllowed(string text) // checks to see if the input given is text
        {
            return !numbersOnly.IsMatch(text);
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e) // method is called when the textbox is active preventing invalid inputs: i.e text
        {
            e.Handled = !isTextAllowed(e.Text);
        }
    }
        
}
