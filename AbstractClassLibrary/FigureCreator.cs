using System;

namespace AbstractClassLibrary
{
    [Serializable]
    public abstract class FigureCreator
    {
        public abstract Figure Create();
    }
}
