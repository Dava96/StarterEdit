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
        PokemonData pokemonData = new PokemonData();
        Offsets offsets = new Offsets();
        static OpenFileDialog openDialog = new OpenFileDialog();
        string[] pkmNames;
        long[] sqrtlOffsets;
        long[] bulbOffsets;
        long[] charmOffsets;
        long[] firstBattleLevels;
        long[] firstBattlePokemon;
        long[] rivalChoiceSquirtle;
        long[] rivalChoiceBulbasaur;
        long[] rivalChoiceCharmander;
        long[] romName;
        long[] fileIdentfier;
        byte[] currentPokmon = new byte[3];
        long autoScrollLocation;

        BinaryReader reader;
        StreamWriter writer;
        bool isYellow = false;
        bool isGreen = false;

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

            pkmNames = pokemonData.getPokemonNames();
            romName = offsets.getRomName();

            BattlePokemon1.ItemsSource = pokemonData.getPokemonNames();
            BattlePokemon2.ItemsSource = pokemonData.getPokemonNames();
            BattlePokemon3.ItemsSource = pokemonData.getPokemonNames();
            BattlePokemon4.ItemsSource = pokemonData.getPokemonNames();
            BattlePokemon5.ItemsSource = pokemonData.getPokemonNames();
            BattlePokemon6.ItemsSource = pokemonData.getPokemonNames();
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
                else if (readerHelper.getRomVersion(reader, fileIdentfier).Equals("green"))
                {
                    bulbOffsets = offsets.greenBulbasuarOffsets;
                    charmOffsets = offsets.greenCharmanderOffsets;
                    sqrtlOffsets = offsets.greenSquirtleOffsets;
                    isGreen = true;
                    setupForGreen();
                }

                StarterEditWindow.Title = "Starter Edit | " + readerHelper.getNameOfRomLoaded(reader, romName); // Sets rom name as title
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

                if (writeHelper.checkInputs(LevelBox, LevelBox2, LevelBox3, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6, Pikachu_LevelBox)) // If inputs aren't valid correct them
                {
                    if (isYellow)
                    {
                        writeHelper.writeStarterPokemon(pokemonYellowOffsets.yellowFirstBattle, writer, NameList2.SelectedIndex, 1); // Writes the users first pokemon
                        writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowFirstBattleUserLvl, writer, Pikachu_LevelBox); // writes the users first level
                        writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowFirstBattleRivalLvl, writer, LevelBox2); // writes the rivals levels for the first battle

                        writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowFirstBattleRival, writer, NameList5.SelectedIndex);

                        canSaveYellow(BattleLocations.SelectedIndex, BattlePokemon1.SelectedIndex, BattlePokemon2.SelectedIndex, BattlePokemon3.SelectedIndex,
                            BattlePokemon4.SelectedIndex, BattlePokemon5.SelectedIndex, BattlePokemon6.SelectedIndex);
                    }
                    else if (isGreen)
                    {
                        writeHelper.writeStarterPokemon(sqrtlOffsets, writer, NameList.SelectedIndex, 0);
                        writeHelper.writeStarterPokemon(bulbOffsets, writer, NameList2.SelectedIndex, 1);
                        writeHelper.writeStarterPokemon(charmOffsets, writer, NameList3.SelectedIndex, 2);

                        writeHelper.writeBattleLvls(firstBattleLevels, writer, LevelBox, LevelBox2, LevelBox3); // writes the rivals levels for the first battle
                        writeHelper.writeBattlePkm(firstBattlePokemon, writer, NameList4.SelectedIndex, NameList5.SelectedIndex, NameList6.SelectedIndex);
                        writeHelper.writePatches(autoScrollLocation, writer, autoScroll);

                        canSaveChoiceSquirtle(BattleLocations.SelectedIndex, BattlePokemon1.SelectedIndex, BattlePokemon2.SelectedIndex, BattlePokemon3.SelectedIndex,
                            BattlePokemon4.SelectedIndex, BattlePokemon5.SelectedIndex, BattlePokemon6.SelectedIndex);

                        canSaveChoiceBulbasaur(BattleLocations.SelectedIndex, BattlePokemon1.SelectedIndex, BattlePokemon2.SelectedIndex, BattlePokemon3.SelectedIndex,
                            BattlePokemon4.SelectedIndex, BattlePokemon5.SelectedIndex, BattlePokemon6.SelectedIndex);

                        canSaveChoiceCharmander(BattleLocations.SelectedIndex, BattlePokemon1.SelectedIndex, BattlePokemon2.SelectedIndex, BattlePokemon3.SelectedIndex,
                            BattlePokemon4.SelectedIndex, BattlePokemon5.SelectedIndex, BattlePokemon6.SelectedIndex);
                    }
                    else // Red and Blue
                    {
                        writeHelper.writeStarterPokemon(sqrtlOffsets, writer, NameList.SelectedIndex, 0);
                        writeHelper.writeStarterPokemon(bulbOffsets, writer, NameList2.SelectedIndex, 1);
                        writeHelper.writeStarterPokemon(charmOffsets, writer, NameList3.SelectedIndex, 2);

                        writeHelper.writeBattleLvls(offsets.FirstBattleLevels, writer, LevelBox, LevelBox2, LevelBox3); // writes the rivals levels for the first battle
                        writeHelper.writeBattlePkm(offsets.FirstBattlePokemon, writer, NameList4.SelectedIndex, NameList5.SelectedIndex, NameList6.SelectedIndex);
                        writeHelper.writePatches(offsets.autoScroll, writer, autoScroll);

                        canSaveChoiceSquirtle(BattleLocations.SelectedIndex, BattlePokemon1.SelectedIndex, BattlePokemon2.SelectedIndex, BattlePokemon3.SelectedIndex,
                            BattlePokemon4.SelectedIndex, BattlePokemon5.SelectedIndex, BattlePokemon6.SelectedIndex);

                        canSaveChoiceBulbasaur(BattleLocations.SelectedIndex, BattlePokemon1.SelectedIndex, BattlePokemon2.SelectedIndex, BattlePokemon3.SelectedIndex,
                            BattlePokemon4.SelectedIndex, BattlePokemon5.SelectedIndex, BattlePokemon6.SelectedIndex);

                        canSaveChoiceCharmander(BattleLocations.SelectedIndex, BattlePokemon1.SelectedIndex, BattlePokemon2.SelectedIndex, BattlePokemon3.SelectedIndex,
                            BattlePokemon4.SelectedIndex, BattlePokemon5.SelectedIndex, BattlePokemon6.SelectedIndex);
                    }
                    MessageBox.Show("Changes saved succesfully", "Changes saved");
                }

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
            playerChoice.IsChecked = true;

            readerHelper.readBattleLvls(offsets.FirstBattleLevels, reader, LevelBox, LevelBox2, LevelBox3);
            autoScroll.IsChecked = readerHelper.readPatches(offsets.autoScroll, reader);
        }

        public void setupForGreen()
        {

            playersChoiceSquirtle.setOffsetsIfGreen(isGreen);
            playersChoiceBulbasaur.setOffsetsIfGreen(isGreen);
            playersChoiceCharmander.setOffsetsIfGreen(isGreen);

            readStarterPokemon(sqrtlOffsets, reader, Starter1, NameList, 0);
            readStarterPokemon(bulbOffsets, reader, Starter2, NameList2, 1);
            readStarterPokemon(charmOffsets, reader, Starter3, NameList3, 2);

            firstBattleLevels = offsets.greenFirstBattleLevels;
            firstBattlePokemon = offsets.greenFirstBattlePokemon;
            rivalChoiceSquirtle = offsets.greenSquirtleOffsets;
            rivalChoiceBulbasaur = offsets.greenBulbasuarOffsets;
            rivalChoiceCharmander = offsets.greenCharmanderOffsets;
            autoScrollLocation = offsets.greenAutoScroll;

            readStarterPokemon(rivalChoiceSquirtle, reader, RivalStarter, NameList4, 0);
            readStarterPokemon(rivalChoiceBulbasaur, reader, RivalStarter2, NameList5, 1);
            readStarterPokemon(rivalChoiceCharmander, reader, RivalStarter3, NameList6, 2);

            playerChoice.IsEnabled = true;
            playerChoice2.IsEnabled = true;
            playerChoice3.IsEnabled = true;
            playerChoice.IsChecked = true;

            readerHelper.readBattleLvls(firstBattleLevels, reader, LevelBox, LevelBox2, LevelBox3);
            autoScroll.IsChecked = readerHelper.readPatches(autoScrollLocation, reader);
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

                readStarterPokemon(pokemonYellowOffsets.yellowFirstBattle, reader, Starter2, NameList2, 1);
                patches.Visibility = Visibility.Hidden;

                playerChoice_Pikachu.Visibility = Visibility.Visible;
                playerChoice_Pikachu.IsChecked = true;
                hideRadioButtons();
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

                    if (isYellow)
                    {
                        hideCaseRadioButtons();
                    }
                    refershData(getCurrentChoice());
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

                    if (isYellow)
                    {
                        hideCaseRadioButtons();
                    }
                    refershData(getCurrentChoice());
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

                    if (isYellow)
                    {
                        hideCaseRadioButtons();
                    }
                    refershData(getCurrentChoice());
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

                    if (isYellow)
                    {
                        showCaseRadioButtons();
                        refershData(getCurrentCase());
                    } else
                    {
                        refershData(getCurrentChoice());
                    }
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

                    if (isYellow)
                    {
                        showCaseRadioButtons();
                        refershData(getCurrentCase());
                    } else
                    {
                        refershData(getCurrentChoice());
                    }
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

                    if (isYellow)
                    {
                        showCaseRadioButtons();
                        refershData(getCurrentCase());
                    }
                    else
                    {
                        refershData(getCurrentChoice());
                    }
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

                    if (isYellow)
                    {
                        showCaseRadioButtons();
                        refershData(getCurrentCase());
                    }
                    else
                    {
                        refershData(getCurrentChoice());
                    }
                    break;
            }
        }

        public RadioButton getCurrentChoice()
        {
            if (isSquirtleChecked())
            {
                return playerChoice;
            }
            if (isBulbasuarChecked())
            {
                return playerChoice2;
            }
            if (isCharmanderChecked())
            {
                return playerChoice3;
            }
            if (isPikachuChecked() && isYellow)
            {
                return playerChoice_Pikachu;
            }
            return null;
        }

        public RadioButton getCurrentCase()
        {
            if (isYellow)
            {
                if (isCase1Checked())
                {
                    return case_1;
                }

                if (isCase2Checked())
                {
                    return case_2;
                }

                if (isCase3Checked())
                {
                    return case_3;
                }
            }
            case_1.IsChecked = true; // when none are selected select case 1
            return case_1;
        }

        private void playerChoice_Checked(object sender, RoutedEventArgs e) // squirtle radio button
        {
            int battleSelected = BattleLocations.SelectedIndex;

            if (isSquirtleChecked() && battleSelected == 0)
            {
                playerChoice2.SetCurrentValue(RadioButton.IsCheckedProperty, false);
                playerChoice3.SetCurrentValue(RadioButton.IsCheckedProperty, false);

                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattleRoute22Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattleRoute22Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);

            }
            else if (isSquirtleChecked() && battleSelected == 1) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattleCeruleanCityLvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattleCeruleanCityPkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (battleSelected == 2) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattleSSAnneLvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattleSSAnnePkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (battleSelected == 3) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattlePokemonTowerLvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattlePokemonTowerPkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (battleSelected == 4) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattleSilphCoLvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattleSilphCoPkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (battleSelected == 5) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattleRoute22Lvl2, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattleRoute22Pkm2, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (battleSelected == 6) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceSquirtle.squirtleBattleIndigoPlateauLvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceSquirtle.squirtleBattleIndigoPlateauPkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
        }

        private void playerChoice2_Checked(object sender, RoutedEventArgs e) // bulbasaur radio button
        {
            int battleSelected = BattleLocations.SelectedIndex;

            if (playerChoice2.IsChecked == true && battleSelected == 0)
            {
                playerChoice.SetCurrentValue(RadioButton.IsCheckedProperty, false);
                playerChoice3.SetCurrentValue(RadioButton.IsCheckedProperty, false);

                readerHelper.readBattleLvls(playersChoiceBulbasaur.bulbasaurBattleRoute22Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceBulbasaur.bulbasaurBattleRoute22Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (battleSelected == 1) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceBulbasaur.bulbasaurBattleCeruleanCityLvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceBulbasaur.bulbasaurBattleCeruleanCityPkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (battleSelected == 2) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceBulbasaur.bulbasaurBattleSSAnneLvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceBulbasaur.bulbasaurBattleSSAnnePkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (battleSelected == 3)
            {
                readerHelper.readBattleLvls(playersChoiceBulbasaur.bulbasaurBattlePokemonTowerLvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceBulbasaur.bulbasaurBattlePokemonTowerPkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (battleSelected == 4) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceBulbasaur.bulbasaurBattleSilphCoLvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceBulbasaur.bulbasaurBattleSilphCoPkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (battleSelected == 5) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceBulbasaur.bulbasaurBattleRoute22Lvl2, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceBulbasaur.bulbasaurBattleRoute22Pkm2, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (battleSelected == 6) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceBulbasaur.bulbasaurBattleIndigoPlateauLvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceBulbasaur.bulbasaurBattleIndigoPlateauPkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
        }

        private void playerChoice3_Checked(object sender, RoutedEventArgs e) // charmander radio button 
        {
            int battleSelected = BattleLocations.SelectedIndex;
            if (playerChoice3.IsChecked == true && battleSelected == 0)
            {
                playerChoice2.SetCurrentValue(RadioButton.IsCheckedProperty, false);
                playerChoice.SetCurrentValue(RadioButton.IsCheckedProperty, false);

                readerHelper.readBattleLvls(playersChoiceCharmander.charmanderBattleRoute22Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceCharmander.charmanderBattleRoute22Pkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
                
            }
            else if (battleSelected == 1) // if another battle location is chosen, read from those offsets
            {
                readerHelper.readBattleLvls(playersChoiceCharmander.charmanderBattleCeruleanCityLvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceCharmander.charmanderBattleCeruleanCityPkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            } 
            else if (battleSelected == 2)
            {
                readerHelper.readBattleLvls(playersChoiceCharmander.charmanderBattleSSAnneLvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceCharmander.charmanderBattleSSAnnePkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (battleSelected == 3)
            {
                readerHelper.readBattleLvls(playersChoiceCharmander.charmanderBattlePokemonTowerLvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceCharmander.charmanderBattlePokemonTowerPkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (battleSelected == 4)
            {
                readerHelper.readBattleLvls(playersChoiceCharmander.charmanderBattleSilphCoLvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceCharmander.charmanderBattleSilphCoPkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (battleSelected == 5)
            {
                readerHelper.readBattleLvls(playersChoiceCharmander.charmanderBattleRoute22Lvl2, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceCharmander.charmanderBattleRoute22Pkm2, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (battleSelected == 6)
            {
                readerHelper.readBattleLvls(playersChoiceCharmander.charmanderBattleIndigoPlateauLvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(playersChoiceCharmander.charmanderBattleIndigoPlateauPkm, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }

        }

        private void playerChoice_Checked_Pikachu(object sender, RoutedEventArgs e) // pikachu
        {
            int battleSelected = BattleLocations.SelectedIndex; 

            if (isPikachuChecked() && battleSelected == 0 && isYellow)
            {
                readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleRoute22Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleRoute22PKM, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (isPikachuChecked() && battleSelected == 1 && isYellow)
            {
                readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleCeruleanCityLvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleCeruleanCityPKM, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
            else if (isPikachuChecked() && battleSelected == 2 && isYellow)
            {
                readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleSSAnneLvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleSSAnnePKM, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
            }
        }

        public void canSaveChoiceSquirtle(int battleSelected, int pkm1, int pkm2, int pkm3, int pkm4, int pkm5, int pkm6)
        {
            if (isSquirtleChecked() && battleSelected == 0) // route 22
            {
                writeHelper.writeBattlePkm(playersChoiceSquirtle.squirtleBattleRoute22Pkm, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceSquirtle.squirtleBattleRoute22Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
            else if (isSquirtleChecked() && battleSelected == 1)  // cerulean city
            {
                writeHelper.writeBattlePkm(playersChoiceSquirtle.squirtleBattleCeruleanCityPkm, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceSquirtle.squirtleBattleCeruleanCityLvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
            else if (isSquirtleChecked() && battleSelected == 2) // SS Anne
            {
                writeHelper.writeBattlePkm(playersChoiceSquirtle.squirtleBattleSSAnnePkm, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceSquirtle.squirtleBattleSSAnneLvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
            else if (isSquirtleChecked() && battleSelected == 3) // pokemon tower
            {
                writeHelper.writeBattlePkm(playersChoiceSquirtle.squirtleBattlePokemonTowerPkm, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceSquirtle.squirtleBattlePokemonTowerLvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
            else if (isSquirtleChecked() && battleSelected == 4) // silph co
            {
                writeHelper.writeBattlePkm(playersChoiceSquirtle.squirtleBattleSilphCoPkm, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceSquirtle.squirtleBattleSilphCoLvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
            else if (isSquirtleChecked() && battleSelected == 5) // route 22, 2nd battle
            {
                writeHelper.writeBattlePkm(playersChoiceSquirtle.squirtleBattleRoute22Pkm2, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceSquirtle.squirtleBattleRoute22Lvl2, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
            else if (isSquirtleChecked() && battleSelected == 6) // indigo plateau
            {
                writeHelper.writeBattlePkm(playersChoiceSquirtle.squirtleBattleIndigoPlateauPkm, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceSquirtle.squirtleBattleIndigoPlateauLvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
        }

        public void canSaveChoiceBulbasaur(int battleSelected, int pkm1, int pkm2, int pkm3, int pkm4, int pkm5, int pkm6)
        {
            if (isBulbasuarChecked() && battleSelected == 0) // route 22
            {
                writeHelper.writeBattlePkm(playersChoiceBulbasaur.bulbasaurBattleRoute22Pkm, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceBulbasaur.bulbasaurBattleRoute22Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
            else if (isBulbasuarChecked() && battleSelected == 1)  // cerulean city
            {
                writeHelper.writeBattlePkm(playersChoiceBulbasaur.bulbasaurBattleCeruleanCityPkm, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceBulbasaur.bulbasaurBattleCeruleanCityLvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
            else if (isBulbasuarChecked() && battleSelected == 2) // SS Anne
            {
                writeHelper.writeBattlePkm(playersChoiceBulbasaur.bulbasaurBattleSSAnnePkm, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceBulbasaur.bulbasaurBattleSSAnneLvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
            else if (isBulbasuarChecked() && battleSelected == 3) // pokemon tower
            {
                writeHelper.writeBattlePkm(playersChoiceBulbasaur.bulbasaurBattlePokemonTowerPkm, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceBulbasaur.bulbasaurBattlePokemonTowerLvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
            else if (isBulbasuarChecked() && battleSelected == 4) // silph co
            {
                writeHelper.writeBattlePkm(playersChoiceBulbasaur.bulbasaurBattleSilphCoPkm, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceBulbasaur.bulbasaurBattleSilphCoLvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
            else if (isBulbasuarChecked() && battleSelected == 5) // route 22, 2nd battle
            {
                writeHelper.writeBattlePkm(playersChoiceBulbasaur.bulbasaurBattleRoute22Pkm2, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceBulbasaur.bulbasaurBattleRoute22Lvl2, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
            else if (isBulbasuarChecked() && battleSelected == 6) // indigo plateau
            {
                writeHelper.writeBattlePkm(playersChoiceBulbasaur.bulbasaurBattleIndigoPlateauPkm, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceBulbasaur.bulbasaurBattleIndigoPlateauLvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
        }

        public void canSaveChoiceCharmander(int battleSelected, int pkm1, int pkm2, int pkm3, int pkm4, int pkm5, int pkm6)
        {
            if (isCharmanderChecked() && battleSelected == 0) // route 22
            {
                writeHelper.writeBattlePkm(playersChoiceCharmander.charmanderBattleRoute22Pkm, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceCharmander.charmanderBattleRoute22Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
            else if (isCharmanderChecked() && battleSelected == 1)  // cerulean city
            {
                writeHelper.writeBattlePkm(playersChoiceCharmander.charmanderBattleCeruleanCityPkm, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceCharmander.charmanderBattleCeruleanCityLvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
            else if (isCharmanderChecked() && battleSelected == 2) // SS Anne
            {
                writeHelper.writeBattlePkm(playersChoiceCharmander.charmanderBattleSSAnnePkm, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceCharmander.charmanderBattleSSAnneLvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
            else if (isCharmanderChecked() && battleSelected == 3) // pokemon tower
            {
                writeHelper.writeBattlePkm(playersChoiceCharmander.charmanderBattlePokemonTowerPkm, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceCharmander.charmanderBattlePokemonTowerLvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
            else if (isCharmanderChecked() && battleSelected == 4) // silph co
            {
                writeHelper.writeBattlePkm(playersChoiceCharmander.charmanderBattleSilphCoPkm, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceCharmander.charmanderBattleSilphCoLvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
            else if (isCharmanderChecked() && battleSelected == 5) // route 22, 2nd battle
            {
                writeHelper.writeBattlePkm(playersChoiceCharmander.charmanderBattleRoute22Pkm2, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceCharmander.charmanderBattleRoute22Lvl2, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
            else if (isCharmanderChecked() && battleSelected == 6) // indigo plateau
            {
                writeHelper.writeBattlePkm(playersChoiceCharmander.charmanderBattleIndigoPlateauPkm, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(playersChoiceCharmander.charmanderBattleIndigoPlateauLvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            }
        }

        public void canSaveYellow(int battleSelected, int pkm1, int pkm2, int pkm3, int pkm4, int pkm5, int pkm6)
        {
            //pkm1 - pkm6 is the choice index selected
            if (isPikachuChecked() && battleSelected == 0) // route 22
            {
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleRoute22PKM, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleRoute22Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
            } 
            else if (isPikachuChecked() && battleSelected == 1) // cerulean city
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleCeruleanCityLvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleCeruleanCityPKM, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
            }
            else if (isPikachuChecked() && battleSelected == 2) // SS Anne
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleSSAnneLvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleSSAnnePKM, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
            }
            else if (isPikachuChecked() && battleSelected == 3 && isCase1Checked()) // Pokemon tower Case 1
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC1Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC1PKM, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 3 && isCase2Checked()) // Pokemon tower Case 2
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC2Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC2PKM, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 3 && isCase3Checked()) // Pokemon tower Case 3
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC3Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC3PKM, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 4 && isCase1Checked()) // Silph Co. Case 1
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleSilphCoC1Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleSilphCoC1PKM, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 4 && isCase2Checked()) // Silph Co. Case 2
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleSilphCoC2Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleSilphCoC2PKM, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 4 && isCase3Checked()) // Silph Co. Case 3
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleSilphCoC3Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleSilphCoC3PKM, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 5 && isCase1Checked()) // Route 22 (2) case 1
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleRoute22C1Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleRoute22C1PKM, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 5 && isCase2Checked()) // Route 22 (2) case 2
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleRoute22C2Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleRoute22C2PKM, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 5 && isCase3Checked()) // Route 22 (2) case 3
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleRoute22C3Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleRoute22C3PKM, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 6 && isCase1Checked()) // Indigo Plateau case 1
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC1Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC1PKM, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 6 && isCase2Checked()) // Indigo Plateau case 2
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC2Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC2PKM, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 6 && isCase3Checked()) // Indigo Plateau case 3
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC3Lvl, writer, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC3PKM, writer, pkm1, pkm2, pkm3, pkm4, pkm5, pkm6);
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

        public Boolean isCase1Checked()
        {
            return (bool)case_1.IsChecked;
        }

        public Boolean isCase2Checked()
        {
            return (bool)case_2.IsChecked;
        }

        public Boolean isCase3Checked()
        {
            return (bool)case_3.IsChecked;
        }

        public Boolean isPikachuChecked()
        {
            return (bool)playerChoice_Pikachu.IsChecked;
        }

        public void hideRadioButtons()
        {
            playerChoice.Visibility = Visibility.Hidden;
            playerChoice2.Visibility = Visibility.Hidden;
            playerChoice3.Visibility = Visibility.Hidden;
        }

        public void showCaseRadioButtons()
        {
            case_1.Visibility = Visibility.Visible;
            case_2.Visibility = Visibility.Visible;
            case_3.Visibility = Visibility.Visible;
            case_1.IsEnabled = true;
            case_2.IsEnabled = true;
            case_3.IsEnabled = true;
        }

        public void hideCaseRadioButtons()
        {
            case_1.Visibility = Visibility.Hidden;
            case_2.Visibility = Visibility.Hidden;
            case_3.Visibility = Visibility.Hidden;
            case_1.IsEnabled = false;
            case_2.IsEnabled = false;
            case_3.IsEnabled = false;
        }

        public void refershData(RadioButton currentChoice)
        {
            if (currentChoice == null)
            {
                return;
            } 
            else
            {
                currentChoice.IsChecked = false;
                currentChoice.IsChecked = true;
            }
        }

        private void case_1_Checked(object sender, RoutedEventArgs e)
        {
            if (isYellow)
            {
                int battleSelected = BattleLocations.SelectedIndex;

                if (battleSelected == 3)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC1Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC1PKM, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
                }
                else if (battleSelected == 4)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleSilphCoC1Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleSilphCoC1PKM, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
                }
                else if (battleSelected == 5)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleRoute22C1Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleRoute22C1PKM, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
                }
                else if (battleSelected == 6)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC1Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC1PKM, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
                }
            }
        }

        private void case_2_Checked(object sender, RoutedEventArgs e)
        {
            if (isYellow)
            {
                int battleSelected = BattleLocations.SelectedIndex;

                if (battleSelected == 3)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC2Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC2PKM, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
                }
                else if (battleSelected == 4)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleSilphCoC2Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleSilphCoC2PKM, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
                }
                else if (battleSelected == 5)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleRoute22C2Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleRoute22C2PKM, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
                }
                else if (battleSelected == 6)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC2Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC2PKM, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
                }
            }
        }

        private void case_3_Checked(object sender, RoutedEventArgs e)
        {
            if (isYellow)
            {
                int battleSelected = BattleLocations.SelectedIndex;

                if (battleSelected == 3)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC3Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC3PKM, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
                } 
                else if (battleSelected == 4)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleSilphCoC3Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleSilphCoC3PKM, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
                }
                else if (battleSelected == 5)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleRoute22C3Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleRoute22C3PKM, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
                }
                else if (battleSelected == 6)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC3Lvl, reader, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6);
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC3PKM, reader, BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6);
                }
            }
        }
    }    
}
