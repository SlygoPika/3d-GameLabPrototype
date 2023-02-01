using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    bool over = false;

    public void GameOver()
    {
        if (!over)
        {
            over = true;
            Debug.Log("GameOver");
            Restart();
        }

    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
