using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class LogSaveManager : SingletonMonoBehaviour<LogSaveManager>
{

    #region Private Member

    private IRegisterableGameEnd _registerableGameEnd = null;
    private LogData _initialSaveData;

    #endregion

    #region Private Member

    private LogData _saveData;

    #endregion

    #region Public Delegate

    public event Action OnSaveDataLoded;

    public event Action OnSaveDataLoadFailed;

    #endregion

    #region Unity Methods

    protected override void Awake()
    {
        base.Awake();

        TryGetComponent(out _registerableGameEnd);
        RPCManager.Instance.OnStartGame += SetGameStartDelegate;
         _registerableGameEnd.OnGameEnd += SetGameEndDelegate;
    }

    private void OnDisable()
    {
        RPCManager.Instance.OnStartGame -= SetGameStartDelegate;
        _registerableGameEnd.OnGameEnd -= SetGameEndDelegate;
    }

    #endregion

    #region Public Methods

    public async UniTask SaveAsync(LogData saveData)
    {
        _saveData = saveData;
        await JsonUtils.CreateJsonAsync(_saveData, $"{FileUtils.GetWritableDirectoryPath()}/{FileUtils.SAVE_DATA_JSON_FILE_NAME}");
    }

    public async UniTask ResetSaveDataAsync()
    {
        _saveData = _initialSaveData;
        await JsonUtils.CreateJsonAsync(_saveData, $"{FileUtils.GetWritableDirectoryPath()}/{FileUtils.SAVE_DATA_JSON_FILE_NAME}");
    }

    public async UniTask LoadSaveData()
    {
        await UniTask.WaitForFixedUpdate();

        // セーブデータのロードを試みる
        try
        {
            _saveData = await JsonUtils.LoadJsonAsync<LogData>($"{FileUtils.GetWritableDirectoryPath()}/{FileUtils.SAVE_DATA_JSON_FILE_NAME}");
            CheckSaveData();
        }
        catch (Exception) // 初回ロード時はデータが作成されてないので例外をキャッチする
        {
            // セーブデータを作る
            await JsonUtils.CreateJsonAsync(_initialSaveData, $"{FileUtils.GetWritableDirectoryPath()}/{FileUtils.SAVE_DATA_JSON_FILE_NAME}");

            try
            {
                // 作ったデータのロードを試みる
                _saveData = await JsonUtils.LoadJsonAsync<LogData>($"{FileUtils.GetWritableDirectoryPath()}/{FileUtils.SAVE_DATA_JSON_FILE_NAME}");
                CheckSaveData();
            }
            catch (Exception)
            {
                OnSaveDataLoadFailed?.Invoke();
                return;
            }
        }

        OnSaveDataLoded?.Invoke();
    }

    #endregion

    #region Private Method

    private void CheckSaveData()
    {
        if (_saveData.SelectCards == null || _saveData.UseCards == null || _saveData.CardAddDamage == null || string.IsNullOrWhiteSpace(_saveData.FirstCardName) || _saveData.TurnCount == 0)
        {
            Debug.LogError("データが空です");
        }
    }

    private void SetGameStartDelegate()
    {
        LogManager.Init();
    }

    private void SetGameEndDelegate(LogData logData)
    {
        SaveAsync(logData).Forget();
    }

    #endregion
}

[Serializable]
public class LogData
{
    #region Properties

    public IReadOnlyList<string> SelectCards => _selectCards;
    public IReadOnlyList<string> UseCards => _useCards;
    public IReadOnlyList<CardAddDamageLogData> CardAddDamage => _cardAddDamage;
    public string FirstCardName => _firstCardName;
    public int TurnCount => _turnCount;

    #endregion

    #region Private Members

    /// <summary>
    /// デッキに入れたカードのリスト
    /// </summary>
    private string[] _selectCards = new string[5];

    /// <summary>
    /// 使用したカードのリスト
    /// </summary>
    private List<string> _useCards = new();

    /// <summary>
    /// カード毎の与えたダメージ
    /// </summary>
    private List<CardAddDamageLogData> _cardAddDamage = new();

    /// <summary>
    /// 最初に出したカードの名前
    /// </summary>
    private string _firstCardName;

    /// <summary>
    /// ターン数
    /// </summary>
    private int _turnCount;

    #endregion

    #region Constructor

    public LogData(string[] selectCards, List<string> useCards, List<CardAddDamageLogData> cardAddDamage, string firstCardName, int turnCount)
    {
        _selectCards = selectCards;
        _useCards = useCards;
        _cardAddDamage = cardAddDamage;
        _firstCardName = firstCardName;
        _turnCount = turnCount;
    }

    #endregion
}