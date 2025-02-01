using UnityEngine;
using UnityEngine.UI;

namespace GGJGame
{
  public class WaitingScreen : MonoBehaviour
  {
    public GameObject screen;
    public GameObject waitingForPlayersLabel;
    public Button startGameButton;
    public GameObject winLoseLabel;
    public Text winLoseText;
    public GameObject scoreLabel;
    public Text scoreText;
    public Text waitingText;
    public Text roomNameText;
    public Text inGameScoreText;

    private bool hadOneGame = false;
    private float nextUpdate = 0;

    private void OnEnable()
    {
      hadOneGame = false;
      screen.SetActive(false);
      GameState.OnGameEnd += HandleOnGameEnd;
      GameState.OnGameStart += HandleOnGameStart;
      ClientInstantiator.OnInstanceCreated += HandleOnInstanceCreated;
      Alien.OnTotalChanged += CheckPlayers;
      Helicopter.OnTotalChanged += CheckPlayers;
    }

    private void OnDisable()
    {
      GameState.OnGameEnd -= HandleOnGameEnd;
      GameState.OnGameStart -= HandleOnGameStart;
      ClientInstantiator.OnInstanceCreated -= HandleOnInstanceCreated;
      Alien.OnTotalChanged -= CheckPlayers;
      Helicopter.OnTotalChanged -= CheckPlayers;
    }

    private void Update()
    {
      if (!GameState.Instance.inGame)
      {
        return;
      }
      if (nextUpdate > Time.realtimeSinceStartup)
      {
        return;
      }
      nextUpdate = Time.realtimeSinceStartup + 1;
      inGameScoreText.text = $"O stolen:\n{GameState.Instance.airStolen} / {GameState.minAirAlienNeeds}";
    }

    private void HandleOnGameEnd()
    {
      CheckState();
      inGameScoreText.text = $"O stolen:\n{GameState.Instance.airStolen} / {GameState.minAirAlienNeeds}";
    }

    private void HandleOnGameStart()
    {
      hadOneGame = true;
      CheckState();
    }

    private void HandleOnInstanceCreated()
    {
      CheckState();
    }

    private void CheckState()
    {
      if (GameState.Instance.inGame || ClientInstantiator.Instance == null)
      {
        screen.SetActive(false);
        return;
      }
      screen.SetActive(true);
      bool isHelicopter = ClientInstantiator.Instance.GetIsHelicopter();
      bool helicopterWin = GameState.Instance.airStolen < GameState.minAirAlienNeeds;
      winLoseLabel.SetActive(hadOneGame);
      winLoseText.text = isHelicopter ?
        (helicopterWin ? "Helicopters win!" : "Helicopters lost!") :
        (helicopterWin ? "Aliens lost!" : "Aliens win!");
      scoreLabel.SetActive(hadOneGame);
      scoreText.text = $"{GameState.Instance.airStolen} kg Oxygen stolen\nof {GameState.minAirAlienNeeds} Alien goal";
      roomNameText.text = $"Room: {Coherence.Samples.RoomsDialog.RoomsDialogUI.currentRoomName}";
      CheckPlayers();
    }

    private void CheckPlayers()
    {
      startGameButton.interactable = Helicopter.totalHelicopters > 0 && Alien.totalAliens > 0;
      waitingText.text = Helicopter.totalHelicopters > 0 ? "Waiting for more Aliens" : "Waiting for more Helicopters";
      waitingForPlayersLabel.SetActive(!startGameButton.interactable);
    }

    public void StartGameClicked()
    {
      GameState.Instance.StartGame();
    }
  }
}
