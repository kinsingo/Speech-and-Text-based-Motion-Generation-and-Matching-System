using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.SMPLX.Scripts.Enums;
using LightweightMatrixCSharp;
using System;


public class JointManager
{
    public Dictionary<string, Transform> _transformFromName;
    public string[] _bodyJointNames = new string[] { "pelvis", "left_hip", "right_hip", "spine1", "left_knee", "right_knee", "spine2", "left_ankle", "right_ankle", "spine3", "left_foot", "right_foot", "neck", "left_collar", "right_collar", "head", "left_shoulder", "right_shoulder", "left_elbow", "right_elbow", "left_wrist", "right_wrist", "jaw", "left_eye_smplhf", "right_eye_smplhf", "left_index1", "left_index2", "left_index3", "left_middle1", "left_middle2", "left_middle3", "left_pinky1", "left_pinky2", "left_pinky3", "left_ring1", "left_ring2", "left_ring3", "left_thumb1", "left_thumb2", "left_thumb3", "right_index1", "right_index2", "right_index3", "right_middle1", "right_middle2", "right_middle3", "right_pinky1", "right_pinky2", "right_pinky3", "right_ring1", "right_ring2", "right_ring3", "right_thumb1", "right_thumb2", "right_thumb3" };
    public ModelType modelType = ModelType.Unknown;
    public bool usePoseCorrectives = true;
    GameObject character;
    SkinnedMeshRenderer _smr;

    public JointManager(GameObject character, SkinnedMeshRenderer _smr)
    {
        this.character = character;
        this._smr = _smr;
        _transformFromName = new Dictionary<string, Transform>();
        Transform[] transforms = character.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in transforms)
        {
            Debug.Log(t.name);
            _transformFromName.Add(t.name, t);
        }  
    }

    public void SetLocalJointRotation(string name, Quaternion quatLocal)
    {
        if (_transformFromName.ContainsKey(name) == false)
            throw new Exception($"[JointManager's SetLocalJointRotation(..)] _transformFromName doesn't contain key = {name}");

        Transform joint = _transformFromName[name];
        joint.localRotation = quatLocal; 
    }

    public void SetBetaShapes(float[] betas)
    {
        for (int i = 0; i < SMPLX.NUM_BETAS; i++)
            _smr.SetBlendShapeWeight(i, betas[i] * 100); // blend shape weights are specified in percentage

        Mesh _bakedMesh = new Mesh();
        _smr.BakeMesh(_bakedMesh);
        Vector3[] vertices = _bakedMesh.vertices;
        float yMin = float.MaxValue;
        for (int i = 0; i < vertices.Length; i++)
            if (vertices[i].y < yMin)
                yMin = vertices[i].y;
        Vector3 localPosition = character.transform.localPosition;
        character.transform.localPosition = new Vector3(localPosition.x, -yMin, localPosition.z);
    }

    public void SetExpressions(float[] expressions)
    {
        for (int i = 0; i < SMPLX.NUM_EXPRESSIONS; i++)
            _smr.SetBlendShapeWeight(i + SMPLX.NUM_BETAS, expressions[i] * 100); // blend shape weights are specified in percentage
    }

    public void UpdatePoseCorrectives()
    {
        for (int i = 1; i < _bodyJointNames.Length; i++)
        {
            string name = _bodyJointNames[i];
            Quaternion quat = _transformFromName[name].localRotation;
            Quaternion quatSMPLX = new Quaternion(-quat.x, quat.y, quat.z, -quat.w);
            Matrix4x4 m = Matrix4x4.Rotate(quatSMPLX);
            m[0, 0] = m[0, 0] - 1.0f;
            m[1, 1] = m[1, 1] - 1.0f;
            m[2, 2] = m[2, 2] - 1.0f;

            int poseStartIndex = SMPLX.NUM_BETAS + SMPLX.NUM_EXPRESSIONS + (i - 1) * 9;

            _smr.SetBlendShapeWeight(poseStartIndex + 0, 100.0f * m[0, 0]);
            _smr.SetBlendShapeWeight(poseStartIndex + 1, 100.0f * m[0, 1]);
            _smr.SetBlendShapeWeight(poseStartIndex + 2, 100.0f * m[0, 2]);

            _smr.SetBlendShapeWeight(poseStartIndex + 3, 100.0f * m[1, 0]);
            _smr.SetBlendShapeWeight(poseStartIndex + 4, 100.0f * m[1, 1]);
            _smr.SetBlendShapeWeight(poseStartIndex + 5, 100.0f * m[1, 2]);

            _smr.SetBlendShapeWeight(poseStartIndex + 6, 100.0f * m[2, 0]);
            _smr.SetBlendShapeWeight(poseStartIndex + 7, 100.0f * m[2, 1]);
            _smr.SetBlendShapeWeight(poseStartIndex + 8, 100.0f * m[2, 2]);
        }
    }
}

