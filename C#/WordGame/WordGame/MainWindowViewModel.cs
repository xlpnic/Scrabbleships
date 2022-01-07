namespace WordGame
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IGamePage currentGamePageViewModel;

        public MainWindowViewModel()
        {
            var gameViewModel = new GameViewModel();
            this.currentGamePageViewModel = gameViewModel;
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
