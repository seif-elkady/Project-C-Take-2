using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] pathPoints;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i <= pathPoints.Length - 2; i++)
        {
            Gizmos.DrawLine(new Vector3(pathPoints[i].position.x, pathPoints[i].position.y),
                new Vector3(pathPoints[i + 1].position.x, pathPoints[i + 1].position.y));
        }

    }
}

