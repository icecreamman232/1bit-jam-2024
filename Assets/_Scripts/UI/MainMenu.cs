using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadToGameplayScene()
    {
        SceneManager.LoadSceneAsync("GameplayScene",LoadSceneMode.Single);
    }
}
