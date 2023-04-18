using Python.Runtime;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.Networking;
using TMPro;

public class Gesticulator : MonoBehaviour
{
    [SerializeField]
    GameObject character;
    [SerializeField]
    TextMeshProUGUI ModeStatusText;


    List<Gesticulator_OneFrameData> final_Audio_data = null;
    int updateCount;
    bool IsStart;
    float cur_jaw_X_Rotation;


    private void Start()
    {
        IsStart = false;
        cur_jaw_X_Rotation = 0;
    }

    public void Show_SMPL_Movement_By_Saved_wav()
    {
        IsStart = false;
        SMPLX smplx = character.GetComponent<SMPLX>();
        PyGesticulatorTestor pyGesticulatorTestor = new PyGesticulatorTestor(smplx.jointManager);
        final_Audio_data = pyGesticulatorTestor.Get_final_Audio_data();
        updateCount = 0;
        //Time.fixedDeltaTime = 0.05f;//Gesticulator MOTION (Frame Time: 0.05)
        AudioSource audioSource = GetComponent<AudioSource>();
        StartCoroutine(GetAudioClip(audioSource));
        IsStart = true;
        ModeStatusText.color = new Color(0.0f, 0.0f, 0.5f);
        ModeStatusText.text = "Start Moving";
    }

    IEnumerator GetAudioClip(AudioSource audioSource)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(PyGesticulatorTestor.audioPath, AudioType.WAV))
        {
            yield return www.SendWebRequest();
            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.clip = audioClip;
            Debug.Log("audioClip.clip[sec]: " + audioClip.length);
            float seconds = audioClip.length;
            float length = Convert.ToSingle(final_Audio_data.Count);
            Time.fixedDeltaTime = seconds / length;
            audioSource.Play();
            
        }
    }

    private void FixedUpdate()
    {
        if(IsStart)
        {
            Debug.Log("FixedUpdate time :" + Time.deltaTime);
            if (final_Audio_data != null && updateCount < final_Audio_data.Count)
            {
                Debug.Log($"updateCount : {updateCount},cur_jaw_X_Rotation:{cur_jaw_X_Rotation},final_Audio_data.Count:{final_Audio_data.Count}");
                final_Audio_data[updateCount++].UpdateJoints(jaw_X_Rotation : Get_jaw_X_Rotation(cur_jaw_X_Rotation));
            }
                
            if (final_Audio_data != null && updateCount == final_Audio_data.Count)
            {
                IsStart = false;
                ModeStatusText.text = "Finished";
            }     
        }
    }

    private float Get_jaw_X_Rotation(float jaw_X_Rotation)
    {
        if (updateCount % 4 == 0)
            return 1;
        else if (updateCount % 4 == 1)
            return 2;
        else if (updateCount % 4 == 2)
            return 3;
        else if (updateCount % 4 == 3)
            return 2;
        else 
            return jaw_X_Rotation;
    }

}

class PyGesticulatorTestor
{
    JointManager jointManager;
    //public const string audioPath = @"C:\Users\jongh\OneDrive\바탕 화면\Metaver_Project_120220121_Shinjonghyun\pythonGesticulator\demo\input\jeremy_howard.wav";
    public const string audioPath = UserSpeechSaver.audioPath;


    void AddEnvPath(params string[] paths)
    {      // PC에 설정되어 있는 환경 변수를 가져온다.
        var envPaths = Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator).ToList();
        // 중복 환경 변수가 없으면 list에 넣는다.
        envPaths.InsertRange(0, paths.Where(x => x.Length > 0 && !envPaths.Contains(x)).ToArray());
        // 환경 변수를 다시 설정한다.
        Environment.SetEnvironmentVariable("PATH", string.Join(Path.PathSeparator.ToString(), envPaths), EnvironmentVariableTarget.Process);
    }

    public PyGesticulatorTestor(JointManager jointManager)
    {
        this.jointManager = jointManager;

        Runtime.PythonDLL = @"C:\Users\jongh\Anaconda3\envs\gest_env_py37\python37.dll";
        var PYTHON_HOME = Environment.ExpandEnvironmentVariables(@"C:\Users\jongh\Anaconda3\envs\gest_env_py37");
        AddEnvPath(PYTHON_HOME, Path.Combine(PYTHON_HOME, @"Library\bin"));
        PythonEngine.PythonHome = PYTHON_HOME;
        PythonEngine.PythonPath = string.Join
        (
            Path.PathSeparator.ToString(),
            new string[]
            {
                      PythonEngine.PythonPath,
                      Path.Combine(PYTHON_HOME, @"Lib\site-packages"),
                      @"C:\Users\jongh\OneDrive\바탕 화면\Metaver_Project_120220121_Shinjonghyun\pythonGesticulator"
            }
        );
        PythonEngine.Initialize();
    }

    void Add_PySysPath(string path)
    {
        dynamic pysys = Py.Import("sys");   // import sys module from  PythonEngine.PythonPath 
        string[] sysPathArray = (string[])pysys.path;
        string EnvPath = path;
        if (sysPathArray.Contains(EnvPath) == false)
            pysys.path.append(EnvPath);
    }

    public List<Gesticulator_OneFrameData> Get_final_Audio_data()
    {
        using (Py.GIL())
        {
            dynamic motionPythonArray = Get_motionPythonArray();
            List<Gesticulator_OneFrameData> final_Audio_data = Get_final_Audio_data(motionPythonArray);
            return final_Audio_data;
        }
        
    }
    dynamic Get_motionPythonArray()
    {
        dynamic os = Py.Import("os");
        dynamic pycwd = os.getcwd();
        string cwd = (string)pycwd;
        Debug.Log($"[before]cwd:{cwd}");
        Add_PySysPath(path: @"C:\Users\jongh\OneDrive\바탕 화면\Metaver_Project_120220121_Shinjonghyun\pythonGesticulator");
        Add_PySysPath(path: @"C:\Users\jongh\OneDrive\바탕 화면\Metaver_Project_120220121_Shinjonghyun\pythonGesticulator\gesticulator\visualization");

        //string text = "Deep learning is an algorithm inspired by how the human brain works, and as a result it's an algorithm which has no theoretical limitations on what it can do. The more data you give it and the more computation time you give it, the better it gets. The New York Times also showed in this article another extraordinary result of deep learning which I'm going to show you now. It shows that computers can listen and understand.";
        dynamic wav2text = Py.Import("wav_to_text");
        string text = wav2text.get_large_audio_transcription(audioPath);
        Debug.Log(text);

        dynamic demo_py = Py.Import("demo.demo");
        dynamic motionPythonArray = demo_py.Get_2DListInUnity(audioPath, text);
        os.chdir(cwd);
        Debug.Log($"[after]cwd:{cwd}");
        return motionPythonArray;
    }

    List<Gesticulator_OneFrameData> Get_final_Audio_data(dynamic motionPythonArray)
    {
        List<List<float>> data = new List<List<float>>();
        foreach (dynamic oneframePyData in PyList.AsList(motionPythonArray))
        {
            List<float> oneFrameDataList = new List<float>();
            foreach (dynamic d in PyList.AsList(oneframePyData))
                oneFrameDataList.Add(Convert.ToSingle(d));
            data.Add(oneFrameDataList);
        }

        List<Gesticulator_OneFrameData> final_Audio_data = new List<Gesticulator_OneFrameData>();
        foreach (List<float> framedata in data)
            final_Audio_data.Add(new Gesticulator_OneFrameData(framedata, jointManager));
        return final_Audio_data;
    }
}


