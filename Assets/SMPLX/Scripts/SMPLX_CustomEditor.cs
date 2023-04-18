using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LightweightMatrixCSharp;
using Assets.SMPLX.Scripts.Enums;

[CustomEditor(typeof(SMPLX))]
public class SMPLX_CustomEditor : Editor
{
    SMPLX smpl_x;
    
    void Awake()
    {
        smpl_x = (SMPLX)target;
        smpl_x.Awake(); 
    }

    public override void OnInspectorGUI()
    {
        Undo.RecordObject(smpl_x, smpl_x.name); // allow GUI undo in custom editor
        using (new EditorGUILayout.VerticalScope("Box"))
        {
            Update_Information();
            Update_ShapeCategories(defaultColor: GUI.backgroundColor);
            Update_ExpressionCategories(defaultColor: GUI.backgroundColor);
            Update_PoseCategories(defaultColor: GUI.backgroundColor);
        }
        serializedObject.ApplyModifiedProperties();
    }

    void Update_Information()
    {
        string message = $"Model: {smpl_x._numBetaShapes} beta shapes," +
            $" {smpl_x._numExpressions} expressions, " +
            $"{smpl_x._numPoseCorrectives} pose correctives";
        EditorGUILayout.HelpBox(message, MessageType.None);
    }
    void Update_ShapeCategories(Color defaultColor)
    {
        using (new EditorGUILayout.VerticalScope("Box"))
        {
            using (new EditorGUILayout.VerticalScope("Box"))
            {
                GUI.backgroundColor = Color.yellow;
                GUILayout.Button("Shape");
                GUI.backgroundColor = defaultColor;
                float labelWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 200;
                EditorGUIUtility.labelWidth = labelWidth;
            }
            using (new EditorGUILayout.VerticalScope("Box"))
            {
                for (int i = 0; i < SMPLX.NUM_BETAS; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Beta " + i, GUILayout.Width(50));
                    smpl_x.betas[i] = EditorGUILayout.Slider(smpl_x.betas[i], -5, 5);
                    EditorGUILayout.EndHorizontal();
                }
        
                float labelWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 200;
                EditorGUIUtility.labelWidth = labelWidth;
            }
            using (new EditorGUILayout.VerticalScope("Box"))
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Set"))
                {
                    smpl_x.SetBetaShapes();
                }
                if (GUILayout.Button("Random"))
                {
                    for (int i = 0; i < SMPLX.NUM_BETAS; i++)
                        smpl_x.betas[i] = Random.Range(-2.0f, 2.0f);
                    smpl_x.SetBetaShapes();
                }
                if (GUILayout.Button("Reset"))
                {
                    for (int i = 0; i < SMPLX.NUM_BETAS; i++)
                        smpl_x.betas[i] = 0.0f;
                    smpl_x.SetBetaShapes();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
    void Update_ExpressionCategories(Color defaultColor)
    {
        using (new EditorGUILayout.VerticalScope("Box"))
        {
            using (new EditorGUILayout.VerticalScope("Box"))
            {
                GUI.backgroundColor = Color.yellow;
                GUILayout.Button("Expression");
                GUI.backgroundColor = defaultColor;
            }
            using (new EditorGUILayout.VerticalScope("Box"))
            {
                for (int i = 0; i < SMPLX.NUM_BETAS; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Exp " + i, GUILayout.Width(50));
                    smpl_x.expressions[i] = EditorGUILayout.Slider(smpl_x.expressions[i], -2, 2);
                    EditorGUILayout.EndHorizontal();
                }
            }
            using (new EditorGUILayout.VerticalScope("Box"))
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Set"))
                {
                    smpl_x.SetExpressions();
                }
                if (GUILayout.Button("Random"))
                {
                    for (int i = 0; i < SMPLX.NUM_EXPRESSIONS; i++)
                        smpl_x.expressions[i] = Random.Range(-2.0f, 2.0f);
                    smpl_x.SetExpressions();
                }
                if (GUILayout.Button("Reset"))
                {
                    for (int i = 0; i < SMPLX.NUM_EXPRESSIONS; i++)
                        smpl_x.expressions[i] = 0.0f;
                    smpl_x.SetExpressions();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
    void Update_PoseCategories(Color defaultColor)
    {
        using (new EditorGUILayout.VerticalScope("Box"))
        {
            using (new EditorGUILayout.VerticalScope("Box"))
            {
                GUI.backgroundColor = Color.yellow;
                GUILayout.Button("Pose");
                GUI.backgroundColor = defaultColor;
            }
            using (new EditorGUILayout.VerticalScope("Box"))
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Body Pose", GUILayout.Width(100));
                if (GUILayout.Button("T-Pose"))
                    smpl_x.SetBodyPose(BodyPose.T);
                if (GUILayout.Button("A-Pose"))
                    smpl_x.SetBodyPose(BodyPose.A);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Hand Pose", GUILayout.Width(100));
                if (GUILayout.Button("Flat"))
                    smpl_x.SetHandPose(HandPose.Flat);
                if (GUILayout.Button("Relaxed"))
                    smpl_x.SetHandPose(HandPose.Relaxed);
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}


