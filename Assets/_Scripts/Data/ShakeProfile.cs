using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(fileName = "ShakeProfile", menuName = "SGGames/Data/ShakeProfile", order = 0)]
    public class ShakeProfile : ScriptableObject
    {
        [SerializeField] private float m_power;
        [SerializeField] private float m_frequency;
        [SerializeField] private float m_duration;
        
        public float Power => m_power;
        public float Frequency => m_frequency;
        public float Duration => m_duration;
    }
}

