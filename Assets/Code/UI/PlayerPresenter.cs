using Infrastructure;

namespace UI
{
    public sealed class PlayerPresenter
    {
        private readonly PlayerInfoView _playerInfoView;
        private readonly ITickController _tickController;
        private PlayerViewModel _viewModel;

        internal PlayerPresenter(PlayerInfoView playerInfoView, ITickController tickController)
        {
            _playerInfoView = playerInfoView;
            _tickController = tickController;
        }

        public void Show(IPlayerRuntimeInfoProvider provider)
        {
            _viewModel?.Close();
            _viewModel = new PlayerViewModel(provider, _tickController);
            _playerInfoView.Initialize(_viewModel);
        }

        public void Hide()
        {
            _viewModel?.Close();
            _viewModel = null;
        }
    }
}