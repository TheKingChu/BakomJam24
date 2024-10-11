using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownRingEvent : EventTile
{
    public override void TriggerEvent(PlayerMovement player)
    {
        //make the player fall down a ring (based on grid position)
        CircleGrid grid = player.grid;
        int currentRing = grid.GetRingForPosition(player.currentGridIndex);

        if(currentRing > 0)
        {
            int segmentInLowerRing = grid.segmentsPerRing[currentRing - 1];
            int newRingStartIndex = grid.GetRingStartIndex(currentRing - 1);
            int newGridIndex = newRingStartIndex + (player.currentGridIndex % segmentInLowerRing);

            //move player to the new position in the lower ring
            player.MovePlayerToIndex(newGridIndex);
        }
        else
        {
            Debug.Log("player is already on the lowest ring!");
        }
    }
}
