using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarManager : MonoBehaviour
{
    [SerializeField] UI_EnemyHealthBar healthBarPrefab;
    [SerializeField] Transform helathBarParent;

    List<UI_EnemyHealthBar> healthBarList = new List<UI_EnemyHealthBar>();

    private void FixedUpdate() {
        
    }

    public UI_EnemyHealthBar GetHealthBar() {
        UI_EnemyHealthBar healthBar = Instantiate(healthBarPrefab, helathBarParent);
        healthBarList.Add(healthBar);
        return healthBar;
    }
}
