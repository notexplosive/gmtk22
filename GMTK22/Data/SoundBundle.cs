using System;
using Machina.Engine;
using Microsoft.Xna.Framework.Audio;

namespace GMTK22.Data
{
    public class SoundBundle
    {
        private readonly SoundEffectInstance[] soundEffects;
        private int sittingIndex;

        public SoundBundle(string baseName, int count)
        {
            this.soundEffects = new SoundEffectInstance[count];

            for (int i = 1; i <= count; i++)
            {
                var sound = MachinaClient.Assets.GetSoundEffectInstance(baseName + i);
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

        public PlayableSoundEffect GetNext()
        {
            var result = new PlayableSoundEffect(this.soundEffects[this.sittingIndex]);
            this.sittingIndex++;
            this.sittingIndex %= this.soundEffects.Length;
            return result;
        }
    }

    public readonly struct PlayableSoundEffect
    {
        private readonly SoundEffectInstance instance;

        public PlayableSoundEffect(SoundEffectInstance soundEffect)
        {
            this.instance = soundEffect;
        }

        public void Play()
        {
            try
            {
                this.instance.Stop();
                this.instance.Play();
            }
            catch (InstancePlayLimitException)
            {
                
            }
        }
    }
}
