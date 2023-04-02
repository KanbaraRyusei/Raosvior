using UnityEngine;

/// <summary>
/// ゲームの進行管理をするクラス
/// </summary>
public static class PhaseManager
{
    #region public property

    /// <summary>
    /// 現在のフェーズを公開するプロパティ
    /// </summary>
    public static PhaseParameter CurrentPhaseProperty { get; private set;}

    public static IntervetionParameter IntervetionProperty { get; private set; }

    public static bool IsClient { get; private set; }

    #endregion

    #region private member

    private static PhaseParameter _currentPhase;// 現在のフェーズ

    private static PhaseParameter _oldPhase;// 1つ前のフェーズ

    #endregion

    #region public method

    /// <summary>
    /// フェーズを進める関数
    /// 介入処理があるときのみ引数にtrueを渡す
    /// </summary>
    public static void OnNextPhase(HandEffect handEffect = null)
    {
        if(_currentPhase == PhaseParameter.Judgement)// もし決着判定だったら
        {
            _oldPhase = _currentPhase;// 前のフェーズを保存
            _currentPhase = PhaseParameter.CardSelect;// 最初のフェーズに戻る
            CurrentPhaseProperty = _currentPhase;// 外部に現在のフェーズを公開する
            return;
        }
        if(handEffect != null)// もし介入処理があったら
        {
            var player =
                handEffect.Player.PlayerParameter.PlayerSetHand.HandEffect.Player.PlayerParameter;
            if (player == PlayerManager.Players[0].PlayerParameter) IsClient = true;
            else IsClient = false;

            var type = handEffect.Player.PlayerParameter.PlayerSetHand.GetType();
            if (type == typeof(FPaperCardJudgmentOfAigis))
                IntervetionProperty = IntervetionParameter.FPaperCardJudgmentOfAigis;

            else if(type == typeof(PaperCardDrainShield))
                IntervetionProperty = IntervetionParameter.PaperCardDrainShield;

            else if(type == typeof(ScissorsCardChainAx))
                IntervetionProperty = IntervetionParameter.ScissorsCardChainAx;

            else
            {
                IntervetionProperty = IntervetionParameter.LeaderCardShaman;
                player =
                    handEffect.Player.PlayerParameter.LeaderHand.HandEffect.Player.PlayerParameter;
                if (player == PlayerManager.Players[0].PlayerParameter) IsClient = true;
                else IsClient = false;
            }

            _oldPhase = _currentPhase;// 前のフェーズを保存
            _currentPhase = PhaseParameter.Intervention;// 介入フェーズにする
            CurrentPhaseProperty = _currentPhase;// 外部に現在のフェーズを公開する
            return;
        }
        if(_currentPhase == PhaseParameter.Intervention)// もし介入処理だったら
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
