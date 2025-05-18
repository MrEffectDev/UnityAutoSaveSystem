using UnityEditor;
using UnityEngine;

namespace MrEffectDev.AutoSave
{
    /// <summary>
    /// This class provides a settings window for configuring the auto-save functionality.
    /// It allows users to set the save interval and whether to show a warning popup before saving.
    /// </summary>
    [InitializeOnLoad]
    public class AutoSaveSettings : EditorWindow
    {
        private const string IntervalKey = "AutoSave_Interval";
        private const string ShowPopupKey = "AutoSave_ShowPopup";

        private static float _intervalMinutes = 15f;
        private static bool _showPopup = true;

        [MenuItem("File/Auto Save Settings")]
        public static void ShowWindow()
        {
            GetWindow<AutoSaveSettings>("Auto Save Settings");
        }

        private void OnEnable()
        {
            _intervalMinutes = EditorPrefs.GetFloat(IntervalKey, 5f);
            _showPopup = EditorPrefs.GetBool(ShowPopupKey, true);
        }

        private void OnGUI()
        {
            GUILayout.Label("Auto Save Settings", EditorStyles.boldLabel);

            _intervalMinutes = EditorGUILayout.FloatField("Save Interval (minutes)", _intervalMinutes);
            _showPopup = EditorGUILayout.Toggle("Show Warning Popup", _showPopup);

            if (_intervalMinutes < 0.5f) _intervalMinutes = 0.5f;

            if (GUILayout.Button("Save Settings"))
            {
                EditorPrefs.SetFloat(IntervalKey, _intervalMinutes);
                EditorPrefs.SetBool(ShowPopupKey, _showPopup);
                EditorUtility.DisplayDialog("Saved", "Auto Save settings updated!", "OK");
            }
        }

        public static float GetInterval() => EditorPrefs.GetFloat(IntervalKey, 5f);
        public static bool ShouldShowPopup() => EditorPrefs.GetBool(ShowPopupKey, true);
    }
}