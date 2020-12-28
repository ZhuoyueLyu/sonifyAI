
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Controller : MonoBehaviour {


    public bool tempWait = false;
    public bool tempRightEnter = false;
    public bool tempDestroy = false;
    public Node tempRemoveNode;






    private Client client;
    public Node nodePrefab;
    public Link linkPrefab;
    public Transform centerMass;
    public GameObject L1Center;
    public GameObject L2Center;
    public Transform leftHand;
    public Transform rightHand;
    public static bool isWaiting = false;

    int nodeCount = 0;
    int linkCount = 0;

    public static Hashtable nodes;
    public static Hashtable links;

    float x = 0;
    float y = 0;
    float z = 0;

    public static int layer1Count = 3;
    public static int layer2Count = 4;
    private int newlayer1Count = 0;
    private int newlayer2Count = 0;
    // private bool newNodePickedUp = false;
    private Node newL1Node;
    private GameObject oldL1Node;
    private bool newL1NodeAssigned = false;
    private bool oldL1NodeRemoved = false;
    private Node newL2Node;
    private GameObject oldL2Node;
    private bool newL2NodeAssigned = false;
    private bool oldL2NodeRemoved = false;

    int k = 10; // since the value of weight is pretty small, we need to multiply it by k

    void GenerateGraph(){
        nodes = new Hashtable();
        links = new Hashtable();

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

    void DestroyGraph(){
        foreach(int key in nodes.Keys) {
            Node node = nodes[key] as Node;
            Destroy(node);
        }
        foreach(int key in links.Keys) {
            Link link = links[key] as Link;
            Destroy(link);
        }
        nodes.Clear();
        links.Clear();
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

    Vector3 getCenter (string tag) {
      Vector3 center = new Vector3(0, 0, 0);
      switch (tag)
        {
        case "all":
            foreach(int key in nodes.Keys) {
                Node node = nodes[key] as Node;
                center += node.transform.position;
            }
            return center/nodeCount;
        case "L2":
            for (int i = 0; i < layer2Count; i++) {
                Node node = nodes[i + 20] as Node;
                center += node.transform.position;
            }
            return center/layer2Count;
        case "L1":
            for (int i = 0; i < layer1Count; i++) {
                Node node = nodes[i] as Node;
                center += node.transform.position;
            }
            return center/layer1Count;
        default:
            return center;
        }
    }

    void Start () {
        GenerateGraph();
        client = GameObject.FindObjectOfType<Client>();
        // Time.fixedDeltaTime = 0.2f;
    }

    // update the force among the nodes from the same layer
    void Update () {

    if (tempDestroy) {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);// this line will restart the scene (not sure if this will cause python connection problem or not)
         tempDestroy = false;
    }



    //    Debug.Log("L--Offset");
    //    Debug.Log(Vector3.Distance(center/nodeCount, leftHand.transform.position));
    // if the distance between left hand to the center mass of the system is smaller than 5, pulse the graph
       Vector3 allCenter = getCenter("all");
       if (Vector3.Distance(allCenter, leftHand.transform.position) < 5) {
        // if (tempWait){
           centerMass.GetComponent<Renderer>().enabled = true;
           client.requester.SetMessage("wait");
           isWaiting = true;

            if (newL1NodeAssigned && Vector3.Distance(newL1Node.transform.position, L1Center.transform.position) > 1) {
                newlayer1Count ++;
                newL1NodeAssigned = false;
            }
            if (newL2NodeAssigned && Vector3.Distance(newL2Node.transform.position, L2Center.transform.position) > 1) {
                newlayer2Count ++;
                newL2NodeAssigned = false;
            }

            if (oldL1NodeRemoved &&  Vector3.Distance(rightHand.transform.position, L1Center.transform.position) > 1 ) {
                newlayer1Count --;
                // oldL1Node.GetComponent<Collider>().enabled = false;
                // oldL1Node.GetComponent<Renderer>().enabled = false;
                oldL1NodeRemoved = false;
            }

            if (oldL2NodeRemoved &&  Vector3.Distance(rightHand.transform.position, L2Center.transform.position) > 1 ) {
                newlayer2Count --;
                // oldL2Node.GetComponent<Collider>().enabled = false;
                // oldL2Node.GetComponent<Renderer>().enabled = false;
                oldL2NodeRemoved = false;
            }


       } else {
           L1Center.transform.position = getCenter("L1");
           L2Center.transform.position = getCenter("L2");
           centerMass.position = allCenter;
           centerMass.GetComponent<Renderer>().enabled = false;
           isWaiting = false;
           client.requester.SetMessage("nothing");
           if (newlayer1Count != 0 || newlayer2Count != 0) {
            //    DestroyGraph();
               layer1Count = layer1Count + newlayer1Count;
               layer2Count = layer2Count + newlayer2Count;
            //    GenerateGraph();
               newlayer1Count = 0;
               newlayer2Count = 0;
               Debug.Log("We are looking at the count");
               Debug.Log(layer1Count);
               Debug.Log(layer2Count);
            //    Application.LoadLevel(Application.loadedLevel);
               SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);// this line will restart the scene (not sure if this will cause python connection problem or not)
           }
       }
        // Debug.Log("L--We ran to here~");
       float L1CenterToRightHand = Vector3.Distance(L1Center.transform.position, rightHand.transform.position);
       float L2CenterToRightHand = Vector3.Distance(L2Center.transform.position, rightHand.transform.position);

       if (L1CenterToRightHand < 1) {
        // if (tempRightEnter) {
           L1Center.GetComponent<Renderer>().enabled = true;

           // the following line doesn't work..
        //    L1Center.GetComponent<MeshRenderer>().material._TintColor.a = (1 - L1CenterToRightHand) / L1CenterToRightHand;

            // remove node
            if (L1CenterToRightHand < 0.5) {
            // if (tempRightEnter) {
                OVRGrabbable grabbedObject = rightHand.GetComponent<OVRGrabber>().grabbedObject;
                if (grabbedObject) {
                    GameObject grabbedNode = grabbedObject.gameObject;
                    // Node grabbedNode = tempRemoveNode;
                    if (grabbedNode.tag=="L1" && !oldL1NodeRemoved) {
                            grabbedNode.GetComponent<Renderer>().enabled = false;
                            grabbedNode.GetComponent<Collider>().enabled = false;
                            // rightHand.GetComponent<OVRGrabber>().ForceRelease(grabbedObject);
                            // Destroy(grabbedObject);
                            oldL1Node = grabbedNode;
                            oldL1NodeRemoved = true;
                    }
                }
                else if( !newL1NodeAssigned) { // add node
                    Debug.Log("L--Yo~ new L1 generated!");
                    newL1Node = Instantiate(nodePrefab, L1Center.transform.position, Quaternion.identity) as Node;
                    newL1Node.tag = "newL1";
                    newL1NodeAssigned = true;
                }
        }

       } else {
           L1Center.GetComponent<Renderer>().enabled = false;
       }

        if (L2CenterToRightHand < 1) {
            L2Center.GetComponent<Renderer>().enabled = true;
            // L2Center.GetComponent<MeshRenderer>().material._TintColor.a = (1 - L2CenterToRightHand) / L2CenterToRightHand;
            // if (L2CenterToRightHand < 0.5 && !newL2NodeAssigned) {
            //     Debug.Log("L--Yo~ new L2 generated!");
            //     newL2Node = Instantiate(nodePrefab, L2Center.transform.position, Quaternion.identity) as Node;
            //     newL2Node.tag = "newL2";
            //     newL2NodeAssigned = true;
            // }
                        // remove node
            if (L2CenterToRightHand < 0.5) {
            // if (tempRightEnter) {
                OVRGrabbable grabbedObject = rightHand.GetComponent<OVRGrabber>().grabbedObject;
                if (grabbedObject) {
                    GameObject grabbedNode = grabbedObject.gameObject;
                    // Node grabbedNode = tempRemoveNode;
                    if (grabbedNode.tag=="L2" && !oldL2NodeRemoved) {
                            grabbedNode.GetComponent<Renderer>().enabled = false;
                            grabbedNode.GetComponent<Collider>().enabled = false;
                            // rightHand.GetComponent<OVRGrabber>().ForceRelease(grabbedObject);
                            // Destroy(grabbedObject);
                            oldL2Node = grabbedNode;
                            oldL2NodeRemoved = true;
                    }
                }
                else if( !newL2NodeAssigned) { // add node
                    Debug.Log("L--Yo~ new L2 generated!");
                    newL2Node = Instantiate(nodePrefab, L2Center.transform.position, Quaternion.identity) as Node;
                    newL2Node.tag = "newL2";
                    newL2NodeAssigned = true;
                }
        }
        } else {
            L2Center.GetComponent<Renderer>().enabled = false;
        }

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
                // Debug.Log("L--W1LinkIndex" + i.ToString());
                link.FaBetween = k * W1ByLinks[i];
                link.c.a = W1ByLinks[i];
                // Debug.Log("L--W1ByLinkss" + W1ByLinks[i].ToString());
            }

            //  (layer 1 -> layer 2)
            int offset1To2 = layer1Count;
            for (int i = 0; i < layer1Count * layer2Count; i++)
            {

                Link link = links[i + offset1To2] as Link;
                // Debug.Log("L--W2LinkIndex" + (i + offset1To2).ToString());
                link.FaBetween = k * W2ByLinks[i]; // TODO 对，其实这玩意儿这样不大好，因为这里的力是weight越大越靠近，但是我们拉动的时候是越远weight越大。
                link.c.a = W2ByLinks[i];
                // Debug.Log("L--W2ByLinkss" + (W2ByLinks[i]).ToString());
            }
            //(layer 2 -> output)
            int offset2ToOut = layer1Count * layer2Count + offset1To2;
            for (int i = 0; i < layer2Count; i++)
            {
                Link link = links[i + offset2ToOut] as Link;
                // Debug.Log("L--W3LinkIndex" + (i + offset2ToOut).ToString());
                link.FaBetween = k * W3ByLinks[i];
                link.c.a = W3ByLinks[i];
                // Debug.Log("L--W3ByLinkss" + (W3ByLinks[i]).ToString());
            }
        }
    }
}
