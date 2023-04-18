using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.SMPLX.Scripts.Enums;
using LightweightMatrixCSharp;

internal class HandPoseManager
{
    const int HandJoingLength = 15;
    float[] _handFlatLeft = new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    float[] _handFlatRight = new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    float[] _handRelaxedLeft = new float[] { 0.11167871206998825f, 0.042892176657915115f, -0.41644182801246643f, 0.10881132632493973f, -0.06598567962646484f, -0.7562199831008911f, -0.09639296680688858f, -0.09091565757989883f, -0.18845929205417633f, -0.1180950403213501f, 0.050943851470947266f, -0.529584527015686f, -0.14369840919971466f, 0.055241700261831284f, -0.7048571109771729f, -0.01918291673064232f, -0.09233684837818146f, -0.33791351318359375f, -0.4570329785346985f, -0.1962839514017105f, -0.6254575252532959f, -0.21465237438678741f, -0.06599828600883484f, -0.5068942308425903f, -0.3697243630886078f, -0.060344625264406204f, -0.07949022948741913f, -0.1418696939945221f, -0.08585263043642044f, -0.6355282664299011f, -0.3033415973186493f, -0.05788097530603409f, -0.6313892006874084f, -0.17612089216709137f, -0.13209307193756104f, -0.37335458397865295f, 0.8509643077850342f, 0.27692273259162903f, -0.09154807031154633f, -0.4998394250869751f, 0.02655647136271f, 0.05288087576627731f, 0.5355591773986816f, 0.04596104100346565f, -0.2773580253124237f };
    float[] _handRelaxedRight = new float[] { 0.11167871206998825f, -0.042892176657915115f, 0.41644182801246643f, 0.10881132632493973f, 0.06598567962646484f, 0.7562199831008911f, -0.09639296680688858f, 0.09091565757989883f, 0.18845929205417633f, -0.1180950403213501f, -0.050943851470947266f, 0.529584527015686f, -0.14369840919971466f, -0.055241700261831284f, 0.7048571109771729f, -0.01918291673064232f, 0.09233684837818146f, 0.33791351318359375f, -0.4570329785346985f, 0.1962839514017105f, 0.6254575252532959f, -0.21465237438678741f, 0.06599828600883484f, 0.5068942308425903f, -0.3697243630886078f, 0.060344625264406204f, 0.07949022948741913f, -0.1418696939945221f, 0.08585263043642044f, 0.6355282664299011f, -0.3033415973186493f, 0.05788097530603409f, 0.6313892006874084f, -0.17612089216709137f, 0.13209307193756104f, 0.37335458397865295f, 0.8509643077850342f, -0.27692273259162903f, 0.09154807031154633f, -0.4998394250869751f, -0.02655647136271f, -0.05288087576627731f, 0.5355591773986816f, -0.04596104100346565f, 0.2773580253124237f };
    string[] _handLeftJointNames = new string[] { "left_index1", "left_index2", "left_index3", "left_middle1", "left_middle2", "left_middle3", "left_pinky1", "left_pinky2", "left_pinky3", "left_ring1", "left_ring2", "left_ring3", "left_thumb1", "left_thumb2", "left_thumb3" };
    string[] _handRightJointNames = new string[] { "right_index1", "right_index2", "right_index3", "right_middle1", "right_middle2", "right_middle3", "right_pinky1", "right_pinky2", "right_pinky3", "right_ring1", "right_ring2", "right_ring3", "right_thumb1", "right_thumb2", "right_thumb3" };

    JointManager JointManager;
    public HandPoseManager(JointManager JointManager) => this.JointManager = JointManager;
    public void SetHandPose(HandPose pose)
    {
        float[] left = (pose == HandPose.Flat) ? _handFlatLeft : _handRelaxedLeft;
        float[] right = (pose == HandPose.Flat) ? _handFlatRight : _handRelaxedRight;
        for (int i = 0; i < HandJoingLength; i++)
        {
            Quaternion left_quat = QuatFromRodrigues(rodX: left[i * 3 + 0], rodY: left[i * 3 + 1], rodZ: left[i * 3 + 2]);
            JointManager.SetLocalJointRotation(name: _handLeftJointNames[i], left_quat);

            Quaternion right_quat = QuatFromRodrigues(rodX : right[i * 3 + 0], rodY: right[i * 3 + 1], rodZ: right[i * 3 + 2]);
            JointManager.SetLocalJointRotation(name: _handRightJointNames[i], right_quat);
        }
    }
    Quaternion QuatFromRodrigues(float rodX, float rodY, float rodZ)
    {
        Vector3 axis = new Vector3(-rodX, rodY, rodZ);
        float angle_deg = -axis.magnitude * Mathf.Rad2Deg;
        Vector3.Normalize(axis);
        return Quaternion.AngleAxis(angle_deg, axis); ;
    }
}
