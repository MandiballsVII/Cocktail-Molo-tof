using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    [SerializeField] private GameObject pausePanel;

    public bool IsPaused { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        if (IsPaused)
            return;

        IsPaused = true;

        Time.timeScale = 0f;

        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        if (!IsPaused)
            return;

        IsPaused = false;

        Time.timeScale = 1f;

        pausePanel.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;

        IsPaused = false;

        GameManager.Instance.LoadScene("MainMenu");
    }
}