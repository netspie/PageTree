using Common.Basic.Collections;
using Common.Basic.Functional;
using PageTree.App.Pages.Queries;

namespace PageTree.Client.Shared.Views.Page
{
    public static class PropertyExtensions
    {
        public static Property.IdentityVM ToPropertyIdentityVM(this PageTree.App.Pages.Queries.IdentityVM vm) =>
            new() { ID = vm.ID, Name = vm.Name };

        public static Property.ArtifactVM ToNameArtifactVM(this PropertyVM vm, Property.VisualInfoVM visualInfoVM = null) =>
            new()
            {
                Type = Property.ArtifactType.Name,
                Identity = vm.Identity.ToPropertyIdentityVM(),
                VisualInfo = visualInfoVM ?? new()
            };

        public static Property.ArtifactVM ToSignatureArtifactVM(this PropertyVM vm, Property.VisualInfoVM visualInfoVM = null) =>
            new() 
            { 
                Type = Property.ArtifactType.Signature, 
                Identity = vm.SignatureIdentity.ToPropertyIdentityVM(), 
                VisualInfo = visualInfoVM ?? new()
            };

        public static Property.ArtifactVM ToArtifactVM(this PropertyVM vm, PageTree.App.Entities.Styles.StyleArtifactType type, Property.VisualInfoVM visualInfoVM = null) =>
           type switch
           {
               PageTree.App.Entities.Styles.StyleArtifactType.Name => vm.ToNameArtifactVM(visualInfoVM),
               PageTree.App.Entities.Styles.StyleArtifactType.Signature => vm.ToSignatureArtifactVM(visualInfoVM),
               _ => throw new NotImplementedException()
           };
            
        public static Property.VisualInfoVM OverrideBy(this Property.VisualInfoVM vmStyle, PageTree.App.Entities.Styles.VisualInfo style)
        {
            if (vmStyle == null || style == null)
                return vmStyle;

            vmStyle.Font.OverrideBy(style.Font);

            return vmStyle;
        }

        public static Property.FontVM OverrideBy(this Property.FontVM vmFont, PageTree.App.Entities.Styles.FontInfo font)
        {
            if (vmFont == null || font == null)
                return vmFont;

            vmFont.Font = font.Font.IsNullOrEmpty() ? vmFont.Font : font.Font;
            vmFont.FontSize = font.FontSize ?? vmFont.FontSize;
            vmFont.FontWeight = font.FontWeight.IsNull() ? vmFont.FontWeight : (Property.FontWeightType) font.FontWeight;

            return vmFont;
        }
    }
}
