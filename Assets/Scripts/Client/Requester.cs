using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;

public class Requester : RunAbleThread
{
    private Sonify sonify = GameObject.FindObjectOfType<Sonify> ();
    private Controller controller = GameObject.FindObjectOfType<Controller>();
    private string sendMessage = "S";
    protected override void Run()
    {
        // while (Running) {
        //     ForceDotNet.Force();
        //     using (RequestSocket client = new RequestSocket())
        //     {
        //         // client.Connect("tcp://127.0.0.1:123");
        //         client.Connect("tcp://10.0.0.163:123");
        //         for (int i = 0; i < 10 && Running; i++)
        //         {
        //             client.SendFrame(sendMessage);
        //             string message = client.ReceiveFrameString();
        //             if (!Controller.isWaiting) {
        //                 // sonify.MappingSound(message);
        //                 controller.UpdateConnections(message);
        //             }
        //         }
        //     }
        //     NetMQConfig.Cleanup();
        // }

    }
    public void SetMessage(string msg) {
        if (msg != sendMessage) {
            sendMessage = msg;
        }
    }
}