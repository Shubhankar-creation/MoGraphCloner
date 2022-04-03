using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cloner))]
public class CloneEditor : Editor
{
    Cloner cloner;

    Editor positionEditor;
    Editor scaleEditor;
    Editor rotationEditor;
    Editor stepRotationEditor;
    Editor radiusEditor;

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();

            GUIContent label = new GUIContent("Clone Type");
            cloner.typeInd = EditorGUILayout.Popup(label, cloner.typeInd, cloner.clonerType.ToArray());

            if (check.changed)
            {
                if (cloner.count == 0)
                {
                    cloner.k = 0;
                    cloner.DeleteModel();
                }
                if (cloner.count < cloner.initialCount)
                {
                    cloner.DeleteModel();
                }
                else
                {
                    cloner.GenerateModel();
                }
            }
        }

        if (GUILayout.Button("GenerateModel"))
        {
            cloner.GenerateModel();
        }

        if (cloner.typeInd == 0)
        {
            DrawSettingEditor(cloner.positionSetting, cloner.positionChanged, ref cloner.posFoldout, ref positionEditor);
            DrawSettingEditor(cloner.scaleSetting, cloner.scaleChanged, ref cloner.scaleFoldout, ref scaleEditor);
            DrawSettingEditor(cloner.rotationSetting, cloner.rotationChanged, ref cloner.rotFoldout, ref rotationEditor);
            DrawSettingEditor(cloner.stepRotationSetting, cloner.stepRotChanged, ref cloner.stepFoldout, ref stepRotationEditor);
        }
        
        else if(cloner.typeInd == 1)
        {
            DrawSettingEditor(cloner.radiusSetting, cloner.radiusChanged, ref cloner.radiusFoldout, ref radiusEditor);
            DrawSettingEditor(cloner.scaleSetting, cloner.scaleChanged, ref cloner.scaleFoldout, ref scaleEditor);
            DrawSettingEditor(cloner.rotationSetting, cloner.rotationChanged, ref cloner.rotFoldout, ref rotationEditor);
        }
    }

    void DrawSettingEditor(Object setting, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    {
        if (setting != null)
        {
            foldout = EditorGUILayout.InspectorTitlebar(foldout, setting);

            using (var check = new EditorGUI.ChangeCheckScope())
            {

                if (foldout)
                {
                    CreateCachedEditor(setting, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (onSettingsUpdated != null)
                            onSettingsUpdated();
                    }
                }

            }
        }
    }
    private void OnEnable()
    {
        cloner = (Cloner)target;
    }
}
