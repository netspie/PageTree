namespace PageTree.App.UseCases.Styles
{
    public class UpdatePropertyStyleCommand
    {
        public string PropertyStyleID { get; init; }

        public string DefaultPropertyStyleID { get; init; }


        // ---------- NAME-----------

        // Name Font 
        public string NameFontID { get; init; }
        public string NameFontSize { get; init; }
        public string NameFontWeightType { get; init; }

        // Name Font Color
        public string NameFontColor { get; init; }
        public string NameFontHoverColor { get; init; }
        public string NameFontEditColor { get; init; }

        // Name Background Color
        public string NameBackgroundColor { get; init; }
        public string NameBackgroundHoverColor { get; init; }
        public string NameBackgroundEditColor { get; init; }

        // Name Background Color
        public bool NameDelimiterBeforeEnabled { get; init; }
        public bool NameDelimiterAfterEnabled { get; init; }
    }
}
