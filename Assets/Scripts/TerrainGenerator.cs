using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int brushRadius;
    [SerializeField] private float brushStrength;
    [SerializeField] private float isoValue = 1f;

    [SerializeField] private int width, height;

    [SerializeField] private float gridScale;

    private float[,] grid;

    private void Start()
    {
        grid = new float[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                grid[x, y] = isoValue + 0.1f;
            }
        }
        InputHandler.OnTouching += HandleOnTouching;
    }

    private void HandleOnTouching(Vector3 worldPosition)
    {
        Debug.Log(worldPosition);

        worldPosition.z = 0;

        Vector2Int gridPosition = GetGridPosition(worldPosition);

        for (int y = gridPosition.y - brushRadius; y <= gridPosition.y + brushRadius; y++)
        {
            for (int x = gridPosition.x - brushRadius; x <= gridPosition.x + brushRadius; x++)
            {
                Vector2Int currentGridPosition = new Vector2Int(x, y);

                if (!IsValidGridPosition(currentGridPosition))
                {
                    Debug.LogError("Invalid position");
                    continue;
                }

                grid[gridPosition.x, gridPosition.y] -= brushStrength;
            }
        }
    }

    private bool IsValidGridPosition(Vector2Int gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < width && gridPosition.y >= 0 && gridPosition.y < height;
    }

    private Vector2 GetWorldPosition(int x, int y)
    {
        Vector2 worldPosition = new Vector2(x, y) * gridScale;
        worldPosition.x -= (width * gridScale) * 0.5f - gridScale * 0.5f;
        worldPosition.y -= (height * gridScale) * 0.5f - gridScale * 0.5f;

        return worldPosition;
    }

    private Vector2Int GetGridPosition(Vector2 worldPosition)
    {
        Vector2Int gridPosition = new Vector2Int
        {
            x = Mathf.FloorToInt(worldPosition.x / gridScale + width * 0.5f - gridScale * 0.5f),
            y = Mathf.FloorToInt(worldPosition.y / gridScale + height * 0.5f - gridScale * 0.5f)
        };

        return gridPosition;
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if (!EditorApplication.isPlaying)
            return;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2 worldPosition = GetWorldPosition(x, y);

                Gizmos.DrawSphere(worldPosition, gridScale * 0.2f);
            }
        }
    }
#endif
}