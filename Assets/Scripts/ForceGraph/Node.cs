using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : MonoBehaviour {

    public int id;
    // Attraction between nodes from the same layer
    public float FaSame = 3.0f;
    // Repulsion between nodes from the same layer
    public float FrSame = 500.0f;


    void Start () {
        //color link according to status
        Color c;
        if (gameObject.tag == "L1") {
            c = Color.white;
            FaSame = 10.0f;
        }
        else
            c = Color.red;
        c.a = 0.5f;

        //draw line
        gameObject.GetComponent<Renderer>().material.SetColor ("_Color", c);

    }

    void Update () {
        GameObject[] Nodes = GameObject.FindGameObjectsWithTag(gameObject.tag);
        foreach (GameObject Node in Nodes)
        {   if (Node.Equals(this.gameObject))
                 continue; // skip this node itself
            // Euclidean distance between them (sqrt)
            float distance = Vector3.Distance(Node.transform.position, transform.position);
            // Apply attraction/repulsion from the other nodes to this node
            Vector3 direction = Node.transform.position - transform.position;
            // Apply attraction/repulsion
            Vector3 directionNorm = direction/distance;
            // Vector3 directionNorm = direction.normalized;
            gameObject.GetComponent<Rigidbody>().AddForce(FaSame * direction);
            gameObject.GetComponent<Rigidbody>().AddForce((-FrSame / Mathf.Pow(distance, 2f)) * directionNorm);
            Debug.Log("Node: " + FrSame);
        }
    }
}

