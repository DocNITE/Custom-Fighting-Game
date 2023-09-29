using System.Linq;
using System.Numerics;
using System.Text;
using Content.Client.Graphics.Fonts;
using Robust.Client.Animations;
using Robust.Shared.Animations;

namespace Content.Client.UserInterfaces.Controls;

// TODO Add shader support for fonts
// TODO Add normal Y Padding with font height
// TODO PRIORITY Make FontTexture is static data for GtkLabel. We should update it only when set a new text
[Virtual]
public partial class GtkLabel : GtkWidget
{
    private readonly char _wrapIdentifier = ' ';
    private float _xPadding = 1.0f;
    private float _yPadding = 10.0f; // need rework

    private string _content = "";

    public float XPadding
    {
        get => _xPadding;
        set => _xPadding = value;
    }

    public float YPadding
    {
        get => _yPadding;
        set => _yPadding = value;
    }

    public string Content
    {
        get => _content;
        set
        {
            _content = value;
        }
    }

    public string FontId { get; set; } = "kitchen-sink";
    public float FontScale { get; set; } = 1.0f;

    public Color Color { get; set; } = Robust.Shared.Maths.Color.White;
    public FontStyle FontStyle = FontStyle.Box;

    public override bool OnDraw(GtkDrawingHandle handle)
    {
        if (!base.OnDraw(handle))
            return false;

        var data = new DrawingTextData(Content, FontId, FontStyle, _xPadding, handle.CurrentRenderScale);

        var contentList = new List<TextLine>();
        var contentSplit = _content.Split(_wrapIdentifier);

        var currentContent = "";
        foreach (var str in contentSplit)
        {
            currentContent += str + _wrapIdentifier;

            if ((data.GetSize(currentContent) * FontScale) <= Size.X && !data.GetLineBreaker(str + _wrapIdentifier))
            {
                contentList.Add(new TextLine(str + _wrapIdentifier, false));
            }
            else
            {
                contentList.Add(new TextLine(str + _wrapIdentifier, true));
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

        return true;
    }

    public Vector2 DrawText(GtkDrawingHandle handle, DrawingTextData data, TextLine line, float xPos, float yPos)
    {
        var textures = data.GetTextures(line.Content);
        foreach (var tex in textures)
        {
            if (tex == null)
            {
                //yPos += _yPadding * handle.CurrentRenderScale;
                //xPos = 0.0f;
                continue;
            }

            tex.Scale = new Vector2(FontScale, FontScale);
            tex.Position = new Vector2(xPos * FontScale, yPos * FontScale);
            DrawTexture(handle, tex, Color);
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
