using Ebac.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtManager : Singleton<ArtManager>
{
    public enum ArtType
    {
        TYPE_01,
        TYPE_02,
        TYPE_03,
        BEACH,
        SNOW
    }

    public List<ArtSetup> artSetups;

    public GameObject GetArtByType(ArtType artType)
    {
        ArtSetup setup = artSetups.Find(i => i.artType == artType);
        return setup != null ? setup.artPrefab : null;
    }

}

[System.Serializable]
public class ArtSetup
{
    public ArtManager.ArtType artType;
    public GameObject artPrefab;
}
