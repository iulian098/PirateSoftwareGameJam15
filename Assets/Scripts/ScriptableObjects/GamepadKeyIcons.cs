using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "GamepadKeyIcons", menuName = "Scriptable Objects/GamepadKeyIcons")]
public class GamepadKeyIcons : ScriptableObject
{
    [System.Serializable]
    public class ActionIcon
    {
        public string inputPath;
        public Sprite icon;
        public string spriteName;
    }

    [System.Serializable]
    public class KeyData
    {
        public InputDeviceType deviceType;
        public List<ActionIcon> icons;
    }

    [SerializeField] List<KeyData> icons;

    public List<KeyData> Icons => icons;
}
