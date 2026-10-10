using Microsoft.AspNetCore.Mvc;
using PaintCatalogue_WebAPI.Models;
using PaintCatalogue_WebAPI.Storage;

namespace PaintCatalogue_WebAPI.Controllers;

[ApiController]
[Route("api/paints")]
[Produces("application/json")]
public class PaintController : ControllerBase
{
    private static readonly PaintStorage Paints = new();
    private static readonly BrandStorage Brands = new();

    /// <summary>
    /// Returns a list of all paints in the catalogue.
    /// </summary>
    /// <returns>A collection of paints.</returns>
    /// <response code="200">All paints (or an empty array) are returned successfully.</response>
    [HttpGet(Name = "GetAllPaints")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<Paint> Get()
    {
        return Paints.GetAll();
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
    public ActionResult<Paint> Get(int id)
    {
        var paint = Paints.Get(id);
        if (paint == null) return NotFound();

        return Ok(paint);
    }

    /// <summary>
    /// Retrieves a list of paints associated with the specified brand.
    /// </summary>
    /// <param name="id">The unique identifier of the brand.</param>
    /// <returns>A collection of paints of this brand.</returns>
    /// <response code="200">All paints of requested brand (or an empty list) are returned successfully.</response>
    /// <response code="404">If the brand with the specified ID does not exist in the catalogue.</response>
    [HttpGet("brand/{id:int:min(1)}", Name = "GetAllPaintsByBrandId")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public ActionResult<IEnumerable<Paint>> GetByBrand(int id)
    {
        var brandExists = Brands.Get(id) != null;
        if (!brandExists) return NotFound(new { Error = $"Brand with ID {id} is not found." });

        var paintsOfBrand = Paints.GetByBrandId(id);

        return Ok(paintsOfBrand);
    }

    /// <summary>
    /// Creates a new paint and adds it to the catalogue.
    /// </summary>
    /// <param name="newPaint">The paint object containing details like name and brand ID.</param>
    /// <returns>The newly created paint along with its generated ID.</returns>
    /// <response code="201">New paint is created successfully.</response>
    /// <response code="400">
    /// If the provided paint data is invalid, or if the specified brand ID does not exist in the catalogue.
    /// </response>
    [HttpPost(Name = "CreatePaint")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public ActionResult<Paint> Create([FromBody] Paint newPaint)
    {
        var brandExists = Brands.Get(newPaint.BrandId) != null;

        if (!brandExists)
            return BadRequest(new
            {
                Error = $"Brand with ID {newPaint.BrandId} is not found. " +
                        $"Paint must belong to an existing brand."
            });

        var createdPaint = Paints.Add(newPaint);

        return CreatedAtAction(nameof(Get), new { id = createdPaint.Id }, createdPaint);
    }

    /// <summary>
    /// Updates an existing paint's details.
    /// </summary>
    /// <param name="id">The unique identifier of the paint to update.</param>
    /// <param name="updatedPaint">The updated paint data.</param>
    /// <returns>The updated paint.</returns>
    /// <response code="200">The paint is updated successfully.</response>
    /// <response code="400">
    /// If the provided paint data is invalid, or if the specified brand ID does not exist in the catalogue.
    /// </response>
    /// <response code="404">If the paint with the specified ID is not found.</response>
    [HttpPatch("{id:int:min(1)}", Name = "UpdatePaintById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Paint> Update(int id, [FromBody] Paint updatedPaint)
    {
        var existingPaint = Paints.Get(id);
        if (existingPaint == null) return NotFound(new { Error = $"Paint with ID {id} is not found." });

        var brandExists = Brands.Get(updatedPaint.BrandId) != null;
        if (!brandExists)
            return BadRequest(new
            {
                Error = $"Brand with ID {updatedPaint.BrandId} is not found. " +
                        $"Paint must belong to an existing brand."
            });

        var result = Paints.Update(id, updatedPaint);

        return Ok(result);
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
    public IActionResult Delete(int id)
    {
        var existingPaint = Paints.Get(id);
        if (existingPaint == null) return NotFound();

        Paints.Delete(id);
        return NoContent();
    }

    /// <summary>
    /// Deletes all paints from the catalogue.
    /// </summary>
    /// <response code="204">All paints are deleted successfully .</response>
    [HttpDelete(Name = "DeleteAllPaints")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteAll()
    {
        Paints.DeleteAll();
        return NoContent();
    }
}