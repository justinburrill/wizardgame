using System;
using System.Linq;

using Godot;

using wizardgame.hud;
using wizardgame.scripts;
using wizardgame.spells;
using wizardgame.utils;

namespace wizardgame.characters;

public partial class Player : Character
{
    private AnimatedSprite2D elementCircle;
    private SpellBindings spellBindings = new();
    private Element currentElement;
    private Vector2 LastMoveDirection = new(0, 1);
    private Camera camera;
    private ManaBar manaBar;
    private float MaxMana = 100;
    private float _mana;
    private float ManaRegenRate = 30;
    private bool casting = false;

    private float TotalXPGained;
    private float XP;

    private DebugText debugText;

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
        //GD.Print($"Inside wizard ready: GetChildCount(): {GetChildCount()}");
        Sprite = GetChild<AnimatedSprite2D>(1);
        healthBar = GetChild<HealthBar>(2);
        manaBar = GetChild<ManaBar>(3);
        elementCircle = GetChild<AnimatedSprite2D>(4);
        camera = level.GetNode<Camera>("Camera");
        debugText = new DebugText(camera);
        InitProperties(200, 6000, 10000, 600, 70);
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
                aimDirection = moveInputted ? moveDirection : LastMoveDirection;
            }
            if (CastSpellByInput(aimDirection, (AttackButton)attackInput))
            {
                Sprite.Play("cast");
                casting = true;
            }
        }

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

    private Element? CheckElementInput()
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
        else
        {
            return Input.IsActionJustPressed("element_air") ? Element.Air : null;
        }
    }

    private AttackButton? CheckAttackInput()
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
        else
        {
            return Input.IsActionJustPressed("attack_1") ? AttackButton.LB : null;
        }
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
        return st == null ? false : CastSpell((SpellType)st, aimDirection);
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

        Sprite.Play(animation);

    }

    public void OnDebugTextEvent(object sender, DebugTextUpdateArgs args)
    {
        var t = debugText.Text;
        debugText.Text = args.Action switch
        {
            TextAction.Append => t + "\n" + args.Text,
            TextAction.Remove => string.Join("\n", t.Split("\n").Where(x => x != args.Text)),
            TextAction.Set => args.Text,
            TextAction.Replace => string.Join("\n", t.Split("\n").Select((string s) => s == args.OldText ? args.Text : s)),
            _ => throw new NotImplementedException("unexpected text action"),
        };
    }

    public void _on_animated_sprite_2d_animation_finished()
    {
        if (Sprite.Animation == "cast")
        {
            HandleWalkAnimation(LastMoveDirection);
            casting = false;
        }
    }
}
