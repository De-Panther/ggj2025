using UnityEngine;
using UnityEngine.UI;

namespace GGJGame
{
  public class WaitingScreen : MonoBehaviour
  {
    public GameObject screen;
    public GameObject waitingForPlayersLabel;
    public Button startGameButton;
    public GameObject youWinLabel;
    public GameObject youLoseLabel;
    public GameObject scoreLabel;
    public Text scoreText;

    private bool hadOneGame = false;

    private void OnEnable()
    {
      GameState.OnGameEnd += HandleOnGameEnd;
      GameState.OnGameStart += HandleOnGameStart;
    }

    private void OnDisable()
    {
      GameState.OnGameEnd -= HandleOnGameEnd;
      GameState.OnGameStart -= HandleOnGameStart;
    }

    private void HandleOnGameEnd()
    {
      CheckState();
    }

    private void HandleOnGameStart()
    {
      hadOneGame = true;
      CheckState();
    }

    private void CheckState()
    {
      if (GameState.Instance.inGame)
      {
        screen.SetActive(false);
        return;
      }
      screen.SetActive(true);
      startGameButton.interactable = true;
      bool isHelicopter = ClientInstantiator.Instance.GetIsHelicopter();
      bool helicopterWin = GameState.Instance.airStolen < GameState.minAirAlienNeeds;
      youWinLabel.SetActive(hadOneGame && isHelicopter && helicopterWin);
      youLoseLabel.SetActive(hadOneGame && !youWinLabel.activeSelf);
      scoreLabel.SetActive(hadOneGame);
      scoreText.text = $"{GameState.Instance.airStolen} kg Oxygen stolen";
    }

    public void StartGameClicked()
    {
      GameState.Instance.StartGame();
    }
  }
}
