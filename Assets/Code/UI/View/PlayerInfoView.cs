using TMPro;
using UnityEngine;

namespace UI
{
    internal sealed class PlayerInfoView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _coords;
        [SerializeField] private TMP_Text _angles;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _weaponCharges;
        [SerializeField] private TMP_Text _chargeTime;

        private IPlayerViewModel _viewModel;

        public void Initialize(IPlayerViewModel viewModel)
        {
            _viewModel = viewModel;
            viewModel.OnClose += CloseView;
            viewModel.OnUpdate += UpdateView;
            gameObject.SetActive(true);
        }

        private void UpdateView()
        {
            _score.text = _viewModel.Score.ToString();
            _coords.text = $"{_viewModel.Position.x:F2}x{_viewModel.Position.y:F2}";
            Vector3 angles = _viewModel.Rotation.eulerAngles;
            _angles.text = $"{angles.x:F2}x{angles.y:F2}";
            _speed.text = _viewModel.Speed.ToString("F2");
            _weaponCharges.text = $"{_viewModel.WeaponCharges}/{_viewModel.WeaponMaxCharges}";
            _chargeTime.text = _viewModel.ChargeTime.ToString("F2");
        }

        private void CloseView(IViewModel viewModel)
        {
            _viewModel.OnClose -= CloseView;
            _viewModel.OnUpdate -= UpdateView;
            _viewModel = null;
            gameObject.SetActive(false);
        }
    }
}