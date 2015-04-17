using UnityEditor;

[CustomEditor(typeof(CreatureStats))]
public class CreatureStatsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CreatureStats myTarget = (CreatureStats)target;
        if (!myTarget || myTarget.GridPosition == null)
        {
            return;
        }

        EditorGUILayout.TextField("Grid position: (" + myTarget.GridPosition.x + ", " + myTarget.GridPosition.y + ")");
    }
}