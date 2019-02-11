using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RSDKvB
{
    public class BitmapFont
    {

        public class Header
        {
            string IntroFace = "";
            /// <summary>
            /// the font size
            /// </summary>
            public int Size;
            /// <summary>
            /// is the font bold?
            /// </summary>
            public bool Bold;
            /// <summary>
            /// is the font in italics?
            /// </summary>
            public bool Italic;
            public string CharSet = "";
            public int Unicode;
            public int StretchH;
            public bool Smooth;
            public bool AntiAlias;
            public System.Drawing.Rectangle Padding = new System.Drawing.Rectangle(); 
            public System.Drawing.Point Spacing = new System.Drawing.Point();
            public int CommonLineHeight;
            public int Base;
            public int ScaleW;
            public int ScaleH;
            public int Pages;
            public bool Packed;
            public int PageID;
            /// <summary>
            /// the file name for the sheet to display the font
            /// </summary>
            public string Filename = "";
            public int CharCount;

            public Header()
            {

            }

            public Header(StreamReader reader)
            {
                char[] Tmp = new char[100];
                string filler = "";

                //IntroFace
                string size = "";
                string bold = "";
                string italic = "";
                //CharSet
                string unicode = "";
                string stretchH = "";
                string smooth = "";
                string antialias = "";
                string paddingx = "";
                string paddingy = "";
                string paddingw = "";
                string paddingh = "";
                string spacingx = "";
                string spacingy = "";
                string commonlineheight = "";
                string BASE = "";
                string scaleW = "";
                string scaleH = "";
                string pages = "";
                string packed = "";
                string pageID = "";
                //Filename
                string charcount = "";

                bool continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '"')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//Face

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == '"')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        IntroFace = IntroFace + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                } //Size=
                continueRead = true;

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        size = size + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//bold=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        bold = bold + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//italic=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        italic = italic + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '"')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//CharSet=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == '"')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        CharSet = CharSet + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//Unicode=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        unicode = unicode + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//StretchH=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        stretchH = stretchH + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//Smooth=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        smooth = smooth + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//AA=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        antialias = antialias + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//PaddingX=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ',')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        paddingx = paddingx + read;
                    }
                }
                continueRead = true; 
                
                //PaddingY=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ',')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        paddingy = paddingy + read;
                    }
                }
                continueRead = true; 
                
                //PaddingW=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ',')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        paddingw = paddingw + read;
                    }
                }
                continueRead = true; 
                
                //PaddingH=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        paddingh = paddingh + read;
                    }
                }
                continueRead = true;

                reader.ReadBlock(Tmp, 0, 8); //SpacingX=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ',')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        spacingx = spacingy + read;
                    }
                }
                continueRead = true; 
                
                //SpacingY=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == 'c')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        spacingy = spacingy + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//Lineheight=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        commonlineheight = commonlineheight + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//BASE=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        BASE = BASE + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//ScaleW=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        scaleW = scaleW + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//ScaleH=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        scaleH = scaleH + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//Pages=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        pages = pages + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//packed=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == 'p')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        packed = packed + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//pageID=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        pageID = pageID + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '"')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//FilePath=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == '"')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        Filename = Filename + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true; //CharCount =

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == 'c')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        charcount = charcount + read;
                    }
                }
                continueRead = true;

                reader.BaseStream.Position -= 1;

                Size = Int32.Parse(size);
                Bold = Int32.Parse(bold) != 0;
                Italic = Int32.Parse(italic) != 0;
                Unicode = Int32.Parse(unicode);
                StretchH = Int32.Parse(stretchH);
                Smooth = Int32.Parse(smooth) != 0;
                AntiAlias = Int32.Parse(antialias) != 0;
                Padding.X = Int32.Parse(paddingx);
                Padding.Y = Int32.Parse(paddingy);
                Padding.Width = Int32.Parse(paddingw);
                Padding.Height = Int32.Parse(paddingh);
                Spacing.X = Int32.Parse(spacingx);
                Spacing.Y = Int32.Parse(spacingy);
                CommonLineHeight = Int32.Parse(commonlineheight);
                Base = Int32.Parse(BASE);
                ScaleW = Int32.Parse(scaleW);
                ScaleH = Int32.Parse(scaleH);
                Pages = Int32.Parse(pages);
                Packed = Int32.Parse(packed) != 0;
                PageID = Int32.Parse(pageID);
                CharCount = Int32.Parse(charcount);
            }

            public void Write(StreamWriter writer)
            {
                int b = 0; if (Bold) b = 1;
                int i = 0; if (Italic) i = 1;
                int s = 0; if (Smooth) s = 1;
                int a = 0; if (AntiAlias) a = 1;
                int p = 0; if (Packed) p = 1;

                writer.Write("info face=" + '"' + IntroFace +'"');
                writer.Write(" size=" + Size);
                writer.Write(" bold=" + b);
                writer.Write(" italic=" + i);
                writer.Write(" charset=" + '"' + CharSet + '"');
                writer.Write(" unicode=" + Unicode);
                writer.Write(" stretchH=" + StretchH);
                writer.Write(" smooth=" + s);
                writer.Write(" aa=" + a);
                writer.Write(" padding=" + Padding.X + "," + Padding.Y + "," + Padding.Width + "," + Padding.Height);
                writer.Write(" spacing=" + Spacing.X + "," + Spacing.Y + Environment.NewLine);
                writer.Write("common");
                writer.Write(" lineHeight=" + CommonLineHeight);
                writer.Write(" base=" + Base);
                writer.Write(" scaleW=" + ScaleW);
                writer.Write(" scaleH=" + ScaleH);
                writer.Write(" pages=" + Pages);
                writer.Write(" packed=" + p + Environment.NewLine);
                writer.Write("page id = " + PageID);
                writer.Write(" file=" + '"' + IntroFace + '"' + Environment.NewLine);
                writer.Write("chars count=" + CharCount);

            }

        }

        public class Character
        {
            public int CharID;
            public System.Drawing.Rectangle SrcRect = new System.Drawing.Rectangle();
            public System.Drawing.Point Offsets = new System.Drawing.Point();
            public int XAdvance;
            public int Page;
            public int Channel;
            public string Letter;

            public Character()
            {

            }

            public Character(StreamReader reader)
            {
                char[] Tmp = new char[100];
                string filler = "";

                string charID = "";
                string srcx = "";
                string srcy = "";
                string srcw = "";
                string srch = "";
                string offsetx = "";
                string offsety = "";
                string xadvance = "";
                string page = "";
                string channel = "";

                bool continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//Char ID=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        charID = charID + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//x=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        srcx = srcx + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//y=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        srcy = srcy + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//width=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        srcw = srcw + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//height=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        srch = srch + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//xoffset=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        offsetx = offsetx + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//yoffset=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        offsety = offsety + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//xadvance=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        xadvance = xadvance + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//page=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        page = page + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '=')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//chnl=

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == ' ')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        channel = channel + read;
                    }
                }
                continueRead = true;

                while (continueRead) //Filler
                {
                    char read = (char)reader.Read();
                    if (read == '"')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        filler = filler + read;
                    }
                }
                continueRead = true;//letter="

                while (continueRead)
                {
                    char read = (char)reader.Read();
                    if (read == '"')
                    {
                        continueRead = false;
                        break;
                    }
                    else
                    {
                        Letter = Letter + read;
                    }
                }
                continueRead = true;

                CharID = Int32.Parse(charID);
                SrcRect.X = Int32.Parse(srcx);
                SrcRect.Y = Int32.Parse(srcy);
                SrcRect.Width = Int32.Parse(srcw);
                SrcRect.Height = Int32.Parse(srch);
                Offsets.X = Int32.Parse(offsetx);
                Offsets.Y = Int32.Parse(offsety);
                XAdvance = Int32.Parse(xadvance);
                Page = Int32.Parse(page);
                Channel = Int32.Parse(channel);
            }

            public void Write(StreamWriter writer)
            {
                writer.Write("char id =");
                writer.Write(CharID);
                writer.Write("     x=");
                writer.Write(SrcRect.X);
                writer.Write("   y=");
                writer.Write(SrcRect.Y);
                writer.Write("   width=");
                writer.Write(SrcRect.Width);
                writer.Write("     height=");
                writer.Write(SrcRect.Height);
                writer.Write("     xoffset=");
                writer.Write(Offsets.X);
                writer.Write("     yoffset=");
                writer.Write(Offsets.Y);
                writer.Write("   xadvance=");
                writer.Write(XAdvance);
                writer.Write("    page=");
                writer.Write(Page);
                writer.Write(" chnl=");
                writer.Write(Channel);
                writer.Write("letter = " + '"' + Letter + '"');
                writer.Write(Environment.NewLine);
            }
        }

        public Header header = new Header();

        public List<Character> characters = new List<Character>();

        public BitmapFont()
        {

        }

        public BitmapFont(StreamReader reader)
        {
            header = new Header(reader);

            for (int c = 0; c < header.CharCount; c++)
            {
                characters.Add(new Character(reader));
            }
            Console.WriteLine("File Size: " + reader.BaseStream.Length + " Reader Pos: " + reader.BaseStream.Position + " Data Left: " + (reader.BaseStream.Length - reader.BaseStream.Position));
            reader.Close();
        }

        public void Write(StreamWriter writer)
        {
            header.Write(writer);

            for (int c = 0; c < header.CharCount; c++)
            {
                characters[c].Write(writer);
            }
            writer.Close();
        }
    }
}
