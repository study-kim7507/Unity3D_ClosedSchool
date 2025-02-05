using UnityEngine;
using UnityEngine.Audio;

public class AudioSourceUpdater : MonoBehaviour
{
    public static AudioSourceUpdater instance = null;
    public AudioMixerGroup audioMixerGroup; // ����� �ͼ� �׷��� �巡���Ͽ� �Ҵ�

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(this.gameObject); // �ߺ��� �ν��Ͻ��� �ı�
        }
    }

    private void OnEnable()
    {
        // ��� AudioSource�� ã�� Output�� ����
        AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.outputAudioMixerGroup = audioMixerGroup;
        }
    }
}
