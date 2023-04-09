using MediatorPatternApp.Models;

namespace MediatorPatternApp.Taxi
{
    /// <summary>
    /// Общий класс службы такси
    /// </summary>
    public abstract class TaxiService
    {
        protected TaxiDispatcher mediator;

        public TaxiService(TaxiDispatcher mediator)
        {
            this.mediator = mediator;
        }

        public abstract void Notify(TaxiServiceMessage message, string somdeData);
    }
}
