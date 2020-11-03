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

        // 500::ms => now; is the delay between two sounds (acc & ce) because
        // if there is no delay, they are going to play together as one sound and you can't tell anything
        // .set - ( float, float, WRITE only ) - set freq and Q(resonance) at once
        //  ADSR (Attack, Decay, Sustain, Release)
        Chuck.Manager.RunCode( myChuck1,
            @"
            TriOsc triOsc => ADSR env => NRev verb => dac.left;

            SinOsc sinOsc => ADSR env2 => NRev verb2 => dac.right;

            0.3 => triOsc.gain;
            0.3 => sinOsc.gain;
            0.01 => verb.mix;
            0.01 => verb2.mix;

            env.set(10.0::ms, 60.0::ms, 0.0, 0::ms);
            env2.set(10.0::ms, 60.0::ms, 0.0, 0::ms);

            fun void playSoundCE( float ce )
            {
                ce => triOsc.freq;
                env.keyOn();
            }

            fun void playSoundACC( float acc )
            {
                // 500::ms => now;
                acc => sinOsc.freq;
                env2.keyOn();
            }

            global float ce;
            global float acc;
            global Event play;

            while( true )
            {
                play => now;
                spork ~ playSoundCE( ce );
                spork ~ playSoundACC( acc );
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

        string[] vals  = infoString.Split(',');
        Debug.Log("CE: " + vals[0]);
        Debug.Log("ACC: " + vals[1]);
        float ce = float.Parse(vals[0]) * 1000;
        float acc = float.Parse(vals[1]) * 1000;

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
        Chuck.Manager.SetFloat( myChuck1, "ce", ce);
        Chuck.Manager.SetFloat( myChuck1, "acc", acc);
        Chuck.Manager.BroadcastEvent( myChuck1, "play" );

    }

    void OnApplicationQuit()
	{
		Chuck.Manager.Quit();
	}


}
