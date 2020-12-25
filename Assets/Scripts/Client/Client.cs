using UnityEngine;

public class Client : MonoBehaviour
{
    public Requester requester;

    private void Start()
    {
        requester = new Requester();
        requester.Start();
    }

    private void OnDestroy()
    {
        requester.Stop();
    }
}