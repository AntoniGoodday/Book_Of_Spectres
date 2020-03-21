using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumScript
{
    public enum TileAlignment { Neutral, Friendly, Enemy }

    public enum BulletAlignement { Friendly, Neutral, Enemy }

    public enum SpellType { NoSpell, SpectralKnife, SpiritBlaster, GrabTile, Heal, SpiritBolt, Null }

    public enum TileColour { Standard, Epic }

    public enum Direction { DownLeft, Down, DownRight, Left, Right, UpLeft, Up, UpRight, Reset }

    public enum StatusEffect { SoulSap, Rejuvenation }

    public enum Facing { Right, Left }

    public enum SpellColour { Colourless, Cyan, Magenta, Yellow, Silver, Bronze }

    public enum SpellTags { Projectile, Melee, DoubleEdged }

    public enum CardDestination { Deck, Hand, Combat, Graveyard, Exile }

    public enum BuffType { Health, MaxHealth, Mana, MaxMana, Distance }

    public enum TileEffect { Standard, Broken, Cracked, Empower, Sanctuary, Poison, Steal }

    public enum AffectShape { Spot, Row, Column }
    //Origin independent = spell always hits specific tiles, regardless of where the player stands
    public enum PatternType { Standard, Origin, Continuous, OriginIndependent }

    public class EnumMethods
    {
        public static Vector2Int DirectionToCoords(Direction d)
        {
            switch (d)
            {
                case (Direction.Down):
                    {

                        return new Vector2Int(0, -1);
                    }
                case (Direction.DownLeft):
                    {

                        return new Vector2Int(-1, -1);
                    }
                case (Direction.DownRight):
                    {

                        return new Vector2Int(1, -1);
                    }
                case (Direction.Left):
                    {

                        return new Vector2Int(-1, 0);
                    }
                case (Direction.Right):
                    {

                        return new Vector2Int(1, 0);
                    }
                case (Direction.Up):
                    {

                        return new Vector2Int(0, 1);
                    }
                case (Direction.UpLeft):
                    {

                        return new Vector2Int(-1, 1);
                    }
                case (Direction.UpRight):
                    {

                        return new Vector2Int(1, 1);
                    }
                default:
                    {
                        return new Vector2Int(0, 0);
                    }
            }
        }
    }
        
}

