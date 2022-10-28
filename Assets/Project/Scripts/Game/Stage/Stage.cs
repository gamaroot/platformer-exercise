using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamaPlatform
{
    public class Stage : MonoBehaviour
    {
        [Header("Properties")]
        [Range(0, 1f)]
        [SerializeField] private float m_borderThickness = 1f;
        [Range(10, 20f)]
        [SerializeField] private float m_spawnLimitMargin = 10f;
        [SerializeField] private LayerMask m_stageBorderLayerMask;
        [SerializeField] private LayerMask m_spawnLimitLayerMask;
        [SerializeField] private LayerMask m_groundLayerMask;

        [Header("Components")]
        [SerializeField] private Transform m_cameraTransform;
        [SerializeField] private Platform m_platformPrefab;
        [SerializeField] private StageBorder m_stageBorderPrefab;
        [SerializeField] private SpawnLimitBorder m_spawnLimitBorderPrefab;

        public void CreateBorders(Bounds stageBounds, Vector2 stageSize)
        {
            for (int index = 0; index < Enum<Alignment>.Length() - 1; index++)
            {
                var alignment = (Alignment)index;

                Transform parent = alignment == Alignment.BOTTOM ? base.transform : m_cameraTransform;

                KeyValuePair<Vector2, Vector2> borderParams = this.GetBorderParams(stageBounds, stageSize, alignment);
                Instantiate(this.m_stageBorderPrefab).Setup(new Border
                {
                    Name = $"Border - {alignment}",
                    Alignment = alignment,
                    Position = borderParams.Key,
                    Scale = borderParams.Value,
                    Layer = alignment == Alignment.BOTTOM ? this.m_groundLayerMask : this.m_stageBorderLayerMask
                }, parent);

                if (alignment == Alignment.BOTTOM)
                    continue;

                borderParams = this.GetBorderParams(stageBounds, stageSize, alignment, this.m_spawnLimitMargin);
                Instantiate(this.m_spawnLimitBorderPrefab).Setup(new Border
                {
                    Name = $"Spawn Limit Border - {alignment}",
                    Alignment = alignment,
                    Position = borderParams.Key,
                    Scale = borderParams.Value,
                    Layer = this.m_spawnLimitLayerMask
                }, parent);
            }
        }

        private KeyValuePair<Vector2, Vector2> GetBorderParams(Bounds bounds, Vector2 stageSize, Alignment alignment, float extraMargin = 0)
        {
            float borderMargin = this.m_borderThickness / 2f;

            return alignment switch
            {
                Alignment.LEFT => new KeyValuePair<Vector2, Vector2>(new Vector2(bounds.Left - borderMargin - extraMargin, 0),
                                                                     new Vector2(m_borderThickness, stageSize.y)),

                Alignment.RIGHT => new KeyValuePair<Vector2, Vector2>(new Vector2(bounds.Right + borderMargin + extraMargin, 0),
                                                                      new Vector2(m_borderThickness, stageSize.y)),

                Alignment.TOP => new KeyValuePair<Vector2, Vector2>(new Vector2(0, bounds.Top + borderMargin + extraMargin),
                                                                    new Vector2(stageSize.x, m_borderThickness)),

                Alignment.BOTTOM => new KeyValuePair<Vector2, Vector2>(new Vector2(0, bounds.Bottom - borderMargin - extraMargin),
                                                                       new Vector2(stageSize.x, m_borderThickness)),

                _ => throw new Exception($"Invalid StageBorder.GetBorderParams alignment parameter: {alignment}"),
            };
        }
    }
}