using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Chapter04))]
public class MyChapter04 : Editor
{
    Matrix4x4 matrix = new Matrix4x4();

    //행렬식 구하기 3x3,4x4 
    float determinant3x3;
    float determinant4x4;

    Vector4 rhs;
    Vector4 result;

 

    //GUI 영역을 그리는 함수 
    public override void OnInspectorGUI()
    {
        //Update : 내부 캐쉬에서 최신 데이터를 얻습니다. 항상 다루기 전에 미리 호출 해둬야함.
        //serializedObject.ApplyModifiedProperties : 변경된 사항을 적용
        serializedObject.Update();
        //DrawPropertiesExcluding(serializedObject, new string[] { "m_Script" });
        serializedObject.ApplyModifiedProperties();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField(new GUIContent("Matrix4x4"));

        matrix.SetRow(0, RowVector4Field(matrix.GetRow(0)));
        matrix.SetRow(1, RowVector4Field(matrix.GetRow(1)));
        matrix.SetRow(2, RowVector4Field(matrix.GetRow(2)));
        matrix.SetRow(3, RowVector4Field(matrix.GetRow(3)));

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUI.skin.box);

        determinant3x3 = EditorGUILayout.FloatField("Determinant (3x3)", determinant3x3);
        determinant4x4 = EditorGUILayout.FloatField("Determinant (4x4)", determinant4x4);

        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical(GUI.skin.box);

        rhs = EditorGUILayout.Vector4Field("RHS", rhs);

        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();

        if(GUILayout.Button("operator *"))
        {
            result = matrix * rhs;
        }

        if(GUILayout.Button("MultiplyPoint"))
        {
            result = matrix.MultiplyPoint(rhs);
        }
        
        if(GUILayout.Button("MultiplyPoint3x4"))
        {
            result = matrix.MultiplyPoint3x4(rhs);
        }

        if(GUILayout.Button("MultiplyVector"))
        {
            result = matrix.MultiplyPoint(rhs);
        }

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.Vector4Field("Result", result);
        EditorGUILayout.EndVertical();

        if(EditorGUI.EndChangeCheck())
        {
            determinant3x3 = GetDeterminant3x3(matrix);
            determinant4x4 = GetDeterminant4x4(matrix);
            Undo.RecordObject(target, "Chapter04EditorUndo");
            EditorUtility.SetDirty(target);
        }


        if(GUILayout.Button("테스트트트트"))
        {

        }

    }


    public float GetDeterminant3x3(Matrix4x4 m)
    {
        return m.m00 * m.m11 * m.m22 - m.m00 * m.m12 * m.m21 - m.m01 * m.m10 * m.m22
            + m.m01 * m.m12 * m.m20 + m.m02 * m.m10 * m.m21 - m.m02 * m.m11 * m.m20;
    }

    public float GetDeterminant4x4(Matrix4x4 m)
    {
        return m.m00;
    }


    public static Vector4 RowVector4Field(Vector4 value,params GUILayoutOption[] options)
    {
        Rect pos = EditorGUILayout.GetControlRect(true, 16f, EditorStyles.numberField, options);
        float[] values = new float[] { value.x, value.y, value.z, value.w };

        EditorGUI.BeginChangeCheck();
        EditorGUI.MultiFloatField(pos, new GUIContent[] { new GUIContent(), new GUIContent(), new GUIContent(), new GUIContent() }, values);

        if(EditorGUI.EndChangeCheck())
        {
            value.Set(values[0], values[1], values[2], values[3]);
        }


        return value;
    }
}
