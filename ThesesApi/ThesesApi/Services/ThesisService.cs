using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
using ThesesApi.Models;


namespace ThesesApi.Services;

public class ThesisService
{
    private readonly IThesisRepository _thesisRepository;

    private readonly IMapper _mapper;

    public ThesisService(IThesisRepository thesisRepository, IMapper mapper)
    {
        _thesisRepository = thesisRepository;
        _mapper = mapper;
    }

    public async Task<ThesisResource> CreateThesis(ThesisForm thesisForm)
    {
        var thesis = _mapper.Map<Thesis>(thesisForm);
        thesis.Created = DateTime.Now;
        thesis.Updated = DateTime.Now;

        await _thesisRepository.addAsync(thesis);

        return _mapper.Map<ThesisResource>(thesis);
    }

    public async Task<ThesisTableItemResourceDataTableResult> getPagedThesesAsync(
            int page, 
            int pageSize, 
            Dictionary<string, string>? sortings, 
            Dictionary<string, string>? filters)
    {
        var enumerableTheses  = await _thesisRepository.getAllTheseAsync();

        IQueryable<Thesis> queryableTheses = enumerableTheses.AsQueryable();

        if (filters != null)
        {
            foreach (var filter in filters)
            {
                queryableTheses = queryableTheses.Where(t => FilterThesis(t, filter.Key, filter.Value));
            }
        }
        if (sortings != null)
        {
            foreach (var sorting in sortings)
            {
                queryableTheses = SortTheses(queryableTheses, sorting.Key, sorting.Value);
            }
        }

        var totalItems = enumerableTheses.Count();

        var totalPages = (int)Math.Ceiling(totalItems / (double) pageSize);

        var items = enumerableTheses.Skip((page - 1) * pageSize).Take(pageSize)
        .Select(t => new ThesisTableItemResource
        {
            Id = t.Id,
            MainAuthor = $"{t.MainAuthor.FirstName} {t.MainAuthor.LastName} {t.MainAuthor.MiddleName}",
            ContactEmail = t.ContactEmail,
            Topic = t.Topic,
            Created = t.Created,
            Updated = t.Updated
        })
        .ToList();

        var result = new ThesisTableItemResourceDataTableResult
        {
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize,
            TotalPages = totalPages,
            Items = items
        };

        return result;
    }

    public async Task<IEnumerable<ThesisTableItemResource>> GetAll()
    {
        var theses = await _thesisRepository.getAllTheseAsync();

        return _mapper.Map<IEnumerable<ThesisTableItemResource>>(theses);
    }

    public async Task<ThesisResource> GetThesisResourceById(long id)
    {
        var these = await _thesisRepository.getByIdAsync(id);

        return _mapper.Map<ThesisResource>(these);
    }

    public async Task<Thesis> GetThesisById(long id){
        return await _thesisRepository.getByIdAsync(id);
    }
    
   private bool FilterThesis(Thesis thesis, string key, string value)
    {
        return key.ToLower() switch
        {
            "id" => thesis.Id.ToString().Contains(value),
            "contactemail" => thesis.ContactEmail.Contains(value, StringComparison.OrdinalIgnoreCase),
            "topic" => thesis.Topic.Contains(value, StringComparison.OrdinalIgnoreCase),
            "mainauthor" => $"{thesis.MainAuthor.FirstName} {thesis.MainAuthor.LastName}".Contains(value, StringComparison.OrdinalIgnoreCase),
            "created" => thesis.Created.ToString().Contains(value),
            "updated" => thesis.Updated.ToString().Contains(value),
            _ => false,
        };
    }

   private IQueryable<Thesis> SortTheses(IQueryable<Thesis> theses, string key, string direction)
    {
        switch (key.ToLower())
        {
            case "id":
                return direction.Equals("desc", StringComparison.OrdinalIgnoreCase) ? theses.OrderByDescending(t => t.Id) : theses.OrderBy(t => t.Id);
            case "contactemail":
                return direction.Equals("desc", StringComparison.OrdinalIgnoreCase) ? theses.OrderByDescending(t => t.ContactEmail) : theses.OrderBy(t => t.ContactEmail);
            case "topic":
                return direction.Equals("desc", StringComparison.OrdinalIgnoreCase) ? theses.OrderByDescending(t => t.Topic) : theses.OrderBy(t => t.Topic);
            case "mainauthor":
                return direction.Equals("desc", StringComparison.OrdinalIgnoreCase) ? theses.OrderByDescending(t => $"{t.MainAuthor.FirstName} {t.MainAuthor.LastName}") : theses.OrderBy(t => $"{t.MainAuthor.FirstName} {t.MainAuthor.LastName}");
            case "created":
                return direction.Equals("desc", StringComparison.OrdinalIgnoreCase) ? theses.OrderByDescending(t => t.Created) : theses.OrderBy(t => t.Created);
            case "updated":
                return direction.Equals("desc", StringComparison.OrdinalIgnoreCase) ? theses.OrderByDescending(t => t.Updated) : theses.OrderBy(t => t.Updated);
            default:
                return theses;
        }
    }

    internal async Task DeleteThesis(Thesis thesis)
    {
        await _thesisRepository.deleteAsync(thesis);
    }

    public async Task<ThesisResource> PutThesis(long id, ThesisForm form)
    {
        var thesis = _mapper.Map<Thesis>(form);
        thesis.Updated = DateTime.Now;
        
        await _thesisRepository.updateAsync(thesis);

        return _mapper.Map<ThesisResource>(thesis);
    }
}