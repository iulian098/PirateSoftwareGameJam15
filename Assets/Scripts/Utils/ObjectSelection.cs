using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSelection : MonoBehaviour
{
    [SerializeField] GameObject selectObject;

    public void SelectObject()
    {
        EventSystem.current.SetSelectedGameObject(selectObject);
    }
}
