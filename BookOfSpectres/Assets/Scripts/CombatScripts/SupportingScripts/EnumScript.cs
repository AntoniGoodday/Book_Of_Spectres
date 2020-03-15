using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumScript
{
    public enum TileAlignment { Neutral, Friendly, Enemy}

    public enum BulletAlignement { Friendly, Neutral, Enemy }

    public enum SpellType { NoSpell, SpectralKnife, SpiritBlaster, GrabTile, Heal, SpiritBolt, Null }

    public enum TileColour { Standard, Epic }

    public enum Direction { DownLeft, Down, DownRight, Left, Right, UpLeft, Up, UpRight, Reset }

    public enum StatusEffect { SoulSap, Rejuvenation}

    public enum Facing { Right, Left}

    public enum SpellColour { Colourless, Cyan, Magenta, Yellow, Silver, Bronze}

    public enum SpellTags { Projectile, Melee, DoubleEdged}

    public enum CardDestination { Deck, Hand, Combat, Graveyard, Exile}

    public enum BuffType { Health, MaxHealth, Mana, MaxMana, Distance }

    public enum TileEffect { Standard, Broken, Empower, Sanctuary, Poison, Steal }

    public enum AffectShape { Spot, Row, Column}
    //Origin independent = spell always hits specific tiles, regardless of where the player stands
    public enum PatternType { Standard, Origin, Continuous, OriginIndependent }
}

