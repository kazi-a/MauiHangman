using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace MauiHangman
{
    public partial class MainPage : ContentPage
    {
        private HangmanGame _game;

        // Random number generator
        private Random _random = new Random();

        // List of words 
        private List<string> _words = new List<string>
        {
            "maui", "tropical", "volcano", "island", "adventure",
            "hangman", "ocean", "paradise", "beach", "sunset"
        };

        public MainPage()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            // Select a random word from the list
            string randomWord = _words[_random.Next(_words.Count)];

            // Initialize a new Hangman game with the selected word
            _game = new HangmanGame(randomWord);

            PlayAgainButton.IsVisible = false;
            GameOverMessage.IsVisible = false;
            EnableLetterButtons();
            UpdateUI();
        }

        private void UpdateUI()
        {
            WordDisplay.Text = string.Join(" ", _game.CurrentGuess.ToUpper().ToCharArray());
            AttemptsLabel.Text = $"Attempts left: {_game.AttemptsLeft}";
            GuessedLettersLabel.Text = $"Used: {_game.GuessedLetters.ToUpper()}";

            // Update Hangman image 
            UpdateHangmanImage(_game.IncorrectGuesses);

            if (_game.GameOver)
            {
                GameOverMessage.Text = _game.CurrentGuess == _game.WordToGuess
                    ? "You won!"
                    : $"Game Over! The word was {_game.WordToGuess.ToUpper()}";
                GameOverMessage.IsVisible = true;
                PlayAgainButton.IsVisible = true;
                DisableLetterButtons();
            }
        }

        private void UpdateHangmanImage(int incorrectGuesses)
        {
            string imageName = $"hangman{incorrectGuesses}.gif";
            HangmanImage.Source = ImageSource.FromFile(imageName);
        }

        private void DisableLetterButtons()
        {
            foreach (var view in LetterGrid.Children)
            {
                if (view is Button button)
                {
                    button.IsEnabled = false;
                }
            }
        }

        private void EnableLetterButtons()
        {
            foreach (var view in LetterGrid.Children)
            {
                if (view is Button button)
                {
                    button.IsEnabled = true;
                }
            }
        }

        private void LetterButton_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                char guessedLetter = button.Text[0];
                _game.GuessLetter(guessedLetter);
                button.IsEnabled = false;
                UpdateUI();
            }
        }

        private void PlayAgainButton_Clicked(object sender, EventArgs e)
        {
            StartNewGame();
        }
    }
}
