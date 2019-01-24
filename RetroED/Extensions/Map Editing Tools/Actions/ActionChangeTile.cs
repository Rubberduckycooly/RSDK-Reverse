using System;
using System.Drawing;

namespace RetroED.Tools.MapEditor.Actions
{
    class ActionChangeTile : IAction
    {
        Action<Point, ushort> setLayer;
        Point position;
        private ushort oldValue, newValue;

        public string Description => $"placing tile at postion ({position.X},{position.Y})";

        public ActionChangeTile(Action<Point, ushort> setLayer, Point position, ushort oldValue, ushort newValue)
        {
            this.setLayer = setLayer;
            this.position = position;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        public void Undo()
        {
            setLayer(position, oldValue);
        }

        public IAction Redo()
        {
            return new ActionChangeTile(setLayer, position, newValue, oldValue);
        }
    }
}
