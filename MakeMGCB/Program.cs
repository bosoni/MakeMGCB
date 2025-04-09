/*
make .mgcb 
(c) mjt 2019-2025

*/
using System;
using System.IO;

namespace MakeMGCB
{
    class Program
    {
        static bool vcompress = false, vcompressTextures = false, vgenerateMipmaps = false, vtangent = false;

        const int ALL = 0, FBX = 1, FX = 2;

        static void Main(string[] args)
        {
            Console.WriteLine("make .mgcb (c) mjt, 2019-2025");
            Console.WriteLine("Creates Content.mgcb, Content_fbx.mgcb and Content_fx.mgcb files.");
            Console.WriteLine(@"Parameters:
 -compress       (Brotli compression)
 -compresstex    compress textures
 -mipmap         generate mipmaps
 -tangent        generate tangents
 ");

            foreach (string s in args)
            {
                if (s.Contains("-compresstex")) vcompressTextures = true;
                else if (s.Contains("-compress")) vcompress = true;
                else if (s.Contains("-mipmap")) vgenerateMipmaps = true;
                else if (s.Contains("-tangent")) vtangent = true;
            }

            string compress = vcompress ? "True\n/compression:Brotli" : "False";
            string compressTextures = vcompressTextures ? "Compressed\n/compression:Brotli" : "Color";
            string generateMipmaps = vgenerateMipmaps ? "True" : "False";
            string tangent = vtangent ? "True" : "False";

            string[] allFiles = Directory.GetFiles(".", "*.*", SearchOption.AllDirectories);

            for (int q = 0; q < 3; q++)
            {
                int mode = q;
                string fileName = "";

                if (q == 0)
                    fileName = "Content.mgcb";
                if (q == 1)
                    fileName = "Content_fbx.mgcb";
                if (q == 2)
                    fileName = "Content_fx.mgcb";

                string str = @"
#----------------------------- Global Properties ----------------------------#

/outputDir:bin/$(Platform)
/intermediateDir:obj/$(Platform)
/platform:DesktopGL
/config:
#/profile:Reach
/profile:HiDef
/compress:" + compress + @"

#-------------------------------- References --------------------------------#

/reference:../Lib/SkinnedAnimationPipeline.dll
/reference:../Lib/SkinnedAnimationAux.dll

#---------------------------------- Content ---------------------------------#

";

                foreach (string file in allFiles)
                {
                    string lfile = file.ToLower();
                    lfile = lfile.Replace('\\', '/');

                    if (mode == ALL)
                    {

                        if (lfile.EndsWith(".spritefont"))
                        {
                            str += @"
#begin " + file + @"
/importer:FontDescriptionImporter
/processor:FontDescriptionProcessor
/processorParam:PremultiplyAlpha=True
/processorParam:TextureFormat=" + compressTextures + @"
/build:" + file + "\n";
                            continue;
                        }


                        if (lfile.EndsWith(".png") || lfile.EndsWith(".jpg") || lfile.EndsWith(".dds"))
                        {
                            str += @"
#begin " + file + @"
/importer:TextureImporter
/processor:TextureProcessor
/processorParam:ColorKeyColor=255,0,255,255
/processorParam:ColorKeyEnabled=True
/processorParam:GenerateMipmaps=" + generateMipmaps + @"
/processorParam:PremultiplyAlpha=True
/processorParam:ResizeToPowerOfTwo=False
/processorParam:MakeSquare=False
/processorParam:TextureFormat=" + compressTextures + @"
/build:" + file + "\n";
                            continue;
                        }

                        if (lfile.EndsWith(".wav") || lfile.EndsWith(".ogg"))
                        {
                            string sndImporter;
                            if (lfile.EndsWith(".wav"))
                                sndImporter = "/importer:WavImporter";
                            else
                                sndImporter = "/importer:OggImporter";

                            if (!lfile.Contains("music/"))
                            {
                                str += @"
#begin " + file + "\n" + sndImporter + @"
/processor:SoundEffectProcessor
/processorParam:Quality=Best
/build:" + file + "\n";
                                continue;
                            }
                            else
                            {
                                str += @"
#begin " + file + "\n" + sndImporter + @"
/processor:SongProcessor
/processorParam:Quality=Best
/build:" + file + "\n";
                                continue;
                            }
                        }
                    }

                    if (mode == ALL || mode == FX)
                        if (lfile.EndsWith(".fx"))
                        {
                            str += @"
#begin " + file + @"
/importer:EffectImporter
/processor:EffectProcessor
/processorParam:DebugMode=Auto
/build:" + file + "\n";
                            continue;
                        }

                    if (mode == ALL || mode == FBX)
                        if (lfile.EndsWith(".fbx"))
                            if (!lfile.Contains("animations/"))
                            {
                                str += @"
#begin " + file + @"
/importer:FbxImporter
/processor:ModelProcessor
/processorParam:ColorKeyColor=0,0,0,0
/processorParam:ColorKeyEnabled=True
/processorParam:DefaultEffect=BasicEffect
/processorParam:GenerateMipmaps=" + generateMipmaps + @"
/processorParam:GenerateTangentFrames=" + tangent + @"
/processorParam:PremultiplyTextureAlpha=True
/processorParam:PremultiplyVertexColors=True
/processorParam:ResizeTexturesToPowerOfTwo=False
/processorParam:RotationX=0
/processorParam:RotationY=0
/processorParam:RotationZ=0
/processorParam:Scale=1
/processorParam:SwapWindingOrder=False
/processorParam:TextureFormat=" + compressTextures + @"
/build:" + file + "\n";
                                continue;
                            }
                            else
                            {
                                str += @"
#begin " + file + @"
/importer:FbxImporter
/processor:AnimationProcessor
/processorParam:ColorKeyColor=0,0,0,0
/processorParam:ColorKeyEnabled=True
/processorParam:DefaultEffect=SkinnedEffect
/processorParam:GenerateMipmaps=" + generateMipmaps + @"
/processorParam:GenerateTangentFrames=" + tangent + @"
/processorParam:PremultiplyTextureAlpha=True
/processorParam:PremultiplyVertexColors=True
/processorParam:ResizeTexturesToPowerOfTwo=False
/processorParam:RotationX=0
/processorParam:RotationY=0
/processorParam:RotationZ=0
/processorParam:Scale=1
/processorParam:SwapWindingOrder=False
/processorParam:TextureFormat=" + compressTextures + @"
/build:" + file + "\n";
                                continue;
                            }
                }

                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.WriteLine(str);
                    Console.WriteLine("File " + fileName + " saved.");
                }
            }
        }
    }
}
