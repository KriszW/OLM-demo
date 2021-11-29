﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.TCOSettings.Actions
{
    public class PageIndexChangedAction
    {
        public PageIndexChangedAction(int newPageIndex)
        {
            NewPageIndex = newPageIndex;
        }

        public int NewPageIndex { get; private set; }
    }
}