﻿using System.Collections.Generic;
using ThunderingTanks.Animation;

namespace ThunderingTanks.Animation.DataTypes;

/// <summary>
///     Keyframes are grouped per bone for an animation clip.
/// </summary>
public class AnimationBone
{
    /// <summary>
    ///     The keyframes for this bone.
    /// </summary>
    public List<Keyframe> Keyframes { get; set; } = new();

    /// <summary>
    ///     The bone name for these keyframes.
    ///     Each bone has a name so we can associate it with a runtime model.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}