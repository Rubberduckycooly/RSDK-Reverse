using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetroED.Extensions.EntityToolbar
{
    /// <summary>
    /// /
    /// </summary>
    public class LocalProperty
    {
        private string _name;
        private string _type; //data type
        private int _categoryIndex;
        private string _groupName; //group name
        private string _displayName; //display name
        private Boolean _display;  //true to show
        private Boolean _readonly; //true is readonly
        private string _description;
        private object _value;
        private List<object> _options = new List<object>();
        /// <summary>
        /// construcator
        /// </summary>
        public LocalProperty()
        {
            _display = true;
            _readonly = false;
        }
        public LocalProperty(string name,
                            string type, string description)
        {
            _display = true;
            _readonly = false;
            //
            _name = name;
            _type = type;
            _description = description;
        }
        public LocalProperty(string name,
                             string type,
                             int category_index,
                             string group,
                             string display,
                             Boolean is_display,
                             Boolean is_readonly,
                             object value,
                             string description,
                             List<object> options = null)
        {
            _display = is_display;
            _readonly = is_readonly;
            //
            _name = name;
            _type = type;
            _categoryIndex = category_index;
            _groupName = group;
            _displayName = display;
            _value = value;
            _description = description;
            if (options != null)
            {
                foreach (object v in options)
                {
                    _options.Add(v);
                }
            }
        }
        public void AddOption(object value)
        {
            _options.Add(value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<object> GetOptions()
        {
            return _options;
        }
        public Type ValueType()
        {
            return _value.GetType();
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get { return _name; } set { _name = value; } }
        public string Type { get { return _type; } set { _type = value; } }
        public Boolean Display { get { return _display; } set { _display = value; } }
        public Boolean Readonly { get { return _readonly; } set { _readonly = value; } }
        public string DisplayName { get { return _displayName; } set { _displayName = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public string Group { get { return String.Concat(Enumerable.Repeat("\t", _categoryIndex)) + _groupName; } set { _groupName = value; } }
        public object Value { get { return  _value; } set { _value = value; } }
    }

    public class LocalProperties : Dictionary<string, LocalProperty>
    {
    }

}
