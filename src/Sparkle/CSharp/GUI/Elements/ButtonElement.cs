using System.Numerics;
using Raylib_cs;
using Sparkle.CSharp.GUI.Elements.Data;
using Sparkle.CSharp.Rendering.Helpers;
using Sparkle.CSharp.Windowing;

namespace Sparkle.CSharp.GUI.Elements; 

public class ButtonElement : GuiElement {
    
    public Texture2D? Texture;
    public float Rotation;
    public Color Color;
    public Color HoverColor;
    
    public Font Font;
    public string Text;
    public float FontSize;
    public Vector2 TextSize;
    public Vector2 ScaledTextSize;
    public int Spacing;
    public float TextRotation;
    public Color TextColor;
    public Color TextHoverColor;
    
    protected float CalcFontSize { get; private set; }
    
    /// <summary>
    /// Initializes a new button element with the specified parameters and associated button and label data.
    /// </summary>
    /// <param name="name">The name of the button element.</param>
    /// <param name="buttonData">Button-specific data including texture, rotation, and colors.</param>
    /// <param name="labelData">Label-specific data for text display on the button.</param>
    /// <param name="anchor">The anchor point for positioning the element.</param>
    /// <param name="offset">An optional offset for fine-tuning the position.</param>
    /// <param name="size">An optional size for the button element; if not provided, it's determined by the texture.</param>
    /// <param name="clickClickFunc">An optional function to handle click events.</param>
    public ButtonElement(string name, ButtonData buttonData, LabelData labelData, Anchor anchor, Vector2 offset, Vector2? size = default, Func<bool>? clickClickFunc = default) : base(name, anchor, offset, Vector2.Zero, clickClickFunc) {
        this.Texture = buttonData.Texture;
        this.Size = size ?? (this.Texture != null ? new Vector2(this.Texture.Value.Width, this.Texture.Value.Height) : Vector2.Zero);
        this.Rotation = buttonData.Rotation;
        this.Color = buttonData.Color;
        this.HoverColor = buttonData.HoverColor;
        
        this.Font = labelData.Font;
        this.Text = labelData.Text;
        this.FontSize = labelData.FontSize;
        this.TextSize = Vector2.Zero;
        this.ScaledTextSize = Vector2.Zero;
        this.Spacing = labelData.Spacing;
        this.TextRotation = labelData.Rotation;
        this.TextColor = labelData.Color;
        this.TextHoverColor = labelData.HoverColor;
    }

    protected override void CalculateSize() {
        base.CalculateSize();
        
        float scale = Window.GetRenderHeight() / (float) Game.Instance.Settings.Height;
        this.CalcFontSize = this.FontSize * scale * GuiManager.Scale;
        
        this.TextSize = FontHelper.MeasureText(this.Font, this.Text, this.FontSize, this.Spacing);
        this.ScaledTextSize = FontHelper.MeasureText(this.Font, this.Text, this.CalcFontSize, this.Spacing);
    }

    protected internal override void Draw() {
        Rectangle source = new Rectangle(0, 0, this.ScaledSize.X, this.ScaledSize.Y);
        Rectangle dest = new Rectangle(this.Position.X + (this.ScaledSize.X / 2), this.Position.Y + (this.ScaledSize.Y / 2), this.ScaledSize.X, this.ScaledSize.Y);
        Vector2 origin = new Vector2(dest.Width / 2, dest.Height / 2);
        
        Color color = this.IsHovered ? this.HoverColor : this.Color;
        Color textColor = this.IsHovered ? this.TextHoverColor : this.TextColor;
        
        if (this.Texture != null) {
            this.DrawTexture(this.Texture.Value, source, dest, origin, this.Rotation, color);
        }
        else {
            this.DrawRectangle(dest, origin, this.Rotation, color);
        }

        if (this.Text != string.Empty) {
            this.DrawText(this.Font, this.Text, this.TextRotation, this.CalcFontSize, this.Spacing, textColor);
        }
    }

    /// <summary>
    /// Draws a button with a textured background on the GUI.
    /// </summary>
    protected virtual void DrawTexture(Texture2D texture, Rectangle source, Rectangle dest, Vector2 origin, float rotation, Color color) {
        TextureHelper.DrawPro(texture, source, dest, origin, rotation, color);
    }

    /// <summary>
    /// Draws a color button on the screen.
    /// </summary>
    protected virtual void DrawRectangle(Rectangle dest, Vector2 origin, float rotation, Color color) {
        ShapeHelper.DrawRectangle(dest, origin, rotation, color);

        Rectangle rec = new Rectangle(dest.X - (dest.Width / 2), dest.Y - (dest.Height / 2), dest.Width, dest.Height);
        ShapeHelper.DrawRectangleLines(rec, 4, ColorHelper.Brightness(color, -0.5F));
    }

    /// <summary>
    /// Draws the text of the button element.
    /// </summary>
    protected virtual void DrawText(Font font, string text, float rotation, float fontSize, int spacing, Color color) {
        Vector2 pos = new Vector2(this.Position.X + (this.ScaledSize.X / 2), this.Position.Y + (this.ScaledSize.Y / 2));
        Vector2 origin = new Vector2(this.ScaledTextSize.X / 2, this.ScaledTextSize.Y / 2);
        FontHelper.DrawText(font, text, pos, origin, rotation, fontSize, spacing, color);
    }
}