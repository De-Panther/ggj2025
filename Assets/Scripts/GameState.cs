using System;
using UnityEngine;
using Coherence.Toolkit;

namespace GGJGame
{
  public class GameState : MonoBehaviour
  {
    public static GameState Instance;

    public static Action OnGameEnd;
    public static Action OnGameStart;

    public const float maxAir = 700f;
    public const float minAirAlienNeeds = 400f;
    private const float maxTime = 120f;

    [SerializeField]
    private CoherenceSync coherenceSync;

    public float airStolen;
    public float remainingTime;
    public bool inGame;
    private int prevInGame = -1;

    private void Awake()
    {
      Instance = this;
    }

    private void OnEnable()
    {
      Alien.OnTotalChanged += CheckPlayers;
      Helicopter.OnTotalChanged += CheckPlayers;
    }

    private void OnDisable()
    {
      Alien.OnTotalChanged -= CheckPlayers;
      Helicopter.OnTotalChanged -= CheckPlayers;
    }

    private void Update()
    {
      if (prevInGame < 0 || (prevInGame == 0 && inGame) || (prevInGame == 1 && !inGame))
      {
        prevInGame = inGame ? 1 : 0;
        if (inGame)
        {
          OnGameStart?.Invoke();
        }
        else
        {
          OnGameEnd?.Invoke();
        }
      }
      if (!coherenceSync.HasStateAuthority)
      {
        return;
      }
      if (!inGame)
      {
        return;
      }
      remainingTime -= Time.deltaTime;
      if (remainingTime < 0.01)
      {
        airStolen = UnityEngine.Random.Range(100f, maxAir);
        inGame = false;
      }
    }

    public void StartGame()
    {
      coherenceSync.SendCommand<GameState>(nameof(SetGameForStart),
        Coherence.MessageTarget.AuthorityOnly);
    }

    public void SetGameForStart()
    {
      remainingTime = maxTime;
      inGame = true;
    }

    private void CheckPlayers()
    {
      if (!inGame)
      {
        return;
      }
      inGame = Helicopter.totalHelicopters > 0 && Alien.totalAliens > 0;
    }
  }
}
