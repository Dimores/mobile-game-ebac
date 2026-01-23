using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtPiece : MonoBehaviour
{
    public GameObject currentArt;

    public void SetArt(GameObject artPrefab)
    {
        if (currentArt != null) Destroy(currentArt);

        currentArt = Instantiate(artPrefab, transform);
        currentArt.transform.localPosition = Vector3.zero;
    }
}
