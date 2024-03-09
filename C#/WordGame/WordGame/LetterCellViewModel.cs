namespace WordGame
{
    using System.ComponentModel;
    using System.Windows.Input;

    internal class LetterCellViewModel
    {
        private string cellBoxText;

        private bool cellButtonVisible;

        public event PropertyChangedEventHandler PropertyChanged;

        public LetterCellViewModel()
        {
            this.OnCellTileClicked = new DelegateCommand<object>(this.CellClicked);
            this.CellButtonVisible = true;
            this.overlayEnabled = true;
        }

        public ICommand OnCellTileClicked { get; }

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

        public string CellBoxText
        {
            get => this.cellBoxText;
            set
            {
                this.cellBoxText = value;
                this.OnPropertyChanged(nameof(this.CellBoxText));
            }
        }

        public void CellClicked(object obj)
        {
            if (!string.IsNullOrEmpty(this.cellBoxText))
            {
                // Show letter by hiding button for this cell
                this.CellButtonVisible = false;
            }
            else
            {
                // Show 'miss' image
                // Hide button for this cell
            }

            this.OverlayEnabled = true;
        }

        public bool CellButtonVisible
        {
            get => this.cellButtonVisible;
            set
            {
                this.cellButtonVisible = value;
                this.OnPropertyChanged(nameof(this.CellButtonVisible));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
