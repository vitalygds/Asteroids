using System;

namespace UI
{
    public sealed class MenuPresenter
    {
        private readonly StartWindow _startWindow;
        private readonly RestartWindow _restartWindow;
        private IViewModel _currentViewModel;

        internal MenuPresenter(StartWindow startWindow, RestartWindow restartWindow)
        {
            _startWindow = startWindow;
            _restartWindow = restartWindow;
        }

        public void Show(Action callback)
        {
            StartWindowViewModel viewModel = new StartWindowViewModel(callback);
            _currentViewModel = viewModel;
            _startWindow.Initialize(viewModel);
        }
        
        public void Show(Action callback, int score)
        {
            StartWindowViewModel viewModel = new StartWindowViewModel(callback, score);
            _currentViewModel = viewModel;
            _restartWindow.Initialize(viewModel);
        }
        
        public void Hide()
        {
            _currentViewModel?.Close();
            _currentViewModel = null;
        }
    }
}