﻿using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;

public class Requester : RunAbleThread
{
    private Sonify sonify = GameObject.FindObjectOfType<Sonify> ();
    protected override void Run()
    {
        ForceDotNet.Force(); // this line is needed to prevent unity freeze after one use, not sure why yet
        using (RequestSocket client = new RequestSocket())
        {
            client.Connect("tcp://localhost:5555");

            // for (int i = 0; i < 10 && Running; i++)
            while (Running) // always sending request for data until the python program has no more data to return.
            {
                // Debug.Log("Sending Hello");
                client.SendFrame("Hello");
                // ReceiveFrameString() blocks the thread until you receive the string, but TryReceiveFrameString()
                // do not block the thread, you can try commenting one and see what the other does, try to reason why
                // unity freezes when you use ReceiveFrameString() and play and stop the scene without running the server
//                string message = client.ReceiveFrameString();
//                Debug.Log("Received: " + message);
                string message = null;
                bool gotMessage = false;
                while (Running)
                {
                    gotMessage = client.TryReceiveFrameString(out message); // this returns true if it's successful
                    if (gotMessage) break;
                }

                if (gotMessage) {
                    Debug.Log("Received " + message);
                    sonify.MappingSound(message);
                }
            }
        }

        NetMQConfig.Cleanup(); // this line is needed to prevent unity freeze after one use, not sure why yet
    }
}