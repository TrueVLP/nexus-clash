using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerTraining : MonoBehaviour
{
    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        NetworkRunner networkRunner = GetComponent<NetworkRunner>();

        SceneRef scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);

        networkRunner.StartGame(
            new StartGameArgs()
            {
                GameMode = GameMode.Single,
                SessionName = "SinglePlayer",
                Scene = scene
            });
        
    }
}
