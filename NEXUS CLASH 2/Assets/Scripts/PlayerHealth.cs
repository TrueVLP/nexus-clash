using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{

    const int MAX_Health = 100;

    [Networked]
    public int CurrentHealth { get; private set; }

    private GameplayManager gameplayManager;

    public override void Spawned()
    {
        gameplayManager = FindFirstObjectByType<GameplayManager>();

        CurrentHealth = MAX_Health;
    }

    public void Update()
    {
        if (HasInputAuthority)
        {
            gameplayManager.UpdateHealth(CurrentHealth);
        }

        if (HasStateAuthority)
        {
            if (CurrentHealth <= 0)
            {
                gameplayManager.RespawnPlayer(Object.StateAuthority, GetComponent<PlayerController>());
            }
        }
    }

    public void Change(int amount)
    {
        CurrentHealth += amount;
    }
}
