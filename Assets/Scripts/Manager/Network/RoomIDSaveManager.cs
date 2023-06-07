using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// スクリプト
/// </summary>
public class RoomIDSaveManager
{
    private RoomIDData _initialRoomIDData;
    private RoomIDData _roomIDData;
    private int _limitTime = 30;

    public event Action OnSaveDataLoaded;
    public event Action OnSaveDataLoadFailed;

    public async UniTask SaveAsync(string roomID)
    {
        _roomIDData = new RoomIDData(roomID, DateTime.Now);
        await JsonUtils.CreateJsonAsync(_roomIDData, $"{FileUtils.GetWritableDirectoryPath()}/{FileUtils.ROOM_ID_DATA_JSON_FILE_NAME}");
    }

    public async UniTask ResetSaveDataAsync()
    {
        _roomIDData = _initialRoomIDData;
        await JsonUtils.CreateJsonAsync(_roomIDData, $"{FileUtils.GetWritableDirectoryPath()}/{FileUtils.ROOM_ID_DATA_JSON_FILE_NAME}");
    }

    public async UniTask<string> LoadSaveData()
    {
        await UniTask.WaitForFixedUpdate();

        // セーブデータのロードを試みる
        try
        {
            _roomIDData = await JsonUtils.LoadJsonAsync<RoomIDData>($"{FileUtils.GetWritableDirectoryPath()}/{FileUtils.ROOM_ID_DATA_JSON_FILE_NAME}");
            CheckSaveData();
        }
        catch (Exception) // 初回ロード時はデータが作成されてないので例外をキャッチする
        {
            // セーブデータを作る
            await JsonUtils.CreateJsonAsync(_initialRoomIDData, $"{FileUtils.GetWritableDirectoryPath()}/{FileUtils.ROOM_ID_DATA_JSON_FILE_NAME}");

            try
            {
                // 作ったデータのロードを試みる
                _roomIDData = await JsonUtils.LoadJsonAsync<RoomIDData>($"{FileUtils.GetWritableDirectoryPath()}/{FileUtils.ROOM_ID_DATA_JSON_FILE_NAME}");
                CheckSaveData();
            }
            catch (Exception)
            {
                OnSaveDataLoadFailed?.Invoke();
                return "";
            }
        }
        var priv = _roomIDData.date;
        var current = DateTime.Now;
        if ((current - priv).Minutes > _limitTime)
            _roomIDData.roomID = "";

        OnSaveDataLoaded?.Invoke();

        return _roomIDData.roomID;
    }

    private void CheckSaveData()
    {
        if (_roomIDData.roomID == "" || _roomIDData.date == null)
            Debug.LogError("データが空です");
    }

    public struct RoomIDData
    {
        #region Properties

        public string roomID;

        public DateTime date;

        #endregion

        #region Constructor

        public RoomIDData(string roomID, DateTime date)
        {
            this.roomID = roomID;
            this.date = date;
        }

        #endregion
    }
}
