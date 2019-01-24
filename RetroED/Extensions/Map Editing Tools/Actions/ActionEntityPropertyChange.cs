using System;

namespace RetroED.Tools.MapEditor.Actions
{
    class ActionEntityPropertyChange : IAction
    {
        RSDKvRS.Object entityvRS;
        RSDKv1.Object entityv1;
        RSDKv2.Object entityv2;
        RSDKvB.Object entityvB;
        string tag;
        object oldValue;
        object newValue;
        Action<RSDKvRS.Object, string, object, object> setValuevRS;
        Action<RSDKv1.Object, string, object, object> setValuev1;
        Action<RSDKv2.Object, string, object, object> setValuev2;
        Action<RSDKvB.Object, string, object, object> setValuevB;

        public string Description => $"changing {tag} on Unknown Object from {oldValue} to {newValue}";

        public string DescriptionvRS => $"changing {tag} on {entityvRS.Name} from {oldValue} to {newValue}";
        public string Descriptionv1 => $"changing {tag} on {entityv1.Name} from {oldValue} to {newValue}";
        public string Descriptionv2 => $"changing {tag} on {entityv2.Name} from {oldValue} to {newValue}";
        public string DescriptionvB => $"changing {tag} on {entityvB.Name} from {oldValue} to {newValue}";

        public ActionEntityPropertyChange(RSDKvB.Object entity, string tag, object oldValue, object newValue, Action<RSDKvB.Object, string, object, object> setValue)
        {
            this.entityvB = entity;
            this.tag = tag;
            this.oldValue = oldValue;
            this.newValue = newValue;
            this.setValuevB = setValue;
        }

        public ActionEntityPropertyChange(RSDKv2.Object entity, string tag, object oldValue, object newValue, Action<RSDKv2.Object, string, object, object> setValue)
        {
            this.entityv2 = entity;
            this.tag = tag;
            this.oldValue = oldValue;
            this.newValue = newValue;
            this.setValuev2 = setValue;
        }

        public ActionEntityPropertyChange(RSDKv1.Object entity, string tag, object oldValue, object newValue, Action<RSDKv1.Object, string, object, object> setValue)
        {
            this.entityv1 = entity;
            this.tag = tag;
            this.oldValue = oldValue;
            this.newValue = newValue;
            this.setValuev1 = setValue;
        }

        public ActionEntityPropertyChange(RSDKvRS.Object entity, string tag, object oldValue, object newValue, Action<RSDKvRS.Object, string, object, object> setValue)
        {
            this.entityvRS = entity;
            this.tag = tag;
            this.oldValue = oldValue;
            this.newValue = newValue;
            this.setValuevRS = setValue;
        }

        public void Undo(int RSDKver)
        {
            switch (RSDKver)
            {
                case 0:
                    setValuevB(entityvB, tag, oldValue, newValue);
                    break;
                case 1:
                    setValuev2(entityv2, tag, oldValue, newValue);
                    break;
                case 2:
                    setValuev1(entityv1, tag, oldValue, newValue);
                    break;
                case 3:
                    setValuevRS(entityvRS, tag, oldValue, newValue);
                    break;
            }
        }

        public IAction Redo(int RSDKver)
        {
            switch (RSDKver)
            {
                case 0:
                    return new ActionEntityPropertyChange(entityvB, tag, newValue, oldValue, setValuevB);
                case 1:
                    return new ActionEntityPropertyChange(entityv2, tag, newValue, oldValue, setValuev2);
                case 2:
                    return new ActionEntityPropertyChange(entityv1, tag, newValue, oldValue, setValuev1);
                case 3:
                    return new ActionEntityPropertyChange(entityvRS, tag, newValue, oldValue, setValuevRS);
            }
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
