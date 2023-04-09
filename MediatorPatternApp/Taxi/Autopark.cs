using MediatorPatternApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

namespace MediatorPatternApp.Taxi
{
    /// <summary>
    /// Автопарк службы такси
    /// </summary>
    public class Autopark : TaxiService
    {
        public Autopark(TaxiDispatcher mediator) : base(mediator)
        {
        }

        public override void Notify(TaxiServiceMessage message, string somdeData)
        {
            switch (message)
            {
                case TaxiServiceMessage.OrderCancelled:
                    {
                        mediator.Send(TaxiServiceMessage.OrderCancelled, somdeData);
                        break;
                    }
                case TaxiServiceMessage.LineOverloaded:
                    {
                        if (RandomBool())
                        {
                            mediator.Send(TaxiServiceMessage.NewTaxiOnline, string.Empty);
                        }
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("Неподдерживаемый тип сообщения");
                    }
            }
        }

        private bool RandomBool()
        {
            var rnd = new Random();
            return rnd.Next(10) >= 5;
        }
    }
}
