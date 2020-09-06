using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetadataManager
{
    public static float pathWidth_static;
    public static float pathLength_static;
    public static float numberOfPathSegments_static;

    /* Constructors */
    public MetadataManager(string filename)
    {
        // here we call methods to deserialize metadata file
    }

    public MetadataManager(float pathWidth, float pathSegmentLength, int numberOfPathSegments)
    // this constructor is only for prototyping until we don't have fullfunctionality to read metadata files.
    {
        pathWidth_static = pathWidth;
        pathLength_static = pathSegmentLength;
        numberOfPathSegments_static = numberOfPathSegments;
}

}
