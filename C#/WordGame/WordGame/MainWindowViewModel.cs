namespace WordGame
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IGamePage currentGamePageViewModel;

        public MainWindowViewModel()
        {
            //var initialGameViewModel = new GameViewModel();

            OpponentBoardViewModel initialGameViewModel = new OpponentBoardViewModel();

            this.currentGamePageViewModel = initialGameViewModel;
        }

        public IGamePage CurrentGamePageViewModel
        {
            get => this.currentGamePageViewModel;
            set
            {
                this.currentGamePageViewModel = value;
                this.OnPropertyChanged(nameof(this.CurrentGamePageViewModel));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
