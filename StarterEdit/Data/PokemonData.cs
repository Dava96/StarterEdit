﻿using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace StarterEdit
{
    class PokemonData
    {
        public int[] pokemonDecimals;
        public string[] pokemonHexValues;
        public string[] pokemonNames;
        public string[] pokedexOrderedNames;
        private StreamReader readNames;

        public PokemonData()
        {
            try
            {
                readNames = new StreamReader(setDirectory() + @".\PokemonNames.data");
            } catch (FileNotFoundException)
            {
                MessageBox.Show("Can't find the PokemonNames.data file, please put it in the applications directory: " + setDirectory(), "FileNotFoundException", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
            
            pokemonDecimals = new int[256];
            pokemonHexValues = new string[256];
            pokemonNames = new string[256];
            pokedexOrderedNames = new string[153];

            populateArray(pokemonDecimals);
            populateArray(pokemonNames, readNames);
            populateArray(pokemonHexValues, pokemonDecimals);

            try
            {
                readNames = new StreamReader(setDirectory() + @".\PokemonNamesPokedexOrder.data");
            } catch(FileNotFoundException)
            {
                MessageBox.Show("Can't find the PokemonNamesPokeDexOrder.data file, please put it in the applications directory: " + setDirectory(), "FileNotFoundException", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }

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
    }
}