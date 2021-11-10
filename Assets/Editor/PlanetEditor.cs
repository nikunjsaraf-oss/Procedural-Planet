using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Planet))]
    public class PlanetEditor : UnityEditor.Editor
    {
        private Planet _planet;
        private UnityEditor.Editor _shapeEditor;
        private UnityEditor.Editor _colorEditor;

        public override void OnInspectorGUI()
        {
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                base.OnInspectorGUI();
                if (check.changed)
                {
                    _planet.GeneratePlanet();
                }
            }

            if (GUILayout.Button("Generate Planet"))
            {
                _planet.GeneratePlanet();
            }

            DrawSettingsEditor(_planet.shapeSettings, _planet.OnShapeSettingsUpdated, ref _planet.shapeSettingsFoldout,
                ref _shapeEditor);
            DrawSettingsEditor(_planet.colorSettings, _planet.OnColorSettingsUpdated, ref _planet.colorSettingsFoldout,
                ref _colorEditor);
        }

        private void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldOut,
            ref UnityEditor.Editor editor)
        {
            if (settings == null) return;
            foldOut = EditorGUILayout.InspectorTitlebar(foldOut, settings);
            using var check = new EditorGUI.ChangeCheckScope();
            if (!foldOut) return;
            CreateCachedEditor(settings, null, ref editor);
            editor.OnInspectorGUI();

            if (!check.changed) return;
            onSettingsUpdated?.Invoke();
        }

        private void OnEnable()
        {
            _planet = (Planet) target;
        }
    }
}