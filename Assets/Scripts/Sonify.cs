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
        Chuck.Manager.RunCode( myChuck1,
            @"
            TriOsc triOsc => ADSR env => NRev verb => dac.left;
            SinOsc sinOsc => ADSR env2 => NRev verb2 => dac.right;
            TriOsc sqrOsc => ADSR env3 => NRev verb3 => dac.left;
            SinOsc pulseOsc => ADSR env4 => NRev verb4 => dac.right;

            0.3 => triOsc.gain;
            0.3 => sinOsc.gain;
            0.3 => sqrOsc.gain;
            0.3 => pulseOsc.gain;
            0.01 => verb.mix;
            0.01 => verb2.mix;
            0.01 => verb3.mix;
            0.01 => verb4.mix;

            // Set ASDR (Attack, Decay, Sustain, Release)
            env.set(10.0::ms, 60.0::ms, 0.0, 0::ms);
            env2.set(10.0::ms, 60.0::ms, 0.0, 0::ms);
            env3.set(10.0::ms, 60.0::ms, 0.0, 0::ms);
            env4.set(10.0::ms, 60.0::ms, 0.0, 0::ms);

            fun void playTrainCE( float ce )
            {
                ce => triOsc.freq;
                env.keyOn();
            }

            fun void playTrainACC( float acc )
            {
                acc => sinOsc.freq;
                env2.keyOn();
            }

            fun void playValidCE( float ce )
            {
                ce => sqrOsc.freq;
                env3.keyOn();
            }

            fun void playValidACC( float acc )
            {
                300::ms => now;
                acc => pulseOsc.freq;
                env4.keyOn();
            }

            global float ce;
            global float acc;
            global float isValidation;
            global Event play;

            while( true )
            {
                play => now;
                if( isValidation )
                {
                    // spork ~ playValidCE( ce );
                    spork ~ playValidACC( acc );
                }
                else
                {
                    spork ~ playTrainCE( ce );
                    spork ~ playTrainACC( acc );
                }

            }

            "
        );

    }

    public void MappingSound(string infoString)
    {

        string[] vals  = infoString.Split(',');
        int mode = int.Parse(vals[0]);
        float acc;
        Debug.Log("see the mode");
        Debug.Log(infoString);
        Debug.Log(mode);
        if (mode == 0 || mode == 3) { // "nothing" mode or updateWeights
            // float ce = float.Parse(vals[1]) * 1000;
            acc = float.Parse(vals[2]) * 1000;
            // float isValidation = float.Parse(vals[3]);
            // Chuck.Manager.SetFloat( myChuck1, "ce", ce);
            Chuck.Manager.SetFloat( myChuck1, "acc", acc);
            Chuck.Manager.SetFloat( myChuck1, "isValidation", 1.0f);
            Chuck.Manager.BroadcastEvent( myChuck1, "play" );
        } else if (mode == 1) { // updateMomentum
            acc = float.Parse(vals[1]) * 1000; // here acc us momentum
            Chuck.Manager.SetFloat( myChuck1, "acc", acc);
            Chuck.Manager.SetFloat( myChuck1, "isValidation", 1.0f);
            Chuck.Manager.BroadcastEvent( myChuck1, "play" );
        } else if (mode == 2) { // updateEps
            acc = float.Parse(vals[1]) * 1000; // here acc is eps
            Chuck.Manager.SetFloat( myChuck1, "acc", acc);
            Chuck.Manager.SetFloat( myChuck1, "isValidation", 1.0f);
            Chuck.Manager.BroadcastEvent( myChuck1, "play" );
        } // just don't sonify if it's the other mode (for example 99)
    }

    void OnApplicationQuit()
	{
		Chuck.Manager.Quit();
	}


}
