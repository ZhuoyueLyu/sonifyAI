using System;
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
    private Client client;


    float[] oldDistances;
    float[] newDistances;
    bool oldDisSaved = false;

    void Start()
    {
        if (gameObject.tag == "L1" || gameObject.tag == "newL1")
        {
            gameObject.GetComponent<MeshRenderer>().material = L1;
            FaSame = 10.0f;
        }
        else if (gameObject.tag == "L2"|| gameObject.tag == "newL2")
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
        client = GameObject.FindObjectOfType<Client>();

    }

    void Update()
    {
        if (!Controller.isWaiting) {
            oldDisSaved = false;
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
            { // L1 or L2
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

                    // 其实这里这种细节，为啥同一层就是一个简单的正比于距离的力，也是需要推敲的，没准也需要更新一波。写paper前需要把这些细节背后的道理都解释清楚。
                    gameObject.GetComponent<Rigidbody>().AddForce(FaSame * direction);
                    gameObject.GetComponent<Rigidbody>().AddForce((-FrSame / Mathf.Pow(distance, 2f)) * directionNorm);
                }
            }
        } else { // waiting (all nodes' positions fixed)
            // Check if this node is grabbed by hand
            if (transform.GetComponent<OVRGrabbable>().isGrabbed) {
                // for demo purpose, we currently only allowed the user to adjust the L2 or Input's nodes
                if (gameObject.tag == "L2") {
                    if (!oldDisSaved) { // if the original position is not defined yet
                        oldDistances = new float[Controller.layer1Count + 1];
                        newDistances = new float[Controller.layer1Count + 3];
                        // (layer 1 -> layer 2)
                        for (int i = 0; i < Controller.layer1Count; i++) {
                            Node L1Node = Controller.nodes[i] as Node;
                            oldDistances[i] = Vector3.Distance(L1Node.transform.position, gameObject.transform.position);
                        }
                        // (layer 2 -> output)
                        Node OutNode = Controller.nodes[2000] as Node;
                        oldDistances[Controller.layer1Count] = Vector3.Distance(OutNode.transform.position, gameObject.transform.position);
                        oldDisSaved = true;

                    } else { // if the original position is already saved, we can send the new position info
                        // (layer 1 -> layer 2)
                        for (int i = 0; i < Controller.layer1Count; i++) {
                            Node L1Node = Controller.nodes[i] as Node;
                            newDistances[i] = Vector3.Distance(L1Node.transform.position, gameObject.transform.position)/oldDistances[i];
                        }
                        // (layer 2 -> output)
                        Node OutNode = Controller.nodes[2000] as Node;
                        newDistances[Controller.layer1Count] = Vector3.Distance(OutNode.transform.position, gameObject.transform.position) / oldDistances[Controller.layer1Count];
                        // for convenience, we save the id and tag of this node in this list as well
                        newDistances[Controller.layer1Count + 1] = (float)(id - 20); // id of this node
                        newDistances[Controller.layer1Count + 2] = (float)2; // tag of this node, 2 represent L2
                        string msg = String.Join("_", newDistances);
                        msg = msg + "_updateWeights";
                        client.requester.SetMessage(msg);
                    }
                } else if (gameObject.tag == "Input") {
                    if (!oldDisSaved) {
                        oldDistances = new float[Controller.layer1Count];
                        newDistances = new float[Controller.layer1Count + 2];
                        // (Input -> layer 1)
                        for (int i = 0; i < Controller.layer1Count; i++) {
                            Node L1Node = Controller.nodes[i] as Node;
                            oldDistances[i] = Vector3.Distance(L1Node.transform.position, gameObject.transform.position);
                        }
                        oldDisSaved = true;

                    } else { // if the original position is already saved, we can send the new position info
                        // (Input -> layer 1)
                        for (int i = 0; i < Controller.layer1Count; i++) {
                            Node L1Node = Controller.nodes[i] as Node;
                            newDistances[i] = Vector3.Distance(L1Node.transform.position, gameObject.transform.position)/oldDistances[i];
                        }
                        // for convenience, we save the id and tag of this node in this list as well
                        newDistances[Controller.layer1Count] = (float)(1000); // id of this node
                        newDistances[Controller.layer1Count + 1] = (float)0; // tag of this node, 0 represent Input
                        string msg = String.Join("_", newDistances);
                        msg = msg + "_updateWeights";
                        client.requester.SetMessage(msg);
                    }
                }
            } else { // if this object is released or not grabbed, reset the oldDisSaved
                oldDisSaved = false;
            }
        }

    }
}