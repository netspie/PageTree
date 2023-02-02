using Common.Basic.DDD;
using Corelibs.Basic.Reflection;
using System.ComponentModel;

namespace PageTree.App.Entities.Styles
{
    public class Style : Entity
    {
        /// <summary>
        /// Name of the style.
        /// </summary>
        public string Name { get; init; } = new("");

        /// <summary>
        /// Describes styles for root and children properties.
        /// </summary>
        public StyleOfRootProperty RootProperty { get; set; }

        /// <summary>
        /// Describes an expand state of a page.
        /// </summary>
        public ExpandInfo TreeExpandInfo { get; set; } = new();
    }

    public class ExpandInfo
    {
        public string ID { get; set; } = "";
        public string ParentID { get; set; } = "";
        public string PageID { get; set; } = "";
        public bool HasChildren { get; set; }
        public bool CanExpand { get; set; } = true;
        public bool IsExpanded { get; set; }
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
        public LayoutType Layout { get; set; }

        #endregion

        #region Children

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
        public LayoutType LayoutOfChildren { get; set; }

        #endregion
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
        public int ChildIndex { get; set; }

        /// <summary>
        /// Defines a type of a property that should apply this style.
        /// Applies only if StyleType is set to ApplyStyleBy.PropertyType.
        /// </summary>
        public PropertyType PropertyType { get; set; } // StyleArtifactType.PropertyType only (or should be children)

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
        public List<ContentElement> NameDisplay { get; } = new();
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
        public string ChildSignatureID { get; set; } // only ContentElementType.ChildNameOfSignature

        /// <summary>
        /// Applies only if Type is set to StyleArtifactType.Delimiter.
        /// </summary>
        public string Delimiter { get; set; } // only ContentElementType.Delimiter
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
        public bool Visible { get; set; } = true;
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

    public enum PropertyDisplayType
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
    public enum ApplyStyleBy
    {
        Index, // id?
        PageID,
        SignatureID,
        PropertyType
    }

    //public enum DisplayType
    //{
    //    Top,
    //    Right,
    //    Left
    //}

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
        public ApplyStyleBy StyleType { get; set; }
        //public PropertyStyle PropertyStyle { get; set; }
    }

    public enum LayoutType
    {
        Vertical,
        Horizontal,
        Grid
    }
}
