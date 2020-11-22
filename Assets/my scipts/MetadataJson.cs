using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Any changes done to this class should be matched with format of JSON to be recieved
/// </summary>
public class MetadataJson
{
    public int _id;
    public string pathSegmentLength;
    public string visiblePathSegmentCount;
    public string pathWidth;
    public string rayArrayLength;
    public string playAreaPadding;
    public string returnedPayload;
    public string[] imageURI;
}
