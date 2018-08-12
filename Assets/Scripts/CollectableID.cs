using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableID : MonoBehaviour {
    public CollectableType type;
}

public enum CollectableType
{
    Point,
    BoundBonus,
    Obstacle
}
