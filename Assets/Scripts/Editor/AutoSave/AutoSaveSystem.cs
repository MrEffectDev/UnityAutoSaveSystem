using UnityEditor;
using UnityEngine;

namespace MrEffectDev.AutoSave
{
    /// <summary>
    /// This class handles the auto-saving functionality in Unity Editor.
    /// It saves the project and scene at regular intervals defined in the settings.
    /// </summary>

    [InitializeOnLoad]
    public static class AutoSaveSystem
    {
        private static double _lastSaveTime;
        private static bool _cancelPending = false;
        private static bool _notificationShown = false;

        static AutoSaveSystem()
        {
            _lastSaveTime = EditorApplication.timeSinceStartup;
            EditorApplication.update += OnEditorUpdate;
        }

        private static void OnEditorUpdate()
        {
            float interval = AutoSaveSettings.GetInterval() * 60f;
            double elapsed = EditorApplication.timeSinceStartup - _lastSaveTime;

            if (elapsed >= interval - 10f && elapsed < interval && AutoSaveSettings.ShouldShowPopup() && !_notificationShown)
            {
                AutoSaveNotification.ShowWindow();
                _notificationShown = true;
            }

            if (elapsed >= interval)
            {
                if (!_cancelPending)
                {
                    SaveAll();
                }

                _lastSaveTime = EditorApplication.timeSinceStartup;
                _cancelPending = false;
                _notificationShown = false;
            }
        }

        private static void SaveAll()
        {
            Debug.Log("Auto-saving project and scene...");
            EditorApplication.ExecuteMenuItem("File/Save");
            EditorApplication.ExecuteMenuItem("File/Save Project");
        }

        public static void CancelSave()
        {
            _cancelPending = true;
            Debug.Log("Auto Save Cancelled by User.");
        }
    }
}