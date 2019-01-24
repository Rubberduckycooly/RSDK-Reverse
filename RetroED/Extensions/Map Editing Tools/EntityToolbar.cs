using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroED.Extensions.EntityToolbar
{
    public partial class EntityToolbar : UserControl
    {
        public Action<int> SelectedEntity;
        public Action<RetroED.Tools.MapEditor.Actions.IAction> AddAction;
        public Action<RSDKvRS.Object> SpawnvRS;
        public Action<RSDKv1.Object> Spawnv1;
        public Action<RSDKv2.Object> Spawnv2;
        public Action<RSDKvB.Object> SpawnvB;

        public bool multipleObjects = false;

        public RetroED.Tools.MapEditor.MainView parent;

        public int RSDKVer = 0;

        public List<RSDKvRS.Object> _entitiesvRS = new List<RSDKvRS.Object>();
        public List<RSDKv1.Object> _entitiesv1 = new List<RSDKv1.Object>();
        public List<RSDKv2.Object> _entitiesv2 = new List<RSDKv2.Object>();
        public List<RSDKvB.Object> _entitiesvB = new List<RSDKvB.Object>();
        private List<int> _selectedEntitySlots = new List<int>();
        private BindingSource _bindingSceneObjectsSource = new BindingSource();

        public RSDKvRS.Scene SceneDatavRS
        {
            get { return parent._RSDKRSScene; }
            set { parent._RSDKRSScene = value; }
        }

        public RSDKv1.Scene SceneDatav1
        {
            get { return parent._RSDK1Scene; }
            set { parent._RSDK1Scene = value; }
        }

        public RSDKv2.Scene SceneDatav2
        {
            get { return parent._RSDK2Scene; }
            set { parent._RSDK2Scene = value; }
        }

        public RSDKvB.Scene SceneDatavB
        {
            get { return parent._RSDKBScene; }
            set { parent._RSDKBScene = value; }
        }

        private RSDKvRS.Object currentEntityvRS;
        private RSDKv1.Object currentEntityv1;
        private RSDKv2.Object currentEntityv2;
        private RSDKvB.Object currentEntityvB;

        public List<RSDKvRS.Object> EntitiesvRS
        {
            get
            {
                return _entitiesvRS;
            }
            set
            {
                _entitiesvRS = value.ToList();
                _entitiesvRS.Sort((x, y) => x.id.CompareTo(y.id));
                UpdateEntitiesList();
            }
        }

        public List<RSDKv1.Object> Entitiesv1
        {
            get
            {
                return _entitiesv1;
            }
            set
            {
                _entitiesv1 = value.ToList();
                _entitiesv1.Sort((x, y) => x.id.CompareTo(y.id));
                UpdateEntitiesList();
            }
        }

        public List<RSDKv2.Object> Entitiesv2
        {
            get
            {
                return _entitiesv2;
            }
            set
            {
                _entitiesv2 = value.ToList();
                _entitiesv2.Sort((x, y) => x.id.CompareTo(y.id));
                UpdateEntitiesList();
            }
        }

        public List<RSDKvB.Object> EntitiesvB
        {
            get
            {
                return _entitiesvB;
            }
            set
            {
                _entitiesvB = value.ToList();
                _entitiesvB.Sort((x, y) => x.id.CompareTo(y.id));
                UpdateEntitiesList();
            }
        }

        bool updateSelected = true;

        public List<RSDKvRS.Object> SelectedEntitiesvRS
        {
            set
            {
                UpdateEntitiesProperties(value);
            }
        }

        public List<RSDKv1.Object> SelectedEntitiesv1
        {
            set
            {
                UpdateEntitiesProperties(value);
            }
        }

        public List<RSDKv2.Object> SelectedEntitiesv2
        {
            set
            {
                UpdateEntitiesProperties(value);
            }
        }

        public List<RSDKvB.Object> SelectedEntitiesvB
        {
            set
            {
                UpdateEntitiesProperties(value);
            }
        }

        public bool NeedRefresh;

        public EntityToolbar()
        {
            InitializeComponent();
        }

        public EntityToolbar(List<RSDKvRS.Object> sceneObjects, RetroED.Tools.MapEditor.MainView p)
        {
            parent = p;
            InitializeComponent();
            RefreshObjects(sceneObjects);
        }

        public EntityToolbar(List<RSDKv1.Object> sceneObjects, RetroED.Tools.MapEditor.MainView p)
        {
            parent = p;
            InitializeComponent();
            RefreshObjects(sceneObjects);
        }

        public EntityToolbar(List<RSDKv2.Object> sceneObjects, RetroED.Tools.MapEditor.MainView p)
        {
            parent = p;
            InitializeComponent();
            RefreshObjects(sceneObjects);
        }

        public EntityToolbar(List<RSDKvB.Object> sceneObjects, RetroED.Tools.MapEditor.MainView p)
        {
            parent = p;
            InitializeComponent();
            RefreshObjects(sceneObjects);
        }

        private void UpdateEntitiesList()
        {
            entitiesList.Items.Clear();
            entitiesList.ResetText();

            switch(RSDKVer)
            {
                case 0:
                    if (currentEntityvB != null && _entitiesvB.Contains(currentEntityvB))
                    {
                        entitiesList.SelectedText = String.Format("{0} - {1}", currentEntityvB.id, currentEntityvB.Name);
                    }
                    break;
                case 1:
                    if (currentEntityv2 != null && _entitiesv2.Contains(currentEntityv2))
                    {
                        entitiesList.SelectedText = String.Format("{0} - {1}", currentEntityv2.id, currentEntityv2.Name);
                    }
                    break;
                case 2:
                    if (currentEntityv1 != null && _entitiesv1.Contains(currentEntityv1))
                    {
                        entitiesList.SelectedText = String.Format("{0} - {1}", currentEntityv1.id, currentEntityv1.Name);
                    }
                    break;
                case 3:
                    if (currentEntityvRS != null && _entitiesvRS.Contains(currentEntityvRS))
                    {
                        entitiesList.SelectedText = String.Format("{0} - {1}", currentEntityvRS.id, currentEntityvRS.Name);
                    }
                    break;
            }
        }

        public void RefreshObjects(List<RSDKvRS.Object> sceneObjects)
        {
            _entitiesvRS = SceneDatavRS.objects;
            sceneObjects.Sort((x, y) => x.Name.ToString().CompareTo(y.Name.ToString()));
            var bindingSceneObjectsList = new BindingList<RSDKvRS.Object>(sceneObjects);
            _bindingSceneObjectsSource.DataSource = bindingSceneObjectsList;

            if (_bindingSceneObjectsSource != null && _bindingSceneObjectsSource.Count > 0)
            {
                cbSpawn.DataSource = _bindingSceneObjectsSource;
                cbSpawn.SelectedIndex = 0;
            }
        }

        public void RefreshObjects(List<RSDKv1.Object> sceneObjects)
        {
            _entitiesv1 = SceneDatav1.objects;
            sceneObjects.Sort((x, y) => x.Name.ToString().CompareTo(y.Name.ToString()));
            var bindingSceneObjectsList = new BindingList<RSDKv1.Object>(sceneObjects);
            _bindingSceneObjectsSource.DataSource = bindingSceneObjectsList;

            if (_bindingSceneObjectsSource != null && _bindingSceneObjectsSource.Count > 0)
            {
                cbSpawn.DataSource = _bindingSceneObjectsSource;
                cbSpawn.SelectedIndex = 0;
            }
        }

        public void RefreshObjects(List<RSDKv2.Object> sceneObjects)
        {
            _entitiesv2 = SceneDatav2.objects;
            sceneObjects.Sort((x, y) => x.Name.ToString().CompareTo(y.Name.ToString()));
            var bindingSceneObjectsList = new BindingList<RSDKv2.Object>(sceneObjects);
            _bindingSceneObjectsSource.DataSource = bindingSceneObjectsList;

            if (_bindingSceneObjectsSource != null && _bindingSceneObjectsSource.Count > 0)
            {
                cbSpawn.DataSource = _bindingSceneObjectsSource;
                cbSpawn.SelectedIndex = 0;
            }
        }

        public void RefreshObjects(List<RSDKvB.Object> sceneObjects)
        {
            _entitiesvB = SceneDatavB.objects;
            sceneObjects.Sort((x, y) => x.Name.ToString().CompareTo(y.Name.ToString()));
            var bindingSceneObjectsList = new BindingList<RSDKvB.Object>(sceneObjects);
            _bindingSceneObjectsSource.DataSource = bindingSceneObjectsList;

            if (_bindingSceneObjectsSource != null && _bindingSceneObjectsSource.Count > 0)
            {
                cbSpawn.DataSource = _bindingSceneObjectsSource;
                cbSpawn.SelectedIndex = 0;
            }
        }

        public void entitiesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //String input = entitiesList.Text.ToString();
            ///string output = new string(input.TakeWhile(char.IsDigit).ToArray());
            //int selectedIndex = Int32.Parse(output);
            //int selectedSlotID = GetIndexOfSlotID(selectedIndex);


            /*if (updateSelected && !multipleObjects) SelectedEntity?.Invoke(_entities[entitiesList.SelectedIndex].SlotID);*/
            /*if (updateSelected && multipleObjects) SelectedEntity?.Invoke(_entities[selectedSlotID].SlotID);*/

            switch(RSDKVer)
            {
                case 0:
                    SelectedEntity?.Invoke(_entitiesvB[entitiesList.SelectedIndex].id);
                    List<RSDKvB.Object> seB = new List<RSDKvB.Object>();
                    seB.Add(_entitiesvB[entitiesList.SelectedIndex]);
                    UpdateEntitiesProperties(seB);
                    break;
                case 1:
                    SelectedEntity?.Invoke(_entitiesv2[entitiesList.SelectedIndex].id);
                    List<RSDKv2.Object> se2 = new List<RSDKv2.Object>();
                    se2.Add(_entitiesv2[entitiesList.SelectedIndex]);
                    UpdateEntitiesProperties(se2);
                    break;
                case 2:
                    SelectedEntity?.Invoke(_entitiesv1[entitiesList.SelectedIndex].id);
                    List<RSDKv1.Object> se1 = new List<RSDKv1.Object>();
                    se1.Add(_entitiesv1[entitiesList.SelectedIndex]);
                    UpdateEntitiesProperties(se1);
                    break;
                case 3:
                    SelectedEntity?.Invoke(_entitiesvRS[entitiesList.SelectedIndex].id);
                    List<RSDKvRS.Object> seRS = new List<RSDKvRS.Object>();
                    seRS.Add(_entitiesvRS[entitiesList.SelectedIndex]);
                    UpdateEntitiesProperties(seRS);
                    break;
            }
        }

        public int GetIndexOfSlotID(int slotID)
        {
            int index = 0;


            switch(RSDKVer)
            {
                case 0:
                    for (int i = 0; i < _entitiesvB.Count; i++)
                    {
                        if (_entitiesvB[i].id == (ushort)slotID)
                        {
                            index = i;
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < _entitiesv2.Count; i++)
                    {
                        if (_entitiesv2[i].id == (ushort)slotID)
                        {
                            index = i;
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < _entitiesv1.Count; i++)
                    {
                        if (_entitiesv1[i].id == (ushort)slotID)
                        {
                            index = i;
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < _entitiesvRS.Count; i++)
                    {
                        if (_entitiesvRS[i].id == (ushort)slotID)
                        {
                            index = i;
                        }
                    }
                    break;
            }
            return index;
        }

        private void addProperty(LocalProperties properties, int category_index, string category, string name, string value_type, object value, bool read_only = false)
        {
            properties.Add(String.Format("{0}.{1}", category, name),
                    new LocalProperty(name, value_type, category_index, category, name, true, read_only, value, "")
                );
        }

        private void UpdateEntitiesProperties(List<RSDKvRS.Object> selectedEntities)
        {
            multipleObjects = false;
            bool isCommonObjects = false;

            if (selectedEntities.Count != 1)
            {
                entityProperties.SelectedObject = null;
                currentEntityvRS = null;
                entitiesList.ResetText();
                _selectedEntitySlots.Clear();
                if (selectedEntities.Count > 1)
                {
                    isCommonObjects = true;
                    entitiesList.SelectedText = String.Format("{0} entities selected", selectedEntities.Count);
                    entitiesList.Items.Clear();
                    entitiesList.DisplayMember = "Text";
                    entitiesList.Tag = "Value";
                    foreach (RSDKvRS.Object selectedEntity in selectedEntities)
                    {

                        entitiesList.Items.Add(new { Text = (String.Format("{0} - {1}", selectedEntity.id, selectedEntity.Name)), Value = selectedEntity.id });
                        _selectedEntitySlots.Add(selectedEntity.id);
                    }
                    //entitiesList.Items.AddRange(selectedEntities.Select(x => String.Format("{0} - {1}", x.SlotID, x.Object.Name)).ToArray());
                    multipleObjects = true;
                    //_selectedEntitySlots.AddRange(selectedEntities.Select(x => (int)x.SlotID).ToList());
                    string commonObject = selectedEntities[0].Name;
                    foreach (RSDKvRS.Object selectedEntity in selectedEntities)
                    {
                        if (selectedEntity.Name != commonObject)
                        {
                            isCommonObjects = false;
                        }
                    }

                }
                if (isCommonObjects == true)
                {
                    //Keep Going (if Implemented; which it's not)
                   // return;
                }
                else
                {
                    //return;
                }

            }

            RSDKvRS.Object entity = selectedEntities[0];

            if (entity == currentEntityvRS) return;
            currentEntityvRS = entity;

            if (entitiesList.SelectedIndex >= 0 && entitiesList.SelectedIndex < _entitiesvRS.Count && _entitiesvRS[entitiesList.SelectedIndex] == currentEntityvRS)
            {
                // Than it is called from selected item in the menu, so changeing the text will remove it, we don't want that
            }
            else
            {
                entitiesList.ResetText();
                entitiesList.SelectedText = String.Format("{0} - {1}", currentEntityvRS.id, currentEntityvRS.Name);
                //entitiesList.SelectedIndex = entities.IndexOf(entity);
            }


            LocalProperties objProperties = new LocalProperties();
            int category_index = 3;//+ entity.Attributes.Count;
            addProperty(objProperties, category_index, "object", "name", "string", entity.Name.ToString(), false);
            addProperty(objProperties, category_index, "object", "entitySlot", "ushort", entity.id, false);
            --category_index;
            addProperty(objProperties, category_index, "position", "x", "ushort", entity.xPos);
            addProperty(objProperties, category_index, "position", "y", "ushort", entity.yPos);
            --category_index;
            addProperty(objProperties, category_index, "type", "Type", "byte", entity.type);
            addProperty(objProperties, category_index, "type", "PropertyValue", "byte", entity.subtype);
            --category_index;

            entityProperties.SelectedObject
                = new LocalPropertyGridObject(objProperties);


        }

        private void UpdateEntitiesProperties(List<RSDKv1.Object> selectedEntities)
        {
            multipleObjects = false;
            bool isCommonObjects = false;

            if (selectedEntities.Count != 1)
            {
                entityProperties.SelectedObject = null;
                currentEntityv1 = null;
                entitiesList.ResetText();
                _selectedEntitySlots.Clear();
                if (selectedEntities.Count > 1)
                {
                    isCommonObjects = true;
                    entitiesList.SelectedText = String.Format("{0} entities selected", selectedEntities.Count);
                    entitiesList.Items.Clear();
                    entitiesList.DisplayMember = "Text";
                    entitiesList.Tag = "Value";
                    foreach (RSDKv1.Object selectedEntity in selectedEntities)
                    {

                        entitiesList.Items.Add(new { Text = (String.Format("{0} - {1}", selectedEntity.id, selectedEntity.Name)), Value = selectedEntity.id });
                        _selectedEntitySlots.Add(selectedEntity.id);
                    }
                    //entitiesList.Items.AddRange(selectedEntities.Select(x => String.Format("{0} - {1}", x.SlotID, x.Object.Name)).ToArray());
                    multipleObjects = true;
                    //_selectedEntitySlots.AddRange(selectedEntities.Select(x => (int)x.SlotID).ToList());
                    string commonObject = selectedEntities[0].Name;
                    foreach (RSDKv1.Object selectedEntity in selectedEntities)
                    {
                        if (selectedEntity.Name != commonObject)
                        {
                            isCommonObjects = false;
                        }
                    }

                }
                if (isCommonObjects == true)
                {
                    //Keep Going (if Implemented; which it's not)
                    // return;
                }
                else
                {
                    //return;
                }

            }

            RSDKv1.Object entity = selectedEntities[0];

            if (entity == currentEntityv1) return;
            currentEntityv1 = entity;

            if (entitiesList.SelectedIndex >= 0 && entitiesList.SelectedIndex < _entitiesv1.Count && _entitiesv1[entitiesList.SelectedIndex] == currentEntityv1)
            {
                // Than it is called from selected item in the menu, so changeing the text will remove it, we don't want that
            }
            else
            {
                entitiesList.ResetText();
                entitiesList.SelectedText = String.Format("{0} - {1}", currentEntityv1.id, currentEntityv1.Name);
                //entitiesList.SelectedIndex = entities.IndexOf(entity);
            }


            LocalProperties objProperties = new LocalProperties();
            int category_index = 3;//+ entity.Attributes.Count;
            addProperty(objProperties, category_index, "object", "name", "string", entity.Name.ToString(), false);
            addProperty(objProperties, category_index, "object", "entitySlot", "ushort", entity.id, false);
            --category_index;
            addProperty(objProperties, category_index, "position", "x", "ushort", entity.xPos);
            addProperty(objProperties, category_index, "position", "y", "ushort", entity.yPos);
            --category_index;
            addProperty(objProperties, category_index, "type", "Type", "byte", entity.type);
            addProperty(objProperties, category_index, "type", "PropertyValue", "byte", entity.subtype);
            --category_index;

            entityProperties.SelectedObject
                = new LocalPropertyGridObject(objProperties);


        }

        private void UpdateEntitiesProperties(List<RSDKv2.Object> selectedEntities)
        {
            multipleObjects = false;
            bool isCommonObjects = false;

            if (selectedEntities.Count != 1)
            {
                entityProperties.SelectedObject = null;
                currentEntityv2 = null;
                entitiesList.ResetText();
                _selectedEntitySlots.Clear();
                if (selectedEntities.Count > 1)
                {
                    isCommonObjects = true;
                    entitiesList.SelectedText = String.Format("{0} entities selected", selectedEntities.Count);
                    entitiesList.Items.Clear();
                    entitiesList.DisplayMember = "Text";
                    entitiesList.Tag = "Value";
                    foreach (RSDKv2.Object selectedEntity in selectedEntities)
                    {

                        entitiesList.Items.Add(new { Text = (String.Format("{0} - {1}", selectedEntity.id, selectedEntity.Name)), Value = selectedEntity.id });
                        _selectedEntitySlots.Add(selectedEntity.id);
                    }
                    //entitiesList.Items.AddRange(selectedEntities.Select(x => String.Format("{0} - {1}", x.SlotID, x.Object.Name)).ToArray());
                    multipleObjects = true;
                    //_selectedEntitySlots.AddRange(selectedEntities.Select(x => (int)x.SlotID).ToList());
                    string commonObject = selectedEntities[0].Name;
                    foreach (RSDKv2.Object selectedEntity in selectedEntities)
                    {
                        if (selectedEntity.Name != commonObject)
                        {
                            isCommonObjects = false;
                        }
                    }

                }
                if (isCommonObjects == true)
                {
                    //Keep Going (if Implemented; which it's not)
                    // return;
                }
                else
                {
                    //return;
                }

            }

            RSDKv2.Object entity = selectedEntities[0];

            if (entity == currentEntityv2) return;
            currentEntityv2 = entity;

            if (entitiesList.SelectedIndex >= 0 && entitiesList.SelectedIndex < _entitiesv2.Count && _entitiesv2[entitiesList.SelectedIndex] == currentEntityv2)
            {
                // Than it is called from selected item in the menu, so changeing the text will remove it, we don't want that
            }
            else
            {
                entitiesList.ResetText();
                entitiesList.SelectedText = String.Format("{0} - {1}", currentEntityv2.id, currentEntityv2.Name);
                //entitiesList.SelectedIndex = entities.IndexOf(entity);
            }


            LocalProperties objProperties = new LocalProperties();
            int category_index = 3;//+ entity.Attributes.Count;
            addProperty(objProperties, category_index, "object", "name", "string", entity.Name.ToString(), false);
            addProperty(objProperties, category_index, "object", "entitySlot", "ushort", entity.id, false);
            --category_index;
            addProperty(objProperties, category_index, "position", "x", "ushort", entity.xPos);
            addProperty(objProperties, category_index, "position", "y", "ushort", entity.yPos);
            --category_index;
            addProperty(objProperties, category_index, "type", "Type", "byte", entity.type);
            addProperty(objProperties, category_index, "type", "PropertyValue", "byte", entity.subtype);
            --category_index;


            entityProperties.SelectedObject
                = new LocalPropertyGridObject(objProperties);
        }

        private void UpdateEntitiesProperties(List<RSDKvB.Object> selectedEntities)
        {
            multipleObjects = false;
            bool isCommonObjects = false;

            if (selectedEntities.Count != 1)
            {
                entityProperties.SelectedObject = null;
                currentEntityvB = null;
                entitiesList.ResetText();
                _selectedEntitySlots.Clear();
                if (selectedEntities.Count > 1)
                {
                    isCommonObjects = true;
                    entitiesList.SelectedText = String.Format("{0} entities selected", selectedEntities.Count);
                    entitiesList.Items.Clear();
                    entitiesList.DisplayMember = "Text";
                    entitiesList.Tag = "Value";
                    foreach (RSDKvB.Object selectedEntity in selectedEntities)
                    {

                        entitiesList.Items.Add(new { Text = (String.Format("{0} - {1}", selectedEntity.id, selectedEntity.Name)), Value = selectedEntity.id });
                        _selectedEntitySlots.Add(selectedEntity.id);
                    }
                    //entitiesList.Items.AddRange(selectedEntities.Select(x => String.Format("{0} - {1}", x.SlotID, x.Object.Name)).ToArray());
                    multipleObjects = true;
                    //_selectedEntitySlots.AddRange(selectedEntities.Select(x => (int)x.SlotID).ToList());
                    string commonObject = selectedEntities[0].Name;
                    foreach (RSDKvB.Object selectedEntity in selectedEntities)
                    {
                        if (selectedEntity.Name != commonObject)
                        {
                            isCommonObjects = false;
                        }
                    }

                }
                if (isCommonObjects == true)
                {
                    //Keep Going (if Implemented; which it's not)
                    // return;
                }
                else
                {
                    //return;
                }

            }

            RSDKvB.Object entity = selectedEntities[0];

            if (entity == currentEntityvB) return;
            currentEntityvB = entity;

            if (entitiesList.SelectedIndex >= 0 && entitiesList.SelectedIndex < _entitiesvB.Count && _entitiesvB[entitiesList.SelectedIndex] == currentEntityvB)
            {
                // Than it is called from selected item in the menu, so changeing the text will remove it, we don't want that
            }
            else
            {
                entitiesList.ResetText();
                entitiesList.SelectedText = String.Format("{0} - {1}", currentEntityvB.id, currentEntityvB.Name);
                //entitiesList.SelectedIndex = entities.IndexOf(entity);
            }


            LocalProperties objProperties = new LocalProperties();
            int category_index = 4;
            addProperty(objProperties, category_index, "object", "name", "string", entity.Name.ToString(), false);
            addProperty(objProperties, category_index, "object", "entitySlot", "ushort", entity.id, false);
            --category_index;
            addProperty(objProperties, category_index, "position", "x", "float", entity.position.X.High + ((float)entity.position.X.Low / 0x10000));
            addProperty(objProperties, category_index, "position", "y", "float", entity.position.Y.High + ((float)entity.position.Y.Low / 0x10000));
            --category_index;
            addProperty(objProperties, category_index, "type", "Type", "byte", entity.type);
            addProperty(objProperties, category_index, "type", "PropertyValue", "byte", entity.subtype);
            --category_index;
            addProperty(objProperties, category_index, "attribute", "attributeType", "ushort", entity.AttributeType);
            addProperty(objProperties, category_index, "attribute", "attributeValue", "int", entity.attribute);
            --category_index;

            entityProperties.SelectedObject
                = new LocalPropertyGridObject(objProperties);

        }

        public void UpdateCurrentEntityProperites()
        {
            var obj = entityProperties.SelectedObject as LocalPropertyGridObject;
            if (obj != null)
            {

                switch(RSDKVer)
                {
                    case 0:
                        obj.setValue("position.x", currentEntityvB.position.X.High + ((float)currentEntityvB.position.X.Low / 0x10000));
                        obj.setValue("position.y", currentEntityvB.position.Y.High + ((float)currentEntityvB.position.Y.Low / 0x10000));
                        obj.setValue("type.Type", currentEntityvB.type);
                        obj.setValue("type.PropertyValue", currentEntityvB.subtype);
                        obj.setValue("attribute.attributeType", currentEntityvB.AttributeType);
                        obj.setValue("attribute.attributeValue", currentEntityvB.attribute);
                        break;
                    case 1:
                        obj.setValue("position.x", currentEntityv2.xPos);
                        obj.setValue("position.y", currentEntityv2.yPos);
                        obj.setValue("type.Type", currentEntityv2.type);
                        obj.setValue("type.PropertyValue", currentEntityv2.subtype);
                        break;
                    case 2:
                        obj.setValue("position.x", currentEntityv1.xPos);
                        obj.setValue("position.y", currentEntityv1.yPos);
                        obj.setValue("type.Type", currentEntityv1.type);
                        obj.setValue("type.PropertyValue", currentEntityv1.subtype);
                        break;
                    case 3:
                        obj.setValue("position.x", currentEntityvRS.xPos);
                        obj.setValue("position.y", currentEntityvRS.yPos);
                        obj.setValue("type.Type", currentEntityvRS.type);
                        obj.setValue("type.PropertyValue", currentEntityvRS.subtype);
                        break;
                }
                NeedRefresh = true;
                parent._mapViewer.DrawScene();
            }
        }

        public void PropertiesRefresh()
        {
            entityProperties.Refresh();
            NeedRefresh = false;
        }

        private void setEntitiyProperty(RSDKvRS.Object entity, string tag, object value, object oldValue)
        {
            string[] parts = tag.Split('.');
            string category = parts[0];
            string name = parts[1];
            if (category == "position")
            {
                float fvalue = (Int32)value;
                if (fvalue < Int16.MinValue || fvalue > Int16.MaxValue)
                {
                    // Invalid
                    var obj = (entityProperties.SelectedObject as LocalPropertyGridObject);
                    obj.setValue(tag, oldValue);
                    return;
                }
                var Xpos = entity.xPos;
                var Ypos = entity.yPos;
                if (name == "x")
                {
                    Xpos = (ushort)fvalue;
                }
                else if (name == "y")
                {
                    Ypos = (ushort)fvalue;
                }
                entity.xPos = Xpos;
                entity.yPos = Ypos;
                Console.WriteLine(Xpos + " " + Ypos);
                if (entity == currentEntityvRS)
                    UpdateCurrentEntityProperites();
            }
            else if (category == "type")
            {
                int tmp = (Int32)value;
                int fvalue = (byte)tmp;
                if (fvalue < Int16.MinValue || fvalue > Int16.MaxValue)
                {
                    // Invalid
                    var obj = (entityProperties.SelectedObject as LocalPropertyGridObject);
                    obj.setValue(tag, oldValue);
                    return;
                }

                var type = entity.type;
                var subtype = entity.subtype;
                if (name == "Type")
                {
                    type = (byte)fvalue;
                }
                else if (name == "PropertyValue")
                {
                    subtype = (byte)fvalue;
                }
                entity.type = type;
                entity.subtype = subtype;
                if (entity == currentEntityvRS)
                    UpdateCurrentEntityProperites();
            }
            else if (category == "object")
            {
                if (name == "name" && oldValue != value)
                {
                    var objects = ((BindingList<RSDKvRS.Object>)_bindingSceneObjectsSource.DataSource).ToList();
                    var obj = objects.FirstOrDefault(t => t.Name == value as string);
                }
                if (name == "entitySlot" && oldValue != value)
                {
                    int tmp = (Int32)value;
                    ushort newSlot = (ushort)tmp;
                    // Check if slot has been used
                    var objects = ((BindingList<RSDKvRS.Object>)_bindingSceneObjectsSource.DataSource).ToList();
                    foreach (var obj in objects)
                    {
                        if (obj.id == newSlot)
                        {
                            System.Windows.Forms.MessageBox.Show($"Slot {newSlot} is currently being used by a {obj.Name.ToString()}",
                                "Slot in use!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    // Passed
                    entity.id = newSlot;
                }
                // Update Properties
                currentEntityvRS = null;
                UpdateEntitiesProperties(new List<RSDKvRS.Object>() { entity });
            }
        }

        private void setEntitiyProperty(RSDKv1.Object entity, string tag, object value, object oldValue)
        {
            string[] parts = tag.Split('.');
            string category = parts[0];
            string name = parts[1];
            if (category == "position")
            {
                float fvalue = (Int32)value;
                if (fvalue < Int16.MinValue || fvalue > Int16.MaxValue)
                {
                    // Invalid
                    var obj = (entityProperties.SelectedObject as LocalPropertyGridObject);
                    obj.setValue(tag, oldValue);
                    return;
                }
                var Xpos = entity.xPos;
                var Ypos = entity.yPos;
                if (name == "x")
                {
                    Xpos = (ushort)fvalue;
                }
                else if (name == "y")
                {
                    Ypos = (ushort)fvalue;
                }
                entity.xPos = Xpos;
                entity.yPos = Ypos;
                Console.WriteLine(Xpos + " " + Ypos);
                if (entity == currentEntityv1)
                    UpdateCurrentEntityProperites();
            }
            else if (category == "type")
            {
                int tmp = (Int32)value;
                int fvalue = (byte)tmp;
                if (fvalue < Int16.MinValue || fvalue > Int16.MaxValue)
                {
                    // Invalid
                    var obj = (entityProperties.SelectedObject as LocalPropertyGridObject);
                    obj.setValue(tag, oldValue);
                    return;
                }

                var type = entity.type;
                var subtype = entity.subtype;
                if (name == "Type")
                {
                    type = (byte)fvalue;
                }
                else if (name == "PropertyValue")
                {
                    subtype = (byte)fvalue;
                }
                entity.type = type;
                entity.subtype = subtype;
                if (entity == currentEntityv1)
                    UpdateCurrentEntityProperites();
            }
            else if (category == "object")
            {
                if (name == "name" && oldValue != value)
                {
                    var objects = ((BindingList<RSDKv1.Object>)_bindingSceneObjectsSource.DataSource).ToList();
                    var obj = objects.FirstOrDefault(t => t.Name == value as string);
                }
                if (name == "entitySlot" && oldValue != value)
                {
                    int tmp = (Int32)value;
                    ushort newSlot = (ushort)tmp;
                    // Check if slot has been used
                    var objects = ((BindingList<RSDKv1.Object>)_bindingSceneObjectsSource.DataSource).ToList();
                    foreach (var obj in objects)
                    {
                        if (obj.id == newSlot)
                        {
                            System.Windows.Forms.MessageBox.Show($"Slot {newSlot} is currently being used by a {obj.Name.ToString()}",
                                "Slot in use!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    // Passed
                    entity.id = newSlot;
                }
                // Update Properties
                currentEntityv1 = null;
                UpdateEntitiesProperties(new List<RSDKv1.Object>() { entity });
            }
        }

        private void setEntitiyProperty(RSDKv2.Object entity, string tag, object value, object oldValue)
        {
            string[] parts = tag.Split('.');
            string category = parts[0];
            string name = parts[1];
            if (category == "position")
            {
                float fvalue = (Int32)value;
                if (fvalue < Int16.MinValue || fvalue > Int16.MaxValue)
                {
                    // Invalid
                    var obj = (entityProperties.SelectedObject as LocalPropertyGridObject);
                    obj.setValue(tag, oldValue);
                    return;
                }
                var Xpos = entity.xPos;
                var Ypos = entity.yPos;
                if (name == "x")
                {
                    Xpos = (ushort)fvalue;
                }
                else if (name == "y")
                {
                    Ypos = (ushort)fvalue;
                }
                entity.xPos = Xpos;
                entity.yPos = Ypos;
                Console.WriteLine(Xpos + " " + Ypos);
                if (entity == currentEntityv2)
                    UpdateCurrentEntityProperites();
            }
            else if (category == "type")
            {
                int tmp = (Int32)value;
                int fvalue = (byte)tmp;
                if (fvalue < Int16.MinValue || fvalue > Int16.MaxValue)
                {
                    // Invalid
                    var obj = (entityProperties.SelectedObject as LocalPropertyGridObject);
                    obj.setValue(tag, oldValue);
                    return;
                }

                var type = entity.type;
                var subtype = entity.subtype;
                if (name == "Type")
                {
                    type = (byte)fvalue;
                }
                else if (name == "PropertyValue")
                {
                    subtype = (byte)fvalue;
                }
                entity.type = type;
                entity.subtype = subtype;
                if (entity == currentEntityv2)
                    UpdateCurrentEntityProperites();
            }
            else if (category == "object")
            {
                if (name == "name" && oldValue != value)
                {
                    var objects = ((BindingList<RSDKv2.Object>)_bindingSceneObjectsSource.DataSource).ToList();
                    var obj = objects.FirstOrDefault(t => t.Name == value as string);
                }
                if (name == "entitySlot" && oldValue != value)
                {
                    int tmp = (Int32)value;
                    ushort newSlot = (ushort)tmp;
                    // Check if slot has been used
                    var objects = ((BindingList<RSDKv2.Object>)_bindingSceneObjectsSource.DataSource).ToList();
                    foreach (var obj in objects)
                    {
                        if (obj.id == newSlot)
                        {
                            System.Windows.Forms.MessageBox.Show($"Slot {newSlot} is currently being used by a {obj.Name.ToString()}",
                                "Slot in use!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    // Passed
                    entity.id = newSlot;
                }
                // Update Properties
                currentEntityv2 = null;
                UpdateEntitiesProperties(new List<RSDKv2.Object>() { entity });
            }
        }

        private void setEntitiyProperty(RSDKvB.Object entity, string tag, object value, object oldValue)
        {
            string[] parts = tag.Split('.');
            string category = parts[0];
            string name = parts[1];
            if (category == "position")
            {
                float fvalue = (Int32)value;
                if (fvalue < Int16.MinValue || fvalue > Int16.MaxValue)
                {
                    // Invalid
                    var obj = (entityProperties.SelectedObject as LocalPropertyGridObject);
                    obj.setValue(tag, oldValue);
                    return;
                }
                var pos = entity.position;
                if (name == "x")
                {
                    pos.X.High = (short)fvalue;
                    pos.X.Low = (ushort)(fvalue * 0x10000);
                }
                else if (name == "y")
                {
                    pos.Y.High = (short)fvalue;
                    pos.Y.Low = (ushort)(fvalue * 0x10000);
                }
                entity.position = pos;
                if (entity == currentEntityvB)
                    UpdateCurrentEntityProperites();
            }
            else if (category == "type")
            {
                int tmp = (Int32)value;
                int fvalue = (byte)tmp;
                if (fvalue < Int16.MinValue || fvalue > Int16.MaxValue)
                {
                    // Invalid
                    var obj = (entityProperties.SelectedObject as LocalPropertyGridObject);
                    obj.setValue(tag, oldValue);
                    return;
                }

                var type = entity.type;
                var subtype = entity.subtype;
                if (name == "Type")
                {
                    type = (byte)fvalue;
                }
                else if (name == "PropertyValue")
                {
                    subtype = (byte)fvalue;
                }
                entity.type = type;
                entity.subtype = subtype;
                if (entity == currentEntityvB)
                    UpdateCurrentEntityProperites();
            }
            else if (category == "attribute(?)")
            {
                //, ""
                int fvalue = (byte)value;
                if (fvalue < Int16.MinValue || fvalue > Int16.MaxValue)
                {
                    // Invalid
                    var obj = (entityProperties.SelectedObject as LocalPropertyGridObject);
                    obj.setValue(tag, oldValue);
                    return;
                }

                var attrib = entity.type;
                if (name == "attribute")
                {
                    attrib = (byte)fvalue;
                }
                entity.attribute = attrib;
                if (entity == currentEntityvB)
                    UpdateCurrentEntityProperites();
            }
            else if (category == "object")
            {
                if (name == "name" && oldValue != value)
                {
                    var objects = ((BindingList<RSDKvB.Object>)_bindingSceneObjectsSource.DataSource).ToList();
                    var obj = objects.FirstOrDefault(t => t.Name == value as string);
                }
                if (name == "entitySlot" && oldValue != value)
                {
                    int tmp = (Int32)value;
                    ushort newSlot = (ushort)tmp;
                    // Check if slot has been used
                    var objects = ((BindingList<RSDKvB.Object>)_bindingSceneObjectsSource.DataSource).ToList();
                    foreach (var obj in objects)
                    {
                        if (obj.id == newSlot)
                        {
                            System.Windows.Forms.MessageBox.Show($"Slot {newSlot} is currently being used by a {obj.Name.ToString()}",
                                "Slot in use!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    // Passed
                    entity.id = newSlot;
                }
                // Update Properties
                currentEntityvB = null;
                UpdateEntitiesProperties(new List<RSDKvB.Object>() { entity });
            }
        }

        private void entityProperties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            string tag = e.ChangedItem.PropertyDescriptor.Name;

            switch (RSDKVer)
            {
                case 0:
                    AddAction?.Invoke(new RetroED.Tools.MapEditor.Actions.ActionEntityPropertyChange(currentEntityvB, tag, e.OldValue, e.ChangedItem.Value, new Action<RSDKvB.Object, string, object, object>(setEntitiyProperty)));
                    setEntitiyProperty(currentEntityvB, tag, e.ChangedItem.Value, e.OldValue);
                    break;
                case 1:
                    AddAction?.Invoke(new RetroED.Tools.MapEditor.Actions.ActionEntityPropertyChange(currentEntityv2, tag, e.OldValue, e.ChangedItem.Value, new Action<RSDKv2.Object, string, object, object>(setEntitiyProperty)));
                    setEntitiyProperty(currentEntityv2, tag, e.ChangedItem.Value, e.OldValue);
                    break;
                case 2:
                    AddAction?.Invoke(new RetroED.Tools.MapEditor.Actions.ActionEntityPropertyChange(currentEntityv1, tag, e.OldValue, e.ChangedItem.Value, new Action<RSDKv1.Object, string, object, object>(setEntitiyProperty)));
                    setEntitiyProperty(currentEntityv1, tag, e.ChangedItem.Value, e.OldValue);
                    break;
                case 3:
                    AddAction?.Invoke(new RetroED.Tools.MapEditor.Actions.ActionEntityPropertyChange(currentEntityvRS, tag, e.OldValue, e.ChangedItem.Value, new Action<RSDKvRS.Object, string, object, object>(setEntitiyProperty)));
                    setEntitiyProperty(currentEntityvRS, tag, e.ChangedItem.Value, e.OldValue);
                    break;
            }
        }

        private void entitiesList_DropDown(object sender, EventArgs e)
        {
            // It is slow to update the list, so lets generate it when the menu opens
            entitiesList.Items.Clear();
            switch (RSDKVer)
            {
                case 0:
                    entitiesList.Items.AddRange(_entitiesvB.Select(x => String.Format("{0} - {1}", x.id, x.Name)).ToArray());
                    break;
                case 1:
                    entitiesList.Items.AddRange(_entitiesv2.Select(x => String.Format("{0} - {1}", x.id, x.Name)).ToArray());
                    break;
                case 2:
                    entitiesList.Items.AddRange(_entitiesv1.Select(x => String.Format("{0} - {1}", x.id, x.Name)).ToArray());
                    break;
                case 3:
                    entitiesList.Items.AddRange(_entitiesvRS.Select(x => String.Format("{0} - {1}", x.id, x.Name)).ToArray());
                    break;
            }
        }

        private void btnSpawn_Click(object sender, EventArgs e)
        {
            switch (RSDKVer)
            {
                case 0:
                    if (cbSpawn?.SelectedItem != null && cbSpawn.SelectedItem is RSDKvB.Object)
                    {
                        if ((SceneDatavB.objects.Count + 1) < SceneDatavB.MaxObjectCount)
                        {
                            RSDKvB.Object Obj = cbSpawn.SelectedItem as RSDKvB.Object;
                            Obj.id = (ushort)_entitiesvB.Count;
                            _entitiesvB.Add(Obj);
                        }
                        else
                        {
                            MessageBox.Show("You've exceeded the max amount of objects for this stage! Remove objects to spawn a new one!");
                        }
                    }
                    break;
                case 1:
                    if (cbSpawn?.SelectedItem != null && cbSpawn.SelectedItem is RSDKv2.Object)
                    {
                        if ((SceneDatav2.objects.Count + 1) < SceneDatav2.MaxObjectCount)
                        {
                            RSDKv2.Object Obj = cbSpawn.SelectedItem as RSDKv2.Object;
                            Obj.id = (ushort)_entitiesv2.Count;
                            _entitiesv2.Add(Obj);
                        }
                        else
                        {
                            MessageBox.Show("You've exceeded the max amount of objects for this stage! Remove objects to spawn a new one!");
                        }
                    }
                    break;
                case 2:
                    if (cbSpawn?.SelectedItem != null && cbSpawn.SelectedItem is RSDKv1.Object)
                    {
                        if ((SceneDatav1.objects.Count + 1) < SceneDatav1.MaxObjectCount)
                        {
                            RSDKv1.Object Obj = cbSpawn.SelectedItem as RSDKv1.Object;
                            Obj.id = (ushort)_entitiesv1.Count;
                            _entitiesv1.Add(Obj);
                        }
                        else
                        {
                            MessageBox.Show("You've exceeded the max amount of objects for this stage! Remove objects to spawn a new one!");
                        }
                    }
                    break;
                case 3:
                    if (cbSpawn?.SelectedItem != null && cbSpawn.SelectedItem is RSDKvRS.Object)
                    {
                        if ((SceneDatavRS.objects.Count + 1) < SceneDatavRS.MaxObjectCount)
                        {
                            RSDKvRS.Object Obj = cbSpawn.SelectedItem as RSDKvRS.Object;
                            Obj.id = (ushort)_entitiesvRS.Count;
                            _entitiesvRS.Add(Obj);
                        }
                        else
                        {
                            MessageBox.Show("You've exceeded the max amount of objects for this stage! Remove objects to spawn a new one!");
                        }
                    }
                    break;
            }
        }

        private void cbSpawn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSpawn_Click(sender, e);
            }
        }
    }
}
