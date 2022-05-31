using Godot;

public class Melee_Fighter : Player
{
    [Export]
    float attackTime = 0.3f;

    [Export]
    byte damage = 100;

    bool attackTimeout = false;

    bool isAttacking = false;
    


    public virtual void MelleeAttack()
    {
        if (attackTimeout == false)
        {
            GetNode<Timer>("CollisionShape/DamageBox/AttackTimer").Start();
            attackTimeout = true;
            GetNode<CollisionShape>("CollisionShape/DamageBox/CollisionShape").SetDeferred("disabled", true);
            GetNode<AnimationPlayer>("AnimationPlayer").Play("Attack");
        }




    }


    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("Attack_Shoot"))
        {
            isAttacking = true;
            //
            
        }
        else if (@event.IsActionReleased("Attack_Shoot"))
        {
            isAttacking = false;

        }

    }


    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        if (isAttacking)
        {
            MelleeAttack();
        }
           


        
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<Timer>("CollisionShape/DamageBox/AttackTimer").WaitTime = attackTime;
        GetNode<Timer>("CollisionShape/DamageBox/AttackTimer").Connect("timeout", this, "_OnAttackFinished");

        
    }

    public void _OnAttackFinished()
    {
        GetNode<AnimationPlayer>("AnimationPlayer").Play("AttackReload");
        attackTimeout = false;
        GetNode<CollisionShape>("CollisionShape/DamageBox/CollisionShape").SetDeferred("disabled", false);
        
    }

}
