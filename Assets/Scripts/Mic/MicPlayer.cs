using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Assets.Scripts.Mic
{
    internal class MicPlayer
    {
        AudioSource source;
        AudioClip mic;
        List<float> readSamples;
        float readFlushTimer;
        TextMeshProUGUI InializedStatusText;
        string Initial_Text;
        public MicPlayer(AudioSource source, AudioClip mic, List<float> readSamples, TextMeshProUGUI InializedStatusText)
        {
            this.source = source;
            this.mic = mic;
            this.readSamples = readSamples;
            this.readFlushTimer = 0.0f;
            this.InializedStatusText = InializedStatusText;
            this.Initial_Text = InializedStatusText.text;
        }

        public void PlayMic()
        {
            const float READ_FLUSH_TIME = 0.5f;
            this.readFlushTimer += Time.deltaTime;
            Update_InializedStatusText(this.readFlushTimer, READ_FLUSH_TIME);
            if (IsPlaySource(this.readFlushTimer, READ_FLUSH_TIME))
            {
                InitializeSourceClip();
                PlaySourceClip();
                UpdateStatus();
            }
        }

        void Update_InializedStatusText(float readFlushTimer, float READ_FLUSH_TIME)
        {
            string UpdatedText = this.Initial_Text + $"READ_FLUSH_TIME:{READ_FLUSH_TIME}\nreadFlushTimer:{readFlushTimer}\nreadSamples.Count:{readSamples.Count}";
            this.InializedStatusText.text = UpdatedText;
        }


        bool IsPlaySource(float readFlushTimer, float READ_FLUSH_TIME)
        {
            bool condition1 = readFlushTimer > READ_FLUSH_TIME;
            bool condition2 = this.readSamples != null;
            bool condition3 = this.readSamples.Count > 0;
            return condition1 && condition2 && condition3;
        }

        void InitializeSourceClip()
        {
            this.source.clip = AudioClip.Create("Real_time", lengthSamples: this.readSamples.Count,
                channels: this.mic.channels, frequency: this.mic.frequency, stream: false);
            this.source.spatialBlend = 0;//2D sound
        }

        void PlaySourceClip()
        {
            this.source.clip.SetData(this.readSamples.ToArray(), offsetSamples: 0);
            this.source.Play();
        }

        void UpdateStatus()
        {
            this.readSamples.Clear();
            this.readFlushTimer = 0.0f;
        }
    }
}
