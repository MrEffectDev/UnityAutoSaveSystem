using UnityEditor;
using UnityEngine;
using System;

namespace MrEffectDev.AutoSave
{
    /// <summary>
    /// This class handles the auto-save notification popup in Unity Editor.
    /// It shows a countdown timer and allows the user to cancel the auto-save operation.
    /// </summary>

    public class AutoSaveNotification : EditorWindow
    {
        private const float WindowWidth = 260;
        private const float WindowHeight = 70;
        private static double _startTime;
        private const float Duration = 10f;

        private void OnEnable()
        {
            _startTime = EditorApplication.timeSinceStartup;
            EditorApplication.update += UpdateCountdown;
        }

        private void OnDisable()
        {
            EditorApplication.update -= UpdateCountdown;
        }

        private void UpdateCountdown()
        {
            double elapsed = EditorApplication.timeSinceStartup - _startTime;

            if (elapsed >= Duration)
            {
                Close();
            }

            Repaint();
        }

        private void OnGUI()
        {
            double remaining = Duration - (EditorApplication.timeSinceStartup - _startTime);
            if (remaining < 0) remaining = 0;

            GUILayout.Label("Auto Save Incoming", EditorStyles.boldLabel);
            GUILayout.Label($"Saving in {Math.Ceiling(remaining)} seconds...", EditorStyles.label);

            if (GUILayout.Button("Cancel Auto Save"))
            {
                AutoSaveSystem.CancelSave();
                Close();
            }
        }

        public static void ShowWindow()
        {
            AutoSaveNotification window = CreateInstance<AutoSaveNotification>();

            Rect mainWindow = EditorGUIUtility.GetMainWindowPosition();
            float x = mainWindow.xMax - WindowWidth - 15;
            float y = mainWindow.yMin + 40;

            window.position = new Rect(x, y, WindowWidth, WindowHeight);
            window.titleContent = new GUIContent("Auto Save");
            window.ShowPopup();
        }
    }
}