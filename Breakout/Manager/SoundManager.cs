using Microsoft.Xna.Framework.Audio;

namespace Breakout.Manager
{
    public class SoundManager
    {
        private readonly SoundEffect _soundEffect;
        private readonly SoundEffectInstance _soundEffectInstance;

        public SoundManager(SoundEffect soundEffect)
        {
            _soundEffect = soundEffect;
            _soundEffectInstance = _soundEffect.CreateInstance();
        }

        public void PlaySound()
        {
            if (_soundEffectInstance.State != SoundState.Playing)
            {
                _soundEffectInstance.Play();
            }
        }
    }
}
