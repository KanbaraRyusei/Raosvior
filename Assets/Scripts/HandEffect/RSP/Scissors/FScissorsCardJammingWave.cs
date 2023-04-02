/// <summary>
/// ジャミングウェーブ・F(チョキ)
/// 勝利した時ダメージを与える前に相手のシールドトークンを全て破壊する
/// </summary>
public class FScissorsCardJammingWave : RSPHandEffect
{
    #region Public Methods

    public override void Effect()
    {
        var allShield = Enemy.PlayerParameter.Shield;
        Enemy.ChangeableLife.GetShield(-allShield);//相手のシールドを全て破壊
        Enemy.ChangeableLife.ReceiveDamage();
    }

    #endregion
}
