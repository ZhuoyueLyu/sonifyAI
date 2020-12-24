using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;

public class Requester : RunAbleThread
{
    private Sonify sonify = GameObject.FindObjectOfType<Sonify> ();
    private Controller controller = GameObject.FindObjectOfType<Controller>();
    protected override void Run()
    {
        while (Running) {
            Debug.Log("New client created");
            ForceDotNet.Force();
            using (RequestSocket client = new RequestSocket())
            {
                client.Connect("tcp://10.0.0.163:123");
                for (int i = 0; i < 10 && Running; i++)
                {
                    client.SendFrame("H");
                    string message = client.ReceiveFrameString();
                    // sonify.MappingSound(message);
                    controller.UpdateConnections(message);
                }
            }
            NetMQConfig.Cleanup();
        }

    }
}