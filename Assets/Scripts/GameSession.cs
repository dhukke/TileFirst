using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
            return;
        }

        ResetGameSession();
    }

    private void TakeLife()
    {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
