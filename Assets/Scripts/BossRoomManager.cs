using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomManager : MonoBehaviour
{
    [SerializeField] BossEnemy boss;
    [SerializeField] BossDoor[] doors;
    [SerializeField] string dialog;
    [SerializeField] string bossBeatenDialog;
    [SerializeField] string playerLostDialog;

    Player player;
    bool bossActivated;

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player") || bossActivated) return;

        if (dialog != string.Empty)
            DialogSystem.DialogSystem.Instance.ShowDialog(dialog, onDialogEnd: () => BeginFight(other));
        else
            BeginFight(other);
    }

    private void OnDestroy() {
        if (bossActivated) {
            if(player != null)
                player.HealthComponent.OnDied -= PlayerBeatenDialog;
            boss.HealthComponent.OnDied -= ShowBeatenDialog;
        }

    }

    void BeginFight(Collider playerColl) {
        player = playerColl.GetComponent<Player>();
        bossActivated = true;
        boss.Activated = true;
        boss.SetPlayerTarget(player);
        boss.HealthComponent.OnDied += ShowBeatenDialog;
        player.HealthComponent.OnDied += PlayerBeatenDialog;
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
