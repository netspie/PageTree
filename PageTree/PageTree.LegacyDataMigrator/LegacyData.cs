extern alias CommonLegacy;
extern alias PracticerLegacy;

using Page = PracticerLegacy::Practicer.Domain.Pages.Content.Page;
using Signature = PracticerLegacy::Practicer.Domain.Templates.TemplateSignature;

namespace PageTree.LegacyDataMigrator;

public record LegacyData(
    Page[] Pages = null, 
    Signature[] Signatures = null);