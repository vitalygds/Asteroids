using System;

namespace UI
{
    internal sealed class StartWindowViewModel : IStartWindowViewModel, IRestartWindowViewModel
    {
        public event Action<IViewModel> OnClose;
        private readonly Action _callback;

        public int Score { get; }

        public StartWindowViewModel(Action callback, int score = 0)
        {
            Score = score;
            _callback = callback;
        }

        public void Start()
        {
            _callback?.Invoke();
        }

        public void Close()
        {
            OnClose?.Invoke(this);
        }
    }
}