using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CardData))]
public class CardDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CardData myTarget = (CardData)target;

        if (myTarget.TypeOfCard == CardType.Spell) {
            RuntimeAnimatorController anim = EditorGUILayout.ObjectField("Spell Animation Controller", myTarget.SpellAnimationController, typeof (RuntimeAnimatorController),
                false) as RuntimeAnimatorController;

            myTarget.SpellAnimationController = anim;
        }

        if (GUI.changed) {
            EditorUtility.SetDirty(myTarget);
        }
        EditorUtility.SetDirty(this);
    }
}
