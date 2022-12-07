using UnityEngine;

[DefaultExecutionOrder(-105)]

[CreateAssetMenu(menuName = "Scriptables/GameData")]
public class GameData : DataHolder
{
    #region Singleton
    private static GameData _default;
    public static GameData Default => _default;
    #endregion

    [Header("Layers/Tags")]
    [Space(10)]
    public LayerMask groundLayer;
    public LayerMask allyLayer;
    public LayerMask enemyLayer;

    [Header("Input")]
    [Space(10)]
    public float cameraRotationSpeed;
    public string xAxisName;

    override public void Init()
    {
        _default = this;
    }
}