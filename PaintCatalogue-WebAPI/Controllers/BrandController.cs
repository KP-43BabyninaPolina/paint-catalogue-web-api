using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaintCatalogue_WebAPI.DataModel.Entities;
using PaintCatalogue_WebAPI.Dtos.Brand;
using PaintCatalogue_WebAPI.Dtos.Paint;
using PaintCatalogue_WebAPI.Storage.Contracts;

namespace PaintCatalogue_WebAPI.Controllers;

[ApiController]
[Route("api/brands")]
[Produces("application/json")]
public class BrandController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IBrandRepository _brands;
    private readonly IPaintRepository _paints;
    
    public BrandController(IMapper mapper, IBrandRepository brands, IPaintRepository paints)
    {
        _mapper = mapper;
        _brands = brands;
        _paints = paints;
    }

    /// <summary>
    /// Returns a list of all brands in the catalogue.
    /// </summary>
    /// <response code="200">All brands (or an empty array) are returned successfully.</response>
    [HttpGet(Name = "GetAllBrands")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BrandOutputDto>>> Get()
    {
        // Замінити на ToListAsync() з _mapper.ProjectTo
        // коли in-memory сховище буде повністю замінене на Entity Framework Core.
        // Наразі використовується ToList() для сумісності з локальним List<Brand>.
        var brandsEntities = _brands.GetAll().ToList();
        var result = _mapper.Map<IEnumerable<BrandOutputDto>>(brandsEntities);
    
        return Ok(result);
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
    public async Task<ActionResult<BrandOutputDto>> Get(int id)
    {
        var brand = await _brands.FindAsync(id);
        if (brand is null) return NotFound();

        return Ok(_mapper.Map<BrandOutputDto>(brand));
    }

    /// <summary>
    /// Retrieves a list of paints associated with the specified brand.
    /// </summary>
    /// <param name="id">The unique identifier of the brand.</param>
    /// <returns>A collection of paints of this brand.</returns>
    /// <response code="200">All paints of requested brand (or an empty list) are returned successfully.</response>
    /// <response code="404">If the brand with the specified ID does not exist in the catalogue.</response>
    [HttpGet("{id:int:min(1)}/paints", Name = "GetAllPaintsByBrandId")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<ActionResult<IEnumerable<PaintOutputDto>>> GetPaints(int id)
    {
        var noBrand = await _brands.FindAsync(id) is null;
        if (noBrand) return NotFound(new { Error = $"Brand with ID {id} is not found." });

        var paintsOfBrand = await _paints.GetByBrandIdAsync(id);

        return Ok(_mapper.Map<IEnumerable<PaintOutputDto>>(paintsOfBrand));
    }

    /// <summary>
    /// Creates a new brand and adds it to the catalogue.
    /// </summary>
    /// <param name="newBrandDto">The brand object containing details like name and country.</param>
    /// <returns>The newly created brand along with its generated ID.</returns>
    /// <response code="201">New brand is created successfully.</response>
    /// <response code="400">If the provided brand data is invalid.</response>
    [HttpPost(Name = "CreateBrand")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<ActionResult<BrandOutputDto>> Create([FromBody] BrandCreateDto newBrandDto)
    {
        var brandEntity = _mapper.Map<Brand>(newBrandDto);
        await _brands.AddAsync(brandEntity);

        var outputDto = _mapper.Map<BrandOutputDto>(brandEntity);
        return CreatedAtAction(nameof(Get), new { id = outputDto.Id }, outputDto);
    }

    /// <summary>
    /// Updates an existing brand's details.
    /// </summary>
    /// <param name="id">The unique identifier of the brand to update.</param>
    /// <param name="updatedBrandDto">The updated brand data.</param>
    /// <returns>The updated brand.</returns>
    /// <response code="200">The brand is updated successfully.</response>
    /// <response code="400">If the provided brand data is invalid.</response>
    /// <response code="404">If the brand with the specified ID is not found.</response>
    [HttpPut("{id:int:min(1)}", Name = "UpdateBrandById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BrandOutputDto>> Update(int id, [FromBody] BrandCreateDto updatedBrandDto)
    {
        var brand = await _brands.FindAsync(id);
        if (brand is null)
        {
            return NotFound(new { Error = $"Brand with ID {id} is not found." });
        }

        _mapper.Map(updatedBrandDto, brand);
        await _brands.UpdateAsync(brand);

        return Ok(_mapper.Map<BrandOutputDto>(brand));
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
    public async Task<IActionResult> Delete(int id)
    {
        var brand = await _brands.FindAsync(id);
        if (brand is null) return NotFound();

        var linkedPaints = await _paints.GetByBrandIdAsync(id);
        if (linkedPaints.Any())
        {
            return Conflict(new { Error = "Cannot delete brand with paint records." });
        }

        await _brands.DeleteAsync(brand);
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
    public async Task<IActionResult> DeleteAll()
    {
        // Замінити на AnyAsync()
        // коли in-memory сховище буде повністю замінене на Entity Framework Core.
        // Наразі використовується ToList() для сумісності з локальним List<Brand>.
        var hasPaints = _paints.GetAll().ToList().Any();
        if (hasPaints)
        {
            return Conflict(new { Error = "Cannot delete all brands: the paint catalogue is not empty." });
        }

        await _brands.DeleteAllAsync();
        return NoContent();
    }
}