using UnityEngine;

public class LogicUnit : MonoBehaviour
{
    public Vector2 position;       // vị trí unit
    public Vector2 direction;      // hướng nhìn

    public bool isAttacking; //try

    public EAnimParametor state = EAnimParametor.Idle;
    public EFsmAction fsmAction = 0;

    protected bool Inited = false;
    public virtual void InitData() { }

    public virtual void HandleFsmAction(EFsmAction action) { }

    public virtual void StopMove() { }
    public virtual void Move() { }

    public virtual void Teleport(Vector2 position) { }

    public virtual void UpdateUI() { }

    public virtual void SetAnimParametor(EAnimParametor parametor) { }

    public virtual void TakeDamage(int damage) { }
}
    