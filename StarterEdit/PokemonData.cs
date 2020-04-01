using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace StarterEdit
{
    class PokemonData
    {

        private int[] pokemonDecimals;
        private string[] pokemonHexValues;
        private string[] pokemonNames;
        private StreamReader readNames;

        
        public PokemonData()
        {
            readNames = new StreamReader(setDirectory() + @".\PokemonNames.txt");

            pokemonDecimals = new int[256];
            pokemonHexValues = new string[256];
            pokemonNames = new string[256];

            populateArray(pokemonDecimals);
            populateArray(pokemonNames, readNames);
            populateArray(pokemonHexValues, pokemonDecimals);
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

    }
}