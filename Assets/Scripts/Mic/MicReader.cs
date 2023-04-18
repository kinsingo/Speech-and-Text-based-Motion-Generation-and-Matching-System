using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Mic
{
    internal class MicReader
    {
        string microPhoneName;
        AudioClip mic;
        List<float> readSamples;
        int lastSample;

        public MicReader(string microPhoneName, AudioClip mic, List<float> readSamples)
        {
            this.microPhoneName = microPhoneName;
            this.mic = mic;
            this.readSamples = readSamples;
            this.lastSample = 0;
        }

        public void ReadMic()
        {
            int curSample = Microphone.GetPosition(this.microPhoneName);
            Debug.Log($"curSample:{curSample}");
            int diff = curSample - this.lastSample;
            if (IsUpdateReadSamples(diff))
                Update_ReadSamples(diff);
            this.lastSample = curSample;
        }

        bool IsUpdateReadSamples(int diff) => diff > 0;
        void Update_ReadSamples(int diff)
        {
            float[] samples = new float[diff * this.mic.channels];
            this.mic.GetData(samples, this.lastSample);
            this.readSamples.AddRange(samples);
        }
    }
}
