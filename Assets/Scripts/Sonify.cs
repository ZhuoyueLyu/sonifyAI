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
        // Chuck.Manager.RunCode( myChuck1,
		// 		string.Format(
		// 			@"
		// 			SinOsc foo => dac;
        //             {0} => foo.freq;
        //             200::ms => now;
		// 			",
		// 			freq
		// 		)
		// );

        // 那个verb是可以加上reverb的效果，就更有空间感的声音
        //  verb => dac;
        //  0.1 => verb.mix;




        Chuck.Manager.RunCode( myChuck1,
        string.Format(
            @"
            SinOsc foo => dac;
            {0} => foo.freq;
            100::ms => now;
            ",
            freq
        )
		);
        // Chuck.Manager.RunCode( myChuck1,
		// 	@"
		// 	SinOsc foo => dac;

		// 	while( true )
		// 	{
		// 		Math.random2f( 300, 1000 ) => foo.freq;
		// 		100::ms => now;
		// 	}
		// 	"
		// );
    }

    void OnApplicationQuit()
	{
		Chuck.Manager.Quit();
	}


}
