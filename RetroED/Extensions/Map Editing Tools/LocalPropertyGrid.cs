using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Dynamic;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace RetroED.Extensions.EntityToolbar
{
    /// <summary>
    /// this object and "DynamicPropertyDescriptor" are supposed to be used for display the dynamic attributes
    /// in the PropertyGrid
    /// copied from http://www.codeproject.com/Articles/193462/Using-PropertyGrid-to-Display-and-Edit-Dynamic-Obj 
    /// </summary>
    public class LocalPropertyGridObject : DynamicObject, ICustomTypeDescriptor, INotifyPropertyChanged
    {
        private LocalProperties _properties;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_props"></param>
        public LocalPropertyGridObject(LocalProperties _props)
        {
            _properties = _props;
            if (_properties == null)
            {
                _properties = new LocalProperties();
            }
        }
        public void setValue(string tag, object value)
        {
            if( tag != null) _properties[tag].Value = value;
        }

        #region Implementation of ICustomTypeDescriptor
        public PropertyDescriptorCollection GetProperties()
        {
            // of course, here must be the attributes associated
            // with each of the dynamic properties
            var properties = _properties
               .Select(pair => new DynamicPropertyDescriptor(this,
                   pair.Key, pair.Value.Value.GetType(), pair.Value));
            return new PropertyDescriptorCollection(properties.ToArray());
        }
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return this.GetProperties();
        }
        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }
        public Object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
        public EventDescriptorCollection GetEvents()
        {
            return EventDescriptorCollection.Empty;
        }
        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return EventDescriptorCollection.Empty;
        }
        public Object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }
        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }
        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }
        public string GetComponentName()
        {
            return null;
        }
        public AttributeCollection GetAttributes()
        {
            return AttributeCollection.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetClassName()
        {
            return GetType().Name;
        }
        #endregion

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            var eventArgs = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, eventArgs);
        }
        private void NotifyToRefreshAllProperties()
        {
            OnPropertyChanged(String.Empty);
        }
        #endregion

        /// <summary>
        /// ////
        /// </summary>
        private class DynamicPropertyDescriptor : PropertyDescriptor
        {
            private readonly LocalPropertyGridObject _propertyGridObject;
            private readonly LocalProperty _propertyObject;
            private readonly Type _propertyType;
            public DynamicPropertyDescriptor(LocalPropertyGridObject propertyGridObject,
                string propertyName, Type propertyType, LocalProperty propertyObject)
                : base(propertyName, null)
            {
                this._propertyGridObject = propertyGridObject;
                this._propertyObject = propertyObject;
                this._propertyType = propertyType;
                //
                var attributes = new List<Attribute>();
                if (propertyObject.GetOptions().Count > 0
                    && !(_propertyObject.ValueType() == typeof(DateTime)
                          || _propertyObject.ValueType() == typeof(System.Drawing.Color)))
                {
                    //don't use this for the Datetime and color value
                    // system will create them by default.
                    attributes.Add(new TypeConverterAttribute(typeof(OptionTypeConverter)));
                }
                attributes.Add(new DisplayNameAttribute(_propertyObject.DisplayName == null ?
                                                        _propertyObject.Name : _propertyObject.DisplayName));
                attributes.Add(new CategoryAttribute(_propertyObject.Group));
                attributes.Add(new BrowsableAttribute(_propertyObject.Display));
                attributes.Add(new DescriptionAttribute(_propertyObject.Description));
                this.AttributeArray = attributes.ToArray();
            }
            public override string Name
            {
                get
                {
                    return base.Name;
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public LocalProperty GetPropertyObject()
            {
                return _propertyObject;
            }
            /// <summary>
            /// Can the propert value be resetable
            /// </summary>
            /// <param name="component"></param>
            /// <returns></returns>
            public override bool CanResetValue(object component)
            {
                return true;
            }
            /// <summary>
            /// Get the property value
            /// </summary>
            /// <param name="component"></param>
            /// <returns></returns>
            public override object GetValue(object component)
            {
                return _propertyGridObject._properties[Name].Value;
            }
            /// <summary>
            /// reset the property value
            /// </summary>
            /// <param name="component"></param>
            public override void ResetValue(object component)
            {
                _propertyGridObject._properties[Name].Value = null;
            }
            /// <summary>
            /// set the property value
            /// </summary>
            /// <param name="component"></param>
            /// <param name="value"></param>
            public override void SetValue(object component, object value)
            {
                _propertyGridObject._properties[Name].Value = value;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="component"></param>
            /// <returns></returns>
            public override bool ShouldSerializeValue(object component)
            {
                return false;
            }
            /// <summary>
            /// return the Compnenet Type
            /// </summary>
            public override Type ComponentType
            {
                get { return typeof(LocalPropertyGridObject); }
            }
            /// <summary>
            /// is the property readonly
            /// </summary>
            public override bool IsReadOnly
            {
                get { return _propertyObject.Readonly; }
            }
            /// <summary>
            /// return the property type
            /// </summary>
            public override Type PropertyType
            {
                get { return _propertyType; }
            }
        }
        /// <summary>
        /// Option type convertor, used to display the dropdown list
        /// </summary>
        private class OptionTypeConverter : TypeConverter
        {
            public OptionTypeConverter()
            {
            }
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                DynamicPropertyDescriptor _context
                    = context.PropertyDescriptor as DynamicPropertyDescriptor;
                return new StandardValuesCollection(_context.GetPropertyObject().GetOptions());
            }
            public override object ConvertFrom(ITypeDescriptorContext context,
                                               System.Globalization.CultureInfo culture,
                                               object value)
            {
                //convert to string
                DynamicPropertyDescriptor _context
                    = context.PropertyDescriptor as DynamicPropertyDescriptor;
                if (_context.GetPropertyObject().Value.GetType()
                    != value.GetType())
                {
                    //convert to string;
                    return value.ToString();
                }
                else
                {
                    return base.ConvertFrom(context, culture, value);
                }
            }
            public override object ConvertTo(ITypeDescriptorContext context,
                                             System.Globalization.CultureInfo culture,
                                             object value,
                                             Type destinationType)
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    } //end of class LocalPropertyGridObject
}
