using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure
{
    internal sealed class UnityEventManager
    {
        public enum UnityEventType
        {
            Update = 0,
            FixedUpdate = 1,
            LateUpdate = 2,
        }

        private readonly UnityEventHolder _eventHolder;

        public UnityEventManager()
        {
            _eventHolder = new GameObject("UnityEventHolder").AddComponent<UnityEventHolder>();
            Object.DontDestroyOnLoad(_eventHolder);
        }

        public void Dispose()
        {
            Object.Destroy(_eventHolder);
        }

        public IDisposable Subscribe(UnityEventType eventTypeType, Action action)
        {
            switch (eventTypeType)
            {
                case UnityEventType.Update:
                    _eventHolder.OnUpdate += action;
                    break;
                case UnityEventType.FixedUpdate:
                    _eventHolder.OnFixedUpdate += action;
                    break;
                case UnityEventType.LateUpdate:
                    _eventHolder.OnLateUpdate += action;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(eventTypeType), eventTypeType, null);
            }

            return new DisposableActionHolder(() => { Unsubscribe(eventTypeType, action); });
        }

        private void Unsubscribe(UnityEventType eventTypeType, Action action)
        {
            switch (eventTypeType)
            {
                case UnityEventType.Update:
                    _eventHolder.OnUpdate -= action;
                    break;
                case UnityEventType.FixedUpdate:
                    _eventHolder.OnFixedUpdate -= action;
                    break;
                case UnityEventType.LateUpdate:
                    _eventHolder.OnLateUpdate -= action;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(eventTypeType), eventTypeType, null);
            }
        }
    }
}