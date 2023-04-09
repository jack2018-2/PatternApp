using MediatorPatternApp.Models;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace MediatorPatternApp.Taxi
{
    /// <summary>
    /// Диспетчер службы такси (класс-медиатор)
    /// </summary>
    public class TaxiDispatcher : IDispatcher
    {
        private static TaxiDispatcher Instance {get;set;}

        private List<LightTaxi> _taxiOnLine { get; set; }

        private Autopark _autopark { get; set; }

        private TechnicalService _techinalService { get; set; }

        private TaxiDispatcher()
        {
            _taxiOnLine = new List<LightTaxi>();
            _autopark = new Autopark(this);
            _techinalService = new TechnicalService(this);
        }

        public static TaxiDispatcher GetInstance()
        {
            if (Instance is null)
            {
                Instance = new TaxiDispatcher();
            }
            return Instance;
        }

        public void Send(TaxiServiceMessage message, string someData)
        {
            LightTaxi? thisTaxi;
            switch (message)
            {
                case TaxiServiceMessage.NewOrder:
                    Random rnd = new Random();
                    thisTaxi = _taxiOnLine.FirstOrDefault(_ => _.IsAvailiable);
                    if (thisTaxi == null)
                    {
                        _autopark.Notify(TaxiServiceMessage.LineOverloaded, someData);
                    }
                    else
                    {
                        thisTaxi.Notify(TaxiServiceMessage.NewOrder, someData);
                    }
                    break;

                case TaxiServiceMessage.OrderCancelled:
                    thisTaxi = _taxiOnLine.FirstOrDefault(_ => _.Number == someData);

                    if (thisTaxi == null)
                    {
                        Debug.WriteLine("Не найдена машина по указанному госномеру");
                        break;
                    }

                    thisTaxi.Notify(TaxiServiceMessage.OrderCancelled, someData);
                    thisTaxi.IsAvailiable = true;

                    break;

                case TaxiServiceMessage.OrderInProcess:
                    _taxiOnLine.FirstOrDefault(_ => _.Number == someData).IsAvailiable = false;
                    break;

                case TaxiServiceMessage.OrderFinished:
                    _taxiOnLine.FirstOrDefault(_ => _.Number == someData).IsAvailiable = true;
                    break;


                case TaxiServiceMessage.TechnicalIssue:
                    _techinalService.Notify(TaxiServiceMessage.TechnicalIssue, someData);

                    // Непустой CurrentOrder означает, что поездка в процессе и ее нужно отменить
                    if (_taxiOnLine.FirstOrDefault(_ => _.Number == someData).CurrentOrder != string.Empty) 
                        _autopark.Notify(TaxiServiceMessage.OrderCancelled, someData);

                    _taxiOnLine.FirstOrDefault(_ => _.Number == someData).IsAvailiable = false;
                    break;

                case TaxiServiceMessage.TechnicianBusy:
                    Task.Run(() =>
                    {
                        Thread.Sleep(5000);
                        _techinalService.Notify(TaxiServiceMessage.TechnicalIssue, someData);
                    });
                    break;

                case TaxiServiceMessage.TechnicalIssueFixed:
                    _taxiOnLine.FirstOrDefault(_ => _.Number == someData).IsAvailiable = true;
                    break;
                    

                case TaxiServiceMessage.LineOverloaded:
                    _autopark.Notify(TaxiServiceMessage.LineOverloaded, someData);
                    break;

                case TaxiServiceMessage.NewTaxiOnline:
                    _taxiOnLine.Add(new LightTaxi(this));
                    break;

                default:
                    throw new ArgumentException("Неподдерживаемый тип сообщения");
            }
        }

        public LightTaxi SendTaxiToClient(string someData)
        {
            Send(TaxiServiceMessage.NewOrder, someData);
            return _taxiOnLine.FirstOrDefault(_ => _.CurrentOrder == someData);
        }

        public void CancelOrder(string someData)
        {
            Send(TaxiServiceMessage.OrderCancelled, someData);
        }
    }
}
