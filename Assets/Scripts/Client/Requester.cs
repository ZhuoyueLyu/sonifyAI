using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;

public class Requester : RunAbleThread
{
    private Sonify sonify = GameObject.FindObjectOfType<Sonify> ();
    private Controller controller = GameObject.FindObjectOfType<Controller>();
    private string sendMessage = "S";
    private string receiveMessage;
    // private bool locked = false;
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
                    // Debug.Log("N--");
                    // Debug.Log(sendMessage);
                    // Debug.Log(receiveMessage);
                    // if (receiveMessage == "received") {
                    //     unlockRequester();
                    if (!Controller.isWaiting) {
                        // sonify.MappingSound(receiveMessage);
                        controller.UpdateConnections(receiveMessage);
                    }
                }
            }
            NetMQConfig.Cleanup();
        }

    }
    public void SetMessage(string msg) {
        if (msg != sendMessage) {
            sendMessage = msg;
        }
    }
    public string GetMessage() {
       return receiveMessage;
    }

    // public void lockRequester(){
    //     locked = true;
    // }
    // public void unlockRequester(){
    //     locked = false;
    // }
}