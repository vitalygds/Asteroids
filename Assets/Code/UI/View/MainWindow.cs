using UnityEngine;

namespace UI
{
    internal sealed class MainWindow : MonoBehaviour
    {
        [SerializeField] private StartWindow _startWindow;
        [SerializeField] private RestartWindow _restartWindow;
        [SerializeField] private PlayerInfoView _playerInfoView;

        public StartWindow StartWindow => _startWindow;
        public RestartWindow RestartWindow => _restartWindow;
        public PlayerInfoView PlayerInfoView => _playerInfoView;
    }
}