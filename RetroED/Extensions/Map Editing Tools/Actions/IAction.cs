using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetroED.Tools.MapEditor.Actions
{
    public interface IAction
    {
        void Undo();
        IAction Redo();

        string Description { get; }
    }
}
