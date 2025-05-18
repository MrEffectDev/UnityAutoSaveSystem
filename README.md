---

# Unity Auto Save System

An automatic save system for the Unity Editor that saves your scene and project at configurable intervals, with an optional warning popup before saving.

---

## Installation

1. Download the latest release `.unitypackage` file from the Releases section.
2. Import the package into your Unity project via **Assets > Import Package > Custom Package...**
3. That’s it! Auto-saving will start automatically when you open Unity.

---

## Features

* Automatically saves your scene and project at a configurable interval (in minutes).
* Optional warning popup with a countdown before auto-saving.
* Ability to cancel the upcoming auto-save via the notification window.
* Settings accessible through **File > Auto Save Settings** menu.
* Settings persist between editor sessions.

---

## Configuration

Open **File > Auto Save Settings** to:

* Set the auto-save interval (minimum 0.5 minutes).
* Enable or disable the warning popup before saving.

---

## Warning Popup Behavior

* Appears 10 seconds before the auto-save.
* Displays in the top-right corner of the Unity Editor.
* Allows canceling the upcoming auto-save.

---

## Implementation Details

* Works only inside the Unity Editor.
* Uses `EditorPrefs` to store settings persistently.
* Time tracking uses `EditorApplication.timeSinceStartup`.
* Notification implemented as a non-modal editor window.

---

## File Structure

```
Assets/
└── Scripts/
    └── Editor/
        ├── AutoSaveSystem.cs
        ├── AutoSaveSettings.cs
        └── AutoSaveNotification.cs
```

---

## License

This project is licensed under the MIT License.
Feel free to use and modify it in personal and commercial projects.

---
