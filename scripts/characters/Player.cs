using Godot;
using System;
using wizardgame.scripts;
using wizardgame.scripts.spells;
using wizardgame.scripts.utils;
public partial class Player : Character
{
    AnimatedSprite2D sprite;
    AnimatedSprite2D elementCircle;
    SpellBindings spellBindings = new();
    Element currentElement;
    Vector2 LastMoveDirection = new Vector2(0, 1);
    ManaBar manaBar;
    float MaxMana = 100;
    private float _mana;
    float ManaRegenRate = 30;
    bool casting = false;

    float TotalXPGained;
    float XP;

    public float Mana
    {
        get { return _mana; }
        set
        {
            _mana = value;
            manaBar.MaxValue = MaxMana;
            manaBar.Amount = value;
            //GD.Print($"Changing mana to {value}");
        }
    }

    public override void _Ready()
    {
        base._Ready();
        sprite = GetChild<AnimatedSprite2D>(1);
        healthBar = GetChild<HealthBar>(2);
        manaBar = GetChild<ManaBar>(3);
        elementCircle = GetChild<AnimatedSprite2D>(4);
        SetProperties(200, 6000, 10000, 600, 70);
        Mana = MaxMana;
        Health = MaxHealth;
        currentElement = Element.Earth; // default element
        ApplyDefaultBindings();
    }

    public override void _Process(double delta)
    {
        CheckAxisInputs(out Vector2 moveDirection, out Vector2 aimDirection);
        bool moveInputted = !moveDirection.Equals(new Vector2());
        bool aimInputted = !aimDirection.Equals(new Vector2());

        Element? element = CheckElementInput();
        if (element != null)
        {
            currentElement = element.Value;
            var animationname = element switch
            {
                Element.Air => "air",
                Element.Earth => "earth",
                Element.Fire => "fire",
                Element.Water => "water",
                _ => throw new NotSupportedException("nah what the hell bruh")
            };
            elementCircle.Play(animationname);
        }

        AttackButton? attackInput = CheckAttackInput();
        if (attackInput.HasValue)
        {

            if (!aimInputted)
            {
                if (moveInputted)
                {
                    aimDirection = moveDirection;
                }
                else
                {
                    aimDirection = LastMoveDirection;
                }
            }
            if (CastSpellByInput(aimDirection, (AttackButton)attackInput))
            {
                sprite.Play("cast");
                casting = true;
            }
        }

        //if (moveInputted)
        //{
        //    Velocity += Accel * (float)delta * moveDirection;
        //}
        //else
        //{
        //    if (!Velocity.Equals(new Vector2()))
        //    {
        //        ApplyFrictionToVelocity(delta);
        //    }
        //}

        if (!casting) { HandleWalkAnimation(moveDirection); }


        if (Mana < MaxMana)
        {
            Mana += ManaRegenRate * (float)delta;
        }

        Move(moveDirection, (float)delta);
    }


    private void CheckAxisInputs(out Vector2 moveDirection, out Vector2 aimDirection)
    {
        moveDirection = Input.GetVector("left", "right", "up", "down").Normalized();
        if (!moveDirection.Equals(new Vector2(0, 0))) // if input is non-zero, save as last direction
        {
            LastMoveDirection = moveDirection;
        }
        aimDirection = Input.GetVector("aim_left", "aim_right", "aim_up", "aim_down").Normalized();
    }

    Element? CheckElementInput()
    {
        if (Input.IsActionJustPressed("element_earth"))
        {
            return Element.Earth;
        }
        else if (Input.IsActionJustPressed("element_fire"))
        {
            return Element.Fire;
        }
        else if (Input.IsActionJustPressed("element_water"))
        {
            return Element.Water;
        }
        else if (Input.IsActionJustPressed("element_air"))
        {
            return Element.Air;
        }
        else return null;
    }

    AttackButton? CheckAttackInput()
    {
        if (Input.IsActionJustPressed("attack_4"))
        {
            return AttackButton.RT;
        }
        else if (Input.IsActionJustPressed("attack_3"))
        {
            return AttackButton.LT;
        }
        else if (Input.IsActionJustPressed("attack_2"))
        {
            return AttackButton.RB;
        }
        else if (Input.IsActionJustPressed("attack_1"))
        {
            return AttackButton.LB;
        }
        else return null;
    }

    public void ApplyDefaultBindings()
    {
        spellBindings.AssignSpell(AttackButton.RT, SpellType.FireBall, Element.Fire);
        spellBindings.AssignSpell(AttackButton.RT, SpellType.MudWall, Element.Earth);
        spellBindings.AssignSpell(AttackButton.RT, SpellType.Freeze, Element.Water);
    }

    public bool CastSpellByInput(Vector2 aimDirection, AttackButton attackInput)
    {
        var st = spellBindings.GetSpell(attackInput, currentElement);
        if (st == null)
        {
            return false;
        }
        return CastSpell((SpellType)st, aimDirection);
        //CastSpell(SpellType.MudWall, aimDirection);
        //CastSpell(SpellType.FireBall, aimDirection);

    }
    public bool CastSpell(SpellType spelltype, Vector2 dir)
    {
        PackedScene spellScene = Spell.SpellSceneFromSpellType(spelltype);
        Spell spell = spellScene.Instantiate<Spell>();
        if (Mana > spell.ManaCost)
        {
            spell.Cast(this, dir, level);
            Mana -= spell.ManaCost;
            return true;
        }
        else { return false; }
    }

    public void HandleWalkAnimation(Vector2 inputMove)
    {
        bool moving = inputMove.Length() > 0;
        // just continue last animation
        if (!moving)
        {
            return;
        }

        Direction4 dir = Maths.GetVectorDirection4(inputMove);

        string animation = dir switch
        {
            Direction4.Left => "stand_left",
            Direction4.Right => "stand_right",
            Direction4.Up => "stand_up",
            Direction4.Down => "stand_down",
            _ => throw new Exception("player animation switch broken"),
        };

        sprite.Play(animation);


    }


    public void _on_animated_sprite_2d_animation_finished()
    {
        if (sprite.Animation == "cast")
        {
            HandleWalkAnimation(LastMoveDirection);
            casting = false;
        }

    }
}
