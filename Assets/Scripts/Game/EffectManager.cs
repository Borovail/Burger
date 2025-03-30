using UnityEngine;
using Utils;

namespace DefaultNamespace
{
    public class EffectManager : Singleton<EffectManager>
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;

        public AudioClip TakeOrderAudio;
        public AudioClip SellOrderAudio;
        public AudioClip KnifeStubAudio;
        public AudioClip RatDeathAudio;

        public ParticleSystem SellOrderParticle;
        public ParticleSystem RatDeathParticle;

        protected override void Awake()
        {
            base.Awake();
            _musicSource.Play();
        }

        public EffectManager PlaySfx(AudioClip audio)
        {
            _sfxSource.PlayOneShot(audio);
            return this;
        }

        public EffectManager PlayParticles(ParticleSystem particle,Vector3 position)
        {
            particle.transform.position = position;
            particle.Play();
            return this;
        }
    }
}