using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void Play()
    {
        GameManager.Instance.LoadScene("Level");
    }

    public void Exit()
    {
        GameManager.Instance.QuitGame();
    }
}