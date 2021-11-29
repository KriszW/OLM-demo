﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Store.Manager.Routing.Times.InduvidualManagers.RoutingProdTime.Actions
{
    public class ProdTimePageIndexChangedAction
    {
        public ProdTimePageIndexChangedAction(int newPageIndex)
        {
            NewPageIndex = newPageIndex;
        }

        public int NewPageIndex { get; private set; }
    }
}
