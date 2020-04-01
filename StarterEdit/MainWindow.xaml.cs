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

namespace StarterEdit
{
    public partial class MainWindow : Window
    {
        Dictionary<int, string> pokemonNames = new Dictionary<int, string>();
        PokemonData pokemonData = new PokemonData();
        Offsets offsets = new Offsets();
        static OpenFileDialog openDialog = new OpenFileDialog();
        static string[] pkmNames;
        static long[] sqrtlOffsets;
        static long[] bulbOffsets;
        static long[] charmOffsets;
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
           
        }

        private void menuOpen_Click(object sender, RoutedEventArgs e)
        {
            openDialog.Filter = "PKM R/B (*.gb)|*.gb|All files (*.*)|*.*";
            openDialog.ShowDialog();
            BinaryReader reader = new BinaryReader(File.OpenRead(openDialog.FileName));

            readStarterPokemon(sqrtlOffsets, reader, Starter1, NameList, 0);
            readStarterPokemon(bulbOffsets, reader, Starter2, NameList2, 1);
            readStarterPokemon(charmOffsets, reader, Starter3, NameList3, 2);
            reader.Close();
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
            StreamWriter writer = new StreamWriter(File.OpenWrite(openDialog.FileName));
            writeStarterPokemon(sqrtlOffsets, writer, NameList.SelectedIndex, 0);
            writeStarterPokemon(bulbOffsets, writer, NameList2.SelectedIndex, 1);
            writeStarterPokemon(charmOffsets, writer, NameList3.SelectedIndex, 2);
            MessageBox.Show("Changes saved succesfully", "Changes saved");
            writer.Close();

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
    }
}
