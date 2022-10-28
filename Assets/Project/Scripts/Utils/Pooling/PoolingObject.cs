using UnityEngine;

public class PoolingObject : MonoBehaviour
{
    private ResourcePool resourcePool;

    private float disableItSelfInSeconds = -1f;

    private void OnEnable()
    {
        this.transform.SetParent(this.resourcePool.GetParent());

        if (this.disableItSelfInSeconds > 0)
        {
            base.Invoke("Disable", this.disableItSelfInSeconds);
        }
    }

    public void ScheduleAutoDisableInSeconds(float time)
    {
        this.disableItSelfInSeconds = time;
    }

    public void SetResourcePool(ResourcePool resourcePool)
    {
        this.resourcePool = resourcePool;
    }

    public void Disable()
    {
        this.OnPoolingObjectDisable();

        base.gameObject.SetActive(false);
    }

    private void OnPoolingObjectDisable()
    {
        this.disableItSelfInSeconds = -1f;

        if (this.resourcePool != null)
        {
            this.resourcePool.SendBackToThePool(base.gameObject);
        }
    }
}