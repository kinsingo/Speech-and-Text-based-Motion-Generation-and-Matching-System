using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Gesticulator_OneFrameData
    {
        JointManager jointManager;
        Dictionary<string, float> dataDict { get; }
        
        public Gesticulator_OneFrameData(List<float> framedata, JointManager jointManager)
        {
            dataDict = new Dictionary<string, float>();
            List<string> dataOrderList = Get_dataOrderList();

            if (dataOrderList.Count != framedata.Count)
                throw new Exception($"[Gesticulator_FrameDataInfo]dataOrders.Count != framedata.Length, dataOrders.Count: {dataOrderList.Count}, framedata.Length:{framedata.Count}");

            for(int i = 0;i< dataOrderList.Count;i++)
                dataDict.Add(dataOrderList[i], framedata[i]);

            this.jointManager = jointManager;
        }


        //Left to Left / Right to Right / XYZ축 모두 반대방향 Version이 제일 적절함 !! 
        public void UpdateJoints(float jaw_X_Rotation)
        {
            //"left_eye_smplhf", "right_eye_smplhf"

            jointManager.SetLocalJointRotation("jaw", Quaternion.Euler(x: jaw_X_Rotation, 0, 0));

            jointManager.SetLocalJointRotation("head", Quaternion.Euler(x: -dataDict["Neck1_Xrotation"], y: -dataDict["Neck1_Yrotation"], z: -dataDict["Neck1_Zrotation"]));

            jointManager.SetLocalJointRotation("head", Quaternion.Euler(x: -dataDict["Neck1_Xrotation"], y: -dataDict["Neck1_Yrotation"], z: -dataDict["Neck1_Zrotation"]));
            jointManager.SetLocalJointRotation("neck", Quaternion.Euler(x: -dataDict["Neck_Xrotation"], y: -dataDict["Neck_Yrotation"], z: -dataDict["Neck_Zrotation"]));
            jointManager.SetLocalJointRotation("root", Quaternion.Euler(x: -dataDict["Spine3_Xrotation"], y: -dataDict["Spine3_Yrotation"], z: -dataDict["Spine3_Zrotation"]));
            jointManager.SetLocalJointRotation("spine3", Quaternion.Euler(x: -dataDict["Spine2_Xrotation"], y: -dataDict["Spine2_Yrotation"], z: -dataDict["Spine2_Zrotation"]));
            jointManager.SetLocalJointRotation("spine2", Quaternion.Euler(x: -dataDict["Spine1_Xrotation"], y: -dataDict["Spine1_Yrotation"], z: -dataDict["Spine1_Zrotation"]));
            jointManager.SetLocalJointRotation("spine1", Quaternion.Euler(x: -dataDict["Spine_Xrotation"], y: -dataDict["Spine_Yrotation"], z: -dataDict["Spine_Zrotation"]));
            jointManager.SetLocalJointRotation("pelvis", Quaternion.Euler(x: -dataDict["Hips_Xrotation"], y: -dataDict["Hips_Yrotation"], z: -dataDict["Hips_Zrotation"]));

            jointManager.SetLocalJointRotation("right_hip", Quaternion.Euler(x: -dataDict["RightUpLeg_Xrotation"], y: -dataDict["RightUpLeg_Yrotation"], z: -dataDict["RightUpLeg_Zrotation"]));
            jointManager.SetLocalJointRotation("right_knee", Quaternion.Euler(x: -dataDict["RightLeg_Xrotation"], y: -dataDict["RightLeg_Yrotation"], z: -dataDict["RightLeg_Zrotation"]));
            jointManager.SetLocalJointRotation("right_ankle", Quaternion.Euler(x: -dataDict["RightFoot_Xrotation"], y: -dataDict["RightFoot_Yrotation"], z: -dataDict["RightFoot_Zrotation"]));
            jointManager.SetLocalJointRotation("right_foot", Quaternion.Euler(x: -dataDict["RightForeFoot_Xrotation"], y: -dataDict["RightForeFoot_Yrotation"], z: -dataDict["RightForeFoot_Zrotation"]));
            jointManager.SetLocalJointRotation("right_collar", Quaternion.Euler(x: -dataDict["RightShoulder_Xrotation"], y: -dataDict["RightShoulder_Yrotation"], z: -dataDict["RightShoulder_Zrotation"]));
            jointManager.SetLocalJointRotation("right_shoulder", Quaternion.Euler(x: -dataDict["RightArm_Xrotation"], y: -dataDict["RightArm_Yrotation"], z: -dataDict["RightArm_Zrotation"]));
            jointManager.SetLocalJointRotation("right_elbow", Quaternion.Euler(x: -dataDict["RightForeArm_Xrotation"], y: -dataDict["RightForeArm_Yrotation"], z: -dataDict["RightForeArm_Zrotation"]));
            jointManager.SetLocalJointRotation("right_wrist", Quaternion.Euler(x: -dataDict["RightHand_Xrotation"], y: -dataDict["RightHand_Yrotation"], z: -dataDict["RightHand_Zrotation"]));
            jointManager.SetLocalJointRotation("right_index1", Quaternion.Euler(x: -dataDict["RightHandIndex1_Xrotation"], y: -dataDict["RightHandIndex1_Yrotation"], z: -dataDict["RightHandIndex1_Zrotation"]));
            jointManager.SetLocalJointRotation("right_index2", Quaternion.Euler(x: -dataDict["RightHandIndex2_Xrotation"], y: -dataDict["RightHandIndex2_Yrotation"], z: -dataDict["RightHandIndex2_Zrotation"]));
            jointManager.SetLocalJointRotation("right_index3", Quaternion.Euler(x: -dataDict["RightHandIndex3_Xrotation"], y: -dataDict["RightHandIndex3_Yrotation"], z: -dataDict["RightHandIndex3_Zrotation"]));
            jointManager.SetLocalJointRotation("right_middle1", Quaternion.Euler(x: -dataDict["RightHandMiddle1_Xrotation"], y: -dataDict["RightHandMiddle1_Yrotation"], z: -dataDict["RightHandMiddle1_Zrotation"]));
            jointManager.SetLocalJointRotation("right_middle2", Quaternion.Euler(x: -dataDict["RightHandMiddle2_Xrotation"], y: -dataDict["RightHandMiddle2_Yrotation"], z: -dataDict["RightHandMiddle2_Zrotation"]));
            jointManager.SetLocalJointRotation("right_middle3", Quaternion.Euler(x: -dataDict["RightHandMiddle3_Xrotation"], y: -dataDict["RightHandMiddle3_Yrotation"], z: -dataDict["RightHandMiddle3_Zrotation"]));
            jointManager.SetLocalJointRotation("right_pinky1", Quaternion.Euler(x: -dataDict["RightHandPinky1_Xrotation"], y: -dataDict["RightHandPinky1_Yrotation"], z: -dataDict["RightHandPinky1_Zrotation"]));
            jointManager.SetLocalJointRotation("right_pinky2", Quaternion.Euler(x: -dataDict["RightHandPinky2_Xrotation"], y: -dataDict["RightHandPinky2_Yrotation"], z: -dataDict["RightHandPinky2_Zrotation"]));
            jointManager.SetLocalJointRotation("right_pinky3", Quaternion.Euler(x: -dataDict["RightHandPinky3_Xrotation"], y: -dataDict["RightHandPinky3_Yrotation"], z: -dataDict["RightHandPinky3_Zrotation"]));
            jointManager.SetLocalJointRotation("right_ring1", Quaternion.Euler(x: -dataDict["RightHandRing1_Xrotation"], y: -dataDict["RightHandRing1_Yrotation"], z: -dataDict["RightHandRing1_Zrotation"]));
            jointManager.SetLocalJointRotation("right_ring2", Quaternion.Euler(x: -dataDict["RightHandRing2_Xrotation"], y: -dataDict["RightHandRing2_Yrotation"], z: -dataDict["RightHandRing2_Zrotation"]));
            jointManager.SetLocalJointRotation("right_ring3", Quaternion.Euler(x: -dataDict["RightHandRing3_Xrotation"], y: -dataDict["RightHandRing3_Yrotation"], z: -dataDict["RightHandRing3_Zrotation"]));
            jointManager.SetLocalJointRotation("right_thumb1", Quaternion.Euler(x: -dataDict["RightHandThumb1_Xrotation"], y: -dataDict["RightHandThumb1_Yrotation"], z: -dataDict["RightHandThumb1_Zrotation"]));
            jointManager.SetLocalJointRotation("right_thumb2", Quaternion.Euler(x: -dataDict["RightHandThumb2_Xrotation"], y: -dataDict["RightHandThumb2_Yrotation"], z: -dataDict["RightHandThumb2_Zrotation"]));
            jointManager.SetLocalJointRotation("right_thumb3", Quaternion.Euler(x: -dataDict["RightHandThumb3_Xrotation"], y: -dataDict["RightHandThumb3_Yrotation"], z: -dataDict["RightHandThumb3_Zrotation"]));

            jointManager.SetLocalJointRotation("left_hip", Quaternion.Euler(x: -dataDict["LeftUpLeg_Xrotation"], y: -dataDict["LeftUpLeg_Yrotation"], z: -dataDict["LeftUpLeg_Zrotation"]));
            jointManager.SetLocalJointRotation("left_knee", Quaternion.Euler(x: -dataDict["LeftLeg_Xrotation"], y: -dataDict["LeftLeg_Yrotation"], z: -dataDict["LeftLeg_Zrotation"]));
            jointManager.SetLocalJointRotation("left_ankle", Quaternion.Euler(x: -dataDict["LeftFoot_Xrotation"], y: -dataDict["LeftFoot_Yrotation"], z: -dataDict["LeftFoot_Zrotation"]));
            jointManager.SetLocalJointRotation("left_foot", Quaternion.Euler(x: -dataDict["LeftForeFoot_Xrotation"], y: -dataDict["LeftForeFoot_Yrotation"], z: -dataDict["LeftForeFoot_Zrotation"]));
            jointManager.SetLocalJointRotation("left_collar", Quaternion.Euler(x: -dataDict["LeftShoulder_Xrotation"], y: -dataDict["LeftShoulder_Yrotation"], z: -dataDict["LeftShoulder_Zrotation"]));
            jointManager.SetLocalJointRotation("left_shoulder", Quaternion.Euler(x: -dataDict["LeftArm_Xrotation"], y: -dataDict["LeftArm_Yrotation"], z: -dataDict["LeftArm_Zrotation"]));
            jointManager.SetLocalJointRotation("left_elbow", Quaternion.Euler(x: -dataDict["LeftForeArm_Xrotation"], y: -dataDict["LeftForeArm_Yrotation"], z: -dataDict["LeftForeArm_Zrotation"]));
            jointManager.SetLocalJointRotation("left_wrist", Quaternion.Euler(x: -dataDict["LeftHand_Xrotation"], y: -dataDict["LeftHand_Yrotation"], z: -dataDict["LeftHand_Zrotation"]));
            jointManager.SetLocalJointRotation("left_index1", Quaternion.Euler(x: -dataDict["LeftHandIndex1_Xrotation"], y: -dataDict["LeftHandIndex1_Yrotation"], z: -dataDict["LeftHandIndex1_Zrotation"]));
            jointManager.SetLocalJointRotation("left_index2", Quaternion.Euler(x: -dataDict["LeftHandIndex2_Xrotation"], y: -dataDict["LeftHandIndex2_Yrotation"], z: -dataDict["LeftHandIndex2_Zrotation"]));
            jointManager.SetLocalJointRotation("left_index3", Quaternion.Euler(x: -dataDict["LeftHandIndex3_Xrotation"], y: -dataDict["LeftHandIndex3_Yrotation"], z: -dataDict["LeftHandIndex3_Zrotation"]));
            jointManager.SetLocalJointRotation("left_middle1", Quaternion.Euler(x: -dataDict["LeftHandMiddle1_Xrotation"], y: -dataDict["LeftHandMiddle1_Yrotation"], z: -dataDict["LeftHandMiddle1_Zrotation"]));
            jointManager.SetLocalJointRotation("left_middle2", Quaternion.Euler(x: -dataDict["LeftHandMiddle2_Xrotation"], y: -dataDict["LeftHandMiddle2_Yrotation"], z: -dataDict["LeftHandMiddle2_Zrotation"]));
            jointManager.SetLocalJointRotation("left_middle3", Quaternion.Euler(x: -dataDict["LeftHandMiddle3_Xrotation"], y: -dataDict["LeftHandMiddle3_Yrotation"], z: -dataDict["LeftHandMiddle3_Zrotation"]));
            jointManager.SetLocalJointRotation("left_pinky1", Quaternion.Euler(x: -dataDict["LeftHandPinky1_Xrotation"], y: -dataDict["LeftHandPinky1_Yrotation"], z: -dataDict["LeftHandPinky1_Zrotation"]));
            jointManager.SetLocalJointRotation("left_pinky2", Quaternion.Euler(x: -dataDict["LeftHandPinky2_Xrotation"], y: -dataDict["LeftHandPinky2_Yrotation"], z: -dataDict["LeftHandPinky2_Zrotation"]));
            jointManager.SetLocalJointRotation("left_pinky3", Quaternion.Euler(x: -dataDict["LeftHandPinky3_Xrotation"], y: -dataDict["LeftHandPinky3_Yrotation"], z: -dataDict["LeftHandPinky3_Zrotation"]));
            jointManager.SetLocalJointRotation("left_ring1", Quaternion.Euler(x: -dataDict["LeftHandRing1_Xrotation"], y: -dataDict["LeftHandRing1_Yrotation"], z: -dataDict["LeftHandRing1_Zrotation"]));
            jointManager.SetLocalJointRotation("left_ring2", Quaternion.Euler(x: -dataDict["LeftHandRing2_Xrotation"], y: -dataDict["LeftHandRing2_Yrotation"], z: -dataDict["LeftHandRing2_Zrotation"]));
            jointManager.SetLocalJointRotation("left_ring3", Quaternion.Euler(x: -dataDict["LeftHandRing3_Xrotation"], y: -dataDict["LeftHandRing3_Yrotation"], z: -dataDict["LeftHandRing3_Zrotation"]));
            jointManager.SetLocalJointRotation("left_thumb1", Quaternion.Euler(x: -dataDict["LeftHandThumb1_Xrotation"], y: -dataDict["LeftHandThumb1_Yrotation"], z: -dataDict["LeftHandThumb1_Zrotation"]));
            jointManager.SetLocalJointRotation("left_thumb2", Quaternion.Euler(x: -dataDict["LeftHandThumb2_Xrotation"], y: -dataDict["LeftHandThumb2_Yrotation"], z: -dataDict["LeftHandThumb2_Zrotation"]));
            jointManager.SetLocalJointRotation("left_thumb3", Quaternion.Euler(x: -dataDict["LeftHandThumb3_Xrotation"], y: -dataDict["LeftHandThumb3_Yrotation"], z: -dataDict["LeftHandThumb3_Zrotation"]));

           
        }



        List<string> Get_dataOrderList()
        {
            List<string> dataOrders = new List<string>();
            dataOrders.Add("Hips_Xposition");
            dataOrders.Add("Hips_Yposition");
            dataOrders.Add("Hips_Zposition");
            dataOrders.Add("Hips_Zrotation");
            dataOrders.Add("Hips_Xrotation");
            dataOrders.Add("Hips_Yrotation");
            dataOrders.Add("Spine_Zrotation");
            dataOrders.Add("Spine_Xrotation");
            dataOrders.Add("Spine_Yrotation");
            dataOrders.Add("Spine1_Zrotation");
            dataOrders.Add("Spine1_Xrotation");
            dataOrders.Add("Spine1_Yrotation");
            dataOrders.Add("Spine2_Zrotation");
            dataOrders.Add("Spine2_Xrotation");
            dataOrders.Add("Spine2_Yrotation");
            dataOrders.Add("Spine3_Zrotation");
            dataOrders.Add("Spine3_Xrotation");
            dataOrders.Add("Spine3_Yrotation");
            dataOrders.Add("Neck_Zrotation");
            dataOrders.Add("Neck_Xrotation");
            dataOrders.Add("Neck_Yrotation");
            dataOrders.Add("Neck1_Zrotation");
            dataOrders.Add("Neck1_Xrotation");
            dataOrders.Add("Neck1_Yrotation");
            dataOrders.Add("Head_Zrotation");
            dataOrders.Add("Head_Xrotation");
            dataOrders.Add("Head_Yrotation");
            dataOrders.Add("RightShoulder_Zrotation");
            dataOrders.Add("RightShoulder_Xrotation");
            dataOrders.Add("RightShoulder_Yrotation");
            dataOrders.Add("RightArm_Zrotation");
            dataOrders.Add("RightArm_Xrotation");
            dataOrders.Add("RightArm_Yrotation");
            dataOrders.Add("RightForeArm_Zrotation");
            dataOrders.Add("RightForeArm_Xrotation");
            dataOrders.Add("RightForeArm_Yrotation");
            dataOrders.Add("RightHand_Zrotation");
            dataOrders.Add("RightHand_Xrotation");
            dataOrders.Add("RightHand_Yrotation");
            dataOrders.Add("RightHandThumb1_Zrotation");
            dataOrders.Add("RightHandThumb1_Xrotation");
            dataOrders.Add("RightHandThumb1_Yrotation");
            dataOrders.Add("RightHandThumb2_Zrotation");
            dataOrders.Add("RightHandThumb2_Xrotation");
            dataOrders.Add("RightHandThumb2_Yrotation");
            dataOrders.Add("RightHandThumb3_Zrotation");
            dataOrders.Add("RightHandThumb3_Xrotation");
            dataOrders.Add("RightHandThumb3_Yrotation");
            dataOrders.Add("RightHandIndex1_Zrotation");
            dataOrders.Add("RightHandIndex1_Xrotation");
            dataOrders.Add("RightHandIndex1_Yrotation");
            dataOrders.Add("RightHandIndex2_Zrotation");
            dataOrders.Add("RightHandIndex2_Xrotation");
            dataOrders.Add("RightHandIndex2_Yrotation");
            dataOrders.Add("RightHandIndex3_Zrotation");
            dataOrders.Add("RightHandIndex3_Xrotation");
            dataOrders.Add("RightHandIndex3_Yrotation");
            dataOrders.Add("RightHandMiddle1_Zrotation");
            dataOrders.Add("RightHandMiddle1_Xrotation");
            dataOrders.Add("RightHandMiddle1_Yrotation");
            dataOrders.Add("RightHandMiddle2_Zrotation");
            dataOrders.Add("RightHandMiddle2_Xrotation");
            dataOrders.Add("RightHandMiddle2_Yrotation");
            dataOrders.Add("RightHandMiddle3_Zrotation");
            dataOrders.Add("RightHandMiddle3_Xrotation");
            dataOrders.Add("RightHandMiddle3_Yrotation");
            dataOrders.Add("RightHandRing1_Zrotation");
            dataOrders.Add("RightHandRing1_Xrotation");
            dataOrders.Add("RightHandRing1_Yrotation");
            dataOrders.Add("RightHandRing2_Zrotation");
            dataOrders.Add("RightHandRing2_Xrotation");
            dataOrders.Add("RightHandRing2_Yrotation");
            dataOrders.Add("RightHandRing3_Zrotation");
            dataOrders.Add("RightHandRing3_Xrotation");
            dataOrders.Add("RightHandRing3_Yrotation");
            dataOrders.Add("RightHandPinky1_Zrotation");
            dataOrders.Add("RightHandPinky1_Xrotation");
            dataOrders.Add("RightHandPinky1_Yrotation");
            dataOrders.Add("RightHandPinky2_Zrotation");
            dataOrders.Add("RightHandPinky2_Xrotation");
            dataOrders.Add("RightHandPinky2_Yrotation");
            dataOrders.Add("RightHandPinky3_Zrotation");
            dataOrders.Add("RightHandPinky3_Xrotation");
            dataOrders.Add("RightHandPinky3_Yrotation");
            dataOrders.Add("LeftShoulder_Zrotation");
            dataOrders.Add("LeftShoulder_Xrotation");
            dataOrders.Add("LeftShoulder_Yrotation");
            dataOrders.Add("LeftArm_Zrotation");
            dataOrders.Add("LeftArm_Xrotation");
            dataOrders.Add("LeftArm_Yrotation");
            dataOrders.Add("LeftForeArm_Zrotation");
            dataOrders.Add("LeftForeArm_Xrotation");
            dataOrders.Add("LeftForeArm_Yrotation");
            dataOrders.Add("LeftHand_Zrotation");
            dataOrders.Add("LeftHand_Xrotation");
            dataOrders.Add("LeftHand_Yrotation");
            dataOrders.Add("LeftHandThumb1_Zrotation");
            dataOrders.Add("LeftHandThumb1_Xrotation");
            dataOrders.Add("LeftHandThumb1_Yrotation");
            dataOrders.Add("LeftHandThumb2_Zrotation");
            dataOrders.Add("LeftHandThumb2_Xrotation");
            dataOrders.Add("LeftHandThumb2_Yrotation");
            dataOrders.Add("LeftHandThumb3_Zrotation");
            dataOrders.Add("LeftHandThumb3_Xrotation");
            dataOrders.Add("LeftHandThumb3_Yrotation");
            dataOrders.Add("LeftHandIndex1_Zrotation");
            dataOrders.Add("LeftHandIndex1_Xrotation");
            dataOrders.Add("LeftHandIndex1_Yrotation");
            dataOrders.Add("LeftHandIndex2_Zrotation");
            dataOrders.Add("LeftHandIndex2_Xrotation");
            dataOrders.Add("LeftHandIndex2_Yrotation");
            dataOrders.Add("LeftHandIndex3_Zrotation");
            dataOrders.Add("LeftHandIndex3_Xrotation");
            dataOrders.Add("LeftHandIndex3_Yrotation");
            dataOrders.Add("LeftHandMiddle1_Zrotation");
            dataOrders.Add("LeftHandMiddle1_Xrotation");
            dataOrders.Add("LeftHandMiddle1_Yrotation");
            dataOrders.Add("LeftHandMiddle2_Zrotation");
            dataOrders.Add("LeftHandMiddle2_Xrotation");
            dataOrders.Add("LeftHandMiddle2_Yrotation");
            dataOrders.Add("LeftHandMiddle3_Zrotation");
            dataOrders.Add("LeftHandMiddle3_Xrotation");
            dataOrders.Add("LeftHandMiddle3_Yrotation");
            dataOrders.Add("LeftHandRing1_Zrotation");
            dataOrders.Add("LeftHandRing1_Xrotation");
            dataOrders.Add("LeftHandRing1_Yrotation");
            dataOrders.Add("LeftHandRing2_Zrotation");
            dataOrders.Add("LeftHandRing2_Xrotation");
            dataOrders.Add("LeftHandRing2_Yrotation");
            dataOrders.Add("LeftHandRing3_Zrotation");
            dataOrders.Add("LeftHandRing3_Xrotation");
            dataOrders.Add("LeftHandRing3_Yrotation");
            dataOrders.Add("LeftHandPinky1_Zrotation");
            dataOrders.Add("LeftHandPinky1_Xrotation");
            dataOrders.Add("LeftHandPinky1_Yrotation");
            dataOrders.Add("LeftHandPinky2_Zrotation");
            dataOrders.Add("LeftHandPinky2_Xrotation");
            dataOrders.Add("LeftHandPinky2_Yrotation");
            dataOrders.Add("LeftHandPinky3_Zrotation");
            dataOrders.Add("LeftHandPinky3_Xrotation");
            dataOrders.Add("LeftHandPinky3_Yrotation");
            dataOrders.Add("pCube4_Zrotation");
            dataOrders.Add("pCube4_Xrotation");
            dataOrders.Add("pCube4_Yrotation");
            dataOrders.Add("RightUpLeg_Zrotation");
            dataOrders.Add("RightUpLeg_Xrotation");
            dataOrders.Add("RightUpLeg_Yrotation");
            dataOrders.Add("RightLeg_Zrotation");
            dataOrders.Add("RightLeg_Xrotation");
            dataOrders.Add("RightLeg_Yrotation");
            dataOrders.Add("RightFoot_Zrotation");
            dataOrders.Add("RightFoot_Xrotation");
            dataOrders.Add("RightFoot_Yrotation");
            dataOrders.Add("RightForeFoot_Zrotation");
            dataOrders.Add("RightForeFoot_Xrotation");
            dataOrders.Add("RightForeFoot_Yrotation");
            dataOrders.Add("RightToeBase_Zrotation");
            dataOrders.Add("RightToeBase_Xrotation");
            dataOrders.Add("RightToeBase_Yrotation");
            dataOrders.Add("LeftUpLeg_Zrotation");
            dataOrders.Add("LeftUpLeg_Xrotation");
            dataOrders.Add("LeftUpLeg_Yrotation");
            dataOrders.Add("LeftLeg_Zrotation");
            dataOrders.Add("LeftLeg_Xrotation");
            dataOrders.Add("LeftLeg_Yrotation");
            dataOrders.Add("LeftFoot_Zrotation");
            dataOrders.Add("LeftFoot_Xrotation");
            dataOrders.Add("LeftFoot_Yrotation");
            dataOrders.Add("LeftForeFoot_Zrotation");
            dataOrders.Add("LeftForeFoot_Xrotation");
            dataOrders.Add("LeftForeFoot_Yrotation");
            dataOrders.Add("LeftToeBase_Zrotation");
            dataOrders.Add("LeftToeBase_Xrotation");
            dataOrders.Add("LeftToeBase_Yrotation");
            return dataOrders;
        }











































































































































































    }
}
