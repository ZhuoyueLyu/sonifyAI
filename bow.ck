Bowed bow => dac;

3 => int fingering;
1 => int bowOn;
2.0 => float vibrato;

5::ms => dur atomicDuration;
[ 0,  2,  4,  5,
7,  9, 11, 12,
14, 16, 17, 19,
21, 23, 24, 26, 28] @=> int GMajor[];

[ 0,  2,  3,  5,
7,  9, 10, 12,
14, 15, 17, 19,
21, 22, 24, 26, 27] @=> int FMajor[];

while (true) {
    0 => bow.bowPressure;
    2.0 => bow.bowPosition;
    0 => bow.vibratoFreq;
    0 => bow.vibratoGain;
    1 => bow.volume;

    //<<< "---", "" >>>;
    //<<< "bow pressure:", bow.bowPressure() >>>;
    //<<< "bow position:", bow.bowPosition() >>>;
    //<<< "vibrato freq:", bow.vibratoFreq() >>>;
    //<<< "vibrato gain:", bow.vibratoGain() >>>;
    //<<< "volume:", bow.volume() >>>;

    0.8 => bow.noteOn;


    100 => bow.freq;

    atomicDuration => now;
}