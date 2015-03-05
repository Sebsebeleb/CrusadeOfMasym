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
            string anim = EditorGUILayout.TextField("Spell Animation Controller", myTarget.SpellAnimationName);

            myTarget.SpellAnimationName= anim;
        }

        if (GUI.changed) {
            EditorUtility.SetDirty(myTarget);
        }
        EditorUtility.SetDirty(this);
    }
}
