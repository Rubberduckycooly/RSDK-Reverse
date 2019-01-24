using System.Collections.Generic;
using System.Drawing;

namespace RetroED.Tools.MapEditor.Actions
{
    /*
    class ActionMoveEntities : IAction
    {
        List<EditorEntity> entities;
        Point diff;
        bool key;

        public string Description => GenerateActionDescription();

        private string GenerateActionDescription()
        {
            string name = null;

            if (null == entities)
            {
                // this shouldn't happen
                name = "object";
            }
            else if (entities.Count == 1)
            {
                name = entities[0]?.Entity?.Object?.Name?.ToString();
            }
            else
            {
                name = $"{entities.Count} objects";
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                // this probably shouldn't happen either
                name = "object";
            }

            return $"moving {name} ({-(diff.X)},{-(diff.Y)})";
        }

        public ActionMoveEntities(List<EditorEntity> entities, Point diff, bool key=false)
        {
            this.entities = entities;
            this.diff = new Point(-diff.X, -diff.Y);
            this.key = key;
        }

        public bool UpdateFromKey(List<EditorEntity> entities, Point change)
        {
            if (!key) return false;
            if (entities.Count != this.entities.Count) return false;
            for (int i = 0; i < entities.Count; ++i)
                if (entities[i] != this.entities[i])
                    return false;

            diff.X -= change.X;
            diff.Y -= change.Y;
            return true;
        }

        public void Undo()
        {
            foreach (var entity in entities)
                if (Editor.Instance.showGrid == false)
                    entity.Move(diff);
                else
                {
                    entity.Move(diff);
                    //entity.SnapToGrid(diff);
                }

        }

        public IAction Redo()
        {
            // Don't pass key, because we don't want to merge with it after 
            return new ActionMoveEntities(entities, diff);
        }
    }
    */
}
