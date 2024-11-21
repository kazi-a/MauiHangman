using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiHangman
{
    public class HangmanGame
    {
        private readonly string _wordToGuess;
        private readonly HashSet<char> _guessedLetters = new();
        private const int MaxAttempts = 6;

        public string CurrentGuess { get; private set; }
        public int AttemptsLeft { get; private set; }
        public int IncorrectGuesses { get; private set; } 

        // Property to access the word 
        public string WordToGuess => _wordToGuess;

        // Constructor
        public HangmanGame(string wordToGuess)
        {
            _wordToGuess = wordToGuess.ToLower();
            AttemptsLeft = MaxAttempts;
            IncorrectGuesses = 0; // Start with 0 incorrect guesses
            UpdateCurrentGuess();
        }

        // Method to guess a letter
        public bool GuessLetter(char letter)
        {
            letter = char.ToLower(letter);
            if (_guessedLetters.Contains(letter)) return false; 

            _guessedLetters.Add(letter);

            if (_wordToGuess.Contains(letter))
            {
                UpdateCurrentGuess();
                return true;
            }
            else
            {
                AttemptsLeft--;
                IncorrectGuesses++; 
                return false;
            }
        }

        // Updates the current guess 
        private void UpdateCurrentGuess()
        {
            CurrentGuess = new string(_wordToGuess.Select(c => _guessedLetters.Contains(c) ? c : '_').ToArray());
        }

        public string GuessedLetters => string.Join(", ", _guessedLetters.OrderBy(c => c));

        public bool GameOver => AttemptsLeft <= 0 || CurrentGuess == _wordToGuess;
    }
}
