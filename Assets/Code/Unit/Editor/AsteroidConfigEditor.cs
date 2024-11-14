using UnityEditor;

namespace Unit.Editor
{
    [CustomEditor(typeof(AsteroidConfig), true)]
    internal sealed class AsteroidConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            AsteroidConfig config = target as AsteroidConfig;
            if (!config)
                return;

            if (config.SubsCountMax < config.SubsCountMin)
            {
                SerializedProperty prop = serializedObject.FindProperty("_subsCountMax");
                prop.intValue = config.SubsCountMin;
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}