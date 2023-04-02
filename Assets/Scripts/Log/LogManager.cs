using System.Collections.Generic;

public static class LogManager
{
    #region Properties

    public static IReadOnlyList<string> SelectCards => _selectCards;
    public static IReadOnlyList<string> UseCards => _useCards;
    public static IReadOnlyList<CardAddDamageLogData> CardAddDamage => _cardAddDamage;
    public static string FirstCardName => _firstCardName;
    public static int TurnCount => _turnCount;

    #endregion

    #region Private Member

    /// <summary>
    /// デッキに入れたカードのリスト
    /// </summary>
    private static List<string> _selectCards = new();

    /// <summary>
    /// 使用したカードのリスト
    /// </summary>
    private static List<string> _useCards = new();

    /// <summary>
    /// カード毎の与えたダメージ
    /// </summary>
    private static List<CardAddDamageLogData> _cardAddDamage = new();

    /// <summary>
    /// 最初に出したカードの名前
    /// </summary>
    private static string _firstCardName;

    /// <summary>
    /// ターン数
    /// </summary>
    private static int _turnCount;

    #endregion

    #region Public Methods

    /// <summary>
    /// デッキに入れたカード名を格納するメソッド
    /// 引数にカード名を配列で渡す
    /// </summary>
    /// <param name="cards"></param>
    public static void SetSelectCards(string[] cards)
    {
        foreach(var card in cards)
        {
            _selectCards.Add(card);
        }
    }

    /// <summary>
    /// 使用したカードを格納するメソッド
    /// 引数にカード名を渡す
    /// </summary>
    /// <param name="card"></param>
    public static void SetUseCards(string card)
    {
        _useCards.Add(card);
    }

    /// <summary>
    /// カード毎の与えたダメージのログを取るためのクラスを格納するメソッド
    /// 引数にCardAddDamageLogDataを渡す
    /// </summary>
    /// <param name="data"></param>
    public static void SetCardAddDamage(CardAddDamageLogData data)
    {
        _cardAddDamage.Add(data);
    }

    /// <summary>
    /// 最初に出したカードを格納するメソッド
    /// 引数にカード名を渡す
    /// </summary>
    /// <param name="cardName"></param>
    public static void SetFirstCard(string cardName)
    {
        _firstCardName = cardName;
    }

    /// <summary>
    /// 1ゲームが終了したときにターン数を格納するメソッド
    /// 引数にターン数を渡す
    /// </summary>
    /// <param name="turnCount"></param>
    public static void SetTurnCount(int turnCount)
    {
        _turnCount = turnCount;
    }

    #endregion
}

/// <summary>
/// カード毎の与えたダメージのログを取るためのクラス
/// コンストラクタにカード名とダメージ量を渡す
/// </summary>
public class CardAddDamageLogData
{
    #region Properties

    public string cardName;

    public int addDamage;

    #endregion

    #region Constructor

    public CardAddDamageLogData(string cardName, int addDamage)
    {
        this.cardName = cardName;
        this.addDamage = addDamage;
    }

    #endregion
}
