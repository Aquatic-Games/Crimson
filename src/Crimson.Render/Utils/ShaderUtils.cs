using Crimson.Core;
using grabs.Graphics;
using grabs.ShaderCompiler;

namespace Crimson.Render.Utils;

internal static class ShaderUtils
{
    public static void LoadGraphicsShader(Device device, string name, out ShaderModule? vertex, out ShaderModule? pixel)
    {
        Logger.Trace($"Compiling shader '{name}'.");
        
        vertex = null;
        pixel = null;
        
        string hlsl = File.ReadAllText(Path.Combine("Shaders", $"{name}.hlsl"));

        string? vertexEntryPoint = null;
        string? pixelEntryPoint = null;

        int i = 0;
        while ((i = hlsl.IndexOf("#pragma", i)) >= 0)
        {
            int j = hlsl.IndexOf('\n', i);

            string line = hlsl[i..j];

            // Yes i know this is inefficient but it works so I don't care
            string[] splitLine = line.Split(' ');

            switch (splitLine[1])
            {
                case "vertex":
                    vertexEntryPoint = splitLine[2];
                    break;
                case "pixel":
                    pixelEntryPoint = splitLine[2];
                    break;
            }

            i = j;
        }

        if (vertexEntryPoint != null)
        {
            Logger.Trace("    Compiling vertex...");
            byte[] spirv = Compiler.CompileHlsl(ShaderStage.Vertex, hlsl, vertexEntryPoint);
            vertex = device.CreateShaderModule(ShaderStage.Vertex, spirv, vertexEntryPoint);
        }

        if (pixelEntryPoint != null)
        {
            Logger.Trace("    Compiling pixel...");
            byte[] spirv = Compiler.CompileHlsl(ShaderStage.Pixel, hlsl, pixelEntryPoint);
            pixel = device.CreateShaderModule(ShaderStage.Pixel, spirv, pixelEntryPoint);
        }
    }
}