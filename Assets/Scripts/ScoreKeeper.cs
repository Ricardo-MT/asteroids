using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score = 0;

    MyEventManager eventManager;
    void Start()
    {
        eventManager = FindObjectOfType<MyEventManager>();
        eventManager.AddEventListener<EnemyDestroyedEvent>(OnEnemyDestroyed);
    }
    private void OnEnemyDestroyed(EnemyDestroyedEvent enemyDestroyedEvent)
    {
        AddToScore(10);
    }

    public void AddToScore(int scoreToAdd)
    {
        score += scoreToAdd;
        eventManager.Trigger(new ScoreUpdateEvent(score));
    }

    public int GetScore()
    {
        return score;
    }

    void OnDestroy()
    {
        eventManager.RemoveEventListener<EnemyDestroyedEvent>(OnEnemyDestroyed);
    }
}
