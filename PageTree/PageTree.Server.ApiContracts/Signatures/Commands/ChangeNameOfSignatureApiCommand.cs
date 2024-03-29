﻿using Corelibs.Basic.Net;
using PageTree.App.Entities.Signatures;

namespace PageTree.Server.ApiContracts
{
    public class ChangeNameOfSignatureApiCommand
    {
        [FromRoute, AuthorizeResource(typeof(Signature))]
        public string SignatureID { get; set; }
        public string Name { get; set; }
    }
}
