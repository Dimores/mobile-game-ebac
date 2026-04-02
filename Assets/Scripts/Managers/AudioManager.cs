using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Ebac.Core.Singleton;

namespace Orby.Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        public enum AudioType
        {
            COINCOLLECT,
            POWERUPCOIN,
            PIECEPLACE,
            POP
        }

        public List<AudioManagerSetup> audioSetup;

        #region POOLING VARIABLES
        [Header("Pooling Configuration")]
        public int initialPoolSize = 100;
        public int expansionSize = 10;
        private List<AudioSource> audioPool = new List<AudioSource>();
        #endregion

        private void Start()
        {
            InitializePool();
        }

        #region POOLING METHODS
        private void InitializePool()
        {
            ExpandPool(initialPoolSize);
        }

        private void ExpandPool(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject audioObj = new GameObject("PooledAudioSource");
                audioObj.transform.SetParent(this.transform);
                AudioSource audioSource = audioObj.AddComponent<AudioSource>();

                audioObj.SetActive(false);
                audioPool.Add(audioSource);
            }
        }

        private AudioSource GetAudioSourceFromPool()
        {
            for (int i = 0; i < audioPool.Count; i++)
            {
                if (!audioPool[i].gameObject.activeInHierarchy)
                {
                    return audioPool[i];
                }
            }

            int previousSize = audioPool.Count;
            ExpandPool(expansionSize);

            return audioPool[previousSize];
        }

        private IEnumerator DeactivateAudioAfterTime(GameObject audioObj, float time)
        {
            yield return new WaitForSeconds(time);
            audioObj.SetActive(false);
        }
        #endregion

        #region METHODS
        //public void PlayMenuButtonHoverAudio(float volume)
        //{
        //    AudioClip clip = GetAudioClip(AudioType.MENUBUTTONHOVER);
        //    if (clip == null)
        //    {
        //        return;
        //    }

        //    GameObject audioObj = new GameObject($"Audio_MENUBUTTONHOVER");
        //    AudioSource audioSource = audioObj.AddComponent<AudioSource>();
        //    audioSource.clip = clip;
        //    audioSource.volume = volume;

        //    audioSource.Play();

        //    Destroy(audioObj, clip.length);
        //}

        public void PlayAudioByType(AudioType audioType)
        {
            AudioManagerSetup setup = GetSetup(audioType);
            if (setup == null || setup.audioClip == null) return;

            AudioSource audioSource = GetAudioSourceFromPool();
            audioSource.gameObject.name = $"Audio_{audioType}";
            audioSource.clip = setup.audioClip;
            audioSource.volume = 1f;
            audioSource.pitch = 1f;
            audioSource.outputAudioMixerGroup = setup.outputGroup;

            audioSource.gameObject.SetActive(true);
            audioSource.Play();

            StartCoroutine(DeactivateAudioAfterTime(audioSource.gameObject, setup.audioClip.length));
        }

        public void PlayAudioByType(AudioType audioType, float volume)
        {
            AudioManagerSetup setup = GetSetup(audioType);
            if (setup == null || setup.audioClip == null) return;

            AudioSource audioSource = GetAudioSourceFromPool();
            audioSource.gameObject.name = $"Audio_{audioType}";
            audioSource.clip = setup.audioClip;
            audioSource.volume = volume;
            audioSource.pitch = 1f;
            audioSource.outputAudioMixerGroup = setup.outputGroup;

            audioSource.gameObject.SetActive(true);
            audioSource.Play();

            StartCoroutine(DeactivateAudioAfterTime(audioSource.gameObject, setup.audioClip.length));
        }

        public void PlayAudioByTypeWithRandomPitch(AudioType audioType, Vector2 random, float volume)
        {
            AudioManagerSetup setup = GetSetup(audioType);
            if (setup == null || setup.audioClip == null)
            {
                Debug.LogWarning($"Áudio do tipo {audioType} não encontrado!");
                return;
            }

            AudioSource audioSource = GetAudioSourceFromPool();
            audioSource.gameObject.name = $"Audio_{audioType}";
            audioSource.clip = setup.audioClip;
            audioSource.volume = Mathf.Clamp(volume, 0f, 1f);
            audioSource.outputAudioMixerGroup = setup.outputGroup;

            if (random.x > random.y)
            {
                (random.x, random.y) = (random.y, random.x);
            }

            audioSource.pitch = Random.Range(random.x, random.y);

            audioSource.gameObject.SetActive(true);
            audioSource.Play();

            StartCoroutine(DeactivateAudioAfterTime(audioSource.gameObject, setup.audioClip.length / audioSource.pitch));
        }

        private AudioManagerSetup GetSetup(AudioType audioType)
        {
            if (audioSetup == null) return null;

            foreach (AudioManagerSetup configItem in audioSetup)
            {
                if (configItem.audioType == audioType)
                    return configItem;
            }

            return null;
        }
        #endregion
    }

    [System.Serializable]
    public class AudioManagerSetup
    {
        public AudioManager.AudioType audioType;
        public AudioClip audioClip;
        public AudioMixerGroup outputGroup;
    }
}