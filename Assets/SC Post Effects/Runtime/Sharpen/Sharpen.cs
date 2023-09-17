using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

namespace SCPE
{
    [Serializable, VolumeComponentMenu("SC Post Effects/Image/Sharpen")]
    public sealed class Sharpen : VolumeComponent, IPostProcessComponent
    {
        public ClampedFloatParameter amount = new ClampedFloatParameter(0f, 0f, 1f);
        public ClampedFloatParameter radius = new ClampedFloatParameter(1f, 0.1f, 2f);

        public bool IsActive() { return active && amount.value > 0f; }

        public bool IsTileCompatible() => false;
    }
}