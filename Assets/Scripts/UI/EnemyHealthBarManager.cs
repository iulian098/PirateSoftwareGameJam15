using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarManager : MonoBehaviour
{
    [SerializeField] UI_EnemyHealthBar healthBarPrefab;
    [SerializeField] Transform healthBarParent;
    PoolingSystem<UI_EnemyHealthBar> healthBarPool;
    List<UI_EnemyHealthBar> healthBarList = new List<UI_EnemyHealthBar>();

    private void Start() {
        healthBarPool = new PoolingSystem<UI_EnemyHealthBar>(healthBarPrefab, 5, healthBarParent);
    }

    public UI_EnemyHealthBar GetHealthBar() {
        UI_EnemyHealthBar healthBar = healthBarPool.Get();//Instantiate(healthBarPrefab, healthBarParent);
        healthBarList.Add(healthBar);
        return healthBar;
    }

    public void FreeHealthBar(UI_EnemyHealthBar healthBar) { 
        healthBarPool.Disable(healthBar);
    }
}
