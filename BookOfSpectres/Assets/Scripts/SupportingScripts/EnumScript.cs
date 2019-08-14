using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumScript
{
    public enum TileAlignment { Neutral, Friendly, Enemy}

    public enum BulletAlignement { Friendly, Neutral, Enemy }

    public enum SpellType { NoSpell, SpectralKnife, SpiritBlaster, GrabTile, Heal, SpiritBolt, Null }

    public enum TileColour { Standard, Epic }

    public enum TileNeighbourDirection { DownLeft, Down, DownRight, Left, Right, UpLeft, Up, UpRight }

    public enum StatusEffect { SoulSap, Rejuvenation}

    public enum Facing { Right, Left}
}

