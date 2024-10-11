using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CircleGrid grid;
    //current position on the grid
    public int currentGridIndex = 0;
    //player speed
    public float movementSpeed = 2f;

    //check if player is already moving
    private bool isMoving = false;


    public void MovePlayer(int steps)
    {
        //prevent multiple movements at the same time
        if (!isMoving)
        {
            StartCoroutine(MoveToPosition(steps));
        }
    }

    private IEnumerator MoveToPosition(int steps)
    {
        isMoving = true;

        //calculate the total number of points using gridPositions.Count
        int numberOfPoints = grid.gridPositions.Count;

        //calculate new grid index after dice roll
        int targetIndex = (currentGridIndex + steps) % numberOfPoints;

        //move step by step towards the target index 
        while (currentGridIndex != targetIndex)
        {
            //increment the index with wrap-around behavior
            currentGridIndex = (currentGridIndex + 1) % numberOfPoints;

            //get the next position on the grid based on the current index
            Vector2 nextPosition = grid.gridPositions[currentGridIndex];

            //move the player towards the next position
            while ((Vector2)transform.position != nextPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, nextPosition, movementSpeed * Time.deltaTime);
                //wait for the next frame before continuing
                yield return null;
            }

            //small delay between moving to each step
            yield return new WaitForSeconds(0.1f);
        }

        //after reaching the final position check for event tile
        CheckForEventTile();

        //after reaching the target index set moving to false
        isMoving = false;
    }

    private void CheckForEventTile()
    {
        //check for nearby objects
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.1f);

        if (collider != null)
        {
            EventTile eventTile = collider.GetComponent<EventTile>();
            if (eventTile != null)
            {
                //trigger the event if present
                eventTile.TriggerEvent(this);
            }
        }
    }

    //utility method to move the player directly to an index (can be used in some event tiles)
    public void MovePlayerToIndex(int newIndex)
    {
        currentGridIndex = newIndex;
        transform.position = grid.gridPositions[newIndex];
    }
}
