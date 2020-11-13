using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Xml;
using System.IO;

public class Controller : MonoBehaviour {

    public Node nodePrefab;
    public Link linkPrefab;

    int nodeCount = 0;
    int linkCount = 0;

    Hashtable nodes;
    Hashtable links;

    // private IDictionary<int, Node> nodes = new Dictionary<int, Node>();
    // private IDictionary<int, Link> links = new Dictionary<int, Link>();


    void GenerateGraph(){
        int layer1Count = 2;
        int layer2Count = 3;

        // layer 1
        for(int i=0; i<layer1Count; i++){
            createNode(i);
        }
        // layer 2
        for(int j=0; j<layer2Count; j++){
            createNode(10 + j);
        }

        // add link
        for(int i=0; i<layer1Count; i++){
            for(int j=0; j<layer2Count; j++){
                createLink(i, 10 + j);
            }
        }
        //map node edges
        MapLinkNodes();
    }

    //Create nodes
    void createNode(int id) {
        float x = Random.Range(-10, 10);
        float y = Random.Range(-10, 10);
        float z = Random.Range(-10, 10);
        Node nodeObject = Instantiate(nodePrefab, new Vector3(x,y,z), Quaternion.identity) as Node;
        nodeObject.id = id;
        nodes.Add(nodeObject.id, nodeObject);
        nodeCount++;
    }

    //Create links
    void createLink(int sourceId, int targetId){
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

    void Start () {
        nodes = new Hashtable();
        links = new Hashtable();

        GenerateGraph();
    }

}
