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
            SqrOsc sqrOsc => ADSR env3 => NRev verb3 => dac.left;
            PulseOsc pulseOsc => ADSR env4 => NRev verb4 => dac.right;

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
                    spork ~ playValidCE( ce );
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
        //Debug.Log("CE: " + vals[0]);
        //Debug.Log("ACC: " + vals[1]);
        //Debug.Log("IsV: " + vals[2]);
        float ce = float.Parse(vals[0]) * 1000;
        float acc = float.Parse(vals[1]) * 1000;
        float isValidation = float.Parse(vals[2]);

        Chuck.Manager.SetFloat( myChuck1, "ce", ce);
        Chuck.Manager.SetFloat( myChuck1, "acc", acc);
        Chuck.Manager.SetFloat( myChuck1, "isValidation", isValidation);
        Chuck.Manager.BroadcastEvent( myChuck1, "play" );

    }

    void OnApplicationQuit()
	{
		Chuck.Manager.Quit();
	}


}
