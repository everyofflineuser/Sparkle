using Raylib_CSharp;
using Raylib_CSharp.Materials;
using Raylib_CSharp.Shaders;

namespace Sparkle.CSharp.Effects.Types;

public class CrossHatchingEffect : Effect {
    
    public int HatchOffsetYLoc { get; private set; }
    public int LumThreshold01Loc { get; private set; }
    public int LumThreshold02Loc { get; private set; }
    public int LumThreshold03Loc { get; private set; }
    public int LumThreshold04Loc { get; private set; }

    public float HatchOffsetY;
    public float LumThreshold01;
    public float LumThreshold02;
    public float LumThreshold03;
    public float LumThreshold04;
    
    private float _oldHatchOffsetY;
    private float _oldLumThreshold01;
    private float _oldLumThreshold02;
    private float _oldLumThreshold03;
    private float _oldLumThreshold04;
    
    /// <summary>
    /// Constructor for creating a CrossHatchingEffect object.
    /// </summary>
    /// <param name="shader">The shader to be used by the cross hatching effect.</param>
    /// <param name="hatchOffsetY">The vertical offset for the hatching lines.</param>
    /// <param name="lumThreshold01">The first luminance threshold for hatching.</param>
    /// <param name="lumThreshold02">The second luminance threshold for hatching.</param>
    /// <param name="lumThreshold03">The third luminance threshold for hatching.</param>
    /// <param name="lumThreshold04">The fourth luminance threshold for hatching.</param>
    public CrossHatchingEffect(Shader shader, float hatchOffsetY = 5.0F, float lumThreshold01 = 0.9F, float lumThreshold02 = 0.7F, float lumThreshold03 = 0.5F, float lumThreshold04 = 0.3F) : base(shader) {
        this.HatchOffsetY = hatchOffsetY;
        this.LumThreshold01 = lumThreshold01;
        this.LumThreshold02 = lumThreshold02;
        this.LumThreshold03 = lumThreshold03;
        this.LumThreshold04 = lumThreshold04;
    }

    protected internal override void Init() {
        base.Init();
        this.SetLocations();
    }

    public override void Apply(Material? material = default) {
        base.Apply(material);

        if (RayMath.FloatEquals(this.HatchOffsetY, this._oldHatchOffsetY) != 1) {
            this.Shader.SetValue(this.HatchOffsetYLoc, this.HatchOffsetY, ShaderUniformDataType.Float);
            this._oldHatchOffsetY = this.HatchOffsetY;
        }
        
        if (RayMath.FloatEquals(this.LumThreshold01, this._oldLumThreshold01) != 1) {
            this.Shader.SetValue(this.LumThreshold01Loc, this.LumThreshold01, ShaderUniformDataType.Float);
            this._oldLumThreshold01 = this.LumThreshold01;
        }
        
        if (RayMath.FloatEquals(this.LumThreshold02, this._oldLumThreshold02) != 1) {
            this.Shader.SetValue(this.LumThreshold02Loc, this.LumThreshold02, ShaderUniformDataType.Float);
            this._oldLumThreshold02 = this.LumThreshold02;
        }
        
        if (RayMath.FloatEquals(this.LumThreshold03, this._oldLumThreshold03) != 1) {
            this.Shader.SetValue(this.LumThreshold03Loc, this.LumThreshold03, ShaderUniformDataType.Float);
            this._oldLumThreshold03 = this.LumThreshold03;
        }
        
        if (RayMath.FloatEquals(this.LumThreshold04, this._oldLumThreshold04) != 1) {
            this.Shader.SetValue(this.LumThreshold04Loc, this.LumThreshold04, ShaderUniformDataType.Float);
            this._oldLumThreshold04 = this.LumThreshold04;
        }
    }

    /// <summary>
    /// Sets the locations of shader parameters.
    /// </summary>
    private void SetLocations() {
        this.HatchOffsetYLoc = this.Shader.GetLocation("hatchOffsetY");
        this.LumThreshold01Loc = this.Shader.GetLocation("lumThreshold01");
        this.LumThreshold02Loc = this.Shader.GetLocation("lumThreshold02");
        this.LumThreshold03Loc = this.Shader.GetLocation("lumThreshold03");
        this.LumThreshold04Loc = this.Shader.GetLocation("lumThreshold04");
    }
}