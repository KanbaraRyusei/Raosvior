/// <summary>
/// グラップラー
/// グーで勝利したとき
/// カード効果をもう一度発動する。
/// </summary>
public class GrapplerData : LeaderHandEffect
{
    #region Public Method

    public override void CardEffect()
    {
        var playerRSP = Player.PlayerParameter.SetRSPHand.RSPHand.Hand;
        var enemyRSP = Enemy.PlayerParameter.SetRSPHand.RSPHand.Hand;
        if (playerRSP == ROCK && enemyRSP == SCISSORS)//グーで勝利したら
        {
            //効果をもう一度発動
            Player.PlayerParameter.SetRSPHand.HandEffect.Effect();
        }
    }

    #endregion
}
