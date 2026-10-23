using TMPro;
using UnityEngine;
using UnityEngine.UI;

// shows whose turn but it didnt wanna work unfortunately
public class TurnView : MonoBehaviour
{
    [SerializeField] TurnStateEventChannel turnChanged;
    [SerializeField] TMP_Text label;
    [SerializeField] Button endTurnButton;

    void OnEnable() => turnChanged.Raised += Refresh;
    void OnDisable() => turnChanged.Raised -= Refresh;

    void Refresh(TurnState state)
    {
        label.text = state switch
        {
            TurnState.PlayerTurn => "Your turn",
            TurnState.EnemyTurn => "Enemy turn",
            TurnState.Victory => "Victory!",
            TurnState.Defeat => "Defeat...",
            _ => ""
        };

        if (endTurnButton != null)
            endTurnButton.interactable = state == TurnState.PlayerTurn;
    }
}
