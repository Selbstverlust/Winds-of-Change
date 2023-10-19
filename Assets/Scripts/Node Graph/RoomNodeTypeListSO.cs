using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomNodeTypeListSO", menuName = "Scriptable Objects/Dungeon/Room Node Type List")]

public class RoomNodeTypeListSO : ScriptableObject {

    [Space(10)]
    [Header("Room Node Type List")]
    [Tooltip("This list should be populated with all the RoomTypeSO for the game - it is used instead of an enum.")]
    public List<RoomNodeTypeSO> list;

    private void OnValidate() {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(list), list);
    }
}
