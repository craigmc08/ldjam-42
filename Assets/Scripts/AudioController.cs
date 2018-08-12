using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {

    private class AudioSources
    {
        private int number;
        private AudioClip _clip;
        private AudioMixerGroup _mixerGroup;
        private AudioSource[] sources;
        private int itx = -1;

        public AudioSources(int number, GameObject go)
        {
            this.number = number;
            sources = new AudioSource[number];
            for (int i = 0; i < number; i++)
            {
                var source = go.AddComponent<AudioSource>();
                if (clip != null) source.clip = clip;
                if (mixerGroup != null) source.outputAudioMixerGroup = mixerGroup;
                sources[i] = source;
            }
        }

        public bool Play()
        {
            if (sources == null) return false;

            itx++;
            itx = itx % number;
            sources[itx].Play();
            return true;
        }

        public AudioClip clip
        {
            get
            {
                return _clip;
            }
            set
            {
                _clip = value;
                if (sources != null)
                {
                    foreach (AudioSource source in sources)
                    {
                        source.clip = _clip;
                    }
                }
            }
        }
        public AudioMixerGroup mixerGroup
        {
            get
            {
                return _mixerGroup;
            }
            set
            {
                _mixerGroup = value;
                if (sources != null)
                {
                    foreach (AudioSource source in sources)
                    {
                        source.outputAudioMixerGroup = _mixerGroup;
                    }
                }
            }
        }
    }

    public AudioMixerGroup effectsGroup;

    public AudioClip shootSound;
    public AudioClip pickupSound;
    public AudioClip boundarySound;
    public AudioClip explosionSound;

    public int shootSoundSources = 5;
    public int pickupSoundSources = 2;
    public int boundarySoundSources = 2;

    AudioSources shootSources;
    AudioSources pickupSources;
    AudioSources boundarySources;
    AudioSources explosionSource;

    AudioSource ambientSource;

    void Awake()
    {
        ambientSource = GetComponent<AudioSource>();

        shootSources = new AudioSources(shootSoundSources, gameObject);
        pickupSources = new AudioSources(pickupSoundSources, gameObject);
        boundarySources = new AudioSources(boundarySoundSources, gameObject);
        explosionSource = new AudioSources(1, gameObject);

        shootSources.clip = shootSound;
        pickupSources.clip = pickupSound;
        boundarySources.clip = boundarySound;
        explosionSource.clip = explosionSound;

        shootSources.mixerGroup = effectsGroup;
        pickupSources.mixerGroup = effectsGroup;
        boundarySources.mixerGroup = effectsGroup;
        explosionSource.mixerGroup = effectsGroup;
    }

    void OnEnable()
    {
        //Settings.Load();
        //effectsGroup.audioMixer.SetFloat("EffectsVolume", -40f);
    }

    public void PlayShoot()
    {
        shootSources.Play();
    }
    public void PlayPickup()
    {
        pickupSources.Play();
    }
    public void PlayBoundaryBonus()
    {
        boundarySources.Play();
    }
    public void PlayExplosion()
    {
        explosionSource.Play();
    }

    public void PlayAmbient()
    {
        ambientSource.Play();
    }
    public void StopAmbient()
    {
        ambientSource.Stop();
    }
}
