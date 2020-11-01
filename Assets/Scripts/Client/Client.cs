using UnityEngine;

public class Client : MonoBehaviour
{
    private Requester _Requester;

    private void Start()
    {
        _Requester = new Requester();
        _Requester.Start();
    }

    private void OnDestroy()
    {
        _Requester.Stop();
    }
}