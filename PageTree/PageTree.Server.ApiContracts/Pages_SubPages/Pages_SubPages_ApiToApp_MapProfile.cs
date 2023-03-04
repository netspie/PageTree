﻿using PageTree.App.Pages.Commands;

namespace PageTree.Server.ApiContracts
{
    public class PagesSub_ApiToApp_MapProfile : BaseProfile
    {
        public PagesSub_ApiToApp_MapProfile()
        {
            CreateMapCirc<CreateSubPageApiCommand, CreateSubPageCommand>();
        }
    }
}
