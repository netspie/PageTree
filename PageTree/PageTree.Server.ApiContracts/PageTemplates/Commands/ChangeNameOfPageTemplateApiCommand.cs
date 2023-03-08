﻿using Corelibs.Basic.Net;
using PageTree.Domain;

namespace PageTree.Server.ApiContracts
{
    public class ChangeNameOfPageTemplateApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(Page))]
        public string PageTemplateID { get; set; }

        public string Name { get; set; }
    }
}
