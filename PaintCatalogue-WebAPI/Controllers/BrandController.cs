using Microsoft.AspNetCore.Mvc;
using PaintCatalogue_WebAPI.Models;
using PaintCatalogue_WebAPI.Storage;

namespace PaintCatalogue_WebAPI.Controllers;

[ApiController]
[Route("api/brands")]
[Produces("application/json")]
public class BrandController : ControllerBase
{
    private static readonly BrandStorage Brands = new();
    private static readonly PaintStorage Paints = new();

    /// <summary>
    /// Returns a list of all brands in the catalogue.
    /// </summary>
    /// <response code="200">All brands (or an empty array) are returned successfully.</response>
    [HttpGet(Name = "GetAllBrands")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<Brand> Get()
    {
        return Brands.GetAll();
    }

    /// <summary>
    /// Retrieves a specific brand by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the brand.</param>
    /// <returns>The requested brand.</returns>
    /// <response code="200">Requested brand is returned successfully.</response>
    /// <response code="404">If the brand with the specified ID is not found.</response>
    [HttpGet("{id:int:min(1)}", Name = "GetBrandById")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public ActionResult<Brand> Get(int id)
    {
        var brand = Brands.Get(id);
        if (brand == null) return NotFound();

        return Ok(brand);
    }

    /// <summary>
    /// Creates a new brand and adds it to the catalogue.
    /// </summary>
    /// <param name="newBrand">The brand object containing details like name and country.</param>
    /// <returns>The newly created brand along with its generated ID.</returns>
    /// <response code="201">New brand is created successfully.</response>
    /// <response code="400">If the provided brand data is invalid.</response>
    [HttpPost(Name = "CreateBrand")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public ActionResult<Brand> Create([FromBody] Brand newBrand)
    {
        var createdBrand = Brands.Add(newBrand);

        return CreatedAtAction(nameof(Get), new { id = createdBrand.Id }, createdBrand);
    }

    /// <summary>
    /// Updates an existing brand's details.
    /// </summary>
    /// <param name="id">The unique identifier of the brand to update.</param>
    /// <param name="updatedBrand">The updated brand data.</param>
    /// <returns>The updated brand.</returns>
    /// <response code="200">The brand is updated successfully.</response>
    /// <response code="400">If the provided brand data is invalid.</response>
    /// <response code="404">If the brand with the specified ID is not found.</response>
    [HttpPatch("{id:int:min(1)}", Name = "UpdateBrandById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Brand> Update(int id, [FromBody] Brand updatedBrand)
    {
        var existingBrand = Brands.Get(id);
        if (existingBrand == null) return NotFound(new { Error = $"Brand with ID {id} is not found." });

        var result = Brands.Update(id, updatedBrand);

        return Ok(result);
    }

    /// <summary>
    /// Deletes a specific brand. Only allowed if the brand has no associated paints.
    /// </summary>
    /// <param name="id">The unique identifier of the brand to delete.</param>
    /// <response code="204">The brand is successfully deleted.</response>
    /// <response code="404">If the brand with the specified ID was not found.</response>
    /// <response code="409">If the brand cannot be deleted because it has paints linked to it.</response>
    [HttpDelete("{id:int:min(1)}", Name = "DeleteBrandById")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Delete(int id)
    {
        var existingBrand = Brands.Get(id);
        if (existingBrand == null) return NotFound();

        var linkedPaints = Paints.GetByBrandId(id);
        if (linkedPaints.Any()) return Conflict(new { Error = "Cannot delete brand with paint records." });

        Brands.Delete(id);
        return NoContent();
    }

    /// <summary>
    /// Deletes all brands from the catalogue. Only allowed if there are no paints in the catalogue.
    /// </summary>
    /// <response code="204">All brands are successfully deleted.</response>
    /// <response code="409">If all brands cannot be deleted because of the present paints.</response>
    [HttpDelete(Name = "DeleteAllBrands")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult DeleteAll()
    {
        if (Paints.GetAll().Any())
            return Conflict(new { Error = "Cannot delete all brands: the paint catalogue is not empty." });

        Brands.DeleteAll();
        return NoContent();
    }
}