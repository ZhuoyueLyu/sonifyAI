using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : MonoBehaviour {

    public int id;
    public Material Input;
    public Material L1;
    public Material L2;
    public Material Output;
    // Attraction between nodes from the same layer
    public float FaSameSmall = 300f;
    // Repulsion between nodes from the same layer
    public float FrSameSmall = 0.5f;
    // Repulsion between input and output
    public float FrInOutSmall = .1f;


    void Start () {
        // color link according to status
        Color c;
        if (gameObject.tag == "L1")
        {
            //c = Color.white;
            gameObject.GetComponent<MeshRenderer>().material = L1;
            FaSameSmall = 100.0f;
        }
        else if (gameObject.tag == "L2")
        {
            //c = Color.blue;
            FaSameSmall = 60f;
            gameObject.GetComponent<MeshRenderer>().material = L2;
        }
        else if (gameObject.tag == "Output")
        {
            //c = Color.yellow;
            gameObject.GetComponent<MeshRenderer>().material = Output;
        }
        else {
            //c = Color.red;
            gameObject.GetComponent<MeshRenderer>().material = Input;
        }
           
        c.a = 0.5f;

        //gameObject.GetComponent<Renderer>().material.SetColor ("_Color", c);

    }

    void FixedUpdate() {
        //Debug.Log(gameObject.tag);
        if (gameObject.tag == "Input")
        {
            GameObject Node = GameObject.FindGameObjectWithTag("Output");
            float distance = Vector3.Distance(Node.transform.position, transform.position);
            Vector3 direction = Node.transform.position - transform.position;
            Vector3 directionNorm = direction / distance;
            //gameObject.GetComponent<Rigidbody>().AddForce(FaSameSmall * direction);
            gameObject.GetComponent<Rigidbody>().AddForce((-FrInOutSmall / Mathf.Pow(distance, 2f)) * directionNorm);
            Debug.Log("Inttt");
            Debug.Log(FrInOutSmall);
        } else if (gameObject.tag == "Output")
        {
            GameObject Node = GameObject.FindGameObjectWithTag("Input");
            float distance = Vector3.Distance(Node.transform.position, transform.position);
            Vector3 direction = Node.transform.position - transform.position;
            Vector3 directionNorm = direction / distance;
            //gameObject.GetComponent<Rigidbody>().AddForce(FaSameSmall * direction);
            gameObject.GetComponent<Rigidbody>().AddForce((-FrInOutSmall / Mathf.Pow(distance, 2f)) * directionNorm);
            Debug.Log(FrInOutSmall);
        } else
        {
            GameObject[] Nodes = GameObject.FindGameObjectsWithTag(gameObject.tag);
            foreach (GameObject Node in Nodes)
            {
                if (Node.Equals(this.gameObject))
                    continue; // skip this node itself
                              // Euclidean distance between them (sqrt)
                float distance = Vector3.Distance(Node.transform.position, transform.position);
                // Apply attraction/repulsion from the other nodes to this node
                Vector3 direction = Node.transform.position - transform.position;
                // Apply attraction/repulsion
                Vector3 directionNorm = direction / distance;
                // Vector3 directionNorm = direction.normalized;
                gameObject.GetComponent<Rigidbody>().AddForce(FaSameSmall * direction);
                gameObject.GetComponent<Rigidbody>().AddForce((-FrSameSmall / Mathf.Pow(distance, 2f)) * directionNorm);
                Debug.Log("Node: " + FaSameSmall + FrSameSmall + FrInOutSmall);
            }
        }

    }
}

