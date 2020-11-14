using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : MonoBehaviour {

    public int id;
    // // Attraction
    // public float FA = 3.0f;
    // // Repulsion
    // public float FR = 5.0f;


    void Start () {
        //color link according to status
        Color c;
        if (gameObject.tag == "L1")
            c = Color.white;
        else
            c = Color.red;
        c.a = 0.5f;

        //draw line
        gameObject.GetComponent<Renderer>().material.SetColor ("_Color", c);
    }

    void Update () {
        // Nodes = GameObject.FindGameObjectsWithTag(gameObject.tag);
        // foreach (GameObject Node in Nodes)
        // {
        //     // Apply attraction/repulsion from the other nodes to this node
        //     Vector3 direction = Node.transform.position - transform.position;
        //     gameObject.GetComponent<Rigidbody>().AddForce(FA * direction);
        //     gameObject.GetComponent<Rigidbody>().AddForce(-FR * direction);
        // }
    }
}

