namespace Sparkle.csharp.content.type; 

public class TextureContent : IContentType {
    
    public string Path;
    
    public bool AllowDuplicates { get; }
    
    /// <summary>
    /// Initializes a new instance of the TextureContent class with the specified texture image file path and disallows duplicate items.
    /// </summary>
    /// <param name="path">The path to the texture image file.</param>
    public TextureContent(string path) {
        this.Path = path;
        this.AllowDuplicates = false;
    }
}