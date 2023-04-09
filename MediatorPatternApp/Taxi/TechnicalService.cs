using MediatorPatternApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace MediatorPatternApp.Taxi
{
    /// <summary>
    /// Техническая служба
    /// </summary>
    public class TechnicalService : TaxiService
    {

        private bool _technicianBusy;

        public TechnicalService(TaxiDispatcher mediator) : base(mediator)
        {
        }

        public override void Notify(TaxiServiceMessage message, string someData)
        {
            switch (message)
            {
                case TaxiServiceMessage.TechnicalIssue:
                    {
                        
                        if (!_technicianBusy)
                        {
                            _technicianBusy = true;
                            Task.Run(() =>
                            {
                                Thread.Sleep(GetRandomInt() * 25000);
                                mediator.Send(TaxiServiceMessage.TechnicalIssueFixed, someData);
                                _technicianBusy = false;
                            });
                        }
                        else
                        {
                            mediator.Send(TaxiServiceMessage.TechnicianBusy, someData);
                        }
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("Неподдерживаемый тип сообщения");
                    }
            }
        }

        private int GetRandomInt()
        {
            var rnd = new Random();
            return rnd.Next(10);
        }
    }
}
