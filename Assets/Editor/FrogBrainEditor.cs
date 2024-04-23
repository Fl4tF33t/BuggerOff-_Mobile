using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FrogBrain))]
public class FrogBrainEditor : Editor
{
    public override void OnInspectorGUI()
    {
        FrogBrain script = (FrogBrain)target;

        // Draw default inspector
        DrawDefaultInspector();

        // Show/hide variables based on enum value
        if (script.attackType == LogicSO.AttackType.Projectile || script.attackType == LogicSO.AttackType.Other)
        {
            script.projectilePos = EditorGUILayout.ObjectField("Projectile Position", script.projectilePos, typeof(Transform), true) as Transform;
            script.projectile = EditorGUILayout.ObjectField("Projectile", script.projectile, typeof(GameObject), true) as GameObject;
        }
        if (script.attackType == LogicSO.AttackType.Single)
        {
            script.singleAttack = EditorGUILayout.ObjectField("Single Attack", script.singleAttack, typeof(Transform), true) as Transform;
        }
    }
}