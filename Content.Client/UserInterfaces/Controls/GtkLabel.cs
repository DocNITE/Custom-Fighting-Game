using System.Linq;
using System.Numerics;
using System.Text;
using Content.Client.Graphics.Fonts;
using Robust.Client.Animations;
using Robust.Shared.Animations;

namespace Content.Client.UserInterfaces.Controls;

// TODO Add shader support for fonts
// TODO Add normal Y Padding with font height
[Virtual]
public partial class GtkLabel : GtkWidget
{
    private readonly char _wrapIdentifier = ' ';
    private readonly float _xPadding = 1.0f;
    private readonly float _yPadding = 10.0f; // need rework
    
    public string Content = "";
    public string FontId = "kitchen-sink";
    public int FontScale = 1;

    public FontStyle FontStyle = FontStyle.Box;

    public override void Draw(GtkDrawingHandle handle)
    {
        base.Draw(handle);

        var data = new DrawingTextData(Content, FontId, FontStyle, _xPadding, handle.CurrentRenderScale);

        var contentList = new List<TextLine>();
        var contentSplit = Content.Split(_wrapIdentifier);

        var currentContent = "";
        foreach (var str in contentSplit)
        {
            currentContent += str;

            if ((data.GetSize(currentContent) * FontScale) <= Size.X)
            {
                contentList.Add(new TextLine(str, false));
            }
            else
            {
                contentList.Add(new TextLine(str, true));
                currentContent = str;
            }
        }
        
        // Finaly... Drawingum
        var xPos = 0.0f;
        var yPos = 0.0f;
        foreach (var line in contentList)
        {
            if (line.NextLine)
            {
                yPos += _yPadding * handle.CurrentRenderScale;
                xPos = 0.0f;
                var newPos = DrawText(handle, data, line, xPos, yPos);
                xPos = newPos.X;
                yPos = newPos.Y;
            }
            else
            {
                var newPos = DrawText(handle, data, line, xPos, yPos);
                xPos = newPos.X;
                yPos = newPos.Y;
            }
        }
    }

    public Vector2 DrawText(GtkDrawingHandle handle, DrawingTextData data, TextLine line, float xPos, float yPos)
    {
        var textures = data.GetTextures(line.Content + " ");
        foreach (var tex in textures)
        {
            if (tex == null)
            {
                yPos += _yPadding * handle.CurrentRenderScale;
                xPos = 0.0f;
                continue;
            }

            tex.Scale = new Vector2(FontScale, FontScale);
            tex.Position = new Vector2(xPos * FontScale, yPos * FontScale);
            DrawTexture(handle, tex);
            xPos += (tex.Size.X + _xPadding) * handle.CurrentRenderScale;
        }

        return new Vector2(xPos, yPos);
    }

    public struct TextLine
    {
        public bool NextLine { get; }
        public string Content { get; }

        public TextLine(string content, bool nextLine)
        {
            NextLine = nextLine;
            Content = content;
        }
    }
    
    public sealed class DrawingTextData
    {
        private float CharPadding { get; }
        private int RenderScale { get; }

        public IDictionary<char, FontTexture> Content { get; } = new Dictionary<char, FontTexture>();

        public DrawingTextData(string content, string fontId, FontStyle fontStyle, float charPadding = 1.0f, int renderScale = 1)
        {
            var bytes = Encoding.ASCII.GetBytes(content);
            for (var i = 0; i < bytes.Length; i++)
            {
                var byteItem = bytes[i];
                var tex = new FontTexture(fontId, byteItem, fontStyle);
                Content[content[i]] = tex;
            }

            CharPadding = charPadding;
            RenderScale = renderScale;
        }

        public bool GetLineBreaker(string content) =>
            content.Any(item => item == '\n');
        

        public float GetSize(string content) =>
            content.Where(item => item != '\n').Sum(item => (Content[item].Size.X + CharPadding) * RenderScale);


        public List<FontTexture?> GetTextures(string content) => 
            content.Select(item => item != '\n' ? Content[item] : null).ToList();

    }
}