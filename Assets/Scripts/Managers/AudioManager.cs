using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SpaceShooter.Managers
{
    public class AudioManager : MonoBehaviour
    {
        #region --------------- Private Variables ---------------

        #region ----- Serialized Fields -----
        [SerializeField] private List<AudioSource> sfxAudioSourceList = null;
        #endregion ----------------------

        #region ----- Non-Serialized Fields -----
        private static AudioManager _instance;
        #endregion ----------------------

        #endregion --------------------------------------------------------

        #region --------------- Public Variables ---------------
        [HideInInspector] public static AudioManager Instance { get { return _instance; } }
        public List<AudioClip> sfxClips;
        public List<AudioClip> musicClips;
        private AudioSource musicAudioSource = null;
        #endregion --------------------------------------------------------


        #region --------------- Private Methods ---------------

        #region ----- Monobehaviour Methods -----
        private void Awake()
        {
            AudioListener.pause = false;
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            musicAudioSource = gameObject.AddComponent<AudioSource>();
            sfxAudioSourceList = new List<AudioSource>();
        }
        #endregion

        #region ----- Non-Monobehaviour Methods -----
        private void PlaySfxClip(AudioClip clip, float volume, bool isLooping = false)
        {
            // Debug.Log("Playing audio clip: " + clip.name);
            bool foundAudioSource = false;
            for (int k = 0; k < sfxAudioSourceList.Count; k++)
            {
                if (!sfxAudioSourceList[k].isPlaying)
                {
                    foundAudioSource = true;
                    ApplyAudioClip(clip, k, volume, isLooping);
                    break;
                }
            }
            if (!foundAudioSource)
            {
                sfxAudioSourceList.Add(this.gameObject.AddComponent<AudioSource>());
                ApplyAudioClip(clip, sfxAudioSourceList.Count - 1, volume, isLooping);
            }
        }

        private void ApplyAudioClip(AudioClip clip, int index, float volume, bool isLoop = false)
        {
            sfxAudioSourceList[index].clip = clip;
            sfxAudioSourceList[index].volume = volume;
            sfxAudioSourceList[index].loop = isLoop;
            sfxAudioSourceList[index].Play();
        }

        private void PlayMusicClip(AudioClip clip, float volume)
        {
            this.musicAudioSource.clip = clip;
            this.musicAudioSource.volume = volume;
            this.musicAudioSource.loop = true;
            this.musicAudioSource.Play();
        }
        #endregion

        #endregion

        #region --------------- Public Methods ---------------

        #region ----- Audio Event Methods -----
        public void PlayMusic()
        {
            PlayMusicClip(musicClips[0], 0.4f);
        }
        public void PlayerShooting()
        {
            PlaySfxClip(sfxClips[0], 0.5f);
        }
        public void EnemyShooting()
        {
            PlaySfxClip(sfxClips[1], 0.25f);
        }
        public void BulletCollision()
        {
            PlaySfxClip(sfxClips[2], 0.2f);
        }
        public void EnemyDestroyed()
        {
            PlaySfxClip(sfxClips[3], 0.6f);
        }
        public void PlayerDestroyed()
        {
            PlaySfxClip(sfxClips[4], 0.3f);
        }
        #endregion

        public void PlayAudio()
        {
            AudioListener.pause = false;
        }

        public void DontPlayAudio()
        {
            AudioListener.pause = true;
        }

        #endregion
    }
}

