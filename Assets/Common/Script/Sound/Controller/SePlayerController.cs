using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SePlayerController : MonoBehaviour, ISePlayer
{
  [SerializeField]
  int initAudioSourceNum = 3;//初期化で用意するプール

  List<AudioSource> audioSourcePool = new List<AudioSource>();
  Dictionary<string, AudioClip> audioClipDict = new Dictionary<string, AudioClip>();

  void Awake()
  {
    for(int i = 0; i < initAudioSourceNum; i++)
    {
      audioSourcePool.Add(gameObject.AddComponent<AudioSource>());
    }
  }

  //******************************************************
  //LoadAudioClip
  //AudioClipをロードする。一度ロードしたデータはキャッシュする
  //******************************************************
  AudioClip LoadAudioClip(string path)
  {
    if (audioClipDict.ContainsKey(path))
    {
      return audioClipDict[path];
    }

    var clip = Resources.Load<AudioClip>(path);
    audioClipDict.Add(path, clip);

    return clip;
  }

  void ISePlayer.Play(string sePath)
  {
    int idx = SearchAudioSourcePoolEmpty();

    var audioSource = audioSourcePool[idx];

    audioSource.clip = LoadAudioClip(sePath);
    audioSource.volume = 1.0f;
    audioSource.loop = false;
    audioSource.Play();
  }

  int SearchAudioSourcePoolEmpty()
  {
    for(int i = 0; i < audioSourcePool.Count;i++)
    {
      if(!audioSourcePool[i].isPlaying)
      {
        return i;
      }
    }

    audioSourcePool.Add(gameObject.AddComponent<AudioSource>());

    return audioSourcePool.Count - 1;
  }
}
