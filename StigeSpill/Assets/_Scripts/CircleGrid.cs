using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGrid : MonoBehaviour
{
    [Header("Event Tiles")]
    public GameObject[] eventTilePrefabs;
    public int numberOfEventTiles = 10;


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
        PlaceEventTiles();
    }

    public int GetRingForPosition(int gridIndex)
    {
        int accumulatedSegments = 0;
        for(int i = 0; i < segmentsPerRing.Length; i++)
        {
            accumulatedSegments += segmentsPerRing[i];
            if(gridIndex < accumulatedSegments)
            {
                return i; //return ring index
            }
        }
        return -1; //invalid index, should not happen
    }

    public int GetRingStartIndex(int ringIndex)
    {
        int startIndex = 0;
        for(int i = 0; i < ringIndex; i++)
        {
            startIndex += segmentsPerRing[i];
        }
        return startIndex;
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

    private void PlaceEventTiles()
    {
        //shuffle the grid positions to randomly pick event tile positions
        List<int> availablePositions = new List<int>();
        for(int i = 0; i < gridPositions.Count; i++)
        {
            availablePositions.Add(i);
        }

        //shuffle the list to randomize the available positions
        Shuffle(availablePositions);

        //place event tiles in random positions
        for(int i = 0; i < numberOfEventTiles && availablePositions.Count > 0; i++)
        {
            int randomIndex = availablePositions[i];
            Vector2 position = gridPositions[randomIndex];

            //pick a random event tile prefab to place
            GameObject randomEventTilePrefab = eventTilePrefabs[Random.Range(0, eventTilePrefabs.Length)];

            //instantiate the event tile at the grid position
            Instantiate(randomEventTilePrefab, position, Quaternion.identity);

            //removing the used position to prevent it from being reused?
            availablePositions.RemoveAt(i);
        }
    }

    //utility method to shuffle a list (fisher-yates shuffle)
    private void Shuffle(List<int> list)
    {
        for(int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
