using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]GameObject draw;
    public bool GameOver { get; private set; }
    public bool LevelFinished { get; private set; }
    public bool Started { get; private set; }
    public bool Victory { get; private set; }

    public string PlayersRank { get; private set; } 

    private List<Controller> players = new List<Controller>();
    private List<Controller> playersWhoFinished = new List<Controller>();

    public override void Awake()
    {
        Init();
    }

    private void Update()
    {
        ControlPlayerStatus();
        SortPlayersRank();
    }

    // Starts the game by pressing any button
    public void StartTheGame() =>  Started = true;

    // Initialize Game at Start
    public void Init()
    {
        GameOver = false;
        LevelFinished = false;
        Started = false;
        Victory = false;
        if(draw != null) draw.SetActive(false);

        Controller[] _players = Resources.FindObjectsOfTypeAll<Controller>();
        foreach (var _player in _players)
            players.Add(_player);
    }

    public void EnableDrawing()
    {
        if (draw != null)
        {
            draw.SetActive(true);
            Victory = true;
        }
    }

    private void ControlPlayerStatus()
    {

        bool isAnyPlayerAlive = false;
        foreach (var player in players)
        {
            if (player is PlayerController playerController)
            {
                GameOver = !playerController.Alive;
                if (draw == null) Victory = playerController.LevelFinished;
            }

            if (player.Alive) isAnyPlayerAlive = true;

            if (player.LevelFinished)
            {
                LevelFinished = true;
                playersWhoFinished.Add(player);
            }
        }
        if (!isAnyPlayerAlive && !LevelFinished) GameOver = true;

        players.RemoveAll(player => playersWhoFinished.Contains(player));
    }
    private void SortPlayersRank()
    {
        PlayersRank = "";
        int index = 1;
        foreach (var player in playersWhoFinished)
            PlayersRank += $"{(index++):00} : {player.name}\n";

        //players.Sort( OrderByDescending(player => player.transform.position.z);
        //foreach (var player in players)
        //    PlayersRank += $"{(index++):00} : {player.name}\n";
        var rankQueue = players
            .OrderByDescending(player => player.transform.position.z)
            .Select(player => $"{(index++):00} : {player.name}\n");

        foreach (var player in rankQueue)
            PlayersRank += player;
    }
}
