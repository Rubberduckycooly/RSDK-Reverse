using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace RSDKv5
{
    [Serializable]
    public class AttributeValue
    {
        /// <summary>
        /// the uint8 value of the attribute
        /// </summary>
        byte value_uint8;
        /// <summary>
        /// the uint16 value of the attribute
        /// </summary>
        ushort value_uint16;
        /// <summary>
        /// the uint32 value of the attribute
        /// </summary>
        uint value_uint32;
        /// <summary>
        /// the int8 value of the attribute
        /// </summary>
        sbyte value_int8;
        /// <summary>
        /// the int16 value of the attribute
        /// </summary>
        short value_int16;
        /// <summary>
        /// the int32 value of the attribute
        /// </summary>
        int value_int32;
        /// <summary>
        /// the var value of the attribute
        /// </summary>
        uint value_var;
        /// <summary>
        /// the bool value of the attribute
        /// </summary>
        bool value_bool;
        /// <summary>
        /// the string value of the attribute
        /// </summary>
        string value_string = string.Empty; // default to empty string, null causes many problems
        /// <summary>
        /// the position value of the attribute
        /// </summary>
        Position value_position;
        /// <summary>
        /// the colour value of the attribute
        /// </summary>
        Color value_color;

        public readonly AttributeTypes Type;

        Position no_position = new Position(0, 0);

        private void CheckType(AttributeTypes type)
        {
            if (type != Type)
            {
                //throw new Exception("Unexpected value type.");

                switch (type)
                {
                    case AttributeTypes.UINT8:
                        value_uint8 = 0;
                        break;
                    case AttributeTypes.UINT16:
                        value_uint16 = 0;
                        break;
                    case AttributeTypes.UINT32:
                        value_uint32 = 0;
                        break;
                    case AttributeTypes.INT8:
                        value_int8 = 0;
                        break;
                    case AttributeTypes.INT16:
                        value_int16 = 0;
                        break;
                    case AttributeTypes.INT32:
                        value_int32 = 0;
                        break;
                    case AttributeTypes.VAR:
                        value_var = 0;
                        break;
                    case AttributeTypes.BOOL:
                        value_bool = false;
                        break;
                    case AttributeTypes.COLOR:
                        value_color = Color.EMPTY;
                        break;
                    case AttributeTypes.POSITION:
                        value_position = no_position;
                        break;
                    case AttributeTypes.STRING:
                        value_string = string.Empty;
                        break;
                    default:
                        throw new Exception("Unexpected value type.");

                }
            }
        }
        public byte ValueUInt8
        {
            get { CheckType(AttributeTypes.UINT8); return value_uint8; }
            set { CheckType(AttributeTypes.UINT8); value_uint8 = value; }
        }
        public ushort ValueUInt16
        {
            get { CheckType(AttributeTypes.UINT16); return value_uint16; }
            set { CheckType(AttributeTypes.UINT16); value_uint16 = value; }
        }
        public uint ValueUInt32
        {
            get { CheckType(AttributeTypes.UINT32); return value_uint32; }
            set { CheckType(AttributeTypes.UINT32); value_uint32 = value; }
        }
        public sbyte ValueInt8
        {
            get { CheckType(AttributeTypes.INT8); return value_int8; }
            set { CheckType(AttributeTypes.INT8); value_int8 = value; }
        }
        public short ValueInt16
        {
            get { CheckType(AttributeTypes.INT16); return value_int16; }
            set { CheckType(AttributeTypes.INT16); value_int16 = value; }
        }
        public int ValueInt32
        {
            get { CheckType(AttributeTypes.INT32); return value_int32; }
            set { CheckType(AttributeTypes.INT32); value_int32 = value; }
        }
        public uint ValueVar
        {
            get { CheckType(AttributeTypes.VAR); return value_var; }
            set { CheckType(AttributeTypes.VAR); value_var = value; }
        }
        public bool ValueBool
        {
            get { CheckType(AttributeTypes.BOOL); return value_bool; }
            set { CheckType(AttributeTypes.BOOL); value_bool = value; }
        }
        public string ValueString
        {
            get { CheckType(AttributeTypes.STRING); return value_string; }
            set { CheckType(AttributeTypes.STRING); value_string = value; }
        }
        public Position ValuePosition
        {
            get { CheckType(AttributeTypes.POSITION); return value_position; }
            set { CheckType(AttributeTypes.POSITION); value_position = value; }
        }
        public Color ValueColor
        {
            get { CheckType(AttributeTypes.COLOR); return value_color; }
            set { CheckType(AttributeTypes.COLOR); value_color = value; }
        }

        public AttributeValue(AttributeTypes type)
        {
            Type = type;
        }

        public AttributeValue Clone()
        {
            AttributeValue n = new AttributeValue(Type);

            n.value_uint8 = value_uint8;
            n.value_uint16 = value_uint16;
            n.value_uint32 = value_uint32;
            n.value_int8 = value_int8;
            n.value_int16 = value_int16;
            n.value_int32 = value_int32;
            n.value_var = value_var;
            n.value_bool = value_bool;
            n.value_string = value_string;
            n.value_position = value_position;
            n.value_color = value_color;

            return n;
        }

        internal AttributeValue(Reader reader, AttributeTypes type)
        {
            Type = type;
            Read(reader);
        }

        internal void Read(Reader reader)
        {
            switch (Type)
            {
                case AttributeTypes.UINT8:
                    value_uint8 = reader.ReadByte();
                    break;
                case AttributeTypes.UINT16:
                    value_uint16 = reader.ReadUInt16();
                    break;
                case AttributeTypes.UINT32:
                    value_uint32 = reader.ReadUInt32();
                    break;
                case AttributeTypes.INT8:
                    value_int8 = reader.ReadSByte();
                    break;
                case AttributeTypes.INT16:
                    value_int16 = reader.ReadInt16();
                    break;
                case AttributeTypes.INT32:
                    value_int32 = reader.ReadInt32();
                    break;
                case AttributeTypes.VAR:
                    value_var = reader.ReadUInt32();
                    break;
                case AttributeTypes.BOOL:
                    value_bool = reader.ReadUInt32() != 0;
                    break;
                case AttributeTypes.STRING:
                    value_string = reader.ReadRSDKUnicodeString();
                    break;
                case AttributeTypes.POSITION:
                    value_position = new Position(reader);
                    break;
                case AttributeTypes.COLOR:
                    value_color = new Color(reader);
                    break;
            }
        }

        internal void Write(Writer writer)
        {
            switch (Type)
            {
                case AttributeTypes.UINT8:
                    writer.Write(value_uint8);
                    break;
                case AttributeTypes.UINT16:
                    writer.Write(value_uint16);
                    break;
                case AttributeTypes.UINT32:
                    writer.Write(value_uint32);
                    break;
                case AttributeTypes.INT8:
                    writer.Write(value_int8);
                    break;
                case AttributeTypes.INT16:
                    writer.Write(value_int16);
                    break;
                case AttributeTypes.INT32:
                    writer.Write(value_int32);
                    break;
                case AttributeTypes.VAR:
                    writer.Write(value_var);
                    break;
                case AttributeTypes.BOOL:
                    writer.Write((uint)(value_bool ? 1 : 0));
                    break;
                case AttributeTypes.STRING:
                    writer.WriteRSDKUnicodeString(value_string);
                    break;
                case AttributeTypes.POSITION:
                    value_position.Write(writer);
                    break;
                case AttributeTypes.COLOR:
                    value_color.Write(writer);
                    break;
            }
        }

        public override string ToString()
        {
            switch (Type)
            {
                case AttributeTypes.UINT8:
                    return value_uint8.ToString();
                case AttributeTypes.UINT16:
                    return value_uint16.ToString();
                case AttributeTypes.UINT32:
                    return value_uint32.ToString();
                case AttributeTypes.INT8:
                    return value_int8.ToString();
                case AttributeTypes.INT16:
                    return value_int16.ToString();
                case AttributeTypes.INT32:
                    return value_int32.ToString();
                case AttributeTypes.VAR:
                    return value_var.ToString();
                case AttributeTypes.BOOL:
                    return value_bool.ToString();
                case AttributeTypes.STRING:
                    return value_string.ToString();
                case AttributeTypes.POSITION:
                    return value_position.ToString();
                case AttributeTypes.COLOR:
                    return value_color.ToString();
                default:
                    return "Unhandled Type for ToString()";
            }
        }
    }
}
