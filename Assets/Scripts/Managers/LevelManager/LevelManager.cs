using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;

    public List<GameObject> levels;
    public List<LevelPieceBasedSetup> levelPieceBasedSetups;

    private int _index;
    private GameObject _currentLevel;

    private List<LevelPieceBase> _spawnedPieces;
    private LevelPieceBasedSetup _currentLevelPieceSetup;

    private void Awake()
    {
        _spawnedPieces = new List<LevelPieceBase>();
    }

    private void Start()
    {
        ResetLevel();
        CreateLevelPieces();
    }

    private void SpawnNextLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);

            _index++;

            CheckToResetLevel();
        }

        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    private void CheckToResetLevel()
    {
        if (_index >= levels.Count)
        {
            ResetLevel();
        }
    }

    private void ResetLevel()
    {
        _index = 0;
    }

    private void CreateLevelPiece(List<LevelPieceBase> levelList)
    {
        var piece = levelList[(Random.Range(0, levelList.Count))];
        var spawnedPiece = Instantiate(piece, container);

        if (_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];
            spawnedPiece.transform.position = lastPiece.endPiece.position;
        }

        foreach (var p in spawnedPiece.GetComponentsInChildren<ArtPiece>())
        {
            p.SetArt(ArtManager.Instance.GetArtByType(_currentLevelPieceSetup.artType));
        }

        _spawnedPieces.Add(spawnedPiece);
    }

    private void CleanSpawnedPieces()
    {
        foreach (var piece in _spawnedPieces)
        {
            Destroy(piece.gameObject);
        }
        _spawnedPieces.Clear();
    }

    private void CreateLevelPieces()
    {
        CleanSpawnedPieces();

        if (_currentLevelPieceSetup != null)
        {
            _index++;

            if (_index >= levelPieceBasedSetups.Count)
            {
                ResetLevel();
            }
        }

        _currentLevelPieceSetup = levelPieceBasedSetups[_index];

        for (int i = 0; i < _currentLevelPieceSetup.piecesStartAmount; i++)
        {
            CreateLevelPiece(_currentLevelPieceSetup.levelPiecesStart);
        }

        for (int i = 0; i < _currentLevelPieceSetup.piecesAmount; i++)
        {
            CreateLevelPiece(_currentLevelPieceSetup.levelPieces);
        }

        for (int i = 0; i < _currentLevelPieceSetup.piecesEndAmount; i++)
        {
            CreateLevelPiece(_currentLevelPieceSetup.levelPiecesEnd);
        }

        ColorManager.Instance.ChangeColorByType(_currentLevelPieceSetup.artType);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            CreateLevelPieces();
        }
    }
}
