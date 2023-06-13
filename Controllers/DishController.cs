using AutoMapper;
using dotnet_api_test.Exceptions.ExceptionResponses;
using dotnet_api_test.Models.Dtos;
using dotnet_api_test.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet_api_test.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly ILogger<DishController> _logger;
        private readonly IMapper _mapper;
        private readonly IDishRepository _dishRepository;

        public DishController(ILogger<DishController> logger, IMapper mapper, IDishRepository dishRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _dishRepository = dishRepository;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<DishesAndAveragePriceDto> GetDishesAndAverageDishPrice()
        {
            _logger.LogInformation($"Controller action 'GetDishesAndAvarageDishPrice' executed on {DateTime.Now.TimeOfDay}");

            return Ok(new DishesAndAveragePriceDto()
            {
                AveragePrice = _dishRepository.GetAverageDishPrice(),
                Dishes = _dishRepository.GetAllDishes().Select(d => _mapper.Map<ReadDishDto>(d))
            });
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ReadDishDto> GetDishById(int id)
        {
            _logger.LogInformation($"Controller action 'GetDishById' executed on {DateTime.Now.TimeOfDay}");

            var dish = _dishRepository.GetDishById(id);

            if (dish is null)
            {
                _logger.LogInformation($"Dish with ID {id} not found");
                throw new NotFoundRequestExceptionResponse($"Could not find dish with ID {id}");
            }

            _logger.LogInformation($"Retrieved dish with ID {id} successfully");
            return Ok(_mapper.Map<ReadDishDto>(dish));
        }

        [HttpPost]
        [Route("")]
        public ActionResult<ReadDishDto> CreateDish([FromBody] CreateDishDto createDishDto)
        {
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<ReadDishDto> UpdateDishById(int id, UpdateDishDto updateDishDto)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteDishById(int id)
        {
            return Ok();
        }
    }
}