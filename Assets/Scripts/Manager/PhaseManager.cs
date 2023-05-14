/// <summary>
/// ゲームの進行管理をするクラス
/// </summary>
public static class PhaseManager
{
    #region Properties

    /// <summary>
    /// 現在のフェーズのプロパティ
    /// </summary>
    public static PhaseParameter CurrentPhase { get; private set; }

    /// <summary>
    /// 1つ前のフェーズ
    /// </summary>
    public static PhaseParameter OldPhase { get; private set; }

    public static IntervetionParameter Intervetion { get; private set; }

    public static bool IsFirstPlayer { get; private set; }

    #endregion

    #region Public Methods

    public static void Init()
    {
        CurrentPhase = PhaseParameter.LeaderSelect;
        Intervetion = IntervetionParameter.LeaderCardShaman;
        OldPhase = PhaseParameter.LeaderSelect;
    }

    /// <summary>
    /// フェーズを進める関数
    /// 介入処理があるときのみ引数にtrueを渡す
    /// </summary>
    public static void OnNextPhase(HandEffect handEffect = null)
    {
        if (CurrentPhase == PhaseParameter.Init)// もし決着判定だったら
        {
            OldPhase = CurrentPhase;// 前のフェーズを保存
            CurrentPhase = PhaseParameter.CardSelect;// 最初のフェーズに戻る
            return;
        }
        else if (handEffect != null)// もし介入処理があったら
        {
            var player =
                handEffect.Player.PlayerParameter.SetRSPHand.HandEffect.Player.PlayerParameter;
            if (player == PlayerManager.Instance.Players[0].PlayerParameter) IsFirstPlayer = true;
            else IsFirstPlayer = false;

            var type = handEffect.Player.PlayerParameter.SetRSPHand.GetType();
            if (type == typeof(FPaperCardJudgmentOfAigis))
                Intervetion = IntervetionParameter.FPaperCardJudgmentOfAigis;

            else if (type == typeof(PaperCardDrainShield))
                Intervetion = IntervetionParameter.PaperCardDrainShield;

            else if (type == typeof(ScissorsCardChainAx))
                Intervetion = IntervetionParameter.ScissorsCardChainAx;

            else
            {
                Intervetion = IntervetionParameter.LeaderCardShaman;
                player =
                    handEffect.Player.PlayerParameter.LeaderHand.HandEffect.Player.PlayerParameter;
                if (player == PlayerManager.Instance.Players[0].PlayerParameter) IsFirstPlayer = true;
                else IsFirstPlayer = false;
            }

            OldPhase = CurrentPhase;// 前のフェーズを保存
            CurrentPhase = PhaseParameter.Intervention;// 介入フェーズにする
            return;
        }
        else if (CurrentPhase == PhaseParameter.Intervention)// もし介入処理だったら
        {
            CurrentPhase = OldPhase;// 介入前のフェーズに戻す
            return;
        }
        OldPhase = CurrentPhase;// 前のフェーズを保存
        CurrentPhase++;// 現在のフェーズを次のフェーズに進める
    }

    public static void OnGameEndPhase()
    {
        CurrentPhase = PhaseParameter.GameEnd;
    }

    public static void SetPhase(PhaseParameter current, PhaseParameter old)
    {
        CurrentPhase = current;
        OldPhase = old;
    }

    #endregion
}
