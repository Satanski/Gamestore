using Gamestore.BLL.Interfaces;
using Gamestore.BLL.Models;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> GetPublisherByIdAsync(Guid id)
    {
        var publisher = await publisherService.GetPublisherByIdAsync(id);

        return publisher == null ? NotFound() : Ok(publisher);
    }

    // GET: publishers/STRING
    [HttpGet("{companyName}")]
    public async Task<IActionResult> GetPublisherByCompanyNameAsync(string companyName)
    {
        var publisher = await publisherService.GetPublisherByCompanyNameAsync(companyName);

        return publisher == null ? NotFound() : Ok(publisher);
    }

    // GET: publishers/GUID/games
    [HttpGet("{publisherName}/games")]
    public async Task<IActionResult> GetGamesByPublisherNameAsync(string publisherName)
    {
        var games = await publisherService.GetGamesByPublisherNameAsync(publisherName);

        return Ok(games);
    }

    // POST: publishers
    [HttpPost]
    [Authorize(Policy = "ManageEntities")]
    public async Task<IActionResult> AddPublisherAsync([FromBody] PublisherDtoWrapper publisherModel)
    {
        await publisherService.AddPublisherAsync(publisherModel);

        return Ok();
    }

    // PUT: publishers
    [HttpPut]
    [Authorize(Policy = "ManageEntities")]
    public async Task<IActionResult> UpdatePublisherAsync([FromBody] PublisherDtoWrapper publisherModel)
    {
        await publisherService.UpdatePublisherAsync(publisherModel);

        return Ok();
    }

    // DELETE: publishers
    [HttpDelete("{id}")]
    [Authorize(Policy = "ManageEntities")]
    public async Task<IActionResult> DeletePublisherByIdAsync(Guid id)
    {
        await publisherService.DeletPublisherByIdAsync(id);

        return Ok();
    }
}
