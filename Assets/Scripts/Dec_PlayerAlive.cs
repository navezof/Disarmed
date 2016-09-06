using UnityEngine;
using System.Collections;
using System;

/**
 * Check if the player is alive
 */
public class Dec_PlayerAlive : ADecorator
{
    PawnPlayer player;

    void Start()
    {
        player = SwarmController.GetSwarmController().GetPlayer();
    }

    public override bool Try()
    {
        if (player.GetHealth().IsDead())
            return false;
        return true;
    }
}
