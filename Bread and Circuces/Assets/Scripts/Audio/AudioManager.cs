using System;
using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Audio Elements")]
        [SerializeField] public Sprite mutedSprite = null;
        [SerializeField] public Sprite unmutedSprite = null;
        [SerializeField] public GameObject soundButton = null;

        /*[Header("Volume")]
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 0.5f;
    [SerializeField] private float savedVolume;*/

        public bool muted;

        private bool introStartedPlaying = false;
        private Sound intro;
        public Sound[] sounds;

        private void Awake()
        {

            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            DontDestroyOnLoad(gameObject);

            foreach(var sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.loop = sound.loop;
            }

            unmutedSprite = Resources.Load<Sprite>("Sprites/UI/icon_sound_on");
            mutedSprite = Resources.Load<Sprite>("Sprites/UI/icon_sound_off");
        }

        private void Update()
        {
            try
            {
                soundButton = GameObject.FindGameObjectWithTag("SoundButton");
                UpdateSoundUI();
            }
            catch (Exception)
            {
            };
        }

        /*public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        savedVolume = AudioListener.volume;
    }*/

        public void Mute()
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
            muted = true;
            UpdateSoundUI();
        }

        public void Unmute()
        {
            AudioListener.volume = 0.5f;
            //AudioListener.volume = savedVolume;
            PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
            muted = false;
            UpdateSoundUI();
        }

        public void UpdateSoundUI()
        {
            if (muted)
            {
                soundButton.transform.GetChild(0).GetComponent<Image>().sprite = mutedSprite;
            }
            else
            {
                soundButton.transform.GetChild(0).GetComponent<Image>().sprite = unmutedSprite;
            }
        }

        private void Start()
        {
            intro = Array.Find(sounds, sound => sound.name == "Fight Theme Intro");
            Play("Theme");
        }

        private void FixedUpdate()
        {
            if(!intro.source.isPlaying && introStartedPlaying)
            {
                introStartedPlaying = false;
                Play("Fight Theme");
            }
        }

        public void ChangeMusicOnBattle()
        {
            Stop("Theme");
            Play("Fight Theme Intro");
            introStartedPlaying = true;
        }

        public void ChangeMusicOnMain()
        {
            Stop("Fight Theme Intro");
            Stop("Fight Theme");
            Play("Theme");
        }

        public void Play(string name)
        {
            var sound = Array.Find(sounds, sound => sound.name == name);
            sound.source.Play();
        }

        public void Stop(string name)
        {
            var sound = Array.Find(sounds, sound => sound.name == name);
            sound.source.Stop();
        }
    }
}
