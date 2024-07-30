using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomManager : MonoBehaviour
{
    [SerializeField] BossEnemy boss;
    [SerializeField] BossDoor[] doors;
    [SerializeField] Collider2D triggerColl;
    [SerializeField] string dialog;
    [SerializeField] string bossBeatenDialog;
    [SerializeField] string playerLostDialog;

    Player player;
    bool bossActivated;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Player") || bossActivated) return;

        if (dialog != string.Empty)
            DialogSystem.DialogSystem.Instance.ShowDialog(dialog, onDialogEnd: () => BeginFight(collision));
        else
            BeginFight(collision);

    }

    private void OnDestroy() {
        if (bossActivated) {
            if(player != null)
                player.HealthComponent.OnDied -= PlayerBeatenDialog;
            boss.HealthComponent.OnDied -= ShowBeatenDialog;
        }

    }

    void BeginFight(Collider2D playerColl) {
        player = playerColl.GetComponent<Player>();
        bossActivated = true;
        boss.Activated = true;
        boss.SetPlayerTarget(player);
        boss.HealthComponent.OnDied += ShowBeatenDialog;
        player.HealthComponent.OnDied += PlayerBeatenDialog;
        triggerColl.enabled = false;
        foreach (var door in doors)
            door.Close();

    }

    void ShowBeatenDialog() {
        if (bossBeatenDialog != string.Empty)
            DialogSystem.DialogSystem.Instance.ShowDialog(bossBeatenDialog);
        OnFightEnded();
    }

    void PlayerBeatenDialog() {
        if(playerLostDialog != string.Empty)
            DialogSystem.DialogSystem.Instance.ShowDialog(playerLostDialog);
        OnFightEnded();
    }

    void OnFightEnded() {
        player.HealthComponent.OnDied -= PlayerBeatenDialog;
        boss.HealthComponent.OnDied -= ShowBeatenDialog;
        foreach (var door in doors)
            door.Open();
    }
}
