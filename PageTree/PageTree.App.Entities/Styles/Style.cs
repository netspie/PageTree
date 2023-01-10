using Common.Basic.DDD;

namespace PageTree.App.Entities.Styles
{
    public class Style : Entity
    {
        public string Name { get; init; } = new("");
        public SignatureDisplayType SignatureDisplayType { get; set; }

        public VisualInfo VisualInfo { get; set; }
        public bool OverrideChildren { get; set; } = true;

        public List<ChildStyleInfo> ChildStyles { get; set; } = new();

        public TreeExpandInfo TreeExpandInfo { get; set; } = null;
    }

    public class VisualInfo
    {
        public FontInfo Font { get ; set; }
        public ColorInfo FontColor { get; set; }
        public ColorInfo BackgroundColor { get; set; }

        public BorderGroupInfo Borders { get; set; }

        // Delimiter
        public bool DelimiterBeforeEnabled { get; init; }
        public bool DelimiterAfterEnabled { get; init; }
    }

    public class FontInfo
    {
        public string FontID { get; init; }
        public string FontSize { get; init; }
        public string FontWeightType { get; init; }
    }

    public class ColorInfo
    {
        public string Color { get; init; }
        public string HoverColor { get; init; }
        public string EditColor { get; init; }
    }

    public class BorderGroupInfo
    {
        public LineInfo Top { get; set; }
        public LineInfo Bottom { get; set; }
        public LineInfo Right { get; set; }
        public LineInfo Left { get; set; }
    }

    public class LineInfo
    {
        public LineType Type { get; set; }
        public string Color { get; set; }
        public float Thickness { get; set; } = 1;
        public string EditColor { get; set; }
        public string EditThickness { get; set; }
    }

    public enum LineType
    {
        Solid,
        Dashed,
        Dotted
    }

    public enum ApplyChildStyleBy
    {
        Index,
        SignatureID,
        PropertyType
    }

    public enum SignatureDisplayType
    {
        Top,
        Right,
        Left
    }

    public enum SignatureVisibility
    {
        Visible,
        Hidden
    }

    public class ChildStyleInfo
    {
        public ApplyChildStyleBy StyleType { get; set; }
        //public PropertyStyle PropertyStyle { get; set; }
    }

    public enum LayoutType
    {
        Horizontal,
        Vertical,
        Grid
    }

    public class TreeExpandInfo
    { }
}