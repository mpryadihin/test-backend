using Microsoft.AspNetCore.Mvc;
using ThesesApi.Services;

namespace ThesesApi.Controllers;

[Route("api/theses")]
[ApiController]
public class ThesesController : ControllerBase
{
    
    private readonly ThesisService _thesisService;

    public ThesesController(ThesisService thesisService){
        _thesisService = thesisService;
    }

    [HttpPost]
    public async Task<ActionResult<ThesisResource>> PostThesis([FromBody] ThesisForm form){
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _thesisService.CreateThesis(form);
        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<ThesisTableItemResource>>> GetAllTheses(){
        var result = await _thesisService.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ThesisResource>> GetThesisById(long id){
        var result = await _thesisService.GetThesisResourceById(id);
        if (result == null) {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<ThesisTableItemResourceDataTableResult>> GetTheses(
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] Dictionary<string, string>? sortings = null,
        [FromQuery] Dictionary<string, string>? filters = null)
    {
        var result = await _thesisService.getPagedThesesAsync(page, pageSize, sortings, filters);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteThesis(long id){
        var thesis = await _thesisService.GetThesisById(id);
        if (thesis == null) {
            return NotFound();
        }
        await _thesisService.DeleteThesis(thesis);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ThesisResource>>PutThesis(long id, [FromBody] ThesisForm form) 
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var thesis = await _thesisService.GetThesisById(id);
        if (thesis == null) {
            return NotFound();
        }
        var result = await _thesisService.PutThesis(id, form);
        return Ok(result);
    }
}