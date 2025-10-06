using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    [System.Serializable]
    public class EffectData
    {
        public string effectName;  
        public GameObject prefab; 
    }

    [Header("등록된 이펙트 목록")]
    public List<EffectData> effects = new List<EffectData>();

    private Dictionary<string, GameObject> effectDictionary;

    void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeDictionary()
    {
        effectDictionary = new Dictionary<string, GameObject>();
        foreach (var effect in effects)
        {
            if (!effectDictionary.ContainsKey(effect.effectName))
                effectDictionary.Add(effect.effectName, effect.prefab);
        }
    }

    public void PlayEffect(string effectName, Vector3 position, float destroyTime = 2f)
    {
        if (!effectDictionary.ContainsKey(effectName))
        {
            Debug.LogWarning($"EffectManager: {effectName} 이펙트가 등록되지 않았습니다.");
            return;
        }

        GameObject prefab = effectDictionary[effectName];
        GameObject instance = Instantiate(prefab, position, Quaternion.identity);
        Destroy(instance, destroyTime);
    }

    public void PlayEffect(string effectName, Vector3 position, Quaternion rotation, float destroyTime = 2f)
    {
        if (!effectDictionary.ContainsKey(effectName))
        {
            Debug.LogWarning($"EffectManager: {effectName} 이펙트가 등록되지 않았습니다.");
            return;
        }

        GameObject prefab = effectDictionary[effectName];
        GameObject instance = Instantiate(prefab, position, rotation);
        Destroy(instance, destroyTime);
    }
}
