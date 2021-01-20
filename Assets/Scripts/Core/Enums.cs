using UnityEngine;
using System.Collections;

namespace Enums
{
    public enum Anim_ID_Map
    {
        Idle,
        Walk,
        Die,
        ID_00,
        ID_01,
        ID_02,
        ID_03,
        ID_04,
        ID_05,
        ID_06,
        ID_07,
        ID_08,
        ID_09,
        ID_10,
        ID_11,
        ID_12,
        ID_13,
        ID_14,
        ID_15,
        ID_16,
        ID_17,
        ID_18,
        ID_19,

    }

    public enum Action_ID
    {
        Default,
        ID_00,
        ID_01,
        ID_02,
        ID_03,
        ID_04,
        ID_05,
        ID_06,
        ID_07,
        ID_08,
        ID_09,
        ID_10,
        ID_11,
        ID_12,
        ID_13,
        ID_14,
        ID_15,
        ID_16,
        ID_17,
        ID_18,
        ID_19,
    }

    public enum EntityState
    {
        Idle,
        Motion,
        Action
    }

    public enum ItemCategory
    {
        ContructionItem,
        foodItem,
    }

    public enum ScenesID
    {

        UIScene = 2,
        Dunguon_1 = 5,
        Dunguon_2 = 6,
        Dunguon_3 = 7,
        Dunguon_4 = 8,
    }

    public enum LocationId
    {
        Runes_1 = ScenesID.Dunguon_1,
        Runes_2 = ScenesID.Dunguon_2,
        Runes_3 = ScenesID.Dunguon_3,
        Runes_4 = ScenesID.Dunguon_4
    }
}
