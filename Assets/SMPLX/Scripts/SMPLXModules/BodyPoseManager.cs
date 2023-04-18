using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.SMPLX.Scripts.Enums;
using LightweightMatrixCSharp;



internal class BodyPoseManager
{
    JointManager jointManager;

    public BodyPoseManager(JointManager jointManager)
    {
        this.jointManager = jointManager;
    }

    public void SetBodyPose(BodyPose pose)
    {
        ResetBodyPose();
        if (pose == BodyPose.A)
        {
            jointManager.SetLocalJointRotation("left_collar", Quaternion.Euler(0.0f, 0.0f, 10.0f));
            jointManager.SetLocalJointRotation("left_shoulder", Quaternion.Euler(0.0f, 0.0f, 35.0f));
            jointManager.SetLocalJointRotation("right_collar", Quaternion.Euler(0.0f, 0.0f, -10.0f));
            jointManager.SetLocalJointRotation("right_shoulder", Quaternion.Euler(0.0f, 0.0f, -35.0f));
        }
        jointManager.UpdatePoseCorrectives();
    }

    void ResetBodyPose()
    {
        foreach (string name in jointManager._bodyJointNames)
        {
            Transform joint = jointManager._transformFromName[name];
            joint.localRotation = Quaternion.identity;
        }
    }
}

