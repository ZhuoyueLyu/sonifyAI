using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controller : MonoBehaviour {

    public Node nodePrefab;
    public Link linkPrefab;

    int nodeCount = 0;
    int linkCount = 0;

    Hashtable nodes;
    Hashtable links;

    float x = 0;
    float y = 0;
    float z = 0;

    // // Attraction
    // public float FA = 3.0f;
    // // Repulsion
    // public float FR = 5.0f;

    // private IDictionary<int, Node> nodes = new Dictionary<int, Node>();
    // private IDictionary<int, Link> links = new Dictionary<int, Link>();

    // List of nodes at the same layer
    //GameObject[] L1;
    //GameObject[] L2;

    void GenerateGraph(){
        int layer1Count = 2;
        int layer2Count = 3;

        // input layer
        x = 1;
        y = 1;
        z = 1;
        CreateNode(1000, "Input", x, y, z);

        // layer 1
        for(int i=0; i<layer1Count; i++){
            x = Random.Range(10, 12)/10;
            y = Random.Range(10, 12)/10;
            z = Random.Range(10, 12)/10;
            CreateNode(i, "L1", x, y, z);
        }
        // layer 2
        for(int j=0; j<layer2Count; j++){
            x = Random.Range(13, 15) / 10;
            y = Random.Range(13, 15) / 10;
            z = Random.Range(13, 15) / 10;
            CreateNode(20 + j, "L2", x, y, z);
        }
        // output layer
        x = 2;
        y = 2;
        z = 2;
        CreateNode(2000, "Output", x, y, z);

        // add links
        for (int i=0; i<layer1Count; i++){
            CreateLink(i, 1000);
            for(int j=0; j<layer2Count; j++){
                CreateLink(i, 20 + j);
                CreateLink(2000, 20 + j);
            }
        }
        //map node edges
        MapLinkNodes();
    }

    //Create nodes
    void CreateNode(int id, string tag, float x, float y, float z) {
        Node nodeObject = Instantiate(nodePrefab, new Vector3(0,0,0), Quaternion.identity) as Node;
        nodeObject.tag = tag;
        nodeObject.id = id;
        // Drag make sure that the system won't oscillate forever
        nodeObject.GetComponent<Rigidbody>().drag = 10;
        nodes.Add(nodeObject.id, nodeObject);
        nodeCount++;
    }

    //Create links
    void CreateLink(int sourceId, int targetId){
        Link linkObject = Instantiate(linkPrefab, new Vector3(0,0,0), Quaternion.identity) as Link;
        linkObject.id = linkCount;
        linkObject.sourceId = sourceId;
        linkObject.targetId = targetId;
        links.Add(linkObject.id, linkObject);
        linkCount++;

    }

    //Method for mapping links to nodes
    void MapLinkNodes(){
        foreach(int key in links.Keys){
            Link link = links[key] as Link;
            link.source = nodes[link.sourceId] as Node;
            link.target = nodes[link.targetId] as Node;
        }
    }

    // void UpdateSameLayerForces(GameObject[] Nodes) {
    //     foreach (GameObject Node in Nodes)
    //     {
    //         // Apply attraction/repulsion from the other nodes to this node
    //         Vector3 direction = Node.transform.position - transform.position;
    //         gameObject.GetComponent<Rigidbody>().AddForce(FA * direction);
    //         gameObject.GetComponent<Rigidbody>().AddForce(-FR * direction);
    //     }
    // }

    void Start () {
        nodes = new Hashtable();
        links = new Hashtable();

        GenerateGraph();
        // Time.fixedDeltaTime = 0.2f;
        // L1 = GameObject.FindGameObjectsWithTag("L1");
        // L2 = GameObject.FindGameObjectsWithTag("L2");
    }

    // update the force among the nodes from the same layer
    void Update () {


    }

}
