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

    #region Public Methods

    /// <summary>
    /// プレイヤーを切り替えるメソッド
    /// </summary>
    public void ChangePlayersIndex(PlayerInterface player)
    {
        int playerIndex = 1;
        int enemyIndex = 0;

        if (PlayerManager.Players[0].HandCollection == player)
        {
            playerIndex = 0;
            enemyIndex = 1;
        }

        Player = PlayerManager.Players[playerIndex];
        Enemy = PlayerManager.Players[enemyIndex];
    }

    #endregion
}
