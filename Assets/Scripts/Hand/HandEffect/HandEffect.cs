using UnityEngine;
/// <summary>
/// 全てのハンドの効果の基底クラス
/// </summary>
public abstract class HandEffect : MonoBehaviour
{
    #region Properties

    public PlayerInterface Player { get; private set; }

    public PlayerInterface Enemy { get; private set; }

    #endregion

    public void SetPlayerInterface(PlayerInterface player, PlayerInterface enemy)
    {
        Player = player;
        Enemy = enemy;
    }
}
