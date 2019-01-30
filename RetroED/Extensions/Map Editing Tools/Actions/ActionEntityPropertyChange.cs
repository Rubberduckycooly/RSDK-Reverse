using System;

namespace RetroED.Tools.MapEditor.Actions
{
    class ActionEntityPropertyChange : IAction
    {
        Retro_Formats.Object entity;
        string tag;
        object oldValue;
        object newValue;
        Action<Retro_Formats.Object, string, object, object> setValue;

        public string Description => $"changing {tag} on Unknown Object from {oldValue} to {newValue}";

        public ActionEntityPropertyChange(Retro_Formats.Object entity, string tag, object oldValue, object newValue, Action<Retro_Formats.Object, string, object, object> setValue)
        {
            this.entity = entity;
            this.tag = tag;
            this.oldValue = oldValue;
            this.newValue = newValue;
            this.setValue = setValue;
        }

        public void Undo(int RSDKver)
        {
            setValue(entity, tag, oldValue, newValue);
        }

        public IAction Redo(int RSDKver)
        {
            return new ActionEntityPropertyChange(entity, tag, newValue, oldValue, setValue);
            return null;
        }

        public void Undo()
        {
        }

        public IAction Redo()
        {
            return null;
        }
    }
}
