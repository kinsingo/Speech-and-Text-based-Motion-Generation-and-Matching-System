
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.SMPLX.Scripts.Enums;
using LightweightMatrixCSharp;

public class SMPLX : MonoBehaviour
{
    public float[] betas = new float[NUM_BETAS];
    public float[] expressions = new float[NUM_EXPRESSIONS];
    public const int NUM_JOINTS = 55;
    public const int NUM_BETAS = 10;
    public const int NUM_EXPRESSIONS = 10;
    private SkinnedMeshRenderer _smr;
    public int _numBetaShapes, _numExpressions, _numPoseCorrectives;
    public JointManager jointManager;
    HandPoseManager handPoseManager;
    BodyPoseManager bodyPoseManager;

    public void Awake()
    {
        _smr = GetComponentInChildren<SkinnedMeshRenderer>();
        jointManager = new JointManager(character : this.gameObject, _smr);
        handPoseManager = new HandPoseManager(jointManager);
        bodyPoseManager = new BodyPoseManager(jointManager);
        Update_BlenShape_TypeNumbers();
    }

    void Update_BlenShape_TypeNumbers()
    {
        this._numBetaShapes = 0;
        this._numExpressions = 0;
        this._numPoseCorrectives = 0;
        int blendShapeCount = this._smr.sharedMesh.blendShapeCount;
        for (int i = 0; i < blendShapeCount; i++)
        {
            string name = _smr.sharedMesh.GetBlendShapeName(i);
            if (name.StartsWith("Shape"))
                this._numBetaShapes++;
            else if (name.StartsWith("Exp"))
                this._numExpressions++;
            else if (name.StartsWith("Pose"))
                this._numPoseCorrectives++;
        }
    }
    void Update() => jointManager.UpdatePoseCorrectives();
    public void SetBetaShapes() => jointManager.SetBetaShapes(betas);
    public void SetExpressions() => jointManager.SetExpressions(expressions);
    public void SetHandPose(HandPose pose) => handPoseManager.SetHandPose(pose);
    public void SetBodyPose(BodyPose pose) => bodyPoseManager.SetBodyPose(pose);


}
