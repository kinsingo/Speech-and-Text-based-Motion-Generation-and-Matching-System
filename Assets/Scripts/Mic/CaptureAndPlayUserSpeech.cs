using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Mic;
using TMPro;
using System;
public class CaptureAndPlayUserSpeech : MonoBehaviour
{
    MicReader micReader;
    MicPlayer micPlayer;
    bool IsAvaliable;

    [SerializeField]
    TextMeshProUGUI ModeStatusText;

    [SerializeField]
    TextMeshProUGUI InializedStatusText;

    [SerializeField]
    Toggle IsLoop;

    [SerializeField]
    Slider lengthSecSlider;

    [SerializeField]
    Slider frequencySlider;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (IsAvaliable)
        {
            micReader.ReadMic();
            micPlayer.PlayMic();
        }
    }

    public void Initialize()
    {
        List<float> readSamples = new List<float>();
        AudioSource source = this.gameObject.GetComponent<AudioSource>();

        string microPhoneName = Microphone.devices[0];

        bool loop = IsLoop.isOn;
        int lengthSec = Convert.ToInt32(lengthSecSlider.value);
        int frequency = Convert.ToInt32(frequencySlider.value);
        AudioClip mic = Microphone.Start(deviceName: microPhoneName, loop, lengthSec, frequency);

        InializedStatusText.text = $"AudioClip's Current Setting\n" +
            $"loop : {loop}\n" +
            $"lengthSec : {lengthSec} [min:{Convert.ToInt32(lengthSecSlider.minValue)},max:{Convert.ToInt32(lengthSecSlider.maxValue)}]\n" +
            $"frequency : {frequency} [min:{Convert.ToInt32(frequencySlider.minValue)},max:{Convert.ToInt32(frequencySlider.maxValue)}]\n";

        micReader = new MicReader(microPhoneName, mic, readSamples);
        micPlayer = new MicPlayer(source, mic, readSamples, InializedStatusText);
        Stop_ReadAndPlay();
    }

    public void Start_ReadAndPlay()
    {
        ModeStatusText.text = "Status : Play Mode";
        ModeStatusText.color = new Color(0.0f, 0.5f, 0.0f);
        IsAvaliable = true;
    }
    public void Stop_ReadAndPlay()
    {
        ModeStatusText.text = "Status : Stop Mode";
        ModeStatusText.color = new Color(0.5f, 0.0f, 0.0f);
        IsAvaliable = false;
    }
}
