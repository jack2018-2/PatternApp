namespace MediatorPatternApp.Models
{
    /// <summary>
    /// Сообщение коммуникации внутри службы такси
    /// </summary>
    public enum TaxiServiceMessage
    {
        /// <summary>
        /// Новый заказ
        /// </summary>
        NewOrder,

        /// <summary>
        /// Выполняется заказ
        /// </summary>
        OrderInProcess,

        /// <summary>
        /// Заказ завершен
        /// </summary>
        OrderFinished,

        /// <summary>
        /// Заказ был отменен
        /// </summary>
        OrderCancelled,

        /// <summary>
        /// Техническая неисправность
        /// </summary>
        TechnicalIssue,
        
        /// <summary>
        /// Техническая неисправность исправлена
        /// </summary>
        TechnicalIssueFixed,

        /// <summary>
        /// Техник недоступен
        /// </summary>
        TechnicianBusy,

        /// <summary>
        /// Линия перегружена
        /// </summary>
        LineOverloaded,

        /// <summary>
        /// Новая машина выходит на линию
        /// </summary>
        NewTaxiOnline,
    }
}
