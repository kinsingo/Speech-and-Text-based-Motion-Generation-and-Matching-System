using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Mic;
using TMPro;
using System;
using Assets.Scripts;
using System.IO;
using UnityEngine.Networking;

public class UserSpeechSaver : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip micAudioClip;
    string microPhoneName;
    [SerializeField]
    TextMeshProUGUI ModeStatusText;

    public const string audioPath = @"C:\Users\jongh\OneDrive\바탕 화면\Metaver_Project_120220121_Shinjonghyun\pythonGesticulator\demo\input\shinjonghyun_record.wav";

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        microPhoneName = Microphone.devices[0];
    }

    public void Start_Record()
    {
        micAudioClip = Microphone.Start(deviceName: microPhoneName, loop: true, lengthSec: 100, frequency: 44100);
        ModeStatusText.text = "Status : Recording";
        ModeStatusText.color = new Color(0.0f, 0.5f, 0.0f);
    }

    public void Stop_and_Save()
    {
        ModeStatusText.color = new Color(0.5f, 0.0f, 0.0f);
        if (micAudioClip != null)
        {
            wavSaver.Save(audioPath, micAudioClip);
            ModeStatusText.text = "Status : Stop and Saved";
            micAudioClip = null;
        }
        else
        {
            ModeStatusText.text = "[Error] Please Start Record First";
        }
    }
}
