using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    float spaceSize = 1f;

    [SerializeField]
    Vector2 gridSize = new(10,10);

    [SerializeField]
    GameObject prefab;

    readonly Vector3 xz = Vector3.one - Vector3.up;

    public Vector3 WorldPositionFromGridSpace(Vector2 gridSpace)
    {
        Vector3 grid3dSpace = new(gridSpace.x, 0, gridSpace.y);
        Vector3 scaledGrid3dSpace = Vector3.Scale(grid3dSpace, spaceSize * xz);
        Vector3 gridOffset = -new Vector3(gridSize.x, 0, gridSize.y) / 2;
        Vector3 spaceOffset = xz * spaceSize / 2;

        return transform.position + scaledGrid3dSpace + gridOffset + spaceOffset ;
    }

    public Vector2 GridSpaceFromWorldSpace(Vector3 worldSpace) {

        return Vector3.zero;
    
    }
    private void Start()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                GameObject cube = Instantiate(prefab);
                cube.transform.position = WorldPositionFromGridSpace(new Vector2(x, y));
                cube.transform.parent = transform;
            }
        }
    }
}
