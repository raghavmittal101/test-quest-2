using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMetadataInput
{
    float PathSegmentLength();
    int VisiblePathSegmentCount();
    float PathWidth();
    Material Material();
}
