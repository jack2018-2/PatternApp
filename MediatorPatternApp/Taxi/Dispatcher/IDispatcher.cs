using MediatorPatternApp.Models;

namespace MediatorPatternApp.Taxi
{
    /// <summary>
    /// Интерфейс медиатора
    /// </summary>
    public interface IDispatcher
    {
        /// <summary>
        /// Отправить сообщение
        /// </summary>
        /// <param name="message"></param>
        void Send(TaxiServiceMessage message, string someData);
    }
}