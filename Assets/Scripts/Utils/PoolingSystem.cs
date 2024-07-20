using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem<T> where T : Component
{
    T targetObject;
    Queue<T> objects = new Queue<T>();
    Transform parent;

    public PoolingSystem(T targetObject, int amount = 1, Transform parent = null) {
        this.targetObject = targetObject;
        this.parent = parent;
        SpawnObjects(amount);
    }

    void SpawnObjects(int amount) {
        Debug.Log("Spawn " + amount);
        for (int i = 0; i < amount; i++) {
            GameObject obj = GameObject.Instantiate(targetObject.gameObject, parent);
            objects.Enqueue(obj.GetComponent<T>());
            obj.gameObject.SetActive(false);
        }
    }

    public T Get() {
        if (objects.Count == 0)
            SpawnObjects(1);
        return GetObject();
    }

    T GetObject() {
        T obj = objects.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Disable(T obj) {
        objects.Enqueue(obj);
        obj.gameObject.SetActive(false);
    }
}
