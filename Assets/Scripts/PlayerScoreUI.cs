using TMPro;
using UnityEngine;

public class PlayerScoreUI : MonoBehaviour
{
    TextMeshProUGUI text;
    MyEventManager eventManager;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        eventManager = FindObjectOfType<MyEventManager>();
        eventManager.AddEventListener<ScoreUpdateEvent>(OnPlayerScoreChanged);
    }

    void OnPlayerScoreChanged(ScoreUpdateEvent nextEvent)
    {
        text.text = nextEvent.Score.ToString();
    }

    void OnDestroy()
    {
        eventManager.RemoveEventListener<ScoreUpdateEvent>(OnPlayerScoreChanged);
    }
}
