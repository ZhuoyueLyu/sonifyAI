
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controller : MonoBehaviour {

    public Node nodePrefab;
    public Link linkPrefab;
    public Transform centerMass;
    public Transform leftHand;
    private Client client;
    public static bool isWaiting = false;

    int nodeCount = 0;
    int linkCount = 0;

    Hashtable nodes;
    Hashtable links;

    float x = 0;
    float y = 0;
    float z = 0;

    int layer1Count = 3;
    int layer2Count = 4;

    int k = 10; // since the value of weight is pretty small, we need to multiply it by k

    Vector3 center = new Vector3(0, 0, 0);

    // // Attraction
    // public float FA = 3.0f;
    // // Repulsion
    // public float FR = 5.0f;

    // private IDictionary<int, Node> nodes = new Dictionary<int, Node>();
    // private IDictionary<int, Link> links = new Dictionary<int, Link>();

    // List of nodes at the same layer
    // GameObject[] L1;
    // GameObject[] L2;

    void GenerateGraph(){

        // input layer
        x = -4;
        y = 4;
        z = -4;
        CreateNode(1000, "Input", x, y, z);

        // layer 1
        for(int i=0; i<layer1Count; i++){
            x = Random.Range(-3, 0);
            y = Random.Range(-3, 0);
            z = Random.Range(-3, 0);
            CreateNode(i, "L1", x, y, z);
        }
        // layer 2
        for(int j=0; j<layer2Count; j++){
            x = Random.Range(0, 3);
            y = Random.Range(0, 3);
            z = Random.Range(0, 3);
            CreateNode(20 + j, "L2", x, y, z);
        }
        // output layer
        x = -4;
        y = 4;
        z = 4;
        CreateNode(2000, "Output", x, y, z);

        // add links (input -> layer 1)
        for (int i=0; i<layer1Count; i++){
            CreateLink(i, 1000);
        }
        // add links (layer 1 -> layer 2)
        for (int i = 0; i < layer1Count; i++)
        {
            for (int j = 0; j < layer2Count; j++)
            {
                CreateLink(i, 20 + j);
            }
        }
        // add links (layer 2 -> output)
        for (int j = 0; j < layer2Count; j++)
        {
            CreateLink(2000, 20 + j);
        }
        //map node edges
        MapLinkNodes();
    }

    //Create nodes
    void CreateNode(int id, string tag, float x, float y, float z) {
        Node nodeObject = Instantiate(nodePrefab, new Vector3(x, y, z), Quaternion.identity) as Node;
        nodeObject.tag = tag;
        nodeObject.id = id;
        // Drag make sure that the system won't oscillate forever
        nodeObject.GetComponent<Rigidbody>().drag = 10; // 没准后面drag可以用来调整间距用，就，drap100基本把它定死了。
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
        client = GameObject.FindObjectOfType<Client>();
        // Time.fixedDeltaTime = 0.2f;
    }

    // update the force among the nodes from the same layer
    void Update () {
       center = new Vector3(0, 0, 0);
       foreach(int key in nodes.Keys) {
            Node node = nodes[key] as Node;
            if (key == 1) {
                Debug.Log(key);
            Debug.Log(node.transform.position);
            }

            center += node.transform.position;
       }
       Debug.Log("Offset");
       Debug.Log(Vector3.Distance(center/nodeCount, leftHand.transform.position));
       if (Vector3.Distance(center/nodeCount, leftHand.transform.position) < 2) {
           client.requester.SetMessage("wait");
           isWaiting = true;
       } else {
           isWaiting = false;
           client.requester.SetMessage("nothing");
       }
       centerMass.position = center/nodeCount;
    }

    public void UpdateConnections(string infoString) {


        string[] vals = infoString.Split(',');
        //float ce = float.Parse(vals[0]) * 1000;
        //float acc = float.Parse(vals[1]) * 1000;
        bool isValidation = System.Convert.ToBoolean(float.Parse(vals[2]));
        if (isValidation) {
            string W1ByLinksString = vals[3];
            string W2ByLinksString = vals[4];
            string W3ByLinksString = vals[5];

            float[] W1ByLinks = System.Array.ConvertAll(W1ByLinksString.Split('_'), float.Parse);
            float[] W2ByLinks = System.Array.ConvertAll(W2ByLinksString.Split('_'), float.Parse);
            float[] W3ByLinks = System.Array.ConvertAll(W3ByLinksString.Split('_'), float.Parse);

            // update weights on each link
            // (input -> layer 1)
            for (int i = 0; i < layer1Count; i++)
            {
                Link link = links[i] as Link;
                // Debug.Log("W1LinkIndex" + i.ToString());
                link.FaBetween = k * W1ByLinks[i];
                link.c.a = W1ByLinks[i];
                // Debug.Log("W1ByLinkss" + W1ByLinks[i].ToString());
            }

            //  (layer 1 -> layer 2)
            int offset1To2 = layer1Count;
            for (int i = 0; i < layer1Count * layer2Count; i++)
            {

                Link link = links[i + offset1To2] as Link;
                // Debug.Log("W2LinkIndex" + (i + offset1To2).ToString());
                link.FaBetween = k * W2ByLinks[i];
                link.c.a = W2ByLinks[i];
                // Debug.Log("W2ByLinkss" + (W2ByLinks[i]).ToString());
            }
            //(layer 2 -> output)
            int offset2ToOut = layer1Count * layer2Count + offset1To2;
            for (int i = 0; i < layer2Count; i++)
            {
                Link link = links[i + offset2ToOut] as Link;
                Debug.Log("W3LinkIndex" + (i + offset2ToOut).ToString());
                link.FaBetween = k * W3ByLinks[i];
                link.c.a = W3ByLinks[i];
                Debug.Log("W3ByLinkss" + (W3ByLinks[i]).ToString());
            }
        }


        //Chuck.Manager.SetFloat(myChuck1, "ce", ce);
        //Chuck.Manager.SetFloat(myChuck1, "acc", acc);
        //Chuck.Manager.SetFloat(myChuck1, "isValidation", isValidation);


    }
}
