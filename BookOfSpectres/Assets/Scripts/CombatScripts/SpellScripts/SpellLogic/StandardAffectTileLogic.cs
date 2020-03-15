using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using EnumScript;
[CreateAssetMenu(menuName = "SpellLogic/StandardTileEffect")]
public class StandardAffectTileLogic : SpellLogic
{

    public Vector2 offset;
    public TileEffect tileEffect;
    public AffectShape affectShape;
    public Direction direction;
    public int tilesAffected;

    public List<ColourToPattern> colourToPatterns;
    public List<ColourToDirection> colourToDirection;
    public Texture2D patternImage;
    public Texture2D directionImage;
    List<TileClass> tilePattern;
    Vector2 patternOrigin;
    Vector2 offsetOrigin;
    List<TilePatternBehaviour> patternBehaviours;

    CoroutineScript coroutineScript;
    private void Awake()
    {
        
    }
   

    public override void Execute(CombatMiniatureProperties _properties, GameObject _user, EntityStatus _userStatus, Transform _shotOrigin)
    {
        tilePattern.Clear();
        patternBehaviours.Clear();

        _properties.cardHolder.RemoveFirst();
        BattlefieldScript _bfs = _properties.bfs;
        GeneratePattern(_properties, _user, _userStatus, _shotOrigin, _bfs);
        /*if (directionImage == null)
        {
            foreach (TileClass _affectedTile in tilePattern)
            {
                TileEffects(_properties, _user, _userStatus, _shotOrigin, _affectedTile, _bfs);
            }
        }
        else
        {
            var sortedOrderList = patternBehaviours.OrderBy(o => o.order);
            List<TilePatternBehaviour> _tempList = sortedOrderList.ToList();
            objectPooler.StartCoroutine(AdvancedTileEffect(_properties,_user,_userStatus, _tempList, _bfs));
        }*/

        var sortedOrderList = patternBehaviours.OrderBy(o => o.order);
        List<TilePatternBehaviour> _tempList = sortedOrderList.ToList();
        _tempList.Reverse();
        foreach(TilePatternBehaviour tpb in _tempList)
        {
            Debug.Log(tpb.tileClass);
        }
        coroutineScript = CoroutineScript.Instance;

        coroutineScript.StartRemoteCoroutine(AdvancedTileEffect(_properties, _user, _userStatus, _tempList, _bfs));

    }

    void TileEffects(CombatMiniatureProperties _properties, GameObject _user, EntityStatus _userStatus, Transform _shotOrigin, TileClass _currentTile, BattlefieldScript _bfs)
    {
        switch (tileEffect)
        {

            case (TileEffect.Broken):
                {
                        _currentTile.tileEffect = tileEffect;

                        _currentTile.CheckEffect();

                        //_properties.cardHolder.RemoveFirst();
                    
                    break;
                }
            case (TileEffect.Steal):
                {               
                        _currentTile.ChangeAlignment(_bfs.standardColours[0], TileAlignment.Friendly);

                        //_properties.cardHolder.RemoveFirst();
                    
                    break;
                }
        }
    }

    void GeneratePattern(CombatMiniatureProperties _properties, GameObject _user, EntityStatus _userStatus, Transform _shotOrigin, BattlefieldScript _bfs)
    {
        for (int x = 0; x < patternImage.width; x++)
        {
            for (int y = 0; y < patternImage.height; y++)
            {
                FindOrigin(_properties, _user, x, y);
            }
        }

        offsetOrigin = _properties.bfs.playerPosition - patternOrigin;

        for (int x  = 0; x < patternImage.width; x++)
        {
            for (int y = 0; y < patternImage.height; y++)
            {
                GenerateTileEffect(x, y, _properties, _bfs);
            }
        }
    }

    void GenerateTileEffect(int x, int y, CombatMiniatureProperties _properties, BattlefieldScript _bfs)
    {
        TilePatternBehaviour _tpb = new TilePatternBehaviour();

        Color pixelColor = patternImage.GetPixel(x,y);

        if(pixelColor.a == 0)
        {
            return;
        }
        foreach (ColourToPattern colourMapping in colourToPatterns)
        {
            if(colourMapping.color.Equals(pixelColor))
            {
                switch(colourMapping.patternType)
                {
                    case (PatternType.Standard):
                        {

                            if (IsValidTile(x + (int)offsetOrigin.x, y + (int)offsetOrigin.y, _bfs))
                            {
                                _tpb.type = PatternType.Standard;
                                _tpb.tileClass = _bfs.battleTilesGrid[x + (int)offsetOrigin.x, y + (int)offsetOrigin.y].GetComponent<TileClass>();


                                tilePattern.Add(_bfs.battleTilesGrid[x + (int)offsetOrigin.x, y + (int)offsetOrigin.y].GetComponent<TileClass>());
                                
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    case (PatternType.OriginIndependent):
                        {
                            _tpb.type = PatternType.OriginIndependent;
                            _tpb.tileClass = _bfs.battleTilesGrid[x, y].GetComponent<TileClass>();


                            tilePattern.Add(_bfs.battleTilesGrid[x, y].GetComponent<TileClass>());
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }

        if (directionImage != null)
        {
            Color directionColor = directionImage.GetPixel(x, y);

            Color directionColorFullAlpha = new Color(directionColor.r, directionColor.g, directionColor.b, 1);

            if (directionColor.a == 0)
            {
                return;
            }
            
            _tpb.order = directionColor.a;
            


            foreach (ColourToDirection directionMapping in colourToDirection)
            {
                if (directionMapping.color.Equals(directionColorFullAlpha))
                {
                    _tpb.direction = directionMapping.direction;
                }
            }
        }

        patternBehaviours.Add(_tpb);
    }

    void FindOrigin(CombatMiniatureProperties _properties, GameObject _user, int x, int y)
    {  
                Color pixelColor = patternImage.GetPixel(x, y);

                if (pixelColor.a == 0)
                {
                    return;
                }

                foreach (ColourToPattern colourMapping in colourToPatterns)
                {
                    if (colourMapping.color.Equals(pixelColor))
                    {
                        switch (colourMapping.patternType)
                        {
                            case (PatternType.Origin):
                                {
                                    patternOrigin = new Vector2(x, y);
                                    x = patternImage.width;
                                    y = patternImage.height;
                                    break;
                                }
                        }
                    }
                }
        

    }

    bool IsValidTile(int x, int y, BattlefieldScript _bfs)
    {
        if(x < 0 || x >= _bfs.xMax || y < 0 || y >= _bfs.yMax)
        {
            return false; 
        }
        else
        {
            return true;
        }
    }

    IEnumerator AdvancedTileEffect(CombatMiniatureProperties _properties, GameObject _user, EntityStatus _userStatus, List<TilePatternBehaviour> _tiles, BattlefieldScript _bfs)
    {
        float _nextOrderNumber = 1;
        int _currentListItem = 0;
        foreach(TilePatternBehaviour tpb in _tiles)
        {
            if (_tiles.Count > _currentListItem + 1)
            {
                _nextOrderNumber = _tiles[_currentListItem + 1].order;
            }

            switch (tileEffect)
            {

                case (TileEffect.Broken):
                    {
                        tpb.tileClass.tileEffect = tileEffect;

                        tpb.tileClass.CheckEffect();

                        break;
                    }
                case (TileEffect.Steal):
                    {
                        tpb.tileClass.ChangeAlignment(_bfs.standardColours[0], TileAlignment.Friendly);

                        break;
                    }
            }

            if(tpb.type == PatternType.Continuous)
            {
                TilePatternBehaviour _tpb = new TilePatternBehaviour();
                _tpb.direction = tpb.direction;
                _tpb.order = tpb.order - 0.01f;
                _tpb.type = tpb.type;
                if( tpb.tileClass.tileNeighbours.TryGetValue(tpb.direction, out GameObject _tileObject))
                {
                    _tpb.tileClass = _tileObject.GetComponent<TileClass>();
                }

                _tiles.Insert(_currentListItem + 1, _tpb);
            }

            

            if (_nextOrderNumber != tpb.order)
            {
                yield return new WaitForSeconds(0.2f);
            }

            _currentListItem++;

        }
        
    }
}
