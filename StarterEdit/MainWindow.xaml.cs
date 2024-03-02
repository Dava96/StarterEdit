using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using StarterEdit;
using System.Collections.Generic;
using Microsoft.VisualBasic;

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

        Squirtle squirtle = new Squirtle();
        Bulbasaur bulbasaur = new Bulbasaur();
        Charmander charmander = new Charmander();
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

            BattleLocations.ItemsSource = Enum.GetValues(typeof(BattleName)); // gets battle locations

            fileIdentfier = offsets.getFileIdentifier();

            pkmNames = pokemonData.getPokemonNames();
            romName = offsets.getRomName();

            BattlePokemon1.ItemsSource = pokemonData.getPokemonNames();
            BattlePokemon2.ItemsSource = pokemonData.getPokemonNames();
            BattlePokemon3.ItemsSource = pokemonData.getPokemonNames();
            BattlePokemon4.ItemsSource = pokemonData.getPokemonNames();
            BattlePokemon5.ItemsSource = pokemonData.getPokemonNames();
            BattlePokemon6.ItemsSource = pokemonData.getPokemonNames();

            // long a = bulbasaur.getBattle(BattleName.Route22_1)[DataType.Pokemon][0];
            // // var aa = a["Pokemon"][0];
            // MessageBox.Show($"{Enum.GetValues(typeof(BattleName))}");
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
                    setupForRed();
                }
                else if (readerHelper.getRomVersion(reader, fileIdentfier).Equals("blue"))
                {
                    setupForBlue();

                } else if (readerHelper.getRomVersion(reader, fileIdentfier).Equals("yellow"))
                {
                    isYellow = true;
                    setupForYellow();
                }
                else if (readerHelper.getRomVersion(reader, fileIdentfier).Equals("green"))
                {
                    isGreen = true;
                    setupForGreen();
                }
                UiLoad();
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
                string hexVal = string.Format("{0:X}", reader.ReadByte());
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

                int[] pokemonArray = new int[] { BattlePokemon1.SelectedIndex, BattlePokemon2.SelectedIndex, BattlePokemon3.SelectedIndex,
                            BattlePokemon4.SelectedIndex, BattlePokemon5.SelectedIndex, BattlePokemon6.SelectedIndex};
                if (writeHelper.checkInputs(LevelBox, LevelBox2, LevelBox3, BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6, Pikachu_LevelBox)) // If inputs aren't valid correct them
                {
                    if (isYellow)
                    {
                        writeHelper.writeStarterPokemon(pokemonYellowOffsets.yellowFirstBattle, writer, NameList2.SelectedIndex, 1); // Writes the users first pokemon
                        writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowFirstBattleUserLvl, writer, Pikachu_LevelBox); // writes the users first level
                        writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowFirstBattleRivalLvl, writer, LevelBox2); // writes the rivals levels for the first battle

                        writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowFirstBattleRival, writer, NameList5.SelectedIndex);

                        canSaveYellow(BattleLocations.SelectedIndex, pokemonArray);
                    }
                    else
                    {
                        writeHelper.writeStarterPokemon(sqrtlOffsets, writer, NameList.SelectedIndex, 0);
                        writeHelper.writeStarterPokemon(bulbOffsets, writer, NameList2.SelectedIndex, 1);
                        writeHelper.writeStarterPokemon(charmOffsets, writer, NameList3.SelectedIndex, 2);

                        writeHelper.writeBattleLvls(firstBattleLevels, writer, getLevelBoxes()); // writes the rivals levels for the first battle
                        writeHelper.writeBattlePkm(firstBattlePokemon, writer, NameList4.SelectedIndex, NameList5.SelectedIndex, NameList6.SelectedIndex);
                        writeHelper.writePatches(autoScrollLocation, writer, autoScroll);

                        canSaveChoiceSquirtle(BattleLocations.SelectedIndex, pokemonArray);

                        canSaveChoiceBulbasaur(BattleLocations.SelectedIndex, pokemonArray);

                        canSaveChoiceCharmander(BattleLocations.SelectedIndex, pokemonArray);
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

        public void setupForRed()
        {
            bulbOffsets = offsets.getBulbasuarOffsets(); // load blue offsets
            charmOffsets = offsets.getCharmanderOffsets();
            sqrtlOffsets = offsets.getSquirtleOffsets();

            firstBattleLevels = offsets.FirstBattleLevels;
            firstBattlePokemon = offsets.FirstBattlePokemon;
            rivalChoiceSquirtle = offsets.rivalsChoice1; // bulb
            rivalChoiceBulbasaur = offsets.rivalsChoice2; // charmander
            rivalChoiceCharmander = offsets.rivalsChoice3; // squirtle
            autoScrollLocation = offsets.autoScroll;

            choiceSquirtle.IsEnabled = true;
            choiceBulbasaur.IsEnabled = true;
            choiceCharmander.IsEnabled = true;
            choiceSquirtle.IsChecked = true;
        }

        public void setupForBlue()
        {
            bulbOffsets = offsets.getBlueBulbasaurOffsets(); // load blue offsets
            charmOffsets = offsets.getBlueCharmanderOffsets();
            sqrtlOffsets = offsets.getBlueSquirtleOffsets();

            firstBattleLevels = offsets.FirstBattleLevels;
            firstBattlePokemon = offsets.FirstBattlePokemon;
            rivalChoiceSquirtle = offsets.rivalsChoice1 ; // bulb
            rivalChoiceBulbasaur = offsets.rivalsChoice2; // charmander
            rivalChoiceCharmander = offsets.rivalsChoice3; // squirtle
            autoScrollLocation = offsets.autoScroll;

            choiceSquirtle.IsEnabled = true;
            choiceBulbasaur.IsEnabled = true;
            choiceCharmander.IsEnabled = true;
            choiceSquirtle.IsChecked = true;
        }

        public void setupForGreen()
        {
            bulbOffsets = offsets.greenBulbasuarOffsets;
            charmOffsets = offsets.greenCharmanderOffsets;
            sqrtlOffsets = offsets.greenSquirtleOffsets;

            squirtle.setOffsetsIfGreen(isGreen);
            bulbasaur.setOffsetsIfGreen(isGreen);
            charmander.setOffsetsIfGreen(isGreen);

            firstBattleLevels = offsets.greenFirstBattleLevels;
            firstBattlePokemon = offsets.greenFirstBattlePokemon;
            rivalChoiceSquirtle = offsets.greenRivalsChoice1; // bulb
            rivalChoiceBulbasaur = offsets.greenRivalsChoice2; // charmander
            rivalChoiceCharmander = offsets.greenRivalsChoice3; // squirtle
            autoScrollLocation = offsets.greenAutoScroll;

            choiceSquirtle.IsEnabled = true;
            choiceBulbasaur.IsEnabled = true;
            choiceCharmander.IsEnabled = true;
            choiceSquirtle.IsChecked = true;
        }

        public void UiLoad()
        {
            readStarterPokemon(sqrtlOffsets, reader, Starter1, NameList, 0);
            readStarterPokemon(bulbOffsets, reader, Starter2, NameList2, 1);
            readStarterPokemon(charmOffsets, reader, Starter3, NameList3, 2);

            readStarterPokemon(rivalChoiceSquirtle, reader, RivalStarter, NameList4, 0);
            readStarterPokemon(rivalChoiceBulbasaur, reader, RivalStarter2, NameList5, 1);
            readStarterPokemon(rivalChoiceCharmander, reader, RivalStarter3, NameList6, 2);

            choiceSquirtle.IsEnabled = true;
            choiceBulbasaur.IsEnabled = true;
            choiceCharmander.IsEnabled = true;
            choiceSquirtle.IsChecked = true;

            readerHelper.readBattleLvls(firstBattleLevels, reader, getLevelBoxes());
            autoScroll.IsChecked = readerHelper.readPatches(autoScrollLocation, reader);
            StarterEditWindow.Title = "Starter Edit | " + readerHelper.getNameOfRomLoaded(reader, romName); // Sets rom name as title
        }

        public void setupForYellow()
        {
            if (isYellow)
            {
                playerChoice_Pikachu.IsChecked = true;
                Pikachu_Label.Visibility = Visibility.Visible;
                Pikachu_LevelBox.Visibility = Visibility.Visible;
                readStarterPokemon(pokemonYellowOffsets.yellowFirstBattle, reader, Starter2, NameList2, 1);
                readerHelper.readBattleLvls(pokemonYellowOffsets.yellowFirstBattleUserLvl, reader, Pikachu_LevelBox);
                readStarterPokemon(pokemonYellowOffsets.yellowFirstBattleRival, reader, RivalStarter2, NameList5, 1);
                readerHelper.readBattleLvls(pokemonYellowOffsets.yellowFirstBattleRivalLvl, reader, LevelBox2);

                patches.Visibility = Visibility.Hidden;

                playerChoice_Pikachu.Visibility = Visibility.Visible;
         
                NameList.IsEnabled = false;
                NameList3.IsEnabled = false;
                NameList4.IsEnabled = false;
                NameList6.IsEnabled = false;
                LevelBox.IsEnabled = false;
                LevelBox3.IsEnabled = false;
                StarterEditWindow.Title = "Starter Edit | " + readerHelper.getNameOfRomLoaded(reader, romName); // Sets rom name as title
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
                choiceSquirtle.Content = Starter1.Content.ToString();
                choiceBulbasaur.Content = Starter2.Content.ToString();
                choiceCharmander.Content = Starter3.Content.ToString();
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
                return choiceSquirtle;
            }
            if (isBulbasuarChecked())
            {
                return choiceBulbasaur;
            }
            if (isCharmanderChecked())
            {
                return choiceCharmander;
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
            long a = squirtle.getBattle(BattleName.Route22_1)[DataType.Pokemon][0];
            var b = getPokemonBoxes()[0].SelectedIndex;
            // var aa = a["Pokemon"][0];
            MessageBox.Show($"{b}");


            int battleSelected = BattleLocations.SelectedIndex;

            // IPlayersChoice pokemonChoice = getPokemonChoice();

            if (squirtle != null)
            {
                BattleName selectedBattle = (BattleName)battleSelected;
                Dictionary<DataType, long[]> battleData = squirtle.getBattle(selectedBattle);

                readerHelper.readBattleLvls(battleData[DataType.Level], reader, getBattleBoxes());
                readerHelper.readBattlePokemon(battleData[DataType.Pokemon], reader, getPokemonBoxes());
            }
        }

        private void playerChoice2_Checked(object sender, RoutedEventArgs e) // bulbasaur radio button
        {
            // int battleSelected = BattleLocations.SelectedIndex;

            int battleSelected = BattleLocations.SelectedIndex;

            if (bulbasaur != null)
            {
                BattleName selectedBattle = (BattleName)battleSelected;
                Dictionary<DataType, long[]> battleData = bulbasaur.getBattle(selectedBattle);

                readerHelper.readBattleLvls(battleData[DataType.Level], reader, getBattleBoxes());
                readerHelper.readBattlePokemon(battleData[DataType.Pokemon], reader, getPokemonBoxes());
            }
        }

        private void playerChoice3_Checked(object sender, RoutedEventArgs e) // charmander radio button 
        {
            int battleSelected = BattleLocations.SelectedIndex;

            if (charmander != null)
            {
                BattleName selectedBattle = (BattleName)battleSelected;
                Dictionary<DataType, long[]> battleData = charmander.getBattle(selectedBattle);

                readerHelper.readBattleLvls(battleData[DataType.Level], reader, getBattleBoxes());
                readerHelper.readBattlePokemon(battleData[DataType.Pokemon], reader, getPokemonBoxes());
            }
        }

        private void playerChoice_Checked_Pikachu(object sender, RoutedEventArgs e) // pikachu
        {
            int battleSelected = BattleLocations.SelectedIndex; 

            if (isPikachuChecked() && battleSelected == 0 && isYellow)
            {
                readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleRoute22Lvl, reader, getBattleBoxes());
                readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleRoute22PKM, reader, getPokemonBoxes());
            }
            else if (isPikachuChecked() && battleSelected == 1 && isYellow)
            {
                readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleCeruleanCityLvl, reader, getBattleBoxes());
                readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleCeruleanCityPKM, reader, getPokemonBoxes());
            }
            else if (isPikachuChecked() && battleSelected == 2 && isYellow)
            {
                readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleSSAnneLvl, reader, getBattleBoxes());
                readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleSSAnnePKM, reader, getPokemonBoxes());
            }
        }

        public void canSaveChoiceSquirtle(int battleSelected, int[] pokemonArray)
        {
            IPlayersChoice pokemonChoice = getPokemonChoice();

            if (pokemonChoice != null)
            {
                BattleName selectedBattle = (BattleName)battleSelected;
                Dictionary<DataType, long[]> battleData = pokemonChoice.getBattle(selectedBattle);

                writeHelper.writeBattleLvls(battleData[DataType.Level], writer, getBattleBoxes());
                writeHelper.writeBattlePkm(battleData[DataType.Pokemon], writer, pokemonArray);
            }
        }

        public void canSaveChoiceBulbasaur(int battleSelected, int[] pokemonArray)
        {
           IPlayersChoice pokemonChoice = getPokemonChoice();

            if (pokemonChoice != null)
            {
                BattleName selectedBattle = (BattleName)battleSelected;
                Dictionary<DataType, long[]> battleData = pokemonChoice.getBattle(selectedBattle);

                writeHelper.writeBattleLvls(battleData[DataType.Level], writer, getBattleBoxes());
                writeHelper.writeBattlePkm(battleData[DataType.Pokemon], writer, pokemonArray);
            }
        }

        public void canSaveChoiceCharmander(int battleSelected, int[] pokemonArray)
        {
            IPlayersChoice pokemonChoice = getPokemonChoice();

            if (pokemonChoice != null)
            {
                BattleName selectedBattle = (BattleName)battleSelected;
                Dictionary<DataType, long[]> battleData = pokemonChoice.getBattle(selectedBattle);

                writeHelper.writeBattleLvls(battleData[DataType.Level], writer, getBattleBoxes());
                writeHelper.writeBattlePkm(battleData[DataType.Pokemon], writer, pokemonArray);
            }
        }

        public void canSaveYellow(int battleSelected, int[] pokemonArray)
        {
            //pkm1 - pkm6 is the choice index selected
            if (isPikachuChecked() && battleSelected == 0) // route 22
            {
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleRoute22PKM, writer, pokemonArray);
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleRoute22Lvl, writer, getBattleBoxes());
            } 
            else if (isPikachuChecked() && battleSelected == 1) // cerulean city
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleCeruleanCityLvl, writer, getBattleBoxes());
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleCeruleanCityPKM, writer, pokemonArray);
            }
            else if (isPikachuChecked() && battleSelected == 2) // SS Anne
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleSSAnneLvl, writer, getBattleBoxes());
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleSSAnnePKM, writer, pokemonArray);
            }
            else if (isPikachuChecked() && battleSelected == 3 && isCase1Checked()) // Pokemon tower Case 1
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC1Lvl, writer, getBattleBoxes());
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC1PKM, writer, pokemonArray);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 3 && isCase2Checked()) // Pokemon tower Case 2
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC2Lvl, writer, getBattleBoxes());
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC2PKM, writer, pokemonArray);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 3 && isCase3Checked()) // Pokemon tower Case 3
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC3Lvl, writer, getBattleBoxes());
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC3PKM, writer, pokemonArray);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 4 && isCase1Checked()) // Silph Co. Case 1
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleSilphCoC1Lvl, writer, getBattleBoxes());
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleSilphCoC1PKM, writer, pokemonArray);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 4 && isCase2Checked()) // Silph Co. Case 2
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleSilphCoC2Lvl, writer, getBattleBoxes());
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleSilphCoC2PKM, writer, pokemonArray);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 4 && isCase3Checked()) // Silph Co. Case 3
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleSilphCoC3Lvl, writer, getBattleBoxes());
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleSilphCoC3PKM, writer, pokemonArray);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 5 && isCase1Checked()) // Route 22 (2) case 1
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleRoute22C1Lvl, writer, getBattleBoxes());
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleRoute22C1PKM, writer, pokemonArray);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 5 && isCase2Checked()) // Route 22 (2) case 2
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleRoute22C2Lvl, writer, getBattleBoxes());
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleRoute22C2PKM, writer, pokemonArray);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 5 && isCase3Checked()) // Route 22 (2) case 3
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleRoute22C3Lvl, writer, getBattleBoxes());
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleRoute22C3PKM, writer, pokemonArray);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 6 && isCase1Checked()) // Indigo Plateau case 1
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC1Lvl, writer, getBattleBoxes());
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC1PKM, writer, pokemonArray);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 6 && isCase2Checked()) // Indigo Plateau case 2
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC2Lvl, writer, getBattleBoxes());
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC2PKM, writer, pokemonArray);
            }
            else if (isPikachuChecked() && BattleLocations.SelectedIndex == 6 && isCase3Checked()) // Indigo Plateau case 3
            {
                writeHelper.writeBattleLvls(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC3Lvl, writer, getBattleBoxes());
                writeHelper.writeBattlePkm(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC3PKM, writer, pokemonArray);
            }
        }

        public Boolean isSquirtleChecked()
        {
            return (bool)choiceSquirtle.IsChecked;
        }

        public IPlayersChoice getPokemonChoice()
        {
            if (isSquirtleChecked()) {
                resetRadioButtons(choiceBulbasaur, choiceCharmander);
                return squirtle;
            }

            if (isBulbasuarChecked()) {
                resetRadioButtons(choiceSquirtle, choiceCharmander);
                return bulbasaur;
            }

            if (isCharmanderChecked()) {
                resetRadioButtons(choiceSquirtle, choiceBulbasaur);
                return charmander;
            }

            return null;
        }


        public void resetRadioButtons(RadioButton choice1, RadioButton choice2)
        {
            choice1.SetCurrentValue(RadioButton.IsCheckedProperty, false);
            choice2.SetCurrentValue(RadioButton.IsCheckedProperty, false);
        }

        public Boolean isBulbasuarChecked()
        {
            return (bool)choiceBulbasaur.IsChecked;
        }

        public Boolean isCharmanderChecked()
        {
            return (bool)choiceCharmander.IsChecked;
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
            choiceSquirtle.Visibility = Visibility.Hidden;
            choiceBulbasaur.Visibility = Visibility.Hidden;
            choiceCharmander.Visibility = Visibility.Hidden;
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
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC1Lvl, reader, getBattleBoxes());
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC1PKM, reader, getPokemonBoxes());
                }
                else if (battleSelected == 4)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleSilphCoC1Lvl, reader, getBattleBoxes());
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleSilphCoC1PKM, reader, getPokemonBoxes());
                }
                else if (battleSelected == 5)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleRoute22C1Lvl, reader, getBattleBoxes());
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleRoute22C1PKM, reader, getPokemonBoxes());
                }
                else if (battleSelected == 6)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC1Lvl, reader, getBattleBoxes());
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC1PKM, reader, getPokemonBoxes());
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
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC2Lvl, reader, getBattleBoxes());
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC2PKM, reader, getPokemonBoxes());
                }
                else if (battleSelected == 4)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleSilphCoC2Lvl, reader, getBattleBoxes());
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleSilphCoC2PKM, reader, getPokemonBoxes());
                }
                else if (battleSelected == 5)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleRoute22C2Lvl, reader, getBattleBoxes());
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleRoute22C2PKM, reader, getPokemonBoxes());
                }
                else if (battleSelected == 6)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC2Lvl, reader, getBattleBoxes());
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC2PKM, reader, getPokemonBoxes());
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
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC3Lvl, reader, getBattleBoxes());
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattlePokemonTowerC3PKM, reader, getPokemonBoxes());
                } 
                else if (battleSelected == 4)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleSilphCoC3Lvl, reader, getBattleBoxes());
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleSilphCoC3PKM, reader, getPokemonBoxes());
                }
                else if (battleSelected == 5)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleRoute22C3Lvl, reader, getBattleBoxes());
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleRoute22C3PKM, reader, getPokemonBoxes());
                }
                else if (battleSelected == 6)
                {
                    readerHelper.readBattleLvls(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC3Lvl, reader, getBattleBoxes());
                    readerHelper.readBattlePokemon(pokemonYellowOffsets.yellowRivalBattleIndigoPlateauC3PKM, reader, getPokemonBoxes());
                }
            }
        }

        public ComboBox[] getPokemonBoxes()
        {
            return new ComboBox[] { BattlePokemon1, BattlePokemon2, BattlePokemon3, BattlePokemon4, BattlePokemon5, BattlePokemon6 };
        }

        public TextBox[] getBattleBoxes()
        {
            return [BattleLvl, BattleLvl2, BattleLvl3, BattleLvl4, BattleLvl5, BattleLvl6];
        }

        public TextBox[] getLevelBoxes()
        {
            return [LevelBox, LevelBox2, LevelBox3];
        }

        private void menuHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxButton messageOptions = MessageBoxButton.YesNo;
            string messageText = "A tool to edit the starter Pokemon and Rival battles in Pokemon Red, Blue, Yellow and Green.\n" +
                "The Auto Scroll patch makes the text boxes in the game keep scrolling rather than relying on user input.\n" +
                "Do you have a issue with the program?";
            var result = MessageBox.Show(messageText, "Help", messageOptions, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "https://github.com/Dava96/StarterEdit/issues",
                        UseShellExecute = true
                    });
                }
                catch (System.ComponentModel.Win32Exception noBrowser)
                {
                    if (noBrowser.ErrorCode == -2147467259)
                        MessageBox.Show(noBrowser.Message);
                }
                catch (System.Exception other)
                {
                    MessageBox.Show(other.Message);
                }
            }
        }

    }

}
