using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGrid : MonoBehaviour
{
    public int numberOfPoints = 10;
    public float radius = 5f;
    public GameObject gridPointPrefab;
    public List<Vector2> gridPositions = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        CreateCircleGrid();
    }

    private void CreateCircleGrid()
    {
        float angleStep = 360f / numberOfPoints;

        for (int i = 0; i < numberOfPoints; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);

            Vector2 point = new Vector2(x, y);
            gridPositions.Add(point);

            if(gridPointPrefab != null)
            {
                Instantiate(gridPointPrefab, point, Quaternion.identity);
            }
        }
    }
}
