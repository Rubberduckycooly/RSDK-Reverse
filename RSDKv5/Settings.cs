using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace RSDKv5
{
    public class Settings
    {

        public class Game
        {

            public enum Languages
            {
                English,
            }

            public string DataFile = "data.rsdk";
            public bool DevMenu = true;
            public int Language = (int)Languages.English;

            public Game()
            {

            }

            public Game(StreamReader reader)
            {
                while(true)
                {
                    if (reader.EndOfStream)
                    {
                        return;
                    }

                    string line = reader.ReadLine();

                    if (line == "")
                    {
                        return;
                    }

                    if (line.Contains("dataFile="))
                    {
                        line = line.Replace("dataFile=", "").Replace(" ", "");
                        DataFile = line;
                    }
                    if (line.Contains("devMenu="))
                    {
                        line = line.Replace("devMenu=", "").Replace(" ", "");
                        if (line.Contains("y"))
                        {
                            DevMenu = true;
                        }
                        else
                        {
                            DevMenu = false;
                        }
                    }
                    if (line.Contains("language="))
                    {
                        line = line.Replace("language=", "").Replace(" ","");
                        Language = Int32.Parse(line);
                    }
                }
            }

            public void Write(StreamWriter writer)
            {
                writer.WriteLine("[Game]");
                writer.WriteLine("dataFile=" + DataFile);
                string val = DevMenu ? "y" : "n";
                writer.WriteLine("devMenu=" + val);
                writer.WriteLine("language=" + (byte)Language);
            }

        }

        public class Video
        {

            public bool Windowed = true;
            public bool Bordered = true;
            public bool ExclusiveFullscreen = false;
            public bool VSync = true;
            public bool TripleBuffering = true;
            public int PixelWidth = 1;
            public int WindowWidth = 424;
            public int WindowHeight = 240;
            public int FullscreenWidth = 1920;
            public int FullscreenHeight = 1080;
            public int FramesPerSecond = 60;
            public bool ShaderSupport = true;
            public int ScreenShader = 0;

            public Video()
            {

            }

            public Video(StreamReader reader)
            {
                while (true)
                {
                    if (reader.EndOfStream)
                    {
                        return;
                    }

                    string line = reader.ReadLine();

                    if (line == "")
                    {
                        return;
                    }

                    if (line.Contains("windowed="))
                    {
                        line = line.Replace("windowed=", "").Replace(" ", "");
                        if (line.Contains("y"))
                        {
                            Windowed = true;
                        }
                        else
                        {
                            Windowed = false;
                        }
                    }
                    if (line.Contains("border="))
                    {
                        line = line.Replace("border=", "").Replace(" ", "");
                        if (line.Contains("y"))
                        {
                            Bordered = true;
                        }
                        else
                        {
                            Bordered = false;
                        }
                    }
                    if (line.Contains("exclusiveFS="))
                    {
                        line = line.Replace("exclusiveFS=", "").Replace(" ", "");
                        if (line.Contains("y"))
                        {
                            ExclusiveFullscreen = true;
                        }
                        else
                        {
                            ExclusiveFullscreen = false;
                        }
                    }
                    if (line.Contains("vsync="))
                    {
                        line = line.Replace("vsync=", "").Replace(" ", "");
                        if (line.Contains("y"))
                        {
                            VSync = true;
                        }
                        else
                        {
                            VSync = false;
                        }
                    }
                    if (line.Contains("tripleBuffering="))
                    {
                        line = line.Replace("tripleBuffering=", "").Replace(" ", "");
                        if (line.Contains("y"))
                        {
                            TripleBuffering = true;
                        }
                        else
                        {
                            TripleBuffering = false;
                        }
                    }
                    if (line.Contains("pixWidth="))
                    {
                        line = line.Replace("pixWidth=", "").Replace(" ", "");
                        PixelWidth = Int32.Parse(line);
                    }
                    if (line.Contains("winWidth="))
                    {
                        line = line.Replace("winWidth=", "").Replace(" ", "");
                        WindowWidth = Int32.Parse(line);
                    }
                    if (line.Contains("winHeight="))
                    {
                        line = line.Replace("winHeight=", "").Replace(" ", "");
                        WindowHeight = Int32.Parse(line);
                    }
                    if (line.Contains("fswidth="))
                    {
                        line = line.Replace("fswidth=", "").Replace(" ", "");
                        FullscreenHeight = Int32.Parse(line);
                    }
                    if (line.Contains("fsheight="))
                    {
                        line = line.Replace("fsheight=", "").Replace(" ", "");
                        FullscreenHeight = Int32.Parse(line);
                    }
                    if (line.Contains("refreshRate="))
                    {
                        line = line.Replace("refreshRate=", "").Replace(" ", "");
                        FramesPerSecond = Int32.Parse(line);
                    }
                    if (line.Contains("shaderSupport="))
                    {
                        line = line.Replace("shaderSupport=", "").Replace(" ", "");
                        if (line.Contains("y"))
                        {
                            ShaderSupport = true;
                        }
                        else
                        {
                            ShaderSupport = false;
                        }
                    }
                    if (line.Contains("screenShader="))
                    {
                        line = line.Replace("screenShader=", "").Replace(" ", "");
                        ScreenShader = Int32.Parse(line);
                    }
                }
            }

            public void Write(StreamWriter writer)
            {
                writer.WriteLine("[Video]");
                writer.WriteLine("; NB: Fullscreen Resolution can be explicitly set with values fsWidth and fsHeight");
                writer.WriteLine("; If not listed, fullscreen will just use the desktop resolution");
                string val = Windowed ? "y" : "n";
                writer.WriteLine("windowed=" + val);
                val = Bordered ? "y" : "n";
                writer.WriteLine("border=" + val);
                val = ExclusiveFullscreen ? "y" : "n";
                writer.WriteLine("exclusiveFS=" + val);
                val = VSync ? "y" : "n";
                writer.WriteLine("vsync=" + val);
                val = TripleBuffering ? "y" : "n";
                writer.WriteLine("tripleBuffering=" + val);
                writer.WriteLine("pixWidth=" + PixelWidth);
                writer.WriteLine("winWidth=" + WindowWidth);
                writer.WriteLine("winHeight=" + WindowHeight);
                writer.WriteLine("fswidth=" + FullscreenWidth);
                writer.WriteLine("fsheight=" + FullscreenHeight);
                writer.WriteLine("refreshRate=" + FramesPerSecond);
                val = ShaderSupport ? "y" : "n";
                writer.WriteLine("shaderSupport=" + val);
                writer.WriteLine("screenShader=" + ScreenShader);
            }

        }

        public class Audio
        {

            public bool AudioEnabled = true;
            public float MusicVolume = 0.8f;
            public float SFXVolume = 1.0f;

            public Audio()
            {

            }

            public Audio(StreamReader reader)
            {

                while (true)
                {
                    if (reader.EndOfStream)
                    {
                        return;
                    }

                    string line = reader.ReadLine();

                    if (line == "")
                    {
                        return;
                    }

                    if (line.Contains("streamsEnabled="))
                    {
                        line = line.Replace("streamsEnabled=", "").Replace(" ", "");
                        if (line.Contains("y"))
                        {
                            AudioEnabled = true;
                        }
                        else
                        {
                            AudioEnabled = false;
                        }
                    }
                    if (line.Contains("streamVolume="))
                    {
                        line = line.Replace("streamVolume=", "").Replace(" ", "");
                        MusicVolume = float.Parse(line);
                    }
                    if (line.Contains("sfxVolume="))
                    {
                        line = line.Replace("sfxVolume=", "").Replace(" ", "");
                        SFXVolume = float.Parse(line);
                    }
                }
            }

            public void Write(StreamWriter writer)
            {
                writer.WriteLine("[Audio]");
                string val = AudioEnabled ? "y" : "n";
                writer.WriteLine("streamsEnabled=" + val);
                writer.WriteLine("streamVolume=" + MusicVolume);
                writer.WriteLine("sfxVolume=" + SFXVolume);
            }

        }

        public class Input
        {

            public int Up       = 0x26;
            public int Down     = 0x28;
            public int Left     = 0x25;
            public int Right    = 0x27;
            public int ButtonA  = 0x41;
            public int ButtonB  = 0x53;
            public int ButtonC  = 0x44;
            public int ButtonX  = 0x44;
            public int ButtonY  = 0x48;
            public int ButtonZ  = 0x43;
            public int Start    = 0x0D;
            public int Select   = 0x0D;

            public Input()
            {

            }

            public Input(StreamReader reader)
            {
                while (true)
                {
                    if (reader.EndOfStream)
                    {
                        return;
                    }

                    string line = reader.ReadLine();

                    if (line == "")
                    {
                        return;
                    }
                    if (line.Contains("up="))
                    {
                        line = line.Replace("up=", "").Replace(" ", "");
                        Up = Convert.ToInt32(line, 0x10);
                    }
                    if (line.Contains("down="))
                    {
                        line = line.Replace("down=", "").Replace(" ", "");
                        Down = Convert.ToInt32(line, 0x10);
                    }
                    if (line.Contains("left="))
                    {
                        line = line.Replace("left=", "").Replace(" ", "");
                        Left = Convert.ToInt32(line, 0x10);
                    }
                    if (line.Contains("right="))
                    {
                        line = line.Replace("right=", "").Replace(" ", "");
                        Right = Convert.ToInt32(line, 0x10);
                    }
                    if (line.Contains("buttonA="))
                    {
                        line = line.Replace("buttonA=", "").Replace(" ", "");
                        ButtonA = Convert.ToInt32(line, 0x10);
                    }
                    if (line.Contains("buttonB="))
                    {
                        line = line.Replace("buttonB=", "").Replace(" ", "");
                        ButtonB = Convert.ToInt32(line, 0x10);
                    }
                    if (line.Contains("buttonC="))
                    {
                        line = line.Replace("buttonC=", "").Replace(" ", "");
                        ButtonC = Convert.ToInt32(line, 0x10);
                    }
                    if (line.Contains("buttonX="))
                    {
                        line = line.Replace("buttonX=", "").Replace(" ", "");
                        ButtonX = Convert.ToInt32(line, 0x10);
                    }
                    if (line.Contains("buttonY="))
                    {
                        line = line.Replace("buttonY=", "").Replace(" ", "");
                        ButtonY = Convert.ToInt32(line, 0x10);
                    }
                    if (line.Contains("buttonZ="))
                    {
                        line = line.Replace("buttonZ=", "").Replace(" ", "");
                        ButtonZ = Convert.ToInt32(line, 0x10);
                    }
                    if (line.Contains("start="))
                    {
                        line = line.Replace("start=", "").Replace(" ", "");
                        Start = Convert.ToInt32(line, 0x10);
                    }
                    if (line.Contains("select="))
                    {
                        line = line.Replace("select=", "").Replace(" ", "");
                        Select = Convert.ToInt32(line, 0x10);
                    }
                }
            }

            public void Write(StreamWriter writer)
            {
                writer.WriteLine("up=0x" + Up.ToString("x"));
                writer.WriteLine("down=0x" + Down.ToString("x"));
                writer.WriteLine("left=0x" + Left.ToString("x"));
                writer.WriteLine("right=0x" + Right.ToString("x"));
                writer.WriteLine("buttonA=0x" + ButtonA.ToString("x"));
                writer.WriteLine("buttonB=0x" + ButtonB.ToString("x"));
                writer.WriteLine("buttonC=0x" + ButtonC.ToString("x"));
                writer.WriteLine("buttonX=0x" + ButtonX.ToString("x"));
                writer.WriteLine("buttonY=0x" + ButtonY.ToString("x"));
                writer.WriteLine("buttonZ=0x" + ButtonZ.ToString("x"));
                writer.WriteLine("start=0x" + Start.ToString("x"));
                writer.WriteLine("select=0x" + Select.ToString("x"));
            }

        }

        public Game game = new Game();
        public Video video = new Video();
        public Audio audio = new Audio();
        public List<Input> inputs = new List<Input>();

        public Settings()
        {

        }

        public Settings(StreamReader reader)
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();

                if (line.Contains("[Game]"))
                {
                    game = new Game(reader);
                }

                if (line.Contains("[Video]"))
                {
                    video = new Video(reader);
                }

                if (line.Contains("[Audio]"))
                {
                    audio = new Audio(reader);
                }

                if (line.Contains("[Keyboard Map ") && line.Contains("]"))
                {
                    inputs.Add(new Input(reader));
                }
            }
            reader.Close();
        }

        public void Write(StreamWriter writer)
        {
            writer.WriteLine("; Retro Engine Config File");
            writer.WriteLine("");
            game.Write(writer);
            writer.WriteLine("");
            video.Write(writer);
            writer.WriteLine("");
            audio.Write(writer);
            writer.WriteLine("");
            for (int i = 0; i < inputs.Count; i++)
            {
                writer.WriteLine("[Keyboard Map " + (i + 1) + "]");
                inputs[i].Write(writer);
                writer.WriteLine("");
            }
            writer.Close();
        }

    }
}
