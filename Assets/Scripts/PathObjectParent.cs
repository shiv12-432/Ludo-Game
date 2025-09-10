using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathObjectParent : MonoBehaviour
{
    public PathPoint[] CommonPathPoint;
    public PathPoint[] RedPathPoint;
    public PathPoint[] BluePathPoint;
    public PathPoint[] YellowPathPoint;
    public PathPoint[] GreenPathPoint;
    public PathPoint[] BasePoints;

    [Header("Scale and Position Difference")]
    public float[] scales;
    public float[] positionDifference;
}
