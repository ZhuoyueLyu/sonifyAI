using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;

public class ChucKRequester : RunAbleThread
{
    private Sonify sonify = GameObject.FindObjectOfType<Sonify> ();
    private string receiveMessage;
    private string sendMessage = "ChucK";
    protected override void Run()
    {
        while (Running) {
            ForceDotNet.Force();
            using (RequestSocket client = new RequestSocket())
            {
                // client.Connect("tcp://127.0.0.1:123");
                client.Connect("tcp://10.0.0.163:123");
                for (int i = 0; i < 10 && Running; i++)
                {
                    client.SendFrame(sendMessage);
                    receiveMessage = client.ReceiveFrameString();
                    sonify.MappingSound(receiveMessage);
                }
            }
            NetMQConfig.Cleanup();
        }

    }
}