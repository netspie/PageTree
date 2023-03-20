using Common.Basic.Collections;
using Common.Basic.DDD;
using Corelibs.Basic.Reflection;
using System.ComponentModel;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace PageTree.App.Entities.Styles
{
    public class Style : Entity
    {
        /// <summary>
        /// Name of the style.
        /// </summary>
        public string Name { get; set; } = new("");

        /// <summary>
        /// Describes styles for root and children properties.
        /// </summary>
        public StyleOfRootProperty RootProperty { get; set; }

        /// <summary>
        /// Describes an expand state of a page.
        /// </summary>
        public ExpandInfo TreeExpandInfo { get; set; } = new();

        public List<string> SignatureIDs { get; private set; } = new();

        public Style Override(params Style[] other)
        {
            var n = Override(new Style());
            foreach (var o in other)
            {
                if (!o)
                    continue;

                n = n.Override(o);
            }

            return n;
        }

        public Style Override(Style other)
        {
            var n = new Style();

            n.Name = $"{Name} - {other.Name}";
            n.RootProperty = RootProperty.Override(other.RootProperty);

            return n;
        }
    }

    public class ExpandInfo
    {
        public string ID { get; set; } = "";
        public string ParentID { get; set; } = "";
        public bool CanExpand { get; set; } = true;
        public bool HasChildren { get; set; }
        public bool IsExpanded { get; set; }
        public bool AreChildrenExpanded { get; set; }
        public List<ExpandInfo> Children { get; set; } = new();
    }

    public class StyleOfRootProperty
    {
        #region This

        /// <summary>
        /// Describes common style for all of the artifacts of this property
        /// or all of the children artifacts if none overriden.
        /// </summary>
        public VisualInfo VisualInfo { get; set; } = new();

        /// <summary>
        /// Describes which artifacts should be displayed in the property and their styles.
        /// It can be a name, signature or metadata.
        /// </summary>
        public List<StyleOfArtifact> Artifacts { get; set; } = new();

        /// <summary>
        /// Describes which layout should be used to display the artifacts.
        /// </summary>
        public Layout Layout { get; set; }

        /// <summary>
        /// Tells whether the style should override custom defined styles.
        /// </summary>
        public bool? ShouldOverride { get; set; }

        #endregion

        #region Children

        public SignatureFilter SignatureFilter { get; set; }

        /// <summary>
        /// Describes common style for all of this property children.
        /// </summary>
        public VisualInfo VisualInfoOfChildren { get; set; } = new();

        /// <summary>
        /// Describes common style for all of this property direct children artifacts.
        /// </summary>
        public List<StyleOfArtifact> ChildrenStyle { get; set; } = new();

        /// <summary>
        /// Describes custom styles for a children properties of specific type or index.
        /// </summary>
        public List<StyleOfChildProperty> Children { get; set; } = new();

        /// <summary>
        /// Informs which layout should be used to display the children properties.
        /// </summary>
        public Layout LayoutOfChildren { get; set; }

        /// <summary>
        /// Tells whether the style should override custom defined styles of children.
        /// </summary>
        public bool? ShouldOverrideChildren { get; set; }

        #endregion

        public StyleOfRootProperty Override(StyleOfRootProperty other)
        {
            var n = new StyleOfRootProperty();

            n.VisualInfo = VisualInfo.Override(other.VisualInfo);
            n.Layout = Layout.Override(other.Layout);
            n.Artifacts = Artifacts
                .Zip(other.Artifacts, (t, o) => t.Type == o.Type)
                .Select(z => z.Item2.IsNullOrEmpty() ? z.Item1 : z.Item1.Override(z.Item2.First()))
                .ToList();

            n.VisualInfoOfChildren = VisualInfoOfChildren.Override(other.VisualInfoOfChildren);
            n.LayoutOfChildren = LayoutOfChildren.Override(other.LayoutOfChildren);
            n.ChildrenStyle = ChildrenStyle
                .Zip(other.ChildrenStyle, (t, o) => t.Type == o.Type)
                .Select(z => z.Item2.IsNullOrEmpty() ? z.Item1 : z.Item1.Override(z.Item2.First()))
                .ToList();

            n.Children = Children
                .Zip(other.Children, (t, o) => 
                    (t.StyleType == ApplyStyleBy.Index && t.ChildIndex == o.ChildIndex) || 
                    (t.StyleType == ApplyStyleBy.PropertyType && t.PropertyType == o.PropertyType))
                .Select(z => z.Item2.IsNullOrEmpty() ? z.Item1 : z.Item1.Override(z.Item2.First()))
                .ToList();

            return n;
        }
    }

    /// <summary>
    /// Describes which elements of same signature should be presented under the same filter in a view.
    /// </summary>
    public class SignatureFilter
    {
        /// <summary>
        /// Parent signature or page id.
        /// </summary>
        public string ParentID { get; set; } = "";

        /// <summary>
        /// Ids of signature of pages which will be presented under common signature filter.
        /// </summary>
        public List<string> SignatureIDs { get; private set; } = new();
    }

    public class Layout
    {
        public LayoutType Type { get; set; }
        public float? Gap { get; set; }

        public Size ChildWidth { get; set; }
        public Size ChildHeight { get; set; }

        public List<int> ColumnCounts { get; set; }

        public Layout Override(Layout other)
        {
            var n = new Layout();

            n.Type = Type;
            n.Gap = other.Gap ?? Gap;

            n.ChildWidth = other.ChildWidth ?? ChildWidth;
            n.ChildHeight = other.ChildHeight ?? ChildHeight;
            n.ColumnCounts = other.ColumnCounts ?? ColumnCounts;

            return n;
        }
    }

    public enum LayoutType
    {
        Vertical,
        Horizontal,
        Grid
    }

    public class StyleOfChildProperty : StyleOfRootProperty
    {
        /// <summary>
        /// Defines method by which property or group of properties should be destined to apply this style.
        /// </summary>
        public ApplyStyleBy StyleType { get; set; }

        /// <summary>
        /// Defines an index of the property that should apply this style.
        /// Applies only if StyleType is set to ApplyStyleBy.Index.
        /// </summary>
        public int? ChildIndex { get; set; }

        /// <summary>
        /// Defines a type of a property that should apply this style.
        /// Applies only if StyleType is set to ApplyStyleBy.PropertyType.
        /// </summary>
        public PropertyType? PropertyType { get; set; } // StyleArtifactType.PropertyType only (or should be children)

        public StyleOfChildProperty Override(StyleOfChildProperty other)
        {
            var n = new StyleOfChildProperty();

            n.VisualInfo = VisualInfo.Override(other.VisualInfo);
            n.Layout = Layout.Override(other.Layout);
            n.Artifacts = Artifacts
                .Zip(other.Artifacts, (t, o) => t.Type == o.Type)
                .Select(z => z.Item2.IsNullOrEmpty() ? z.Item1 : z.Item1.Override(z.Item2.First()))
                .ToList();

            n.VisualInfoOfChildren = VisualInfoOfChildren.Override(other.VisualInfoOfChildren);
            n.LayoutOfChildren = LayoutOfChildren.Override(other.LayoutOfChildren);
            n.ChildrenStyle = ChildrenStyle
                .Zip(other.ChildrenStyle, (t, o) => t.Type == o.Type)
                .Select(z => z.Item2.IsNullOrEmpty() ? z.Item1 : z.Item1.Override(z.Item2.First()))
                .ToList();

            n.Children = Children
                .Zip(other.Children, (t, o) =>
                    (t.StyleType == ApplyStyleBy.Index && t.ChildIndex == o.ChildIndex) ||
                    (t.StyleType == ApplyStyleBy.PropertyType && t.PropertyType == o.PropertyType))
                .Select(z => z.Item2.IsNullOrEmpty() ? z.Item1 : z.Item1.Override(z.Item2.First()))
                .ToList();

            n.StyleType = other.StyleType;
            n.ChildIndex = other.ChildIndex ?? ChildIndex;
            n.PropertyType = other.PropertyType ?? PropertyType;

            return n;
        }

    }

    public class StyleOfArtifact
    {
        /// <summary>
        /// Defines a type of the artifact.
        /// </summary>
        public StyleArtifactType Type { get; set; }

        /// <summary>
        /// Describes a style for this artifact only.
        /// </summary>
        public VisualInfo VisualInfo { get; set; }

        /// <summary>
        /// Describes which elements should be presented as a name of a property.
        /// Applies only if Type is set to StyleArtifactType.Name.
        /// </summary>
        public List<ContentElement> NameDisplay { get; private set; } = new();

        public StyleOfArtifact Override(StyleOfArtifact other)
        {
            var res = new StyleOfArtifact();

            res.Type = other.Type;
            res.VisualInfo = VisualInfo.Override(other.VisualInfo);
            res.NameDisplay = NameDisplay ?? new(other.NameDisplay);

            return res;
        }
    }

    public class ContentElement
    {
        public ContentElementType Type { get; set; }

        /// <summary>
        /// Applies only if Type is set to StyleArtifactType.ChildNameOfIndex.
        /// </summary>
        public string ChildIndexID { get; set; }

        /// <summary>
        /// Applies only if Type is set to StyleArtifactType.FirstChildNameOfSignature or  StyleArtifactType.AllChildrenNamesOfSignature.
        /// </summary>
        public string ChildSignatureID { get; set; }

        /// <summary>
        /// Applies only if Type is set to StyleArtifactType.Delimiter.
        /// </summary>
        public string Delimiter { get; set; }
    }

    public enum ContentElementType
    {
        Name,

        Delimiter,

        Children,
        ChildNameOfIndex,
        FirstChildNameOfSignature,
        AllChildrenNamesOfSignature,

        ChildCount
    }

    public enum PropertyType
    {
        Subpage,
        Link,
        Query,
        Metadata
    }

    public enum StyleArtifactType
    {
        [Description("Name")]
        Name,

        [Description("Signature")]
        Signature,

        [Description("Metadata")]
        Metadata,
    }

    public static class StyleArtifactTypeExtensions
    {
        public static string AsString(this StyleArtifactType type) =>
            type.GetAttribute<DescriptionAttribute>()?.Description ?? "";
    }

    public class VisualInfo
    {
        public Visibility? Visibility { get; set; }

        public Size Width { get; set; }

        public TextInfo Text { get; set; }

        public FontInfo Font { get ; set; }
        public ColorInfo FontColor { get; set; }
        public ColorInfo BackgroundColor { get; set; }

        public RectArea Margin { get; set; }
        public RectArea Padding { get; set; }

        public Rect Outline { get; set; }
        public Rect Borders { get; set; }

        public Line DelimiterBefore { get; set; }
        public Line DelimiterAfter { get; set; }

        public VisualInfo Override(VisualInfo other)
        {
            var res = new VisualInfo();

            res.Visibility = other.Visibility ?? Visibility;
            res.Width = Width.Override(other.Width);

            res.Text = Text.Override(other.Text);
            res.Font = Font.Override(other.Font);
            res.FontColor = FontColor.Override(other.FontColor);
            res.BackgroundColor = BackgroundColor.Override(other.BackgroundColor);

            res.Margin = Margin.Override(other.Margin);
            res.Padding = Margin.Override(other.Padding);

            res.Outline = Outline.Override(other.Outline);
            res.Borders = Borders.Override(other.Borders);

            res.DelimiterBefore = DelimiterBefore.Override(other.DelimiterBefore);
            res.DelimiterAfter = DelimiterAfter.Override(other.DelimiterAfter);

            return res;
        }
    }

    public class TextInfo
    {
        public TextAlign? TextAlign { get; set; }
        public TextIndent? TextIndent { get; set; }
        public TextWrap? TextWrap { get; set; }
        public TextTransform? TextTransform { get; set; }
        public List<TextDecoration> TextDecorations { get; set; }

        public TextInfo Override(TextInfo other)
        {
            var res = new TextInfo();

            res.TextAlign = other.TextAlign ?? TextAlign;
            res.TextIndent = other.TextIndent ?? TextIndent;
            res.TextWrap = other.TextWrap ?? TextWrap;
            res.TextTransform = other.TextTransform ?? TextTransform;
            res.TextDecorations = other.TextDecorations ?? new(TextDecorations);

            return res;
        } 
    }

    public class FontInfo
    {
        public string Font { get; set; }
        public float? FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }

        public FontInfo Override(FontInfo other)
        {
            var res = new FontInfo();

            res.Font = other.Font ?? Font;
            res.FontSize = other.FontSize ?? FontSize;
            res.FontWeight = other.FontWeight ?? FontWeight;

            return res;
        }
    }

    public enum Visibility
    {
        Always,
        Never,
        IfHasChildren
    }

    public enum FontWeight
    {
        Thin = 100,
        ExtraLight = 200,
        Light = 300,

        Normal = 400,

        Medium = 500,
        SemiBold = 600,
        Bold = 700,
        ExtraBold = 800
    }

    public class ColorInfo
    {
        public int? Default { get; set; }
        public int? Hover { get; set; }
        public int? Edit { get; set; }

        public ColorInfo Override(ColorInfo other)
        {
            var res = new ColorInfo();

            res.Default = other.Default ?? Default;
            res.Hover = other.Hover ?? Hover;
            res.Edit = other.Edit ?? Edit;

            return res;
        }
    }

    public class Rect
    {
        public float? Radius { get; set; }
        public Line Top { get; set; }
        public Line Bottom { get; set; }
        public Line Right { get; set; }
        public Line Left { get; set; }

        public Rect Override(Rect other)
        {
            var res = new Rect();

            res.Radius = other.Radius ?? Radius;
            res.Top = Top.Override(other.Top);
            res.Bottom = Bottom.Override(other.Bottom);
            res.Right = Right.Override(other.Right);
            res.Left = Left.Override(other.Left);

            return res;
        }
    }

    public class RectArea
    {
        public float? All { get; set; }
        public float? Top { get; set; }
        public float? Bottom { get; set; }
        public float? Right { get; set; }
        public float? Left { get; set; }

        public RectArea Override(RectArea other)
        {
            var res = new RectArea();

            res.All = other.All ?? All;
            res.Top = other.Top ?? Top;
            res.Bottom = other.Bottom ?? Bottom;
            res.Right = other.Right ?? Right;
            res.Left = other.Left ?? Left;

            return res;
        }
    }

    public class Line
    {
        public LineType? Type { get; set; }
        public ColorInfo Color { get; set; }
        public float? Thickness { get; set; }

        public Line Override(Line other)
        {
            var res = new Line();

            res.Type = other.Type ?? Type;
            res.Color = Color.Override(other.Color);
            res.Thickness = other.Thickness ?? Thickness;

            return res;
        }
    }

    public class Size
    {
        public SizeType? Type { get; set; }
        public float? Value { get; set; }

        public Size Override(Size other)
        {
            var res = new Size();

            res.Type = other.Type ?? Type;
            res.Value = other.Value ?? Value;

            return res;
        }
    }

    public enum SizeType
    {
        Parent,
        Content,
        Value
    }

    public enum TextIndent {}

    public enum LineType
    {
        Solid,
        Dashed,
        Dotted
    }

    public enum ApplyStyleBy
    {
        Index,
        PropertyType
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

    public enum TextWrap
    {
        Wrap,
        NoWrap,
        Right,
    }
}
