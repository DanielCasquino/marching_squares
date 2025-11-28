using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Chunk : MonoBehaviour
{
    Mesh mesh;
    MeshFilter meshFilter;
    float [,] density;
    public float cellSize{get; private set;}

    public void Initialize(float _cellSize)
    {
        cellSize = _cellSize;
        GenerateMesh();
        GenerateCollider();
    }
    
    public void GenerateMesh()
    {
        mesh = new Mesh();
        mesh.RecalculateBounds();
        meshFilter.mesh = mesh;
    }

    public void GenerateCollider()
    {
        
    }
}
