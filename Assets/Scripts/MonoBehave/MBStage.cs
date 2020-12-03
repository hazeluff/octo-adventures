﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MBStage : MonoBehaviour {
    private static CustomLogger LOGGER = new CustomLogger(typeof(MBStage).ToString());

    [SerializeField]
    private Transform cursor;
    [SerializeField]
    private MBStageCamera mbCamera;
    [SerializeField]
    private GameObject unitMenu;

    private InputManager input;

    public ControlState State { get; private set; }

    public enum ControlState {
        DESELECTED, UNIT_MENU, UNIT_MOVE, UNIT_ATTACK, WAIT
    }

    FactionDiplomacy FactionDiplomacy;
    private ReversableDictionary<MapCoordinate, MBTile> tiles = new ReversableDictionary<MapCoordinate, MBTile>();
    public Dictionary<MapCoordinate, int> Heights { get; private set; }
    private ReversableDictionary<MBUnit, MapCoordinate> units = new ReversableDictionary<MBUnit, MapCoordinate>();


    public bool IsOccupied (MapCoordinate coordinate) {
        return units.ContainsKey(coordinate);
    }

    private MapCoordinate cursorPos = new MapCoordinate(0, 0);

    private MapCoordinate selected = null;

    private HashSet<MapCoordinate> range;

    public bool AnySelected() {
        return selected != null;
    }

    public IStageUnit GetSelected() {
        return units.Get(selected).Unit;
    }


    private bool InBounds(MapCoordinate coordinate) {
        return tiles.ContainsKey(coordinate);
    }

    private void ResetRange() {
        foreach (MapCoordinate coord in range) {
            tiles.Get(coord).setDefaultColor();
        }
        range = null;
    }

    private bool InRange(MapCoordinate coordinate) {
        if (range == null) {
            return true;
        }
        return range.Contains(coordinate);
    }

    public bool IsSelected(MBUnit unit) {        
        return selected != null && units.Get(selected) == unit;
    }

	void Awake () {
        unitMenu.SetActive(false);
        State = ControlState.DESELECTED;
        Heights = new Dictionary<MapCoordinate, int>();

        // Register Tiles
        MBTile[] tileList = FindObjectsOfType<MBTile>();
        foreach (MBTile tile in tileList) {
            Vector3 tilePos = tile.transform.position;
            MapCoordinate mapCoordinate = new MapCoordinate(Mathf.RoundToInt(tilePos.x), Mathf.RoundToInt(tilePos.z));
            tiles.Add(mapCoordinate, tile);
            Heights.Add(mapCoordinate, Mathf.RoundToInt(tilePos.y - tile.transform.localPosition.y));
            tile.setStage(this);
        }

        // Register Factions
        HashSet<Faction> factions = new HashSet<Faction>();
        foreach (MBUnit mbUnit in FindObjectsOfType<MBUnit>()) {
            factions.Add(mbUnit.Unit.Faction);
        }

        FactionDiplomacy = new FactionDiplomacy(factions);

        // Register Units
        foreach (MBUnit mbUnit in FindObjectsOfType<MBUnit>()) {
            Vector3 unitPos = mbUnit.transform.position;
            MapCoordinate mapCoordinate = new MapCoordinate(Mathf.RoundToInt(unitPos.x), Mathf.RoundToInt(unitPos.z));
            units.Add(mbUnit, mapCoordinate);
            mbUnit.setStage(this);
            FactionDiplomacy.RegisterUnit(mbUnit);
        }
    }

    private void Start() {
        input = InputManager.get();
        MoveCursorTo(cursorPos);
    }

    void Update () {
        UpdateCursor();
	}

    private void UpdateCursor() {
        input = InputManager.get();

        if (!mbCamera.Rotating) {
            if (input.RB) {
                mbCamera.Rotate(true);
                return;
            } else if (input.LB) {
                mbCamera.Rotate(false);
                return;
            }
        }

        if (!mbCamera.Tilting) {
            if (input.LT.Pressed) {
                mbCamera.Zoom(true);
                return;
            }
        }

        if (!mbCamera.Zooming) {
            if (input.RT.Pressed) {
                mbCamera.Tilt(true);
                return;
            }
        }

        if (mbCamera.Rotating || mbCamera.Tilting || mbCamera.Zooming) {
            return;
        }

        switch (State) {
            case ControlState.DESELECTED:
                MoveCursor();

                if (input.A) {
                    if (selected == null && IsOccupied(cursorPos)) {
                        OpenUnitMenu();
                        return;
                    }
                }
                break;
            case ControlState.UNIT_MENU:
                break;
            case ControlState.UNIT_MOVE:
                MoveCursor();

                if (input.A) {
                    if (!IsOccupied(cursorPos) && InRange(cursorPos)) {
                        MoveUnit(units.Get(selected), new List<MapCoordinate>() { selected, cursorPos });
                        return;
                    }
                } else if (input.B) {
                    CancelRangeSelection();
                    OpenUnitMenu();
                    return;
                }
                break;
            case ControlState.UNIT_ATTACK:
                if (input.A) {
                    if (!IsOccupied(cursorPos) && InRange(cursorPos)) {
                        ClickUnit(units.Get(cursorPos));
                        return;
                    }
                } else if (input.B) {
                    CancelRangeSelection();
                    OpenUnitMenu();
                    return;
                }
                break;
            default:
                break;
        }
    }

    public void MoveCursor() {
        if (input.DPAD.UP || input.LEFT_STICK.UP) {
            MoveCursor(0);
            return;
        } else if (input.DPAD.RIGHT || input.LEFT_STICK.RIGHT) {
            MoveCursor(1);
            return;
        } else if (input.DPAD.DOWN || input.LEFT_STICK.DOWN) {
            MoveCursor(2);
            return;
        } else if (input.DPAD.LEFT || input.LEFT_STICK.LEFT) {
            MoveCursor(3);
            return;
        }
    }

    public void StartMoveSelection() {
        ChangeState(ControlState.UNIT_MOVE);
        unitMenu.SetActive(false);
        range = FindMoveRange(units.Get(selected), selected);
        foreach (MapCoordinate coord in range) {
            tiles.Get(coord).setMoveRangeColor();
        }
    }

    public void CancelRangeSelection() {
        cursorPos = selected;
        MoveCursorTo(cursorPos);
        ResetRange();
        OpenUnitMenu();
    }

    private HashSet<MapCoordinate> FindMoveRange(IMBUnit mbUnit, MapCoordinate origin) {
        int unitMove = mbUnit.Unit.c_Mv;
        HashSet<MapCoordinate> range = new HashSet<MapCoordinate>();
        FindMoveRange(mbUnit, range, unitMove, origin);
        return range;
    }

    private void FindMoveRange(IMBUnit mbUnit, HashSet<MapCoordinate> listToAddTo, int mvLeft, MapCoordinate currentNode) {
        if (!tiles.ContainsKey(currentNode)) {
            return;
        }

        //TODO: This needs to not return if mvLeft is less than previous path
        if (listToAddTo.Contains(currentNode)) {
            return;
        }

        ITile currentTile = tiles.Get(currentNode).Tile;
        if (!currentTile.Traversable) {
            return;
        }

        listToAddTo.Add(currentNode);

        if (listToAddTo.Count > 0) {
            mvLeft -= currentTile.MoveCost(mbUnit.Unit);
        }

        if (mvLeft < 0) {
            return;
        }

        listToAddTo.Add(currentNode);
        foreach (MapCoordinate direction in new MapCoordinate[] { MapCoordinate.UP, MapCoordinate.DOWN, MapCoordinate.LEFT, MapCoordinate.RIGHT }) {
            MapCoordinate adjacentNode = currentNode + direction;
            FindMoveRange(mbUnit, listToAddTo, mvLeft, adjacentNode);
        }
    }

    public void StartAttackSelection() {
        ChangeState(ControlState.UNIT_ATTACK);
        unitMenu.SetActive(false);
        range = FindAttackRange(units.Get(selected), selected);
        foreach (MapCoordinate coord in range) {
            tiles.Get(coord).setAttackRangeColor();
        }
    }

    private HashSet<MapCoordinate> FindAttackRange(IMBUnit mbUnit, MapCoordinate origin) {
        int maxAttackRange = mbUnit.Unit.UnitProfile.Weapon.RangeMax;
        int minAttackRange = mbUnit.Unit.UnitProfile.Weapon.RangeMin;
        HashSet<MapCoordinate> range = new HashSet<MapCoordinate>();
        for (int xOffset = -maxAttackRange; xOffset <= maxAttackRange; xOffset++) {
            int yAbsRange = maxAttackRange - Math.Abs(xOffset);
            for (int yOffset = -yAbsRange; yOffset <= yAbsRange; yOffset++) {
                MapCoordinate offset = new MapCoordinate(xOffset, yOffset);
                MapCoordinate tile = origin + offset;
                if (tiles.ContainsKey(tile) &&
                    offset != new MapCoordinate(0,0) &&
                    (Math.Abs(xOffset) + Math.Abs(yOffset) >= minAttackRange)) {
                    range.Add(tile);
                }
            }
        }
        return range;
    }

    public void OpenUnitMenu() {
        ChangeState(ControlState.UNIT_MENU);
        unitMenu.SetActive(true);
        selected = cursorPos;
    }

    public void CancelUnitMenu() {
        ChangeState(ControlState.DESELECTED);
        unitMenu.SetActive(false);
        selected = null;
    }

    private void MoveUnit(MBUnit unit, List<MapCoordinate> path) {
        ResetRange();        

        units.Remove(unit);
        units.Add(unit, path[path.Count - 1]);
        unit.Move(path);
        ChangeState(ControlState.DESELECTED);
        selected = null;
    }

    private static readonly MapCoordinate[][] MAP_MOVEMENT_MAP = new MapCoordinate[][] {
        new MapCoordinate[] { MapCoordinate.UP , MapCoordinate.RIGHT, MapCoordinate.DOWN, MapCoordinate.LEFT },
        new MapCoordinate[] { MapCoordinate.LEFT, MapCoordinate.UP , MapCoordinate.RIGHT, MapCoordinate.DOWN },
        new MapCoordinate[] { MapCoordinate.DOWN, MapCoordinate.LEFT, MapCoordinate.UP , MapCoordinate.RIGHT },
        new MapCoordinate[] { MapCoordinate.RIGHT, MapCoordinate.DOWN , MapCoordinate.LEFT, MapCoordinate.UP }
    };

    private void MoveCursor(int direction) {
        MapCoordinate newCursorPos = cursorPos + MAP_MOVEMENT_MAP[mbCamera.Orientation][direction];
        MoveCursorTo(newCursorPos);
    }

    private void MoveCursorTo(MapCoordinate coordinate) {
        if (InBounds(coordinate) && InRange(coordinate)) {
            MapCoordinate oldPos = cursorPos;
            this.cursorPos = coordinate;
            cursor.localPosition = cursorPos.Vector3 + (Vector3.up * Heights[cursorPos]);
            mbCamera.MoveTo(coordinate.Vector3);

            MBTile tile = tiles.Get(coordinate);
            if (tile != null) {
                tile.setSelectedColor();
                if (InRange(oldPos) && State == ControlState.UNIT_MOVE) {
                    tiles.Get(oldPos).setMoveRangeColor();
                } else {
                    tiles.Get(oldPos).setDefaultColor();
                }
            }
        }
    }

    public void ClickTile(MBTile tile) {
        MapCoordinate coordinate = tiles.Get(tile);
        switch (State) {
            case ControlState.DESELECTED:
                MoveCursorTo(coordinate);
                break;
            case ControlState.UNIT_MOVE:
                if (InRange(coordinate)) {
                    MoveUnit(units.Get(selected), new List<MapCoordinate>() { selected, coordinate });
                }
                MoveCursorTo(coordinate);
                break;
            case ControlState.UNIT_ATTACK:
                if (!IsOccupied(cursorPos) && InRange(cursorPos)) {
                    ClickUnit(units.Get(cursorPos));                
                }
                MoveCursorTo(coordinate);
                break;
            default:
                break;
        }
    }

    public void ClickUnit(MBUnit unit) {
        MapCoordinate pos = units.Get(unit);
        MoveCursorTo(pos);

        if (!InRange(pos)) {
            return;
        }

        switch (State) {
            case ControlState.DESELECTED:
                OpenUnitMenu();
                break;
            case ControlState.UNIT_ATTACK:
                GetSelected().Attack(unit.Unit);
                CancelRangeSelection();
                CancelUnitMenu();
                break;
            default:
                break;
        }
    }

    private void ChangeState(ControlState newState) {
        StartCoroutine(IEnumChangeState(newState));
    }

    IEnumerator IEnumChangeState(ControlState newState) {
        State = ControlState.WAIT;
        yield return null;
        State = newState;
    }
}
