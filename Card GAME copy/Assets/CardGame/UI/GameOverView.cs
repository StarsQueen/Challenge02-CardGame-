using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//GameOver stuff that I got from CHATGPT
public class GameOverView : MonoBehaviour
{
    [SerializeField] BoolEventChannel battleEnded;
    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text messageText;

    void OnEnable() => battleEnded.Raised += Show;
    void OnDisable() => battleEnded.Raised -= Show;

    void Start() => panel.SetActive(false);

    void Show(bool playerWon)
    {
        panel.SetActive(true);
        messageText.text = playerWon ? "You win!" : "You lose...";
    }

    // Hooked to the Restart button's OnClick.
    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
