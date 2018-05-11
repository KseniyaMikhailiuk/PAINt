using PluginCreator;
using System;
namespace FigureClassLibrary
{
    [Serializable]
    public abstract class FigureCreator: IPluginCreator
    {
        public abstract Figure Create();
    }
}
