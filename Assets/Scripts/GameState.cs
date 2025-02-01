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

    public const float minAirAlienNeeds = 60f;
    private const float maxTime = 60f;

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
      if (remainingTime < 0.01 || airStolen >= minAirAlienNeeds)
      {
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
      airStolen = 0;
      inGame = true;
    }

    public void AddAirStolen()
    {
      if (!inGame)
      {
        return;
      }
      if (!coherenceSync.HasStateAuthority)
      {
        return;
      }
      airStolen++;
    }

    public void HandleOnStateAuthority()
    {
      CheckPlayers();
    }

    private void CheckPlayers()
    {
      if (!inGame)
      {
        return;
      }
      if (!coherenceSync.HasStateAuthority)
      {
        return;
      }
      inGame = Helicopter.totalHelicopters > 0 && Alien.totalAliens > 0;
    }
  }
}
