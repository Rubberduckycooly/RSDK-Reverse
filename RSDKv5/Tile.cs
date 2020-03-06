using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv5
{
    public class Tile
    {
		public ushort RawData
		{
			get
			{
				ushort Tile = (ushort)Index;

				Tile = SetBit(10, FlipX, Tile);
				Tile = SetBit(11, FlipY, Tile);
				Tile = SetBit(12, SolidTopA, Tile);
				Tile = SetBit(13, SolidLrbA, Tile);
				Tile = SetBit(14, SolidTopB, Tile);
				Tile = SetBit(15, SolidLrbB, Tile);

				return Tile;
			}
			set
			{
				Index = (ushort)(value & 0x3ff);
				FlipX = ((value >> 10) & 1) == 1;
				FlipY = ((value >> 11) & 1) == 1;
				SolidTopA = ((value >> 12) & 1) == 1;
				SolidLrbA = ((value >> 13) & 1) == 1;
				SolidTopB = ((value >> 14) & 1) == 1;
				SolidLrbB = ((value >> 15) & 1) == 1;
			}
		}
		public ushort Index { get; set; } = 0;
		public bool FlipX { get; set; } = false;
		public bool FlipY { get; set; } = false;
		public bool SolidTopA { get; set; } = false;
		public bool SolidLrbA { get; set; } = false;
		public bool SolidTopB { get; set; } = false;
		public bool SolidLrbB { get; set; } = false;
		private static ushort SetBit(int pos, bool Set, int Value) //Shitty Maybe, but idc, it works
		{

			// "Pos" is what bit we are changing
			// "Set" tells it to be either on or off
			// "Value" is the value you want as your source

			if (Set) Value |= 1 << pos;
			if (!Set) Value &= ~(1 << pos);
			return (ushort)Value;
		}

		public Tile(ushort tile)
		{
			RawData = tile;
		}
	}
}
