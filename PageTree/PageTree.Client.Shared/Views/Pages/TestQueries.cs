using PageTree.App.Entities.Styles;
using PageTree.App.Pages.Queries;
using PageTree.App.UseCases.Common;
using System.Drawing;

namespace PageTree.Client.Shared.Views.Pages
{
    public static class TestQueries
    {
        public static GetPageQueryOut Query = new(
            new()
            {
                Identity = new() { ID = "Japanese-Language-ID", Name = "Japanese Language" },
                Path = new IdentityVM[] { new() { ID = "Project-ID", Name = "Project" } },
                Properties = new PropertyVM[]
                {
                    new()
                    {
                        Identity = new() { ID = "Authors-ID", Name = "Authors" },
                        SignatureIdentity = new() { ID = "Authors-Signature-ID", Name = "Authors" },
                        Properties = new PropertyVM[]
                        {
                            new()
                            {
                                IsExpanded = false,
                                Identity = new() { ID = "Misa-Ammo-1-ID", Name = "Misa Ammo" },
                                SignatureIdentity = new() { ID = "Author-Signature-ID", Name = "Author" },
                                Properties = new PropertyVM[]
                                {
                                    new()
                                    {
                                        Identity = new() { ID = "An-ID", Name = "Lessons" },
                                        SignatureIdentity = new() { ID = "Lessons-Signature-ID", Name = "Lessons" },
                                        Properties = new PropertyVM[]
                                        {
                                            new()
                                            {
                                                Identity = new() { ID = "An-ID", Name = "Lesson 1" },
                                                SignatureIdentity = new() { ID = "Lesson-Signature-ID", Name = "Lesson" },
                                                Properties = new PropertyVM[]
                                                {
                                                    new()
                                                    {
                                                        Identity = new() { Name = "長いレッスンになる。" },
                                                        SignatureIdentity = new() { Name = "Phrase" },
                                                        Properties = new PropertyVM[]
                                                        {
                                                            new()
                                                            {
                                                                Identity = new() { Name = "Meanings" },
                                                                SignatureIdentity = new() { Name = "Meanings" },
                                                                Properties = new PropertyVM[]
                                                                {
                                                                    new()
                                                                    {
                                                                        Identity = new() { Name = "It's going to be a long lesson." },
                                                                        SignatureIdentity = new() { Name = "Meaning Value" },
                                                                    }
                                                                }
                                                            },
                                                            new()
                                                            {
                                                                Identity = new() { Name = "Hiragana Writing" },
                                                                SignatureIdentity = new() { Name = "Hiragana Writing" },
                                                            },
                                                            new()
                                                            {
                                                                Identity = new() { Name = "Words" },
                                                                SignatureIdentity = new() { Name = "Words" },
                                                                Properties = new PropertyVM[]
                                                                {
                                                                    new()
                                                                    {
                                                                        Identity = new() { Name = "長い" },
                                                                        SignatureIdentity = new() { Name = "Word" },
                                                                        Properties = new PropertyVM[]
                                                                        {
                                                                            new()
                                                                            {
                                                                                Identity = new() { Name = "long" },
                                                                                SignatureIdentity = new() { Name = "Meaning Value" },
                                                                            }
                                                                        }
                                                                    },
                                                                    new()
                                                                    {
                                                                        Identity = new() { Name = "レッスン" },
                                                                        SignatureIdentity = new() { Name = "Word" },
                                                                        Properties = new PropertyVM[]
                                                                        {
                                                                            new()
                                                                            {
                                                                                Identity = new() { Name = "lesson" },
                                                                                SignatureIdentity = new() { Name = "Meaning Value" },
                                                                            }
                                                                        }
                                                                    },
                                                                    new()
                                                                    {
                                                                        Identity = new() { Name = "に" },
                                                                        SignatureIdentity = new() { Name = "Word" },
                                                                        Properties = new PropertyVM[]
                                                                        {
                                                                            new()
                                                                            {
                                                                                Identity = new() { Name = "Object of a verb" },
                                                                                SignatureIdentity = new() { Name = "Meaning Value" },
                                                                            }
                                                                        }
                                                                    },
                                                                }
                                                            },
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            new()
                            {
                                IsExpanded = false,
                                Identity = new() { ID = "Yuta-Aoki-1-ID", Name = "Yuta Aoki" },
                                SignatureIdentity = new() { ID = "Author-Signature-ID", Name = "Author" },
                            },
                            new()
                            {
                                IsExpanded = false,
                                Identity = new() { ID = "Masa-Sensei-1-ID", Name = "Masa Sensei" },
                                SignatureIdentity = new() { ID = "Author-Signature-ID", Name = "Author" },
                            }
                        }
                    },
                    new()
                    {
                        Identity = new() { Name = "Scripts" },
                        Properties = new PropertyVM[]
                        {
                            new()
                            {
                                Identity = new() { Name = "Kana Scripts" },
                                Properties = new PropertyVM[]
                                {
                                    new()
                                    {
                                        Identity = new() { Name = "Hiragana" },
                                    },
                                    new()
                                    {
                                        Identity = new() { Name = "Katakana" },
                                    }
                                }
                            },
                            new()
                            {
                                Identity = new() { Name = "Kanji Script" },
                                Properties = new PropertyVM[]
                                {
                                    new()
                                    {
                                        Identity = new() { Name = "Radicals" },
                                    },
                                    new()
                                    {
                                        Identity = new() { Name = "Kanjis" },
                                    }
                                }
                            }
                        }
                    },
                    new()
                    {
                        Identity = new() { ID = "Words-ID", Name = "Words" },
                        SignatureIdentity = new() { ID = "Words-Signature-ID", Name = "Words" },
                        Properties = new PropertyVM[]
                        {
                            new()
                            {
                                Identity = new() { ID = "Particles-1-ID", Name = "Particles" },
                                SignatureIdentity = new() { ID = "Part-Of-Speech-ID", Name = "Part Of Speech" },
                            },
                            new()
                            {
                                Identity = new() { ID = "Pronouns-1-ID", Name = "Pronouns" },
                                SignatureIdentity = new() { ID = "Part-Of-Speech-ID", Name = "Part Of Speech" },
                            },
                            new()
                            {
                                Identity = new() { ID = "Nouns-1-ID", Name = "Nouns" },
                                SignatureIdentity = new() { ID = "Part-Of-Speech-ID", Name = "Part Of Speech" },
                            },
                            new()
                            {
                                Identity = new() { ID = "Verbs-1-ID", Name = "Verbs" },
                                SignatureIdentity = new() { ID = "Part-Of-Speech-ID", Name = "Part Of Speech" },
                            }
                        }
                    },
                    new()
                    {
                        Identity = new() { Name = "Similar Sounding Words" },
                        SignatureIdentity = new() { Name = "Similar Sounding Words" },
                    },
                    new()
                    {
                        Identity = new() { Name = "Subcultures" },
                        SignatureIdentity = new() { Name = "Subcultures" },
                    },
                    new()
                    {
                        Identity = new() { Name = "Formality Categorizations" },
                        SignatureIdentity = new() { Name = "Formality Categorizations" },
                    },
                    new()
                    {
                        Identity = new() { Name = "Regional Dialects" },
                        SignatureIdentity = new() { Name = "Regional Dialects" },
                    },
                    new()
                    {
                        Identity = new() { Name = "Age Jargons" },
                        SignatureIdentity = new() { Name = "Age Jargons" },
                    },
                    new()
                    {
                        Identity = new() { Name = "Genders" },
                        SignatureIdentity = new() { Name = "Genders" },
                    }
                },

                StyleOfPage = new()
                {
                    RootProperty = new()
                    {
                        Layout = new()
                        {
                            Gap = 20
                        },
                        VisualInfoOfChildren = new()
                        {
                            BackgroundColor = new()
                            {
                                Default = Color.FromArgb(255, 236, 236, 236).ToArgb()
                            },
                            Padding = new()
                            {
                                All = 20
                            }
                        },
                        ChildrenStyle = new()
                        {
                            new()
                            {
                                Type = StyleArtifactType.Name,
                                VisualInfo = new()
                                {
                                    Font = new()
                                    {
                                        FontSize = 18,
                                        FontWeight = FontWeight.Bold
                                    }
                                }
                            }
                        },
                        Children = new()
                        {
                            //new()
                            //{
                            //    StyleType = ApplyStyleBy.Index,
                            //    ChildIndex = 0,
                            //    VisualInfo = new()
                            //    {
                            //        Font = new()
                            //        {
                            //            FontSize = 30
                            //        }
                            //    },
                            //    Artifacts = new()
                            //    {
                            //        new()
                            //        {
                            //            Type = StyleArtifactType.Signature,
                            //            VisualInfo = new()
                            //            {
                            //                Font = new()
                            //                {
                            //                    FontSize = 8
                            //                }
                            //            }
                            //        }
                            //    },
                            //    VisualInfoOfChildren = new()
                            //    {
                            //        BackgroundColor = new()
                            //        {
                            //            Default = Color.FromArgb(255, 222, 222, 222).ToArgb()
                            //        },
                            //        Padding = new()
                            //        {
                            //            All = 20
                            //        }
                            //    },
                            //}
                        }
                    }
                }
            });
    }
}
