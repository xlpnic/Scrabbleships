namespace WordGame
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Input;

    public class OpponentBoardViewModel : IGamePage, INotifyPropertyChanged
    {
        private string a1BoxText;
        private string a2BoxText;
        private string a3BoxText;
        private string a4BoxText;
        private string a5BoxText;

        private string b1BoxText;
        private string b2BoxText;
        private string b3BoxText;
        private string b4BoxText;
        private string b5BoxText;

        private string c1BoxText;
        private string c2BoxText;
        private string c3BoxText;
        private string c4BoxText;
        private string c5BoxText;

        private string d1BoxText;
        private string d2BoxText;
        private string d3BoxText;
        private string d4BoxText;
        private string d5BoxText;

        private string e1BoxText;
        private string e2BoxText;
        private string e3BoxText;
        private string e4BoxText;
        private string e5BoxText;

        private bool c1ButtonVisible;

        public event PropertyChangedEventHandler PropertyChanged;

        private (int xCoord, int yCoord) EarliestStartingLetterCoords;
        private readonly Dictionary<(int xcoord, int ycoord), char> LettersEntered;

        public TileViewModel TileC1;

        public OpponentBoardViewModel()
        {
            this.a1BoxText = string.Empty;
            this.a2BoxText = string.Empty;
            this.a3BoxText = string.Empty;
            this.a4BoxText = string.Empty;
            this.a5BoxText = string.Empty;

            this.b1BoxText = string.Empty;
            this.b2BoxText = string.Empty;
            this.b3BoxText = string.Empty;
            this.b4BoxText = string.Empty;
            this.b5BoxText = string.Empty;

            this.d1BoxText = string.Empty;
            this.d2BoxText = string.Empty;
            this.d3BoxText = string.Empty;
            this.d4BoxText = string.Empty;
            this.d5BoxText = string.Empty;

            this.e1BoxText = string.Empty;
            this.e2BoxText = string.Empty;
            this.e3BoxText = string.Empty;
            this.e4BoxText = string.Empty;
            this.e5BoxText = string.Empty;

            //string[,] grid = new string[5,5];

            //grid[0, 0] = this.a1BoxText;
            //grid[1, 0] = this.a2BoxText;
            //grid[2, 0] = this.a3BoxText;
            //grid[3, 0] = this.a4BoxText;
            //grid[4, 0] = this.a5BoxText;

            //grid[0, 1] = this.b1BoxText;
            //grid[1, 1] = this.b2BoxText;
            //grid[2, 1] = this.b3BoxText;
            //grid[3, 1] = this.b4BoxText;
            //grid[4, 1] = this.b5BoxText;

            //grid[0, 2] = this.c1BoxText;
            //grid[1, 2] = this.c2BoxText;
            //grid[2, 2] = this.c3BoxText;
            //grid[3, 2] = this.c4BoxText;
            //grid[4, 2] = this.c5BoxText;

            //grid[0, 3] = this.d1BoxText;
            //grid[1, 3] = this.d2BoxText;
            //grid[2, 3] = this.d3BoxText;
            //grid[3, 3] = this.d4BoxText;
            //grid[4, 3] = this.d5BoxText;

            //grid[0, 4] = this.e1BoxText;
            //grid[1, 4] = this.e2BoxText;
            //grid[2, 4] = this.e3BoxText;
            //grid[3, 4] = this.e4BoxText;
            //grid[4, 4] = this.e5BoxText;

            // TODO: When a letter is set, check it's coords against this and set it if it's closer to the top left of the grid.
            // This might be hard because if the player deletes the starting letter, will need to work out from the reamining entered letters which is now the start...
            this.EarliestStartingLetterCoords = (0, 0);

            this.LettersEntered = new Dictionary<(int xcoord, int ycoord), char>();

            this.OnC1TileClicked = new DelegateCommand<object>(this.C1Clicked);

            this.OnGuessALetterClicked = new DelegateCommand<object>(this.GuessALetterClicked);

            this.OnGuessAWordClicked = new DelegateCommand<object>(this.GuessAWordClicked);

            this.OnCancelGuessAWordClicked = new DelegateCommand<object>(this.CancelGuessAWordClicked);

            this.OnGuessClicked = new DelegateCommand<object>(this.GuessClicked);

            this.OnCloseWrongGuessMessageClicked = new DelegateCommand<object>(this.CloseWrongGuessMessageClicked);

            this.c1ButtonVisible = true;

            this.overlayEnabled = true;

            this.guessWordBoxVisible = false;

            this.wrongGuessMessageVisible = false;

            this.TileC1 = new TileViewModel();

            this.SetOpponentsWord();
        }

        private void SetOpponentsWord()
        {
            var opponentsWord = "BINGO";
            var chars = opponentsWord.ToCharArray();


            this.C1BoxText = IsVowel(chars[0]) ? "?" : chars[0].ToString();
            this.C2BoxText = IsVowel(chars[1]) ? "?" : chars[1].ToString();
            this.C3BoxText = IsVowel(chars[2]) ? "?" : chars[2].ToString();
            this.C4BoxText = IsVowel(chars[3]) ? "?" : chars[3].ToString();
            this.C5BoxText = IsVowel(chars[4]) ? "?" : chars[4].ToString();
        }

        private static bool IsVowel(char charProvided)
        {
            if (charProvided.Equals('A') ||
                charProvided.Equals('E') ||
                charProvided.Equals('I') ||
                charProvided.Equals('O') ||
                charProvided.Equals('U'))
            {
                return true;
            }

            return false;
        }

        public ICommand OnC1TileClicked { get; }

        public ICommand OnGuessALetterClicked { get; }

        public ICommand OnGuessAWordClicked { get; }

        public ICommand OnCancelGuessAWordClicked { get; }

        public ICommand OnGuessClicked { get; }

        public ICommand OnCloseWrongGuessMessageClicked { get; }

        private bool wordIsValid;

        public bool WordIsValid
        {
            get => this.wordIsValid;
            set
            {
                this.wordIsValid = value;
                this.OnPropertyChanged(nameof(this.WordIsValid));
            }
        }

        public void C1Clicked(object obj)
        {
            if (!string.IsNullOrEmpty(this.c1BoxText))
            {
                // Show letter by hiding button for this cell
                this.C1ButtonVisible = false;
            }
            else
            {
                // Show 'miss' image
                // Hide button for this cell
            }

            this.OverlayEnabled = true;
        }

        public void GuessALetterClicked(object obj)
        {
            this.OverlayEnabled = false;
        }

        public void GuessAWordClicked(object obj)
        {
            this.GuessWordBoxVisible = true;
        }

        public void CancelGuessAWordClicked(object obj)
        {
            this.GuessWordBoxVisible = false;
        }

        public void CloseWrongGuessMessageClicked(object obj)
        {
            this.WrongGuessMessageVisible = false;
        }

        public void GuessClicked(object obj)
        {
            bool guessIsCorrect = this.CheckGuess();

            if (guessIsCorrect)
            {
                // Show correct guess message

                // Reveal word on grid
            }
            else
            {
                // Show incorrect guess message

                // End turn

                this.WrongGuessMessageVisible = true;
            }

            this.GuessWordBoxVisible = false;
        }

        private bool CheckGuess()
        {
            return false;
        }

        private bool wrongGuessMessageVisible;

        public bool WrongGuessMessageVisible
        {
            get => this.wrongGuessMessageVisible;
            set
            {
                this.wrongGuessMessageVisible = value;
                this.OnPropertyChanged(nameof(this.WrongGuessMessageVisible));
            }
        }

        private bool guessWordBoxVisible;

        public bool GuessWordBoxVisible
        {
            get => this.guessWordBoxVisible;
            set
            {
                this.guessWordBoxVisible = value;
                this.OnPropertyChanged(nameof(this.GuessWordBoxVisible));
            }
        }

        private bool overlayEnabled;

        public bool OverlayEnabled
        {
            get => this.overlayEnabled;
            set
            {
                this.overlayEnabled = value;
                this.OnPropertyChanged(nameof(this.OverlayEnabled));
            }
        }

        private void SetEarliestLetterIfNeeded(int x, int y)
        {
            if (x <= this.EarliestStartingLetterCoords.xCoord && y <= this.EarliestStartingLetterCoords.yCoord)
            {
                this.EarliestStartingLetterCoords = (x, y);
            }
        }

        private void AddValidLetter(int x, int y, char letter)
        {
            (int x, int y) coords = (x, y);
            this.LettersEntered.Add(coords, letter);
        }

        private void RemoveLetter(int x, int y)
        {
            (int x, int y) coords = (x, y);
            this.LettersEntered.Remove(coords);
        }

        public string A1BoxText
        {
            get => this.a1BoxText;
            set
            {
                this.a1BoxText = value;
                this.OnPropertyChanged(nameof(this.A1BoxText));
                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(0, 0);
                    this.AddValidLetter(0, 0, value[0]);
                }
                else
                {
                    this.RemoveLetter(0, 0);
                }
            }
        }

        public string A2BoxText
        {
            get => this.a2BoxText;
            set
            {
                this.a2BoxText = value;
                this.OnPropertyChanged(nameof(this.A2BoxText));
                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(1, 0);
                    this.AddValidLetter(1, 0, value[0]);
                }
                else
                {
                    this.RemoveLetter(1, 0);
                }
            }
        }

        public string A3BoxText
        {
            get => this.a3BoxText;
            set
            {
                this.a3BoxText = value;
                this.OnPropertyChanged(nameof(this.A3BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(2, 0);
                    this.AddValidLetter(2, 0, value[0]);
                }
                else
                {
                    this.RemoveLetter(2, 0);
                }
            }
        }

        public string A4BoxText
        {
            get => this.a4BoxText;
            set
            {
                this.a4BoxText = value;
                this.OnPropertyChanged(nameof(this.A4BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(3, 0);
                    this.AddValidLetter(3, 0, value[0]);
                }
                else
                {
                    this.RemoveLetter(3, 0);
                }
            }
        }

        public string A5BoxText
        {
            get => this.a5BoxText;
            set
            {
                this.a5BoxText = value;
                this.OnPropertyChanged(nameof(this.A5BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(4, 0);
                    this.AddValidLetter(4, 0, value[0]);
                }
                else
                {
                    this.RemoveLetter(4, 0);
                }
            }
        }

        public string B1BoxText
        {
            get => this.b1BoxText;
            set
            {
                this.b1BoxText = value;
                this.OnPropertyChanged(nameof(this.B1BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(0, 1);
                    this.AddValidLetter(0, 1, value[0]);
                }
                else
                {
                    this.RemoveLetter(0, 1);
                }
            }
        }

        public string B2BoxText
        {
            get => this.b2BoxText;
            set
            {
                this.b2BoxText = value;
                this.OnPropertyChanged(nameof(this.B2BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(1, 1);
                    this.AddValidLetter(1, 1, value[0]);
                }
                else
                {
                    this.RemoveLetter(1, 1);
                }
            }
        }

        public string B3BoxText
        {
            get => this.b3BoxText;
            set
            {
                this.b3BoxText = value;
                this.OnPropertyChanged(nameof(this.B3BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(2, 1);
                    this.AddValidLetter(2, 1, value[0]);
                }
                else
                {
                    this.RemoveLetter(2, 1);
                }
            }
        }

        public string B4BoxText
        {
            get => this.b4BoxText;
            set
            {
                this.b4BoxText = value;
                this.OnPropertyChanged(nameof(this.B4BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(3, 1);
                    this.AddValidLetter(3, 1, value[0]);
                }
                else
                {
                    this.RemoveLetter(3, 1);
                }
            }
        }

        public string B5BoxText
        {
            get => this.b5BoxText;
            set
            {
                this.b5BoxText = value;
                this.OnPropertyChanged(nameof(this.B5BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(4, 1);
                    this.AddValidLetter(4, 1, value[0]);
                }
                else
                {
                    this.RemoveLetter(4, 1);
                }
            }
        }

        public string C1BoxText
        {
            get => this.c1BoxText;
            set
            {
                this.c1BoxText = value;
                this.OnPropertyChanged(nameof(this.C1BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(0, 2);
                    this.AddValidLetter(0, 2, value[0]);
                }
                else
                {
                    this.RemoveLetter(0, 2);
                }
            }
        }

        public bool C1ButtonVisible
        {
            get => this.c1ButtonVisible;
            set
            {
                this.c1ButtonVisible = value;
                this.OnPropertyChanged(nameof(this.C1ButtonVisible));
            }
        }

        public string C2BoxText
        {
            get => this.c2BoxText;
            set
            {
                this.c2BoxText = value;
                this.OnPropertyChanged(nameof(this.C2BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(1, 2);
                    this.AddValidLetter(1, 2, value[0]);
                }
                else
                {
                    this.RemoveLetter(1, 2);
                }
            }
        }

        public string C3BoxText
        {
            get => this.c3BoxText;
            set
            {
                this.c3BoxText = value;
                this.OnPropertyChanged(nameof(this.C3BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(2, 2);
                    this.AddValidLetter(2, 2, value[0]);
                }
                else
                {
                    this.RemoveLetter(2, 2);
                }
            }
        }

        public string C4BoxText
        {
            get => this.c4BoxText;
            set
            {
                this.c4BoxText = value;
                this.OnPropertyChanged(nameof(this.C4BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(3, 2);
                    this.AddValidLetter(3, 2, value[0]);
                }
                else
                {
                    this.RemoveLetter(3, 2);
                }
            }
        }

        public string C5BoxText
        {
            get => this.c5BoxText;
            set
            {
                this.c5BoxText = value;
                this.OnPropertyChanged(nameof(this.C5BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(4, 2);
                    this.AddValidLetter(4, 2, value[0]);
                }
                else
                {
                    this.RemoveLetter(4, 2);
                }
            }
        }

        public string D1BoxText
        {
            get => this.d1BoxText;
            set
            {
                this.d1BoxText = value;
                this.OnPropertyChanged(nameof(this.D1BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(0, 3);
                    this.AddValidLetter(0, 3, value[0]);
                }
                else
                {
                    this.RemoveLetter(0, 3);
                }
            }
        }

        public string D2BoxText
        {
            get => this.d2BoxText;
            set
            {
                this.d2BoxText = value;
                this.OnPropertyChanged(nameof(this.D2BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(1, 3);
                    this.AddValidLetter(1, 3, value[0]);
                }
                else
                {
                    this.RemoveLetter(1, 3);
                }
            }
        }

        public string D3BoxText
        {
            get => this.d3BoxText;
            set
            {
                this.d3BoxText = value;
                this.OnPropertyChanged(nameof(this.D3BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(2, 3);
                    this.AddValidLetter(2, 3, value[0]);
                }
                else
                {
                    this.RemoveLetter(2, 3);
                }
            }
        }

        public string D4BoxText
        {
            get => this.d4BoxText;
            set
            {
                this.d4BoxText = value;
                this.OnPropertyChanged(nameof(this.D4BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(3, 3);
                    this.AddValidLetter(3, 3, value[0]);
                }
                else
                {
                    this.RemoveLetter(3, 3);
                }
            }
        }

        public string D5BoxText
        {
            get => this.d5BoxText;
            set
            {
                this.d5BoxText = value;
                this.OnPropertyChanged(nameof(this.D5BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(4, 3);
                    this.AddValidLetter(4, 3, value[0]);
                }
                else
                {
                    this.RemoveLetter(4, 3);
                }
            }
        }

        public string E1BoxText
        {
            get => this.e1BoxText;
            set
            {
                this.e1BoxText = value;
                this.OnPropertyChanged(nameof(this.E1BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(0, 4);
                    this.AddValidLetter(0, 4, value[0]);
                }
                else
                {
                    this.RemoveLetter(0, 4);
                }
            }
        }

        public string E2BoxText
        {
            get => this.e2BoxText;
            set
            {
                this.e2BoxText = value;
                this.OnPropertyChanged(nameof(this.E2BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(1, 4);
                    this.AddValidLetter(1, 4, value[0]);
                }
                else
                {
                    this.RemoveLetter(1, 4);
                }
            }
        }

        public string E3BoxText
        {
            get => this.e3BoxText;
            set
            {
                this.e3BoxText = value;
                this.OnPropertyChanged(nameof(this.E3BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(2, 4);
                    this.AddValidLetter(2, 4, value[0]);
                }
                else
                {
                    this.RemoveLetter(2, 4);
                }
            }
        }

        public string E4BoxText
        {
            get => this.e4BoxText;
            set
            {
                this.e4BoxText = value;
                this.OnPropertyChanged(nameof(this.E4BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(3, 4);
                    this.AddValidLetter(3, 4, value[0]);
                }
                else
                {
                    this.RemoveLetter(3, 4);
                }
            }
        }

        public string E5BoxText
        {
            get => this.e5BoxText;
            set
            {
                this.e5BoxText = value;
                this.OnPropertyChanged(nameof(this.E5BoxText));

                if (value != string.Empty)
                {
                    this.SetEarliestLetterIfNeeded(4, 4);
                    this.AddValidLetter(4, 4, value[0]);
                }
                else
                {
                    this.RemoveLetter(4, 4);
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
