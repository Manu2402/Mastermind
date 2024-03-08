using Aiv.Audio;
using Aiv.Fast2D;
using System.Collections.Generic;

namespace Mastermind
{
    static class GfxMgr
    {
        private static Dictionary<string, Texture> textures;
        private static Dictionary<string, AudioClip> clips;

        static GfxMgr()
        {
            textures = new Dictionary<string, Texture>();
            clips = new Dictionary<string, AudioClip>();
        }

        public static Texture AddTexture(string name, string path)
        {
            Texture t = new Texture(path);

            if (t != null)
            {
                textures[name] = t;
            }

            return t;
        }

        public static Texture GetTexture(string name)
        {
            Texture t = null;

            if (textures.ContainsKey(name))
            {
                t = textures[name];
            }

            return t;
        }

    }
}
