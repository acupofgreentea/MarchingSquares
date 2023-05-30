using UnityEngine;

public class SquareMeshHandler : MonoBehaviour
{
    [SerializeField] private float topRightValue, bottomRightValue, bottomLeftValue, topLeftValue;
    [SerializeField] private float isoValue;

    private MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        Mesh mesh = new Mesh();

        Square square = new Square(Vector2.zero, 1f);
        square.Triangulate(isoValue, new []{topRightValue, bottomRightValue, bottomLeftValue, topLeftValue});

        mesh.vertices = square.GetVertices;
        mesh.triangles = square.GetTriangles;

        meshFilter.mesh = mesh;
    }
}
