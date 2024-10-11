using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGrid : MonoBehaviour
{
    public int numberOfRings = 5;
    //number of segments per ring (ex [8, 12, 16, 20, 24...]
    public int[] segmentsPerRing = new int[] { 8, 16, 24, 32, 40 };
    //distance between each ring
    public float ringSpacing = 2f;
    public GameObject gridPointPrefab;
    //to store all grid positions
    public List<Vector2> gridPositions = new List<Vector2>();


    // Start is called before the first frame update
    void Start()
    {
        CreateCircleGrid();
    }

    private void CreateCircleGrid()
    {
        for (int ring = 0; ring < segmentsPerRing.Length; ring++)
        {
            //radius of the current ring
            float radius = ringSpacing * (ring + 1);
            //segments for the current ring
            int numberOfSegments = segmentsPerRing[ring];
            float angleStep = 360f / numberOfSegments;

            for(int segment = 0; segment < numberOfSegments; segment++)
            {
                //calculate the angle for the current segment
                float angle = segment * angleStep * Mathf.Deg2Rad;

                //calculate the x and y position using polar coordinates
                float x = radius * Mathf.Cos(angle);
                float y = radius * Mathf.Sin(angle);

                //create a vector2 position
                Vector2 gridPosition = new Vector2(x, y);

                //store the position in the list
                gridPositions.Add(gridPosition);

                if(gridPointPrefab != null)
                {
                    Instantiate(gridPointPrefab, gridPosition, Quaternion.identity);
                }
            }
        }
    }
}
