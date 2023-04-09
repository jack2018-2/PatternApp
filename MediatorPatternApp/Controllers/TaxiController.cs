using MediatorPatternApp.Models;
using MediatorPatternApp.Taxi;
using Microsoft.AspNetCore.Mvc;

namespace MediatorPatternApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxiController : ControllerBase
    {
        private readonly ILogger<TaxiController> _logger;
        
        private TaxiDispatcher _dispatcher { get; set; }

        public TaxiController(ILogger<TaxiController> logger)
        {
            _dispatcher = TaxiDispatcher.GetInstance();


            _logger = logger;
        }

        /// <summary>
        /// ����� ����� �����
        /// </summary>
        /// <param name="someData">��������� ������ � ������ ��� ��������</param>
        /// <returns></returns>
        [HttpPost]
        [Route("/new")]
        public string NewOrder([FromBody] string someData)
        {
            someData = DateTime.Now.ToString("dd-MM-yyyy_HH:mm:ss");
            var taxi = _dispatcher.SendTaxiToClient(someData);
            return taxi == null ? "�������� �� ��� ������, ���������� ��� ���!" : $"��� ������� ������ � ���������� {taxi.Number}";
        }

        /// <summary>
        /// ������ ����� �����
        /// </summary>
        /// <param name="someData">��������� ������ � ������ ��� ��������</param>
        /// <returns></returns>
        [HttpPost]
        [Route("/cancel")]
        public void CancelOrder([FromBody] string someData)
        {
            _dispatcher.CancelOrder(someData);
        }
    }
}