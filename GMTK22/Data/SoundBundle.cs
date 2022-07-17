using System;
using Machina.Engine;
using Microsoft.Xna.Framework.Audio;

namespace GMTK22.Data
{
    public class SoundBundle
    {
        private readonly SoundEffectInstance[] soundEffects;

        public SoundBundle(string baseName, int count)
        {
            this.soundEffects = new SoundEffectInstance[count];

            for (int i = 1; i <= count; i++)
            {
                var sound = MachinaClient.Assets.GetSoundEffectInstance(baseName + count);
                sound.Volume = 0.5f;
                this.soundEffects[i - 1] = sound;
            }
        }

        public void PlayRandom()
        {
            try
            {
                var index = Math.Abs(MachinaClient.RandomDirty.Next()) % this.soundEffects.Length;
                this.soundEffects[index].Stop();
                this.soundEffects[index].Play();
            }
            catch(InstancePlayLimitException)
            {
                
            }
        }
    }
}
