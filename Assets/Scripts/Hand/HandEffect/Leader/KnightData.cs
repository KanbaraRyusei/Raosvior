/// <summary>
/// ナイト
/// パーで勝利した時
/// 相手のリザーブの数だけ
/// シールドトークンを獲得する。
/// </summary>
public class KnightData : LeaderHandEffect
{
    #region Public Methods

    public override void CardEffect()
    {
        var playerRSP = Player.PlayerParameter.SetRSPHand.Hand.Hand;
        var enemyRSP = Enemy.PlayerParameter.SetRSPHand.Hand.Hand;
        if (playerRSP == PAPER && enemyRSP == ROCK)//パーで勝利したら
        {
            // 相手のリザーブの数だけシールドトークンを獲得
            var count = Enemy.PlayerParameter.Reserve.Count;
            Player.GetableShield.GetShield(count);
        }
    }

    #endregion
}
