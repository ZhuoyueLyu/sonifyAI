using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour {

    public int id;
    public Node source;
    public Node target;
    public int sourceId;
    public int targetId;

    // Attraction between nodes from different layers
    public float FaBetween = 3.0f;
    // Repulsion between nodes from different layers
    public float FrBetween = 1000.0f; //对，这里的核心问题就是排斥力太小了，5000差不多。但是有一个问题，就，Controller里面如果这个值命名是一样的..会共享...

    private LineRenderer lineRenderer;

    void Start () {
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        //color link according to status
        Color c;
        if (1 > 0)
            c = Color.white;
        // else
        //     c = Color.red;
        c.a = 0.5f;

        //draw line
        lineRenderer.material = new Material (Shader.Find("Self-Illumin/Diffuse"));
        lineRenderer.material.SetColor ("_Color", c);
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(0,0,0));
        lineRenderer.SetPosition(1, new Vector3(1,0,0));

        // put here, so it's a constant force that only get added once
        // (if we put it in the Update, it will be updated multiple times)
        // source.GetComponent<Rigidbody>().AddForce(0, 0, 1);
        // target.GetComponent<Rigidbody>().AddForce(0, 1, 0);
    }

    void Update () {
        if(source && target){
            lineRenderer.SetPosition(0, source.transform.position);
            lineRenderer.SetPosition(1, target.transform.position);
        }

    }

    void FixedUpdate() {
        if(source && target){
            // We don't want the input node and output node to be far away from the layers
            if (source.tag == "Input" || target.tag == "Input" )
            {
                FrBetween = 300f;
            }
            if (target.tag == "Output" || target.tag == "Ouput")
            {
                FrBetween = 100f;
            }
            // Euclidean distance between them (sqrt)
            float distance = Vector3.Distance(source.transform.position, target.transform.position);
            // Apply attraction/repulsion
            Vector3 direction = source.transform.position - target.transform.position;
            // Apply attraction/repulsion
            Vector3 directionNorm = direction/distance;
            // Vector3 directionNorm = direction.normalized;

            // 就下面 direction = 单位向量 * 模长了，因为刚好单位向量的分母是根号，然后模长也是根号，两者消掉了。其实觉得我们的基础教育很适合底层工人，就，计算能力。但不适合创新。
            target.GetComponent<Rigidbody>().AddForce(FaBetween * direction);
            source.GetComponent<Rigidbody>().AddForce(-FaBetween * direction);

            // 下面是标准的用k q1q2/r^2的，但是这个力实在太小了...
            target.GetComponent<Rigidbody>().AddForce((-FrBetween / Mathf.Pow(distance, 2f)) * directionNorm);
            source.GetComponent<Rigidbody>().AddForce(FrBetween / Mathf.Pow(distance, 2f) * directionNorm);
            Debug.Log(FrBetween);
        }

    }

}