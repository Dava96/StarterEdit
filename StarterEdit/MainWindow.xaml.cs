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
        PokemonData pokemonData = new PokemonData();
        Offsets offsets = new Offsets();
        static OpenFileDialog openDialog = new OpenFileDialog();
        string[] pkmNames;
        long[] sqrtlOffsets;
        long[] bulbOffsets;
        long[] charmOffsets;
        long[] romName;
        byte[] currentPokmon = new byte[3];
        BinaryReader reader;    

        PlayersChoiceSquirtle playersChoiceSquirtle = new PlayersChoiceSquirtle();
        PlayersChoiceBulbasaur playersChoiceBulbasaur = new PlayersChoiceBulbasaur();
        PlayersChoiceCharmander playersChoiceCharmander = new PlayersChoiceCharmander();


        public MainWindow()
        {
            InitializeComponent();
            NameList.ItemsSource = pokemonData.getPokemonNames(); // handles your choice
            NameList2.ItemsSource = pokemonData.getPokemonNames();
            NameList3.ItemsSource = pokemonData.getPokemonNames();

            NameList4.ItemsSource = pokemonData.getPokemonNames(); // handles rivals choice
            NameList5.ItemsSource = pokemonData.getPokemonNames();
            NameList6.ItemsSource = pokemonData.getPokemonNames();

            BattleLocations.ItemsSource = pokemonData.getBattleLocations(); // gets battle locations
    
            pkmNames = pokemonData.getPokemonNames();
            bulbOffsets = offsets.getBulbasuarOffsets();
            charmOffsets = offsets.getCharmanderOffsets();
            sqrtlOffsets = offsets.getSquirtleOffsets();
            romName = offsets.getRomName();

            BattlePokemon1.ItemsSource = pokemonData.getPokemonNames();
            BattlePokemon2.ItemsSource = pokemonData.getPokemonNames();
            BattlePokemon3.ItemsSource = pokemonData.getPokemonNames();
            BattlePokemon4.ItemsSource = pokemonData.getPokemonNames();
            BattlePokemon5.ItemsSource = pokemonData.getPokemonNames();
            BattlePokemon6.ItemsSource = pokemonData.getPokemonNames();
            //offsets.FirstBattleOffsets;

        }

        private void menuOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                openDialog.Filter = "PKM R/B (*.gb)|*.gb";
                openDialog.ShowDialog();
                reader = new BinaryReader(File.Open(openDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Write));

                readStarterPokemon(sqrtlOffsets, reader, Starter1, NameList, 0);
                readStarterPokemon(bulbOffsets, reader, Starter2, NameList2, 1);
                readStarterPokemon(charmOffsets, reader, Starter3, NameList3, 2);

                readStarterPokemon(offsets.rivalsChoice1, reader, RivalStarter, NameList4, 0);
                readStarterPokemon(offsets.rivalsChoice2, reader, RivalStarter2, NameList5, 1);
                readStarterPokemon(offsets.rivalsChoice3, reader, RivalStarter3, NameList6, 2);

                playerChoice.IsEnabled = true;
                playerChoice2.IsEnabled = true;
                playerChoice3.IsEnabled = true;

                StarterEditWindow.Title = "Starter Edit | " + getRomLoaded(reader, romName);
                readBattleLvls(offsets.FirstBattleLevels, reader, LevelBox, LevelBox2, LevelBox3);
               // reader.Close();
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
            setPlayerChoice();
        }

        public void readBattlePokemon(long[] offsetArray, BinaryReader reader, ComboBox pkm1, ComboBox pkm2, ComboBox pkm3, ComboBox pkm4, ComboBox pkm5, ComboBox pkm6)
        {

            ComboBox[] pokemon = new ComboBox[] { pkm1, pkm2, pkm3, pkm4, pkm5, pkm6 };
            int decVal = 0;
            for (int i = 0; i < offsetArray.Length; i++)
            {
                if (offsetArray[i] != 0x0)
                {
                reader.BaseStream.Position = offsetArray[i];
                string hexVal = string.Format("{0:X}", reader.ReadByte());
                decVal = Convert.ToInt32(hexVal, 16);
                    pokemon[i].SelectedIndex = decVal;
                } else if (offsetArray[i] == 0x0)
                {
                    pokemon[i].SelectedIndex = -1;
                }
            }
        }

        private void menuSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               

                StreamWriter writer = new StreamWriter(File.Open(openDialog.FileName, FileMode.Open, FileAccess.Write, FileShare.Read));
                writeStarterPokemon(sqrtlOffsets, writer, NameList.SelectedIndex, 0);
                writeStarterPokemon(bulbOffsets, writer, NameList2.SelectedIndex, 1);
                writeStarterPokemon(charmOffsets, writer, NameList3.SelectedIndex, 2);



                isNumbersUnder(LevelBox, LevelBox2, LevelBox3); // if the number inputted is > 254 it will be set to 254 as that's the max number the games can handle, otherwise it glitches out
                
                isNumbersUnder(BattleLvl, BattleLvl2, BattleLvl3); // checks if the battle pokemons levels are under 254
                isNumbersUnder(BattleLvl4, BattleLvl5, BattleLvl6);

                writeBattleLvls(offsets.FirstBattleLevels, writer, LevelBox, LevelBox2, LevelBox3); // writes the rivals levels for the first battle
                writeBattlePkm(offsets.FirstBattlePokemon, writer, NameList4.SelectedIndex, NameList5.SelectedIndex, NameList6.SelectedIndex);

                writeBattlePkm(playersChoiceSquirtle.squirtleBattle1Pkm, writer, BattlePokemon1.SelectedIndex, BattlePokemon2.SelectedIndex, BattlePokemon3.SelectedIndex,
                    BattlePokemon4.SelectedIndex, BattlePokemon5.SelectedIndex, BattlePokemon6.SelectedIndex);

                writeBattleLvls(playersChoiceSquirtle.squirtleBattle1Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);

                MessageBox.Show("Changes saved succesfully", "Changes saved");
                writer.Close();
            }
            catch (ArgumentException args)
            {
                MessageBox.Show("Please Open a file before trying to save.", "No File Selected Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                Console.WriteLine(args);
                //this.Close();
            }
            catch (System.IO.IOException args)
            {
                MessageBox.Show("Error" + args);
                reader.Close();
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

        
        public void readBattleLvls(long[] levelArray, BinaryReader reader, TextBox box1, TextBox box2, TextBox box3, TextBox box4, TextBox box5, TextBox box6 )
        {
            TextBox[] battleBoxes = new TextBox[] { box1, box2, box3, box4, box5, box6 };
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

        public void isNumbersUnder(TextBox levelBox, TextBox levelBox2, TextBox levelBox3)
        {
            try
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
            catch (System.FormatException args)
            {
                MessageBox.Show("Please Enter a number, i.e 25", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private void setPlayerChoice()
        {
            // sets the radio buttons text to what the players choice of starter is
            playerChoice.Content = Starter1.Content.ToString();
            playerChoice2.Content = Starter2.Content.ToString();
            playerChoice3.Content = Starter3.Content.ToString();
        }


        private void BattleLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex;
            selectedIndex = BattleLocations.SelectedIndex;
            switch (selectedIndex)
            {
                case 0: // route 22 battle 1
                    BattleLvl3.IsEnabled = false;
                    BattlePokemon3.IsEnabled = false;
                    BattleLvl4.IsEnabled = false;
                    BattlePokemon4.IsEnabled = false;
                    BattleLvl5.IsEnabled = false;
                    BattlePokemon5.IsEnabled = false;
                    BattleLvl6.IsEnabled = false;
                    BattlePokemon6.IsEnabled = false;

                    if (isSquirtleChecked())
                    {
                        readBattleLvls(playersChoiceSquirtle.squirtleBattle1Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                        readBattlePokemon(playersChoiceSquirtle.squirtleBattle1Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
                    }

                    break;

                case 1: // cerulean city
                    BattleLvl3.IsEnabled = true;
                    BattlePokemon3.IsEnabled = true;
                    BattleLvl4.IsEnabled = true;
                    BattlePokemon4.IsEnabled = true;
                    BattleLvl5.IsEnabled = false;
                    BattlePokemon5.IsEnabled = false;
                    BattleLvl6.IsEnabled = false;
                    BattlePokemon6.IsEnabled = false;
                    break;

                case 2: // S.S Anne
                    BattleLvl3.IsEnabled = true;
                    BattlePokemon3.IsEnabled = true;
                    BattleLvl4.IsEnabled = true;
                    BattlePokemon4.IsEnabled = true;
                    BattleLvl5.IsEnabled = false;
                    BattlePokemon5.IsEnabled = false;
                    BattleLvl6.IsEnabled = false;
                    BattlePokemon6.IsEnabled = false;
                    break;

                case 3: // pokemon tower
                    BattleLvl3.IsEnabled = true;
                    BattlePokemon3.IsEnabled = true;
                    BattleLvl4.IsEnabled = true;
                    BattlePokemon4.IsEnabled = true;
                    BattleLvl5.IsEnabled = true;
                    BattlePokemon5.IsEnabled = true;
                    BattleLvl6.IsEnabled = false;
                    BattlePokemon6.IsEnabled = false;
                    break;

                case 4: // silph co
                    BattleLvl3.IsEnabled = true;
                    BattlePokemon3.IsEnabled = true;
                    BattleLvl4.IsEnabled = true;
                    BattlePokemon4.IsEnabled = true;
                    BattleLvl5.IsEnabled = true;
                    BattlePokemon5.IsEnabled = true;
                    BattleLvl6.IsEnabled = false;
                    BattlePokemon6.IsEnabled = false;
                    break;

                case 5: // route 22 battle 2
                    BattleLvl3.IsEnabled = true;
                    BattlePokemon3.IsEnabled = true;
                    BattleLvl4.IsEnabled = true;
                    BattlePokemon4.IsEnabled = true;
                    BattleLvl5.IsEnabled = true;
                    BattlePokemon5.IsEnabled = true;
                    BattleLvl6.IsEnabled = true;
                    BattlePokemon6.IsEnabled = true;
                    break;

                case 6: // indigo plateau
                    BattleLvl3.IsEnabled = true;
                    BattlePokemon3.IsEnabled = true;
                    BattleLvl4.IsEnabled = true;
                    BattlePokemon4.IsEnabled = true;
                    BattleLvl5.IsEnabled = true;
                    BattlePokemon5.IsEnabled = true;
                    BattleLvl6.IsEnabled = true;
                    BattlePokemon6.IsEnabled = true;
                    break;
            }
        }

        private void playerChoice_Checked(object sender, RoutedEventArgs e) // squirtle radio button
        {
            if (playerChoice.IsChecked == true && BattleLocations.SelectedIndex == 0)
            {
                playerChoice2.SetCurrentValue(RadioButton.IsCheckedProperty, false);
                playerChoice3.SetCurrentValue(RadioButton.IsCheckedProperty, false);

                readBattleLvls(playersChoiceSquirtle.squirtleBattle1Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readBattlePokemon(playersChoiceSquirtle.squirtleBattle1Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);

            } else if (BattleLocations.SelectedIndex == 1)
            {
                readBattleLvls(playersChoiceSquirtle.squirtleBattle2Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readBattlePokemon(playersChoiceSquirtle.squirtleBattle2Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }

        }

        private void playerChoice2_Checked(object sender, RoutedEventArgs e) // bulbasaur radio button
        {
            if (playerChoice2.IsChecked == true)
            {
                playerChoice.SetCurrentValue(RadioButton.IsCheckedProperty, false);
                playerChoice3.SetCurrentValue(RadioButton.IsCheckedProperty, false);

                readBattleLvls(playersChoiceBulbasaur.bulbasurBattle1Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readBattlePokemon(playersChoiceBulbasaur.bulbasaurBattle1Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);

            }

        }


        private void playerChoice3_Checked(object sender, RoutedEventArgs e) // charmander radio button 
        {
            if (playerChoice3.IsChecked == true)
            {
                playerChoice2.SetCurrentValue(RadioButton.IsCheckedProperty, false);
                playerChoice.SetCurrentValue(RadioButton.IsCheckedProperty, false);

                readBattleLvls(playersChoiceCharmander.charmanderBattle1Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readBattlePokemon(playersChoiceCharmander.charmanderBattle1Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
                
            }

        }

        public Boolean isSquirtleChecked()
        {
            return (bool)playerChoice.IsChecked;
        }

        public Boolean isBulbasuarChecked()
        {
            return (bool)playerChoice2.IsChecked;
        }

        public Boolean isCharmanderChecked()
        {
            return (bool)playerChoice3.IsChecked;
        }
    }    
}
