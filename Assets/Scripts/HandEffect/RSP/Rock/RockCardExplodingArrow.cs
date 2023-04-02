/// <summary>
/// 炸裂する矢(グー)
/// 勝利した時相手がシールドトークンを所持しているならさらに1ダメージを与える。
/// </summary>
public class RockCardExplodingArrow : RSPHandEffect
{
    #region Public Methods

    public override void Effect()
    {
        //相手がシールドトークンを所持しているなら
        if (Enemy.PlayerParameter.Shield > 0)
        {
            //さらに1ダメージを与える
            Enemy.ChangeableLife.ReceiveDamage();
        }
    }

    #endregion
}
