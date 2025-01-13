using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour
{
    public Node cameFrom;
    public List<Node> Connections;

    public float gScore;
    public float hScore;

    public float fScore()
    {
        return gScore + hScore;
    }


    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if(Connections.Count > 0)
        {
            for(int i=0;i<Connections.Count;i++)
            {
                Gizmos.DrawLine(transform.position, Connections[i].transform.position);
            }
        }

    }
    */
}
