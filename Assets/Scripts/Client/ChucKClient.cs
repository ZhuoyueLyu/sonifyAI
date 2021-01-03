using UnityEngine;

public class ChucKClient : MonoBehaviour
{
    public ChucKRequester requester;

    private void Start()
    {
        requester = new ChucKRequester();
        requester.Start();
    }

    private void OnDestroy()
    {
        requester.Stop();
    }
}