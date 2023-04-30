/// <summary>
/// リーダーの効果の基底クラス
/// </summary>
public abstract class LeaderHandEffect : HandEffect
{
    #region Constants

    protected const RSPParameter ROCK = RSPParameter.Rock;
    protected const RSPParameter SCISSORS = RSPParameter.Scissors;
    protected const RSPParameter PAPER = RSPParameter.Paper;

    #endregion

    #region Public Methods

    /// <summary>
    /// じゃんけんの勝敗がついたときに呼び出される
    /// </summary>
    public abstract void CardEffect();

    #endregion
}
