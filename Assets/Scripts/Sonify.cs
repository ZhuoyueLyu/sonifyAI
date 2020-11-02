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
        // Chuck.Manager.RunCode( myChuck1,
        //     @"
        //     fun void playSound( float val )
        //     {
        //         SinOsc foo => dac;
        //         val => foo.freq;
        //     }
        //     external float val;
        //     external Event play;

        //     while( true )
        //     {
        //         play => now;
        //         spork ~ playSound( val );
        //     }

        //     "
		// );

        Chuck.Manager.RunCode( myChuck1,
            @"
            TriOsc osc => ADSR env => NRev verb => dac;
            0.3 => osc.gain;
            0.01 => verb.mix;

            env.set(10.0::ms, 60.0::ms, 0.0, 0::ms);

            fun void playSound( float val )
            {
                val => osc.freq;
                env.keyOn();
            }

            global float val;
            global Event play;

            while( true )
            {
                play => now;
                spork ~ playSound( val );
            }

            "
		);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MappingSound(string infoString)
    {

        Debug.Log("Sonify: " + infoString);
        float freq = float.Parse(infoString) * 1000;
        // 那个verb是可以加上reverb的效果，就更有空间感的声音
        //  verb => dac;
        //  0.1 => verb.mix;

        // Chuck.Manager.RunCode( myChuck1,
        // string.Format(
        //     @"
        //     SinOsc foo => dac;
        //     {0} => foo.freq;
        //     100::ms => now;
        //     ",
        //     freq
        // )
		// );
        Chuck.Manager.SetFloat( myChuck1, "val", freq);
        Chuck.Manager.BroadcastEvent( myChuck1, "play" );

    }

    void OnApplicationQuit()
	{
		Chuck.Manager.Quit();
	}


}
