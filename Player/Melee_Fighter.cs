using Godot;

public class Melee_Fighter : Player
{
    float attackTime = 0.3f;

    bool attackTimeout = false;


    public virtual void MelleeAttack()
    {
        if (attackTimeout == false)
        {
            GetNode<Timer>("DamageBox/AttackTimer").Start();
            attackTimeout = true;
            GetNode<CollisionShape>("DamageBox/CollisionShape").SetDeferred("disabled", true);
        }




    }


    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("Attack_Shoot"))
        {
            MelleeAttack();
            GD.Print("Рђрър");
        }


    }


    public override void _PhysicsProcess(float delta)
    {
        Movement();
        base._PhysicsProcess(delta);
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<Timer>("DamageBox/AttackTimer").WaitTime = attackTime;
        GetNode<Timer>("DamageBox/AttackTimer").Connect("timeout", this, "_OnAttackFinished");
    }

    public void _OnAttackFinished()
    {
        GetNode<CollisionShape>("DamageBox/CollisionShape").SetDeferred("disabled", false);

    }

}
