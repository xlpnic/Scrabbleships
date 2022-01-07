namespace WordGame
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;

    public class GameViewModel : IGamePage, INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        (int xCoord, int yCoord) EarliestStartingLetterCoords;

        Dictionary<(int xcoord, int ycoord), char> LettersEntered;

        public GameViewModel()
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

            this.c1BoxText = string.Empty;
            this.c2BoxText = string.Empty;
            this.c3BoxText = string.Empty;
            this.c4BoxText = string.Empty;
            this.c5BoxText = string.Empty;

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
            EarliestStartingLetterCoords = (0, 0);

            LettersEntered = new Dictionary<(int xcoord, int ycoord), char> ();

            this.OnGoButtonClicked = new DelegateCommand<object>(this.ValidateWord);
        }

        public ICommand OnGoButtonClicked { get; }

        private bool wordIsValid;

        public bool WordIsValid
        {
            get => wordIsValid;
            set
            {
                wordIsValid = value;
                this.OnPropertyChanged(nameof(this.WordIsValid));
            }
        }

        public void ValidateWord(object obj)
        {
            if (LettersEntered.Count <= 1)
            {
                WordIsValid = false;
            }

            var firstLetterCoords = GetFirstLetterCoords();

            var wordIsHorizontal = false;
            var wordIsVertical = false;

            foreach (var letter in LettersEntered)
            {
                if (letter.Key.xcoord != firstLetterCoords.xcoord
                    && letter.Key.ycoord != firstLetterCoords.ycoord)
                {
                    // Found a letter that is not in the same row/column as the first letter.
                    WordIsValid = false;
                }

                if (letter.Key.ycoord == firstLetterCoords.ycoord
                    && letter.Key.xcoord != firstLetterCoords.xcoord )
                {
                    wordIsHorizontal = true;
                }

                if (letter.Key.xcoord == firstLetterCoords.xcoord 
                    && letter.Key.ycoord != firstLetterCoords.ycoord)
                {
                    wordIsVertical = true;
                }
            }

            if (wordIsHorizontal && wordIsVertical)
            {
                // Word must be either horizontal or vertical.
                WordIsValid = false;
            }

            var word = string.Empty;

            if (wordIsHorizontal)
            {
                var xcoords = LettersEntered.Keys.Select(k => k.xcoord).ToList();
                xcoords.Sort();

                var lengthOfWord = xcoords.Count;

                var expectedLastXCoord = xcoords[0] + lengthOfWord - 1;

                if (xcoords.Last() != expectedLastXCoord)
                {
                    // If these don't match, there must be a space in the word somewhere.
                    WordIsValid = false;
                }

                foreach(var letterXCoord in xcoords)
                {
                    var coord = (letterXCoord, firstLetterCoords.ycoord);
                    var letter = LettersEntered[coord];

                    word += letter;
                }
            }
            else 
            {
                var ycoords = LettersEntered.Keys.Select(k => k.ycoord).ToList();
                ycoords.Sort();

                var lengthOfWord = ycoords.Count;

                var expectedLastYCoord = ycoords[0] + lengthOfWord - 1;

                if (ycoords.Last() != expectedLastYCoord)
                {
                    // If these don't match, there must be a space in the word somewhere.
                    WordIsValid = false;
                }

                foreach (var letterYCoord in ycoords)
                {
                    var coord = (letterYCoord, firstLetterCoords.xcoord);
                    var letter = LettersEntered[coord];

                    word += letter;
                }
            }

            // If here, word is in a single line, we know if it's vertrical or horizontal, and we know there are no spaces in the word.
            // Next, check word in dictionary.
            WordIsValid = WordIsAValidWord(word);
        }

        private bool WordIsAValidWord(string word)
        {
            // TODO: validate word in dictioanry.

            int initialCapacity = 82765;
            int maxEditDistanceDictionary = 2; //maximum edit distance per dictionary precalculation
            var symSpell = new SymSpell(initialCapacity, maxEditDistanceDictionary);

            //load dictionary
            //string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //string dictionaryPath = baseDirectory + "../../../../SymSpell/frequency_dictionary_en_82_765.txt";
            var dictionaryPath = @"D:\Dev\Scrabbleships\C#\WordGame\WordGame\frequency_dictionary_en_82_765.txt";
            int termIndex = 0; //column of the term in the dictionary text file
            int countIndex = 1; //column of the term frequency in the dictionary text file
            if (!symSpell.LoadDictionary(dictionaryPath, termIndex, countIndex))
            {
                Console.WriteLine("File not found!");
                //press any key to exit program
                Console.ReadKey();
                return false;
            }

            int maxEditDistanceLookup = 1; //max edit distance per lookup (maxEditDistanceLookup<=maxEditDistanceDictionary)
            var suggestionVerbosity = SymSpell.Verbosity.Top; //Top, Closest, All
            var suggestions = symSpell.Lookup(word.ToLower(), suggestionVerbosity, maxEditDistanceLookup);

            //display suggestions, edit distance and term frequency
            //foreach (var suggestion in suggestions)
            //{
            //    Console.WriteLine(suggestion.term + " " + suggestion.distance.ToString() + " " + suggestion.count.ToString("N0"));
            //}

            if (!suggestions.Any())
            {
                return false;
            }

            if (suggestions.First().term.ToUpper() == word.ToUpper())
            {
                return true;
            }

            return false;
        }

        private (int xcoord, int ycoord) GetFirstLetterCoords()
        {
            bool gotAFirstLetter = false;
            (int xcoord, int ycoord) firstLetter = (-1, -1);

            foreach (var coords in LettersEntered.Keys)
            {
                if (!gotAFirstLetter)
                {
                    firstLetter = coords;
                    gotAFirstLetter = true;
                }
                else if (coords.xcoord <= firstLetter.xcoord 
                    && coords.ycoord <= firstLetter.ycoord)
                {
                    firstLetter = coords;
                }
            }

            return firstLetter;
        }

        private void SetEarliestLetterIfNeeded(int x, int y)
        {
            if (x <= EarliestStartingLetterCoords.xCoord && y <= EarliestStartingLetterCoords.yCoord)
            {
                EarliestStartingLetterCoords = (x, y);
            }
        }

        private void AddValidLetter(int x, int y, char letter)
        {
            var coords = (x, y);
            LettersEntered.Add(coords, letter);
        }

        private void RemoveLetter(int x, int y)
        { 
            var coords = (x, y);
            LettersEntered.Remove(coords);
        }

        public string A1BoxText 
        {
            get => a1BoxText; 
            set
            {
                a1BoxText = value;
                this.OnPropertyChanged(nameof(this.A1BoxText));
                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(0, 0);
                    AddValidLetter(0, 0, value[0]);
                }
                else
                {
                    RemoveLetter(0, 0);
                }
            }
        }

        public string A2BoxText
        {
            get => a2BoxText;
            set
            {
                a2BoxText = value;
                this.OnPropertyChanged(nameof(this.A2BoxText));
                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(1, 0);
                    AddValidLetter(1, 0, value[0]);
                }
                else
                {
                    RemoveLetter(1, 0);
                }
            }
        }

        public string A3BoxText
        {
            get => a3BoxText;
            set
            {
                a3BoxText = value;
                this.OnPropertyChanged(nameof(this.A3BoxText));
                
                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(2, 0);
                    AddValidLetter(2, 0, value[0]);
                }
                else
                {
                    RemoveLetter(2, 0);
                }
            }
        }

        public string A4BoxText
        {
            get => a4BoxText;
            set
            {
                a4BoxText = value;
                this.OnPropertyChanged(nameof(this.A4BoxText));
                
                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(3, 0);
                    AddValidLetter(3, 0, value[0]);
                }
                else
                {
                    RemoveLetter(3, 0);
                }
            }
        }

        public string A5BoxText
        {
            get => a5BoxText;
            set
            {
                a5BoxText = value;
                this.OnPropertyChanged(nameof(this.A5BoxText));
                
                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(4, 0);
                    AddValidLetter(4, 0, value[0]);
                }
                else
                {
                    RemoveLetter(4, 0);
                }
            }
        }

        public string B1BoxText
        {
            get => b1BoxText;
            set
            {
                b1BoxText = value;
                this.OnPropertyChanged(nameof(this.B1BoxText));
                
                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(0, 1);
                    AddValidLetter(0, 1, value[0]);
                }
                else
                {
                    RemoveLetter(0, 1);
                }
            }
        }

        public string B2BoxText
        {
            get => b2BoxText;
            set
            {
                b2BoxText = value;
                this.OnPropertyChanged(nameof(this.B2BoxText));
                
                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(1, 1);
                    AddValidLetter(1, 1, value[0]);
                }
                else
                {
                    RemoveLetter(1, 1);
                }
            }
        }

        public string B3BoxText
        {
            get => b3BoxText;
            set
            {
                b3BoxText = value;
                this.OnPropertyChanged(nameof(this.B3BoxText));
                
                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(2, 1);
                    AddValidLetter(2, 1, value[0]);
                }
                else
                {
                    RemoveLetter(2, 1);
                }
            }
        }

        public string B4BoxText
        {
            get => b4BoxText;
            set
            {
                b4BoxText = value;
                this.OnPropertyChanged(nameof(this.B4BoxText));
                
                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(3, 1);
                    AddValidLetter(3, 1, value[0]);
                }
                else
                {
                    RemoveLetter(3, 1);
                }
            }
        }

        public string B5BoxText
        {
            get => b5BoxText;
            set
            {
                b5BoxText = value;
                this.OnPropertyChanged(nameof(this.B5BoxText));

                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(4, 1);
                    AddValidLetter(4, 1, value[0]);
                }
                else
                {
                    RemoveLetter(4, 1);
                }
            }
        }

        public string C1BoxText
        {
            get => c1BoxText;
            set
            {
                c1BoxText = value;
                this.OnPropertyChanged(nameof(this.C1BoxText));

                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(0, 2);
                    AddValidLetter(0, 2, value[0]);
                }
                else
                {
                    RemoveLetter(0, 2);
                }
            }
        }

        public string C2BoxText
        {
            get => c2BoxText;
            set
            {
                c2BoxText = value;
                this.OnPropertyChanged(nameof(this.C2BoxText));

                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(1, 2);
                    AddValidLetter(1, 2, value[0]);
                }
                else
                {
                    RemoveLetter(1, 2);
                }
            }
        }

        public string C3BoxText
        {
            get => c3BoxText;
            set
            {
                c3BoxText = value;
                this.OnPropertyChanged(nameof(this.C3BoxText));

                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(2, 2);
                    AddValidLetter(2, 2, value[0]);
                }
                else
                {
                    RemoveLetter(2, 2);
                }
            }
        }

        public string C4BoxText
        {
            get => c4BoxText;
            set
            {
                c4BoxText = value;
                this.OnPropertyChanged(nameof(this.C4BoxText));

                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(3, 2);
                    AddValidLetter(3, 2, value[0]);
                }
                else
                {
                    RemoveLetter(3, 2);
                }
            }
        }

        public string C5BoxText
        {
            get => c5BoxText;
            set
            {
                c5BoxText = value;
                this.OnPropertyChanged(nameof(this.C5BoxText));

                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(4, 2);
                    AddValidLetter(4, 2, value[0]);
                }
                else
                {
                    RemoveLetter(4, 2);
                }
            }
        }

        public string D1BoxText
        {
            get => d1BoxText;
            set
            {
                d1BoxText = value;
                this.OnPropertyChanged(nameof(this.D1BoxText));

                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(0, 3);
                    AddValidLetter(0, 3, value[0]);
                }
                else
                {
                    RemoveLetter(0, 3);
                }
            }
        }

        public string D2BoxText
        {
            get => d2BoxText;
            set
            {
                d2BoxText = value;
                this.OnPropertyChanged(nameof(this.D2BoxText));

                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(1, 3);
                    AddValidLetter(1, 3, value[0]);
                }
                else
                {
                    RemoveLetter(1, 3);
                }
            }
        }

        public string D3BoxText
        {
            get => d3BoxText;
            set
            {
                d3BoxText = value;
                this.OnPropertyChanged(nameof(this.D3BoxText));

                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(2, 3);
                    AddValidLetter(2, 3, value[0]);
                }
                else
                {
                    RemoveLetter(2, 3);
                }
            }
        }

        public string D4BoxText
        {
            get => d4BoxText;
            set
            {
                d4BoxText = value;
                this.OnPropertyChanged(nameof(this.D4BoxText));

                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(3, 3);
                    AddValidLetter(3, 3, value[0]);
                }
                else
                {
                    RemoveLetter(3, 3);
                }
            }
        }

        public string D5BoxText
        {
            get => d5BoxText;
            set
            {
                d5BoxText = value;
                this.OnPropertyChanged(nameof(this.D5BoxText));

                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(4, 3);
                    AddValidLetter(4, 3, value[0]);
                }
                else
                {
                    RemoveLetter(4, 3);
                }
            }
        }

        public string E1BoxText
        {
            get => e1BoxText;
            set
            {
                e1BoxText = value;
                this.OnPropertyChanged(nameof(this.E1BoxText));

                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(0, 4);
                    AddValidLetter(0, 4, value[0]);
                }
                else
                {
                    RemoveLetter(0, 4);
                }
            }
        }

        public string E2BoxText
        {
            get => e2BoxText;
            set
            {
                e2BoxText = value;
                this.OnPropertyChanged(nameof(this.E2BoxText));

                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(1, 4);
                    AddValidLetter(1, 4, value[0]);
                }
                else
                {
                    RemoveLetter(1, 4);
                }
            }
        }

        public string E3BoxText
        {
            get => e3BoxText;
            set
            {
                e3BoxText = value;
                this.OnPropertyChanged(nameof(this.E3BoxText));

                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(2, 4);
                    AddValidLetter(2, 4, value[0]);
                }
                else
                {
                    RemoveLetter(2, 4);
                }
            }
        }

        public string E4BoxText
        {
            get => e4BoxText;
            set
            {
                e4BoxText = value;
                this.OnPropertyChanged(nameof(this.E4BoxText));

                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(3, 4);
                    AddValidLetter(3, 4, value[0]);
                }
                else
                {
                    RemoveLetter(3, 4);
                }
            }
        }

        public string E5BoxText
        {
            get => e5BoxText;
            set
            {
                e5BoxText = value;
                this.OnPropertyChanged(nameof(this.E5BoxText));

                if (value != string.Empty)
                {
                    SetEarliestLetterIfNeeded(4, 4);
                    AddValidLetter(4, 4, value[0]);
                }
                else
                {
                    RemoveLetter(4, 4);
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
