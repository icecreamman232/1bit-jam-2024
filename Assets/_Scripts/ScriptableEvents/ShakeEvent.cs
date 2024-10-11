using System;
using SGGames.Scripts.Data;
using UnityEngine;

namespace  SGGames.Scripts.ScriptableEvent
{
    [CreateAssetMenu(menuName = "SGGames/Scriptable Event/Shake Event")]
    public class ShakeEvent : ScriptableObject
    {
        private Action<ShakeProfile> m_listeners;
    
        public void AddListener(Action<ShakeProfile> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<ShakeProfile> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(ShakeProfile profile)
        {
            m_listeners?.Invoke(profile);
        }
    }
}
