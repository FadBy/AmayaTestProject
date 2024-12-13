using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelList", menuName = "LevelList", order = 1)]
public class LevelList : ScriptableObject
{
    [SerializeField] private List<LevelConfiguration> _levels;

    public List<LevelConfiguration> Levels => _levels;
}
