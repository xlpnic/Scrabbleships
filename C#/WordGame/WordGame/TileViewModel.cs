namespace WordGame
{
    using System.Windows.Input;

    public class TileViewModel
    {
        public int XCoord = 0;
        public int YCoord = 2;

        public string TileValue = "B";

        public TileViewModel()
        {
            this.OnTileClicked = new DelegateCommand<object>(this.TileClicked);
        }

        public ICommand OnTileClicked { get; }

        public void TileClicked(object obj)
        {
        }
    }
}
