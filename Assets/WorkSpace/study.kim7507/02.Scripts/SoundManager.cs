using UnityEngine;
using UnityEngine.Audio;

public class AudioSourceUpdater : MonoBehaviour
{
    public AudioMixerGroup audioMixerGroup; // ����� �ͼ� �׷��� �巡���Ͽ� �Ҵ�

    void Start()
    {
        // ��� AudioSource�� ã�� Output�� ����
        AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.outputAudioMixerGroup = audioMixerGroup;
        }
    }
}
