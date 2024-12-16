namespace TgYandexBot.Core.Interfaces;

/// <summary>
/// Интерфейс для управления авторизацией и токенами
/// </summary>
public interface IYandexDiskAuth
{
    /// <summary>
    /// Получает токен доступа для работы с API Яндекса
    /// </summary>
    /// <returns>OAuth токен пользователя</returns>
    Task<string> GetAccessTokenAsync();

    /// <summary>
    /// Обновляет токен доступа
    /// </summary>
    /// <param name="refreshToken">Токен с истекающим сроком жизни</param>
    /// <returns></returns>
    Task RefreshAccessTokenAsync(string refreshToken);

    /// <summary>
    /// Проверяет, действителен ли токен
    /// </summary>
    /// <param name="accessToken"></param>
    /// <returns>(true/false) Действителен ли токен</returns>
    bool IsTokenValid(string accessToken);
}