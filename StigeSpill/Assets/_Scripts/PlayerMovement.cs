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

        //calculate new grid index after dice roll
        int targetIndex = (currentGridIndex + steps) % grid.numberOfPoints;
        while(currentGridIndex != targetIndex)
        {
            currentGridIndex = (currentGridIndex + 1) % grid.numberOfPoints;

            Vector2 nextPosition = grid.gridPositions[currentGridIndex];

            while((Vector2)transform.position != nextPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, nextPosition, movementSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
        }

        isMoving = false;
    }
}
