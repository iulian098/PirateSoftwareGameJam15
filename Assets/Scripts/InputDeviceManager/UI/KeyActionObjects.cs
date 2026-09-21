using System;
using UnityEngine;

public class KeyActionObjects : KeyIcon
{
    [System.Serializable]
    public class ObjectsData
    {
        public InputDeviceType device;
        public GameObject[] objects;
    }

    [SerializeField] ObjectsData[] objectsData;

    public override void DeviceChanged(InputDeviceType deviceType)
    {
        for (int i = 0; i < objectsData.Length; i++)
            for (int j = 0; j < objectsData[i].objects.Length; j++)
                objectsData[i].objects[j].gameObject.SetActive(objectsData[i].device == deviceType);
    }
}
