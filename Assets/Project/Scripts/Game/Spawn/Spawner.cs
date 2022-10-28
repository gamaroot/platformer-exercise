using System;
using System.Collections;
using UnityEngine;

namespace GamaPlatform
{
    public class Spawner : MonoBehaviour
    {
        [Header("Properties")]
        [Range(0, 2f)]
        [SerializeField] private float m_interval = 1f;
        [SerializeField] private Spawnable m_spawnablePrefabs;

        private const int SCREEN_SECTIONS = 8;

        private ResourcePool m_resourcePool;
        private CameraHandler m_cameraHandler;

        private void Awake()
        {
            this.m_cameraHandler = new CameraHandler();
            this.m_resourcePool = new ResourcePool(base.transform, this.m_spawnablePrefabs.gameObject, 20);
        }

        private void OnEnable()
        {
            StartCoroutine(this.SpawnRoutine());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            this.m_resourcePool.DeactivateAll();
        }

        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(this.m_interval);

                if (base.isActiveAndEnabled)
                {
                    for (int index = 0; index < SCREEN_SECTIONS; index++)
                    {
                        Platform spawn = this.m_resourcePool.BorrowMeObjectFromPool<Platform>();
                        spawn.transform.SetParent(base.transform, false);
                        spawn.Setup(this.DefineSpawnBounds(index));
                    }
                }
            }
        }

        private Bounds DefineSpawnBounds(int sectionIndex)
        {
            Bounds spawnBounds = this.m_cameraHandler.GetCameraBounds();
            // Divide the screen into sections
            float screenSection = (spawnBounds.Top - spawnBounds.Bottom) / SCREEN_SECTIONS;

            spawnBounds.Bottom += sectionIndex * screenSection;
            spawnBounds.Top = spawnBounds.Bottom + screenSection;

            return spawnBounds;
        }
    }
}