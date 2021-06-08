using System;
using System.Drawing;
using System.Windows.Forms;

public class Block : Button
{
    public Color Color { get; set; }
    public Figure Figure { get; set; }
    public bool Disabled { get; set; }
    public Block() : base()
    {
        Figure = new Figure();
    }
    public void AddFigure(Figure figure)
    {
        this.Figure = figure;

        ChangeBackgroundImage();
    }
    public void ClearFigure()
    {
        this.BackgroundImage = null; // neka si sedi slikata vo slucaj da ne treba da se smeni, samo slikata na kopceto (blokot) se menja
    }
    private void ChangeBackgroundImage()
    {
        this.BackgroundImage = Figure.Icon;
    }
    public void Available()
    {
        this.BackColor = Color.FromArgb(252, 249, 120); // light yellow
    }
}
