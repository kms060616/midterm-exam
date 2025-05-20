using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] ItemSo data;

    public int GetPoint()
    {
        return data.point;
    }
}
