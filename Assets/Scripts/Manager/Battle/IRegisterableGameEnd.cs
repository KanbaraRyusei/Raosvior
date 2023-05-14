using System;
/// <summary>
/// インターフェース
/// </summary>
public interface IRegisterableGameEnd
{
    event Action<LogData> OnGameEnd;
}
