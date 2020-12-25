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


    float[] L2ToL1;
    float L2ToOut = -1;

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
        L2ToL1 = new float[Controller.layer1Count];
        client = GameObject.FindObjectOfType<Client>();

    }

    void Update()
    {
        if (!Controller.isWaiting) {
            L2ToOut = -1;
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

                    // 其实这里这种细节，为啥同一层就是一个简单的正比于距离的力，也是需要推敲的，没准也需要更新一波。写paper前需要把这些细节背后的道理都解释清楚。
                    gameObject.GetComponent<Rigidbody>().AddForce(FaSame * direction);
                    gameObject.GetComponent<Rigidbody>().AddForce((-FrSame / Mathf.Pow(distance, 2f)) * directionNorm);
                }
            }
        } else {
            // just fix the position on waiting
            // gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            // Check if this node is grabbed by hand
            if (transform.GetComponent<OVRGrabbable>().isGrabbed) {
                gameObject.transform.localScale = new Vector3(2, 2, 2);
                if (gameObject.tag == "L1") {


                } else if (gameObject.tag == "L2") {
                    // GameObject Input = GameObject.FindGameObjectWithTag("Output");
                    // GameObject[] Nodes = GameObject.FindGameObjectsWithTag("L1");
                    // foreach (GameObject Node in Nodes)
                    // {
                    // }

                    Debug.Log("hey, condition");
                        Debug.Log(L2ToOut);
                    if (L2ToOut == -1 ) { // if the original position is not defined yet
                        gameObject.transform.localScale = new Vector3(3, 3, 3);
                        // (layer 1 -> layer 2)
                        for (int i = 0; i < Controller.layer1Count; i++)
                        {
                            Debug.Log("Print out distances");
                            Debug.Log(i);
                            Node L1Node = Controller.nodes[i] as Node;
                            Debug.Log(Vector3.Distance(L1Node.transform.position, gameObject.transform.position));
                            L2ToL1[i] = Vector3.Distance(L1Node.transform.position, gameObject.transform.position);
                        }
                        // (layer 2 -> output)
                        Node OutNode = Controller.nodes[2000] as Node;
                        L2ToOut = Vector3.Distance(OutNode.transform.position, gameObject.transform.position);
                        Debug.Log("hey, hereeeeee");
                        Debug.Log(L2ToOut);

                    } else { // if the position is already set, we can send the new position info
                        gameObject.transform.localScale = new Vector3(1, 1, 1);
                        float[] changes = new float[Controller.layer1Count+2];; // changes are the ratio that each link changed
                        for (int i = 0; i < Controller.layer1Count; i++)
                        {
                            Debug.Log("In this loop now");
                            Debug.Log(i);
                            Node L1Node = Controller.nodes[i] as Node;
                            changes[i] = Vector3.Distance(L1Node.transform.position, gameObject.transform.position)/L2ToL1[i];
                        }
                        Node OutNode = Controller.nodes[2000] as Node;
                        changes[Controller.layer1Count] = Vector3.Distance(OutNode.transform.position, gameObject.transform.position)/L2ToOut;
                        changes[Controller.layer1Count + 1] = (float)(id-20); // id of this node
                        string msg = String.Join("_", changes);
                        Debug.Log("Got new positionssss");
                        Debug.Log(msg);
                        client.requester.SetMessage(msg);
                    }


                }
            } else { // if this object is released or not grabbed, reset the L2ToOut
                L2ToOut = -1;
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }

    }
}