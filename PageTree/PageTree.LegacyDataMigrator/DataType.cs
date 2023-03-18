namespace PageTree.LegacyDataMigrator;

[Flags]
public enum DataType : byte
{
    None,
    Page,
    Signature,
    PageTemplate,
    PracticeCategory,
    PracticeTactic,
    Query
}
