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
    public float FaSame = 3.0f;
    // Repulsion between nodes from the same layer
    public float FrSame = 500.0f;
    // Repulsion between input and output
    public float FrInOut = 1000.0f;


    void Start () {
        // color link according to status
        Color c;
        if (gameObject.tag == "L1")
        {
            //c = Color.white;
            gameObject.GetComponent<MeshRenderer>().material = L1;
            FaSame = 10.0f;
        }
        else if (gameObject.tag == "L2")
        {
            //c = Color.blue;
            FaSame = 6.0f;
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
            //gameObject.GetComponent<Rigidbody>().AddForce(FaSame * direction);
            gameObject.GetComponent<Rigidbody>().AddForce((-FrInOut / Mathf.Pow(distance, 2f)) * directionNorm);
            Debug.Log("Inttt");
            Debug.Log(FrInOut);
        } else if (gameObject.tag == "Output")
        {
            GameObject Node = GameObject.FindGameObjectWithTag("Input");
            float distance = Vector3.Distance(Node.transform.position, transform.position);
            Vector3 direction = Node.transform.position - transform.position;
            Vector3 directionNorm = direction / distance;
            //gameObject.GetComponent<Rigidbody>().AddForce(FaSame * direction);
            gameObject.GetComponent<Rigidbody>().AddForce((-FrInOut / Mathf.Pow(distance, 2f)) * directionNorm);
            Debug.Log(FrInOut);
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
                gameObject.GetComponent<Rigidbody>().AddForce(FaSame * direction);
                gameObject.GetComponent<Rigidbody>().AddForce((-FrSame / Mathf.Pow(distance, 2f)) * directionNorm);
                Debug.Log("Node: " + FrSame);
            }
        }

    }
}

