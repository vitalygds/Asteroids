using System;
using Infrastructure;
using UnityEngine;

namespace UI
{
    internal sealed class PlayerViewModel : IPlayerViewModel, ILateUpdate
    {
        private readonly IPlayerRuntimeInfoProvider _provider;
        public event Action<IViewModel> OnClose;
        public event Action OnUpdate;
        private readonly IDisposable _updateSub;
        public int Score => _provider.Score;
        public Vector2 Position => _provider.Position;
        public Quaternion Rotation => _provider.Rotation;
        public float Speed => _provider.Speed;
        public int WeaponCharges => _provider.WeaponCharges;
        public int WeaponMaxCharges => _provider.WeaponMaxCharges;
        public float ChargeTime => _provider.ChargeTime;

        public PlayerViewModel(IPlayerRuntimeInfoProvider provider, ITickController tickController)
        {
            _provider = provider;
            _updateSub = tickController.AddController(this);
        }

        public void Close()
        {
            _updateSub.Dispose();
            OnClose?.Invoke(this);
        }

        void ILateUpdate.UpdateController(float deltaTime)
        {
            OnUpdate?.Invoke();
        }
    }
}