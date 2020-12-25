using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : MonoBehaviour
{

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


    void Start()
    {
        if (gameObject.tag == "L1")
        {
            gameObject.GetComponent<MeshRenderer>().material = L1;
            FaSame = 10.0f;
        }
        else if (gameObject.tag == "L2")
        {
            FaSame = 6.0f;
            gameObject.GetComponent<MeshRenderer>().material = L2;
        }
        else if (gameObject.tag == "Output")
        {
            gameObject.GetComponent<MeshRenderer>().material = Output;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = Input;
        }

    }

    void Update()
    {
        if (!Controller.isWaiting) {
            if (gameObject.tag == "Input")
            {
                GameObject Node = GameObject.FindGameObjectWithTag("Output");
                float distance = Vector3.Distance(Node.transform.position, transform.position);
                Vector3 direction = Node.transform.position - transform.position;
                Vector3 directionNorm = direction / distance;
                //gameObject.GetComponent<Rigidbody>().AddForce(FaSame * direction);
                gameObject.GetComponent<Rigidbody>().AddForce((-FrInOut / Mathf.Pow(distance, 2f)) * directionNorm);
            }
            else if (gameObject.tag == "Output")
            {
                GameObject Node = GameObject.FindGameObjectWithTag("Input");
                float distance = Vector3.Distance(Node.transform.position, transform.position);
                Vector3 direction = Node.transform.position - transform.position;
                Vector3 directionNorm = direction / distance;
                //gameObject.GetComponent<Rigidbody>().AddForce(FaSame * direction);
                gameObject.GetComponent<Rigidbody>().AddForce((-FrInOut / Mathf.Pow(distance, 2f)) * directionNorm);
            }
            else
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
                }
            }
        } else {
            // just fix the position on waiting
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }
}