using Godot;
using System;
namespace wizardgame.menu
{
    public partial class MainMenu : Control
    {
        public override void _Ready()
        {

        }


        public override void _Process(double delta)
        {
            //if (Input.IsActionJustPressed("ui_accept"))
            //{
            //    _on_play_button_pressed();
            //}
        }

        void _on_play_button_pressed()
        {
            // story mode?
            // GetTree().ChangeSceneToFile("res://scenes/Map.tscn");


        }
        void _on_spell_button_pressed()
        {
            GetTree().ChangeSceneToFile("res://scenes/Spellbook.tscn");
        }
        void _on_quit_button_pressed()
        {
            GetTree().Quit();
        }
    }
}
