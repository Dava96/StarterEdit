using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace StarterEdit
{
    class PokemonData
    {

        public int[] pokemonDecimals;
        public string[] pokemonHexValues;
        public string[] pokemonNames;
        public string[] pokedexOrderedNames;
        public string[] battleLocations = new string[] { "Route 22 (1)", "Cerulean City", "S.S Anne", "Pokemon Tower", "Silph Co.", "Route 22 (2)", "Indigo Plateau"};
        private StreamReader readNames;

        // to do make a seperate window for the trainer editor
        // to do make a stat editor for each individual pokemon
        // to do implement the title screen editor
        public PokemonData()
        {
            readNames = new StreamReader(setDirectory() + @".\PokemonNames.txt");

            pokemonDecimals = new int[256];
            pokemonHexValues = new string[256];
            pokemonNames = new string[256];
            pokedexOrderedNames = new string[153];

            populateArray(pokemonDecimals);
            populateArray(pokemonNames, readNames);
            populateArray(pokemonHexValues, pokemonDecimals);
            readNames = new StreamReader(setDirectory() + @".\PokemonNamesPokedexOrder.txt");

            populateArray(pokedexOrderedNames, readNames);
            //Console.WriteLine(pokedexOrderedNames[153]); // this is in preperation of pokemon stat editing capabilites as pokemon names are stored in dex order there


        }

        public int[] populateArray(int[] pokemonDecimals)
        {

            for (int i = 0; i < pokemonDecimals.Length; i++)
            {
                pokemonDecimals[i] = i;
            }
            return pokemonDecimals;
        }

        public string[] populateArray(string[] pokemonNames, StreamReader readNames)
        {
            string pokemonName = readNames.ReadLine();
            int i = 0;
            while (pokemonName != null)
            {
                pokemonNames[i] = pokemonName.Trim();
                pokemonName = readNames.ReadLine();
                i++;
            }
            readNames.Close();
            return pokemonNames;
        }

        public string[] populateArray(string[] pokemonHexValues, int[] pokemonDecimals)
        {
            string hexVal;

            for (int i = 0; i < pokemonDecimals.Length; i++)
            {
                hexVal = (String.Format("{0:X}", pokemonDecimals[i]));
                pokemonHexValues[i] = hexVal;
            }
            return pokemonHexValues;
        }

        private string setDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public string[] getPokemonNames()
        {
            return pokemonNames;
        }

        public string[] getPokemonHexValues()
        {
            return pokemonHexValues;
        }

        public int[] getPokemonDecimals()
        {
            return pokemonDecimals;
        }

        public string[] getBattleLocations()
        {
            return battleLocations;
        }

    }
}