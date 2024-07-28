using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public void PlaySound() {
        SoundManager.Instance.PlaySound(Vector3.zero, "ButtonClick");
    }
}
