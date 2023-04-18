using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SMPLXTestor : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI bodiesText;
    [SerializeField]
    TextMeshProUGUI statusText;
    [SerializeField]
    Slider bodyNumSlider;
    [SerializeField]
    Slider blendShapeNumSlider;

    Stack<SkinnedMeshRenderer> smrStack;
    Stack<GameObject> characterStack;
    int numOfBlendShapes;
    int numOfBodies;

    void Start()
    {
        this.smrStack = new Stack<SkinnedMeshRenderer>();
        this.characterStack = new Stack<GameObject>();
        this.smrStack.Push(this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>());
        this.characterStack.Push(this.gameObject);

        this.numOfBodies = Convert.ToInt32(bodyNumSlider.value);
        this.numOfBlendShapes = Convert.ToInt32(blendShapeNumSlider.value);
    }

    void Update()
    {
        InitializeBlendShapeWeights();
        UpdateBodies();
        SetBlendShapeWeights();
        UpdateStatus();
    }


    void InitializeBlendShapeWeights()
    {
        if (this.numOfBlendShapes != Convert.ToInt32(blendShapeNumSlider.value))
            foreach (SkinnedMeshRenderer smr in this.smrStack)
                for (int i = 0; i < smr.sharedMesh.blendShapeCount; i++)
                    smr.SetBlendShapeWeight(i, 0.0f);
    }

    void UpdateBodies()
    {
        int curNumOfBodies = Convert.ToInt32(bodyNumSlider.value);
        while (numOfBodies < curNumOfBodies)
        {
            GameObject character = Get_NewChracter();
            characterStack.Push(character);
            smrStack.Push(character.GetComponentInChildren<SkinnedMeshRenderer>());
            numOfBodies++;
        }
        while (numOfBodies > curNumOfBodies)
        {
            smrStack.Pop();
            Destroy(characterStack.Pop());
            numOfBodies--;
        }
    }

    void SetBlendShapeWeights()
    {
        float value = Mathf.Sin(2 * Mathf.PI * Time.time) * 50 + 100.0f;
        foreach (SkinnedMeshRenderer smr in smrStack)
            for (int i = 0; i < numOfBlendShapes; i++)
                smr.SetBlendShapeWeight(i, value);
    }

    int GetActiveBlendshapes()
    {
        int activeBlendshapes = 0;
        foreach (SkinnedMeshRenderer smr in this.smrStack)
        {
            for (int i = 0; i < smr.sharedMesh.blendShapeCount; i++)
                if (smr.GetBlendShapeWeight(i) < -0.000001f || smr.GetBlendShapeWeight(i) > 0.000001f)
                    activeBlendshapes++;
        }
        return activeBlendshapes;
    }

    void UpdateStatus()
    {
        this.numOfBodies = Convert.ToInt32(bodyNumSlider.value);
        this.numOfBlendShapes = Convert.ToInt32(blendShapeNumSlider.value);
        this.statusText.text = $"Active Blendshapes: {GetActiveBlendshapes()}";
        this.bodiesText.text = $"Bodies: {this.numOfBodies}";
    }

    GameObject Get_NewChracter()
    {
        float x = 2.5f * Mathf.Sin(2 * Mathf.PI * (this.numOfBodies) / 20.0f);
        float y = this.gameObject.transform.position.y;
        float z = this.numOfBodies * 0.25f;
        Vector3 pos = new Vector3(x, y, z);
        return Instantiate(this.gameObject, pos, Quaternion.Euler(0.0f, 180.0f, 0.0f));
    }

}
