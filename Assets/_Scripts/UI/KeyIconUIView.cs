using UnityEngine;
using UnityEngine.UI;

public class KeyIconUIView : MonoBehaviour
{
    [SerializeField] private Image m_image;
    [SerializeField] private Animator m_animator;

    private int m_appearAnim = Animator.StringToHash("Appear");

    public void ShowDisappear()
    {
        m_image.color = Color.black;
    }
    
    public void ShowAppear()
    {
        m_animator.SetTrigger(m_appearAnim);
    }
}