using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Sonify : MonoBehaviour
{
    public AudioMixer mixerWithChuck;
    private string myChuck1;
    // Start is called before the first frame update
    void Start()
    {
        myChuck1 = "my_chuck";
        Chuck.Manager.Initialize( mixerWithChuck, myChuck1 );
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MappingSound(string infoString)
    {

        Debug.Log("Sonify: " + infoString);
        float freq = float.Parse(infoString)*100;
        Chuck.Manager.RunCode( myChuck1,
				string.Format(
					@"
					SinOsc foo => dac;
                    {0} => foo.freq;
                    200::ms => now;
					",
					freq
				)
			);
    }

    void OnApplicationQuit()
	{
		Chuck.Manager.Quit();
	}


}
