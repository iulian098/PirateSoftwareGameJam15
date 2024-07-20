using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] UI_HealthBar playerHealthBar;
    
    public UI_HealthBar PlayerHealthBar => playerHealthBar;
}
