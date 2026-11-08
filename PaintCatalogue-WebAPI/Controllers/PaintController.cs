using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaintCatalogue_WebAPI.DataModel.Entities;
using PaintCatalogue_WebAPI.Dtos.Paint;
using PaintCatalogue_WebAPI.Storage.Contracts;

namespace PaintCatalogue_WebAPI.Controllers;

[ApiController]
[Route("api/paints")]
[Produces("application/json")]
public class PaintController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPaintRepository _paints;
    private readonly IBrandRepository _brands;
    
    public PaintController(IMapper mapper, IPaintRepository paints, IBrandRepository brands)
    {
        _mapper = mapper;
        _paints = paints;
        _brands = brands;
    }

    /// <summary>
    /// Returns a list of all paints in the catalogue.
    /// </summary>
    /// <returns>A collection of paints.</returns>
    /// <response code="200">All paints (or an empty array) are returned successfully.</response>
    [HttpGet(Name = "GetAllPaints")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PaintOutputDto>>> Get()
    {
        // Замінити на ToListAsync() з _mapper.ProjectTo
        // коли in-memory сховище буде повністю замінене на Entity Framework Core.
        // Наразі використовується ToList() для сумісності з локальним List<Paint>.
        var paintsEntities = _paints.GetAll().ToList();
        var result = _mapper.Map<IEnumerable<PaintOutputDto>>(paintsEntities);
        
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a specific paint by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the paint.</param>
    /// <returns>The requested paint.</returns>
    /// <response code="200">Requested paint is returned successfully.</response>
    /// <response code="404">If the paint with the specified ID is not found.</response>
    [HttpGet("{id:int:min(1)}", Name = "GetPaintById")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<ActionResult<PaintOutputDto>> Get(int id)
    {
        var paint = await _paints.FindAsync(id);
        if (paint == null) return NotFound();

        return Ok(_mapper.Map<PaintOutputDto>(paint));
    }

    /// <summary>
    /// Creates a new paint and adds it to the catalogue.
    /// </summary>
    /// <param name="newPaintDto">The paint object containing details like name and brand ID.</param>
    /// <returns>The newly created paint along with its generated ID.</returns>
    /// <response code="201">New paint is created successfully.</response>
    /// <response code="400">
    /// If the provided paint data is invalid, or if the specified brand ID does not exist in the catalogue.
    /// </response>
    [HttpPost(Name = "CreatePaint")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<ActionResult<PaintOutputDto>> Create([FromBody] PaintCreateDto newPaintDto)
    {
        var brand = await _brands.FindAsync(newPaintDto.BrandId);
        if (brand is null)
        {
            return BadRequest(new
            {
                Error = $"Brand with ID {newPaintDto.BrandId} is not found. " +
                        $"Paint must belong to an existing brand."
            });
        }

        var paintEntity = _mapper.Map<Paint>(newPaintDto);
        paintEntity.Brand = brand;
        
        await _paints.AddAsync(paintEntity);

        var outputDto = _mapper.Map<PaintOutputDto>(paintEntity);
        return CreatedAtAction(nameof(Get), new { id = outputDto.Id }, outputDto);
    }

    /// <summary>
    /// Updates an existing paint's details.
    /// </summary>
    /// <param name="id">The unique identifier of the paint to update.</param>
    /// <param name="updatedPaintDto">The updated paint data.</param>
    /// <returns>The updated paint.</returns>
    /// <response code="200">The paint is updated successfully.</response>
    /// <response code="400">
    /// If the provided paint data is invalid, or if the specified brand ID does not exist in the catalogue.
    /// </response>
    /// <response code="404">If the paint with the specified ID is not found.</response>
    [HttpPut("{id:int:min(1)}", Name = "UpdatePaintById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaintOutputDto>> Update(int id, [FromBody] PaintCreateDto updatedPaintDto)
    {
        var paint = await _paints.FindAsync(id);
        if (paint is null)
        {
            return NotFound(new { Error = $"Paint with ID {id} is not found." });
        }

        var brand = await _brands.FindAsync(updatedPaintDto.BrandId);
        if (brand is null)
        {
            return BadRequest(new
            {
                Error = $"Brand with ID {updatedPaintDto.BrandId} is not found. " +
                        $"Paint must belong to an existing brand."
            });
        }

        _mapper.Map(updatedPaintDto, paint);
        paint.Brand = brand;
        
        await _paints.UpdateAsync(paint);
        
        return Ok(_mapper.Map<PaintOutputDto>(paint));
    }

    /// <summary>
    /// Deletes a specific paint.
    /// </summary>
    /// <param name="id">The unique identifier of the brand to delete.</param>
    /// <response code="204">The paint is deleted successfully .</response>
    /// <response code="404">If the paint with the specified ID was not found.</response>
    [HttpDelete("{id:int:min(1)}", Name = "DeletePaintById")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var paint = await _paints.FindAsync(id);
        if (paint is null)
        {
            return NotFound();
        }

        await _paints.DeleteAsync(paint);
        return NoContent();
    }

    /// <summary>
    /// Deletes all paints from the catalogue.
    /// </summary>
    /// <response code="204">All paints are deleted successfully .</response>
    [HttpDelete(Name = "DeleteAllPaints")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAll()
    {
        await _paints.DeleteAllAsync();
        return NoContent();
    }
}