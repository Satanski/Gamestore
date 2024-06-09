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
    public async Task<IActionResult> GetAsync()
    {
        IEnumerable<PublisherModelDto> publishers;

        publishers = await publisherService.GetAllPublishersAsync();

        return publishers.Any() ? Ok(publishers) : NotFound();
    }

    // GET: publishers/GUID
    [HttpGet("find/{id}")]
    public async Task<IActionResult> GetPublisherById(Guid id)
    {
        var publisher = await publisherService.GetPublisherByIdAsync(id);
        if (publisher == null)
        {
            return NotFound();
        }

        return Ok(publisher);
    }

    // GET: publishers/STRING
    [HttpGet("{companyName}")]
    public async Task<IActionResult> GetPublisherByCompanyName(string companyName)
    {
        var publisher = await publisherService.GetPublisherByCompanyNameAsync(companyName);
        if (publisher == null)
        {
            return NotFound();
        }

        return Ok(publisher);
    }

    // GET: publishers/GUID/games
    [HttpGet("{id}/games")]
    public async Task<IActionResult> GetGamesByPublisher(Guid id)
    {
        var publisher = await publisherService.GetGamesByPublisherIdAsync(id);
        if (publisher == null)
        {
            return NotFound();
        }

        return Ok(publisher);
    }

    // POST: publishers
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] PublisherModelDto publisherModel)
    {
        await publisherService.AddPublisherAsync(publisherModel);

        return Ok();
    }

    // PUT: publishers
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] PublisherModel publisherModel)
    {
        await publisherService.UpdatePublisherAsync(publisherModel);

        return Ok();
    }

    // DELETE: publishers
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await publisherService.DeletPublisherByIdAsync(id);

        return Ok();
    }
}
