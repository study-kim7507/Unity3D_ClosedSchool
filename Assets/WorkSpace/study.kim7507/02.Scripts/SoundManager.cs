using UnityEngine;
using UnityEngine.Audio;

public class AudioSourceUpdater : MonoBehaviour
{
    public AudioMixerGroup audioMixerGroup; // 오디오 믹서 그룹을 드래그하여 할당

    void Start()
    {
        // 모든 AudioSource를 찾고 Output을 변경
        AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.outputAudioMixerGroup = audioMixerGroup;
        }
    }
}
