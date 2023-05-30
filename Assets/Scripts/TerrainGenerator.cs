using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int width, height;

    [SerializeField] private float gridScale;

    private float[,] grid;

    private void Start()
    {
        grid = new float[width,height];
    }

    private Vector2 GetWorldPosition(int x, int y)
    {
        Vector2 worldPosition = new Vector2(x, y) * gridScale;
        worldPosition.x -= (width * gridScale) * 0.5f - gridScale * 0.5f;
        worldPosition.y -= (height * gridScale) * 0.5f - gridScale * 0.5f;

        return worldPosition;
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
