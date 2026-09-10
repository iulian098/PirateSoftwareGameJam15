using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected HealthComponent healthComponent;
    [SerializeField] protected Animator anim;

    public HealthComponent HealthComponent => healthComponent;
    public Animator Animator => anim;

    protected virtual void Start() { }

    public virtual void ReceiveDamage(int damage) {
        healthComponent.ReceiveDamage(damage);
    }

    public virtual void ReceiveDamage(WeaponData weaponData) {
        healthComponent.ReceiveDamage(weaponData.Damage);
    }
}
