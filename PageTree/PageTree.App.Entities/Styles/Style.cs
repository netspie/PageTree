using Common.Basic.DDD;
using Corelibs.Basic.Reflection;
using System.ComponentModel;

namespace PageTree.App.Entities.Styles
{
    public class Style : Entity
    {
        public string Name { get; init; } = new("");
        public StyleProperty Property { get; set; }

        public bool OverrideChildren { get; set; } = true;

        public TreeExpandInfo TreeExpandInfo { get; set; } = null;
    }

    public class StyleProperty
    {
        public List<StyleArtifact> Artifacts { get; } = new();
        public bool Visible { get; set; }
        public object Layout { get; set; }
        public object LayoutOfChildren { get; set; }

        public List<StyleProperty> Children { get; } = new();
    }

    public class StyleArtifact
    {
        public StyleArtifactType Type { get; private set; }
        public string ChildIndex { get; private set; }
        public VisualInfo VisualInfo { get; private set; }
    }

    public enum StyleArtifactType
    {
        [Description("Name")]
        Name,

        [Description("Signature")]
        Signature,

        [Description("Signature")]
        Child,
    }

    public static class StyleArtifactTypeExtensions
    {
        public static string AsString(this StyleArtifactType type) =>
            type.GetAttribute<DescriptionAttribute>()?.Description ?? "";
    }

    public class VisualInfo
    {
        public bool Visible { get; set; }
        public FontInfo Font { get ; set; }
        public ColorInfo FontColor { get; set; }
        public ColorInfo BackgroundColor { get; set; }

        public BorderGroupInfo Borders { get; set; }

        public bool DelimiterBeforeEnabled { get; set; }
        public bool DelimiterAfterEnabled { get; set; }
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
        public float Radius { get; set; }
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

    public enum DisplayType
    {
        None,
        Border,
        Background,
        Shadow
    }

    public enum LineType
    {
        Solid,
        Dashed,
        Dotted
    }

    // done rather from signature or page/property 
    public enum ApplyChildStyleBy
    {
        Index, // id?
        SignatureID,
        PropertyType
    }

    public enum DisplayType
    {
        Top,
        Right,
        Left
    }

    public enum TextTransform
    {
        None,
        Uppercase,
        Lowercase,
        Capitalize,
    }

    public enum TextDecoration
    {
        None,
        Underline,
        Overline,
        LineThrough,
    }

    public enum TextAlign
    {
        Left,
        Center,
        Right,
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
    {}
}