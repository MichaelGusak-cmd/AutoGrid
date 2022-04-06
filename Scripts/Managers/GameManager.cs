using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int RoundsToBeat;
    public static GameManager Instance;
    public GameState GameState;
    
    private int roundCounter;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        roundCounter = 0;
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                MenuManager.Instance.goal = RoundsToBeat;
                MenuManager.Instance.printGoal();
                SelectionManager.Instance.GenerateGrid();
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.RollUnits:
                if (!GridManager.Instance.CheckGameEnd()) SelectionManager.Instance.RollUnits();
                else ChangeState(GameState.Lose);
                break;
            case GameState.CombatTurn:
                roundCounter++;
                MenuManager.Instance.rounds = roundCounter;
                MenuManager.Instance.printGoal();
                GridManager.Instance.Action();
                break;
            case GameState.Lose:
                if (roundCounter < RoundsToBeat)
                {   //actually lost, reset level
                    Debug.Log("Lmao, You Lost");
                    SceneManager.LoadScene(sceneName: "Tutorial");
                }
                else
                {   //win! goto next level
                    Debug.Log("Level Beat!");
                    if (SceneManager.GetActiveScene().name == "Tutorial")
                        SceneManager.LoadScene(sceneName: "Level1");
                    else if (SceneManager.GetActiveScene().name == "Level1")
                        SceneManager.LoadScene(sceneName: "Level2");
                    else if (SceneManager.GetActiveScene().name == "Level2")
                        SceneManager.LoadScene(sceneName: "Tutorial");
                    //move to next level button.active = true
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum GameState
{
    GenerateGrid = 0,
    RollUnits = 1,
    CombatTurn = 2,
    Lose = 3
}
