using PluginCreator;
using System;
using PaintF;

namespace CreatorClassLibrary
{
    [Serializable]
    public abstract class FigureCreator: IPluginCreator
    {
        public abstract Figure Create();
    }
}
