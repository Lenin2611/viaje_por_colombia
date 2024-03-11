using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using API.Dtos;
using Domain.Models;
using Domain.Interfaces;
using API.Controllers;
using AutoMapper;

public class FlightController : BaseController
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FlightController(IHttpClientFactory httpClientFactory, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _httpClientFactory = httpClientFactory;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> SearchFlightRoute(string departure, string arrival)
    {
        var flights = await GetFlightsFromApi();

        var routes = FindRoutes(flights, departure.ToUpper(), arrival.ToUpper());

        if (routes.Flights.Count == 0)
            return NotFound("No se encontró ninguna ruta que haga este recorrido.");

        return Ok(routes);
    }

    private async Task<List<ResponseDto>> GetFlightsFromApi()
    {
        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.GetAsync("https://bitecingcom.ipage.com/testapi/avanzado.js");

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("No se pudo obtener la lista de vuelos.");

        var content = await response.Content.ReadAsStringAsync();
        var flights = JsonConvert.DeserializeObject<List<ResponseDto>>(content);

        return flights;
    }

    private JourneyDto FindRoutes(List<ResponseDto> flights, string departure, string arrival)
    {
        Dictionary<string, int> distance = new Dictionary<string, int>();
        Dictionary<string, string> previous = new Dictionary<string, string>();
        HashSet<string> visited = new HashSet<string>();
        List<ResponseDto> shortestRoute = new List<ResponseDto>();
        foreach (var flight in flights)
        {
            if (!distance.ContainsKey(flight.DepartureStation))
            {
                distance.Add(flight.DepartureStation, int.MaxValue);
                previous.Add(flight.DepartureStation, null);
            }
            if (!distance.ContainsKey(flight.ArrivalStation))
            {
                distance.Add(flight.ArrivalStation, int.MaxValue);
                previous.Add(flight.ArrivalStation, null);
            }
        }
        distance[departure] = 0;
        while (visited.Count < distance.Count)
        {
            string current = null;
            int minDistance = int.MaxValue;
            foreach (var station in distance)
            {
                if (!visited.Contains(station.Key) && station.Value < minDistance)
                {
                    current = station.Key;
                    minDistance = station.Value;
                }
            }
            if (current == null)
                break;
            visited.Add(current);
            foreach (var flight in flights.Where(f => f.DepartureStation == current))
            {
                int alt = distance[current] + 1;
                if (alt < distance[flight.ArrivalStation])
                {
                    distance[flight.ArrivalStation] = alt;
                    previous[flight.ArrivalStation] = current;
                }
            }
        }
        string target = arrival;
        List<FlightDto> listFlights = new List<FlightDto>();
        JourneyDto journey = new JourneyDto
        {
            Origin = departure,
            Destination = arrival,
            Price = 0,
            Flights = listFlights
        };
        while (previous[target] != null)
        {
            var flight = flights.FirstOrDefault(f => f.DepartureStation == previous[target] && f.ArrivalStation == target);
            if (flight != null)
            {
                shortestRoute.Insert(0, flight);
            }
            target = previous[target];
        }
        foreach (var flight in shortestRoute)
        {
            TransportDto transport = new TransportDto
            {
                FlightCarrier = flight.FlightCarrier,
                FlightNumber = flight.FlightNumber
            };
            FlightDto newFlight = new FlightDto
            {
                Origin = flight.DepartureStation,
                Destination = flight.ArrivalStation,
                Price = flight.Price,
                Transport = transport
            };
            listFlights.Add(newFlight);
            journey.Price += flight.Price;
        }
        return journey;
    }
}