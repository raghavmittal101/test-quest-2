using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMetadataInput
{
    float PathSegmentLength();
    int VisiblePathSegmentCount();
    float PathWidth();
    int RayArrayLength();
    List<Texture> ImageTexturesList();
    float PlayAreaPadding();
    string SubjectId();
}
