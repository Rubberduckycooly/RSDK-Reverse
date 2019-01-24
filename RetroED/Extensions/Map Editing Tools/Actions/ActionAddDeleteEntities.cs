using System;
using System.Collections.Generic;

namespace RetroED.Tools.MapEditor.Actions
{
    
    class ActionAddDeleteEntities //: IAction
    {/*
        Action<List<EditorEntity>> addEntity;
        Action<List<EditorEntity>> deleteEntity;
        List<EditorEntity> entities;
        bool add;

        public string Description => GenerateActionDescription();

        public ActionAddDeleteEntities(List<EditorEntity> entities, bool add, Action<List<EditorEntity>> addEntity, Action<List<EditorEntity>> deleteEntity)
        {
            this.entities = entities;
            this.add = add;
            this.addEntity = addEntity;
            this.deleteEntity = deleteEntity;
        }

        public void Undo()
        {
            if (add)
                deleteEntity(entities);
            else
                addEntity(entities);
        }

        public IAction Redo()
        {
            return new ActionAddDeleteEntities(entities, !add, addEntity, deleteEntity);
        }

        private string GenerateActionDescription()
        {
            string action;
            string name = null;
            if (add)
            {
                action = "adding";
            }
            else
            {
                action = "deleting";
            }

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

            return $"{action} {name}";
        }*/

    }
}
