﻿using PluginCreator;
using System;
namespace AbstractFigureClassLibrary
{
    [Serializable]
    public abstract class FigureCreator: IPluginCreator
    {
        public abstract Figure Create();
    }
}
