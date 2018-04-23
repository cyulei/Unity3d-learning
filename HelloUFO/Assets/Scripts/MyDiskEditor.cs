using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(DiskData))]
[CanEditMultipleObjects]
public class MyDEditor : Editor
{
    SerializedProperty score;                              //分数
    SerializedProperty color;                              //颜色
    SerializedProperty scale;                              //大小

    void OnEnable()
    {
        //序列化对象后获得各个值
        score = serializedObject.FindProperty("score");
        color = serializedObject.FindProperty("color");
        scale = serializedObject.FindProperty("scale");
    }

    public override void OnInspectorGUI()
    {
        //开启更新
        serializedObject.Update();
        //设置滑动条
        EditorGUILayout.IntSlider(score, 0, 5, new GUIContent("score"));
  
        if (!score.hasMultipleDifferentValues)
        {
            //显示进度条
            ProgressBar(score.intValue / 5f, "score");
        }
        //显示值
        EditorGUILayout.PropertyField(color);
        EditorGUILayout.PropertyField(scale);
        //应用更新
        serializedObject.ApplyModifiedProperties();
    }
    private void ProgressBar(float value, string label)
    {
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        //中间留一个空行
        EditorGUILayout.Space();
    }
}
