using UnityEngine;
using System.Collections;

public class GizmosManager
{

    public static void DrawGridBaseLayout(Vector3[,] grid, int gridSizeX, int gridSizeY, float gridCellDiameter ,float gridRotation = 0)
    {
        
        
        if (grid != null && grid.Length > 0)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                for (int x = 0; x < gridSizeX; x++)
                {
                    if (x == 0 || y == 0 || x == gridSizeX - 1 || y == gridSizeY - 1)
                    {
                        Gizmos.matrix = Matrix4x4.TRS(grid[x, y], Quaternion.Euler(Vector3.up * gridRotation), new Vector3(gridCellDiameter, .05f, gridCellDiameter));
                        Gizmos.color = Color.red;
                        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
                    }

                }
            }
        }
    }

    public static void DrawSphere(Vector3 point, float radius)
    {
        Gizmos.DrawSphere(point, radius);
    }

    public static void DrawCircle(Vector3 center, float radius, float resolution = 20)
    {
        float inscrement = 360f / resolution;
        Vector3[] Points = new Vector3[(int)resolution];
        for (int i = 0; i < Points.Length; i++)
        {
            float angle = Mathf.Deg2Rad * inscrement * i;
            float x = Mathf.Sin(angle);
            float z = Mathf.Cos(angle);

            Points[i] = new Vector3(center.x + x * radius, center.y, center.z + z * radius);
        }
        
        for (int i = 0; i < Points.Length; i++)
        {
           
            Gizmos.DrawLine(Points[i], Points[(i + 1) % Points.Length]);
        }
    }

}
