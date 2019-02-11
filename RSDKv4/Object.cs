using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv4
{
    public class Object
    {
        public int type;
        public int subtype;
        public int xPos;
        public int yPos;
        static int cur_obj_id = 0;
        int id;

    public Object(int type, int subtype, int xPos, int yPos) : this(type, subtype, xPos, yPos, cur_obj_id++)
    {            
    }

    private Object(int type, int subtype, int xPos, int yPos, int id)
    {
        this.type = type;
        this.subtype = subtype;
        this.xPos = xPos;
        this.yPos = yPos;
        this.id = id;
    }

    public int getType() { return this.type; }
    public void setType(int type) { this.type = type; }

    public int getSubtype() { return this.subtype; }
    public void setSubtype(int subtype) { this.subtype = subtype; }

    public int getXPos() { return this.xPos; }
    public void setXPos(int xPos) { this.xPos = xPos; }

    public int getYPos() { return this.yPos; }
    public void setYPos(int yPos) { this.yPos = yPos; }

}


}
