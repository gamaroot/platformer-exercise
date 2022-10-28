using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResourcePool
{
    public int PoolSize = 1;
    public int ExpandedPoolSize = 1;
    
    [SerializeField] private GameObject resourcePrefab;

    private string Name => $"{this.resourcePrefab.name}'s Pool";

    private readonly Stack<GameObject> Pool = new Stack<GameObject>();
    private readonly Transform parentGameObject;

    public ResourcePool(GameObject resourcePrefab)
    {
        this.resourcePrefab = resourcePrefab;

        var node = new GameObject(this.Name);

        this.parentGameObject = node.transform;

        this.AddObjectsToPool(PoolSize);
    }

    public ResourcePool(Transform parentGameObject, GameObject resourcePrefab)
    {
        this.resourcePrefab = resourcePrefab;

        var node = new GameObject(this.Name);
        node.transform.SetParent(parentGameObject);

        this.parentGameObject = node.transform;
        
        this.AddObjectsToPool(PoolSize);
    }

    public ResourcePool(Transform parentGameObject, GameObject resourcePrefab, int poolSize)
    {
        this.resourcePrefab = resourcePrefab;

        var node = new GameObject(this.Name);
        node.transform.SetParent(parentGameObject);

        this.parentGameObject = node.transform;
        this.PoolSize = poolSize;
        
        this.AddObjectsToPool(PoolSize);
    }

    public AudioSource BorrowMeAudioFromPool()
    {
        if (this.Pool.Count == 0)
        {
            this.ExpandPoolSize();
        }

        GameObject hereToYou = this.Pool.Pop();

        AudioSource audioSource = hereToYou.GetComponent<AudioSource>();
        PoolingObject poolingObject = hereToYou.GetComponent<PoolingObject>();

        poolingObject.ScheduleAutoDisableInSeconds(audioSource.clip.length);

        hereToYou.SetActive(true);

        return audioSource;
    }

    public ParticleSystem BorrowMeVFXFromPool()
    {
        if (this.Pool.Count == 0)
        {
            this.ExpandPoolSize();
        }

        GameObject hereToYou = this.Pool.Pop();

        ParticleSystem fx = hereToYou.GetComponent<ParticleSystem>();
        PoolingObject poolingObject = hereToYou.GetComponent<PoolingObject>();

        poolingObject.ScheduleAutoDisableInSeconds(fx.main.duration);

        hereToYou.SetActive(true);

        return fx;
    }

    public T BorrowMeObjectFromPool<T>(float expirationTimeInSeconds = -1f)
    {
        if (this.Pool.Count == 0)
        {
            this.ExpandPoolSize();
        }

        GameObject hereToYou = this.Pool.Pop();

        if (expirationTimeInSeconds > 0)
        {
            PoolingObject poolingObject = hereToYou.GetComponent<PoolingObject>();
            poolingObject.ScheduleAutoDisableInSeconds(expirationTimeInSeconds);
        }

        hereToYou.SetActive(true);

        return hereToYou.GetComponent<T>();
    }

    public void SendBackToThePool(GameObject giveMeBack)
    {
        giveMeBack.transform.SetParent(this.parentGameObject, true);

        this.Pool.Push(giveMeBack);
    }

    public void DeactivateAll()
    {
        for (int index = 0; index < this.Pool.Count; index++)
        {
            this.Pool.Peek().SetActive(false);
        }
    }

    public Transform GetParent()
    {
        return this.parentGameObject;
    }

    private void AddObjectsToPool(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject newResourceObject = MonoBehaviour.Instantiate(this.resourcePrefab);
            newResourceObject.name += i;
            newResourceObject.transform.SetParent(this.parentGameObject, true);
            newResourceObject.SetActive(false);

            PoolingObject poolingObject = newResourceObject.AddComponent<PoolingObject>();
            poolingObject.SetResourcePool(this);

            this.Pool.Push(newResourceObject);
        }
    }

    private void ExpandPoolSize()
    {
        this.AddObjectsToPool(ExpandedPoolSize);
    }
}