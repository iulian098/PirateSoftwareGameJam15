using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour, IInteractable
{
    [SerializeField] Collider2D coll;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite closedSprite;
    [SerializeField] GameObject opened;
    [SerializeField] GameObject closed;
    [SerializeField] string interactDialog;
    [SerializeField] string closingGateAudio;
    [SerializeField] string openingGateAudio;

    private void Start() {
        if (interactDialog == string.Empty)
            gameObject.layer = 0;
    }

    public void Close() {
        //spriteRenderer.sprite = closedSprite;
        opened.SetActive(false);
        closed.SetActive(true);
        coll.enabled = true;
        SoundManager.Instance.PlaySound(transform.position, closingGateAudio);
    }

    public void Open() {
        opened.SetActive(true);
        closed.SetActive(false);
        coll.enabled = false;
        SoundManager.Instance.PlaySound(transform.position, openingGateAudio);
    }

    public void OnInteract() {
        if (interactDialog == string.Empty) return;
        DialogSystem.DialogSystem.Instance.ShowDialog(interactDialog);
    }
}
