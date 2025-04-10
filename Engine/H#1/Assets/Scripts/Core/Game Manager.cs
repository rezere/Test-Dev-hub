using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int enemyCount = 1;
    public bool isPlay;
    public bool isPause;
    private void Awake()
    {
        Instance = this;
        isPlay = true;
        isPause = false;
        DontDestroyOnLoad(gameObject);
    }
    public void RegisterEnemy()
    {
        enemyCount++;
    }
    public void UnregisterEnemy()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            Victory();
        }
    }

    public void Victory()
    {
        isPlay = false;
        UIManager.Instance.ShowWinScreen();
    }
    public void PlayerDied()
    {
        isPlay = false;
        UIManager.Instance.ShowLoseScreen();
    }
   
}
