using System;
using System.Drawing;

public partial class FogTexture : Godot.TextureRect
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GenerateFogTexture();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }


    public void GenerateFogTexture()
    {
        int w = 1000, h = 1000;

        Bitmap bitmap = new(w, h);
        using (Graphics g = Graphics.FromImage(bitmap))
        {
            g.Clear(Color.Black);
        }



    }

}
