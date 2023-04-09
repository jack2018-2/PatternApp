using MediatorPatternApp.Models;
using System.Diagnostics;

namespace MediatorPatternApp.Taxi
{
    public class LightTaxi : TaxiService
    {
        public string Number { get; set; }

        public bool IsAvailiable { get; set; }

        public string CurrentOrder { get; set; }

        private CancellationTokenSource _carBrokeSource;
        private CancellationToken _carBroke;

        public LightTaxi(TaxiDispatcher mediator) : base(mediator)
        {
            Number = RandomizeNumber();
            IsAvailiable = true;
            var checkTimer = new Timer(RandomTechnicalIssue, TaxiServiceMessage.TechnicalIssue, 0, 10000);
            _carBrokeSource = new CancellationTokenSource();
            _carBroke = _carBrokeSource.Token;
        }

        private string RandomizeNumber()
        {
            //ToDo сделать нормальный генератор
            return "A" + $"{GetRandomInt()}{GetRandomInt()}{GetRandomInt()}" + "ЕО";
        }

        private int GetRandomInt()
        {
            var rnd = new Random();
            var t = rnd.Next(10);
            return t;
        }

        private void RandomTechnicalIssue(object message)
        {
            if (GetRandomInt() >= 9)
            {
                _carBrokeSource.Cancel();
                mediator.Send(TaxiServiceMessage.TechnicalIssue, Number);
            }
        }

        public override void Notify(TaxiServiceMessage message, string somdeData)
        {
            switch (message)
            {
                case TaxiServiceMessage.OrderCancelled:
                    {
                        Debug.WriteLine($"Ушел с линии из-за поломки: {Number}");
                        break;
                    }
                case TaxiServiceMessage.NewOrder:
                    {
                        CurrentOrder = somdeData;
                        Task.Run(() =>
                        {
                            Debug.WriteLine($"Начал заказ: {CurrentOrder}. Госномер: {Number}");
                            Thread.Sleep(GetRandomInt() * 10000);
                            // если за время заказа проиpошла поломка - не будем лишний раз кидать сообщение, и так поездка завершилась досрочно
                            if (!_carBroke.IsCancellationRequested)
                                mediator.Send(TaxiServiceMessage.OrderFinished, Number);

                            CurrentOrder = string.Empty;
                            Debug.WriteLine($"Закончил заказ: {CurrentOrder}. Госномер: {Number}");
                        });
                        
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("Неподдерживаемый тип сообщения");
                    }
            }
        }
    }
}
