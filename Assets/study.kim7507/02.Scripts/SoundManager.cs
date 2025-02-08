using UnityEngine;
using UnityEngine.Audio;

public class AudioSourceUpdater : MonoBehaviour
{
    public static AudioSourceUpdater instance = null;
    public AudioMixerGroup audioMixerGroup; // 오디오 믹서 그룹을 드래그하여 할당

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(this.gameObject); // 중복된 인스턴스는 파괴
        }
    }

    private void OnEnable()
    {
        // 모든 AudioSource를 찾고 Output을 변경
        AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.outputAudioMixerGroup = audioMixerGroup;
        }
    }
}
