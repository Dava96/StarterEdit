using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using System.Text.RegularExpressions;

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
        long[] fileIdentfier;
        String[] redIdentfier;
        byte[] currentPokmon = new byte[3];
        BinaryReader reader;
        StreamWriter writer;
        bool isYellow = false;

        Util.ReaderHelper readerHelper = new Util.ReaderHelper();
        Util.WriteHelper writeHelper = new Util.WriteHelper();

        PlayersChoiceSquirtle playersChoiceSquirtle = new PlayersChoiceSquirtle();
        PlayersChoiceBulbasaur playersChoiceBulbasaur = new PlayersChoiceBulbasaur();
        PlayersChoiceCharmander playersChoiceCharmander = new PlayersChoiceCharmander();
        PokemonYellowOffsets pokemonYellowOffsets = new PokemonYellowOffsets();


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

            fileIdentfier = offsets.getFileIdentifier();
            redIdentfier = offsets.getRedIdenifer();

            pkmNames = pokemonData.getPokemonNames();
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
                openDialog.Filter = "PKM R/B (*.gb)|*.gb|PKM Y (*.gbc)|*.gbc";
         
                openDialog.ShowDialog();
                reader = new BinaryReader(File.Open(openDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Write));

                if (readerHelper.getRomVersion(reader, fileIdentfier).Equals("red"))
                {
                    bulbOffsets = offsets.getBulbasuarOffsets(); // load red offsets
                    charmOffsets = offsets.getCharmanderOffsets();
                    sqrtlOffsets = offsets.getSquirtleOffsets();
                    setupForRedAndBlue();

                }
                else if (readerHelper.getRomVersion(reader, fileIdentfier).Equals("blue"))
                {
                    bulbOffsets = offsets.getBlueBulbasaurOffsets(); // load blue offsets
                    charmOffsets = offsets.getBlueCharmanderOffsets();
                    sqrtlOffsets = offsets.getBlueSquirtleOffsets();
                    setupForRedAndBlue();

                } else if (readerHelper.getRomVersion(reader, fileIdentfier).Equals("yellow"))
                {
                    isYellow = true;
                    setupForYellow();
                }

               // readStarterPokemon(sqrtlOffsets, reader, Starter1, NameList, 0);
               // readStarterPokemon(bulbOffsets, reader, Starter2, NameList2, 1);
               // readStarterPokemon(charmOffsets, reader, Starter3, NameList3, 2);
               //
               // readStarterPokemon(offsets.rivalsChoice1, reader, RivalStarter, NameList4, 0);
               // readStarterPokemon(offsets.rivalsChoice2, reader, RivalStarter2, NameList5, 1);
               // readStarterPokemon(offsets.rivalsChoice3, reader, RivalStarter3, NameList6, 2);
               //
               // playerChoice.IsEnabled = true;
               // playerChoice2.IsEnabled = true;
               // playerChoice3.IsEnabled = true;
               // 
               // StarterEditWindow.Title = "Starter Edit | " + readerHelper.getNameOfRomLoaded(reader, romName);
               // readerHelper.readBattleLvls(offsets.FirstBattleLevels, reader, LevelBox, LevelBox2, LevelBox3);
               // autoScroll.IsChecked = readerHelper.readPatches(offsets.autoScroll, reader);
            }
            catch (Exception args)
            {
                if (args is FileNotFoundException)
                {
                    MessageBox.Show("File Not Found", "File Not found", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Console.WriteLine(args);
                }
                else if (args is ArgumentException) {
                    MessageBox.Show("Please Select a Pokemon Red/Blue/Yellow rom.", "No File selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Console.WriteLine(args);
                }
            }
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

        private void menuSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                writer = new StreamWriter(File.Open(openDialog.FileName, FileMode.Open, FileAccess.Write, FileShare.Read));
                writeHelper.writeStarterPokemon(sqrtlOffsets, writer, NameList.SelectedIndex, 0);
                writeHelper.writeStarterPokemon(bulbOffsets, writer, NameList2.SelectedIndex, 1);
                writeHelper.writeStarterPokemon(charmOffsets, writer, NameList3.SelectedIndex, 2);



                isNumbersUnder(LevelBox, LevelBox2, LevelBox3); // if the number inputted is > 254 it will be set to 254 as that's the max number the games can handle, otherwise it glitches out

                isNumbersUnder(BattleLvl, BattleLvl2, BattleLvl3); // checks if the battle pokemons levels are under 254
                isNumbersUnder(BattleLvl4, BattleLvl5, BattleLvl6);

                writeHelper.writeBattleLvls(offsets.FirstBattleLevels, writer, LevelBox, LevelBox2, LevelBox3); // writes the rivals levels for the first battle
                writeHelper.writeBattlePkm(offsets.FirstBattlePokemon, writer, NameList4.SelectedIndex, NameList5.SelectedIndex, NameList6.SelectedIndex);
                writeHelper.writePatches(offsets.autoScroll, writer, autoScroll);

                canSave();


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

        public void setupForRedAndBlue()
        {
            readStarterPokemon(sqrtlOffsets, reader, Starter1, NameList, 0);
            readStarterPokemon(bulbOffsets, reader, Starter2, NameList2, 1);
            readStarterPokemon(charmOffsets, reader, Starter3, NameList3, 2);

            readStarterPokemon(offsets.rivalsChoice1, reader, RivalStarter, NameList4, 0);
            readStarterPokemon(offsets.rivalsChoice2, reader, RivalStarter2, NameList5, 1);
            readStarterPokemon(offsets.rivalsChoice3, reader, RivalStarter3, NameList6, 2);

            playerChoice.IsEnabled = true;
            playerChoice2.IsEnabled = true;
            playerChoice3.IsEnabled = true;

            StarterEditWindow.Title = "Starter Edit | " + readerHelper.getNameOfRomLoaded(reader, romName);
            readerHelper.readBattleLvls(offsets.FirstBattleLevels, reader, LevelBox, LevelBox2, LevelBox3);
            autoScroll.IsChecked = readerHelper.readPatches(offsets.autoScroll, reader);

        }

        public void setupForYellow()
        {
            if (isYellow)
            {
                NameList.IsEnabled = false;
                NameList3.IsEnabled = false;
                NameList4.IsEnabled = false;
                NameList6.IsEnabled = false;
                LevelBox.IsEnabled = false;
                LevelBox3.IsEnabled = false;

                Pikachu_Label.Visibility = Visibility.Visible;
                Pikachu_LevelBox.Visibility = Visibility.Visible;
                readerHelper.readBattleLvls(pokemonYellowOffsets.yellowFirstBattleUserLvl, reader, Pikachu_LevelBox);
                readStarterPokemon(pokemonYellowOffsets.yellowFirstBattleRival, reader, RivalStarter2, NameList5, 1);
                readerHelper.readBattleLvls(pokemonYellowOffsets.yellowFirstBattleRivalLvl, reader, LevelBox2);

                StarterEditWindow.Title = "Starter Edit | " + readerHelper.getNameOfRomLoaded(reader, romName);
                readStarterPokemon(pokemonYellowOffsets.yellowFirstBattle, reader, Starter2, NameList2, 1);
                autoScroll.IsChecked = readerHelper.readPatches(offsets.autoScroll, reader);


                playerChoice_Pikachu.Visibility = Visibility.Visible;
                playerChoice_Pikachu.IsChecked = true;


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

            if (isYellow)
            {
                playerChoice_Pikachu.Content = Starter2.Content.ToString();
            } else
            {
                playerChoice.Content = Starter1.Content.ToString();
                playerChoice2.Content = Starter2.Content.ToString();
                playerChoice3.Content = Starter3.Content.ToString();
            }
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

                    if (isSquirtleChecked() && isYellow == false)
                    {
                       readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattle1Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                       readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattle1Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
                    }

                    if (isYellow)
                    {
                        readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattle1, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                        readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattle1Lvls, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
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

                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattle1Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattle1Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);

            }
            else if (BattleLocations.SelectedIndex == 1) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattle2Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattle2Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 2) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattle3Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattle3Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 3) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattle4Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattle4Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 4) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattle5Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattle5Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 5) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattle6Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattle6Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 6) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattle7Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattle7Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }

        }

        private void playerChoice2_Checked(object sender, RoutedEventArgs e) // bulbasaur radio button
        {
            if (playerChoice2.IsChecked == true && BattleLocations.SelectedIndex == 0)
            {
                playerChoice.SetCurrentValue(RadioButton.IsCheckedProperty, false);
                playerChoice3.SetCurrentValue(RadioButton.IsCheckedProperty, false);

                readerHelper.readBattleLvls(playersChoiceBulbasaur.bulbasurBattle1Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceBulbasaur.bulbasaurBattle1Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 1) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceBulbasaur.bulbasurBattle2Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceBulbasaur.bulbasaurBattle2Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 2) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceBulbasaur.bulbasurBattle3Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceBulbasaur.bulbasaurBattle3Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 3) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceBulbasaur.bulbasurBattle4Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceBulbasaur.bulbasaurBattle4Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 4) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceBulbasaur.bulbasurBattle5Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceBulbasaur.bulbasaurBattle5Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 5) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceBulbasaur.bulbasurBattle6Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceBulbasaur.bulbasaurBattle6Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 6) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceBulbasaur.bulbasurBattle7Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceBulbasaur.bulbasaurBattle7Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }


        }


        private void playerChoice3_Checked(object sender, RoutedEventArgs e) // charmander radio button 
        {
            if (playerChoice3.IsChecked == true && BattleLocations.SelectedIndex == 0)
            {
                playerChoice2.SetCurrentValue(RadioButton.IsCheckedProperty, false);
                playerChoice.SetCurrentValue(RadioButton.IsCheckedProperty, false);

                readerHelper.readBattleLvls(playersChoiceCharmander.charmanderBattle1Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceCharmander.charmanderBattle1Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
                
            }
            else if (BattleLocations.SelectedIndex == 1) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceCharmander.charmanderBattle2lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceCharmander.charmanderBattle2Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            } 
            else if (BattleLocations.SelectedIndex == 2)
            {
                readerHelper.readBattleLvls(playersChoiceCharmander.charmanderBattle3lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceCharmander.charmanderBattle3Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 3)
            {
                readerHelper.readBattleLvls(playersChoiceCharmander.charmanderBattle4lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceCharmander.charmanderBattle4Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 4)
            {
                readerHelper.readBattleLvls(playersChoiceCharmander.charmanderBattle5lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceCharmander.charmanderBattle5Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 5)
            {
                readerHelper.readBattleLvls(playersChoiceCharmander.charmanderBattle6lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceCharmander.charmanderBattle6Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 6)
            {
                readerHelper.readBattleLvls(playersChoiceCharmander.charmanderBattle7lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceCharmander.charmanderBattle7Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }

        }

        private void playerChoice_Checked_Pikachu(object sender, RoutedEventArgs e) // pikachu
        {
            if (playerChoice_Pikachu.IsChecked == true && BattleLocations.SelectedIndex == 0 && isYellow)
            {
             
                playerChoice2.SetCurrentValue(RadioButton.IsCheckedProperty, false);
                playerChoice3.SetCurrentValue(RadioButton.IsCheckedProperty, false);

                readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattle1, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattle1Lvls, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);

            }
            else if (BattleLocations.SelectedIndex == 1) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattle2Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattle2Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 2) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattle3Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattle3Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 3) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattle4Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattle4Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 4) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattle5Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattle5Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 5) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattle6Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattle6Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (BattleLocations.SelectedIndex == 6) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattle7Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattle7Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }

        }

        private void canSave()
        {
            if (playerChoice.IsChecked == true) // if squirtle is checked allow save
            {

                writeHelper.writeBattlePkm(playersChoiceSquirtle.squirtleBattle1Pkm, writer, BattlePokemon1.SelectedIndex, BattlePokemon2.SelectedIndex, BattlePokemon3.SelectedIndex,
                   BattlePokemon4.SelectedIndex, BattlePokemon5.SelectedIndex, BattlePokemon6.SelectedIndex);

                writeHelper.writeBattleLvls(playersChoiceSquirtle.squirtleBattle1Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);

            }

            if (playerChoice2.IsChecked == true) // if bulbasuar is checked allow save
            {
                writeHelper.writeBattlePkm(playersChoiceBulbasaur.bulbasaurBattle1Pkm, writer, BattlePokemon1.SelectedIndex, BattlePokemon2.SelectedIndex, BattlePokemon3.SelectedIndex,
                   BattlePokemon4.SelectedIndex, BattlePokemon5.SelectedIndex, BattlePokemon6.SelectedIndex);

                writeHelper.writeBattleLvls(playersChoiceBulbasaur.bulbasurBattle1Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }

            if (playerChoice3.IsChecked == true) // if charmander is checked allow save
            {
                writeHelper.writeBattlePkm(playersChoiceCharmander.charmanderBattle1Pkm, writer, BattlePokemon1.SelectedIndex, BattlePokemon2.SelectedIndex, BattlePokemon3.SelectedIndex,
                    BattlePokemon4.SelectedIndex, BattlePokemon5.SelectedIndex, BattlePokemon6.SelectedIndex);

                writeHelper.writeBattleLvls(playersChoiceCharmander.charmanderBattle1Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
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
