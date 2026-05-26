using System;
using System.Collections.Generic;

namespace MorseNap.Structures
{
    public class MorseTree
    {
        public MorseNode Root { get; private set; }
        private readonly Dictionary<char, string> textToMorseMap;

        public MorseTree()
        {
            Root = new MorseNode();
            textToMorseMap = new Dictionary<char, string>();
            InitializeTree();
        }

        private void InitializeTree()
        {
            // Diccionario interno de inicialización
            var morseAlphabet = new Dictionary<char, string>
            {
                {'A', ".-"},   {'B', "-..."}, {'C', "-.-."}, {'D', "-.."},  {'E', "."},
                {'F', "..-."}, {'G', "--."},  {'H', "...."}, {'I', ".."},   {'J', ".---"},
                {'K', "-.-"},  {'L', ".-.."}, {'M', "--"},   {'N', "-."},   {'O', "---"},
                {'P', ".--."}, {'Q', "--.-"}, {'R', ".-."},  {'S', "..."},  {'T', "-"},
                {'U', "..-"},  {'V', "...-"}, {'W', ".--"},  {'X', "-..-"}, {'Y', "-.--"},
                {'Z', "--.."},
                {'1', ".----"}, {'2', "..---"}, {'3', "...--"}, {'4', "....-"}, {'5', "....."},
                {'6', "-...."}, {'7', "--..."}, {'8', "---.."}, {'9', "----."}, {'0', "-----"}
            };

            foreach (var pair in morseAlphabet)
            {
                Insert(pair.Value, pair.Key);
            }
        }

        private void Insert(string morseCode, char character)
        {
            var current = Root;
            foreach (char symbol in morseCode)
            {
                if (symbol == '.')
                {
                    if (current.Left == null) current.Left = new MorseNode();
                    current = current.Left;
                }
                else if (symbol == '-')
                {
                    if (current.Right == null) current.Right = new MorseNode();
                    current = current.Right;
                }
            }
            current.Character = character;
            textToMorseMap[character] = morseCode;
        }

        // TEXTO -> MORSE
        public string TranslateToMorse(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";

            var words = text.ToUpper().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var resultWords = new List<string>();

            foreach (var word in words)
            {
                var resultLetters = new List<string>();
                foreach (char c in word)
                {
                    if (textToMorseMap.ContainsKey(c))
                    {
                        resultLetters.Add(textToMorseMap[c]);
                    }
                }
                // Separación internacional: 3 unidades de espacio entre letras
                resultWords.Add(string.Join("   ", resultLetters));
            }
            // Separación internacional: 7 unidades de espacio entre palabras
            return string.Join("       ", resultWords);
        }

        // MORSE -> TEXTO
        public string TranslateToText(string morseCode, out string errorMessage)
        {
            errorMessage = null;
            if (string.IsNullOrWhiteSpace(morseCode)) return "";

            // Normalizamos los 7 espacios de separación de palabras usando un delimitador seguro
            string normalizedMorse = morseCode.Replace("       ", " | ");
            var words = normalizedMorse.Split(" | ", StringSplitOptions.RemoveEmptyEntries);
            var resultText = new List<string>();

            foreach (var word in words)
            {
                var letters = word.Split(new[] { "   " }, StringSplitOptions.RemoveEmptyEntries);
                var decodedWord = "";

                foreach (var letter in letters)
                {
                    var current = Root;
                    string cleanLetter = letter.Trim();

                    foreach (char symbol in cleanLetter)
                    {
                        if (symbol == '.') current = current.Left;
                        else if (symbol == '-') current = current.Right;
                        else
                        {
                            errorMessage = $"Carácter inválido detectado: '{symbol}'. Solo se permiten puntos (.), rayas (-) y espacios.";
                            return null;
                        }

                        if (current == null)
                        {
                            errorMessage = $"La secuencia '{cleanLetter}' no pertenece a ningún carácter del código internacional.";
                            return null;
                        }
                    }

                    if (current.Character == '\0')
                    {
                        errorMessage = $"La señal '{cleanLetter}' se encuentra incompleta o es inválida.";
                        return null;
                    }

                    decodedWord += current.Character;
                }
                resultText.Add(decodedWord);
            }

            return string.Join(" ", resultText);
        }
    }
}