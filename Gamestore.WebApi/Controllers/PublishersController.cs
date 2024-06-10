using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class PublishersController([FromServices] IPublisherService publisherService) : ControllerBase
{
    // GET: publishers
    [HttpGet]
    public async Task<IActionResult> GetPublishersAsync()
    {
        var publishers = await publisherService.GetAllPublishersAsync();

        return publishers.Any() ? Ok(publishers) : NotFound();
    }

    // GET: publishers/GUID
    [HttpGet("find/{id}")]
    public async Task<IActionResult> GetPublisherById(Guid id)
    {
        var publisher = await publisherService.GetPublisherByIdAsync(id);

        return publisher == null ? NotFound() : Ok(publisher);
    }

    // GET: publishers/STRING
    [HttpGet("{companyName}")]
    public async Task<IActionResult> GetPublisherByCompanyName(string companyName)
    {
        var publisher = await publisherService.GetPublisherByCompanyNameAsync(companyName);

        return publisher == null ? NotFound() : Ok(publisher);
    }

    // GET: publishers/GUID/games
    [HttpGet("{id}/games")]
    public async Task<IActionResult> GetGamesByPublisher(Guid id)
    {
        var games = await publisherService.GetGamesByPublisherIdAsync(id);

        return games.Any() ? NotFound() : Ok(games);
    }

    // POST: publishers
    [HttpPost]
    public async Task<IActionResult> AddPublisherAsync([FromBody] PublisherModelDto publisherModel)
    {
        await publisherService.AddPublisherAsync(publisherModel);

        return Ok();
    }

    // PUT: publishers
    [HttpPut]
    public async Task<IActionResult> UpdatePublisherAsync([FromBody] PublisherModel publisherModel)
    {
        await publisherService.UpdatePublisherAsync(publisherModel);

        return Ok();
    }

    // DELETE: publishers
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePublisherByIdAsync(Guid id)
    {
        await publisherService.DeletPublisherByIdAsync(id);

        return Ok();
    }
}
