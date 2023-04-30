/// <summary>
/// ゲームの進行管理をするクラス
/// </summary>
public class PhaseManager : SingletonMonoBehaviour<PhaseManager>
{
    #region Properties

    /// <summary>
    /// 現在のフェーズを公開するプロパティ
    /// </summary>
    public PhaseParameter CurrentPhaseProperty { get; private set; }

    public IntervetionParameter IntervetionProperty { get; private set; }

    public bool IsFirstPlayer { get; private set; }

    public PlayerInterface[] PlayerInterfaces { get; private set; }

    #endregion

    #region Member Variables

    private PhaseParameter _currentPhase;// 現在のフェーズ

    private PhaseParameter _oldPhase;// 1つ前のフェーズ

    #endregion

    #region Public Methods

    /// <summary>
    /// フェーズを進める関数
    /// 介入処理があるときのみ引数にtrueを渡す
    /// </summary>
    public void OnNextPhase(HandEffect handEffect = null)
    {
        if (_currentPhase == PhaseParameter.Judgement)// もし決着判定だったら
        {
            _oldPhase = _currentPhase;// 前のフェーズを保存
            _currentPhase = PhaseParameter.CardSelect;// 最初のフェーズに戻る
            CurrentPhaseProperty = _currentPhase;// 外部に現在のフェーズを公開する
            return;
        }
        if (handEffect != null)// もし介入処理があったら
        {
            var player =
                handEffect.Player.PlayerParameter.SetRSPHand.HandEffect.Player.PlayerParameter;
            if (player == PlayerInterfaces[0].PlayerParameter) IsFirstPlayer = true;
            else IsFirstPlayer = false;

            var type = handEffect.Player.PlayerParameter.SetRSPHand.GetType();
            if (type == typeof(FPaperCardJudgmentOfAigis))
                IntervetionProperty = IntervetionParameter.FPaperCardJudgmentOfAigis;

            else if (type == typeof(PaperCardDrainShield))
                IntervetionProperty = IntervetionParameter.PaperCardDrainShield;

            else if (type == typeof(ScissorsCardChainAx))
                IntervetionProperty = IntervetionParameter.ScissorsCardChainAx;

            else
            {
                IntervetionProperty = IntervetionParameter.LeaderCardShaman;
                player =
                    handEffect.Player.PlayerParameter.LeaderHand.HandEffect.Player.PlayerParameter;
                if (player == PlayerInterfaces[0].PlayerParameter) IsFirstPlayer = true;
                else IsFirstPlayer = false;
            }

            _oldPhase = _currentPhase;// 前のフェーズを保存
            _currentPhase = PhaseParameter.Intervention;// 介入フェーズにする
            CurrentPhaseProperty = _currentPhase;// 外部に現在のフェーズを公開する
            return;
        }
        if (_currentPhase == PhaseParameter.Intervention)// もし介入処理だったら
        {
            _currentPhase = _oldPhase;// 介入前のフェーズに戻す
            return;
        }
        _oldPhase = _currentPhase;// 前のフェーズを保存
        _currentPhase++;// 現在のフェーズを次のフェーズに進める
        CurrentPhaseProperty = _currentPhase;// 外部に現在のフェーズを公開する
    }

    #endregion
}
