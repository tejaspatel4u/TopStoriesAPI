using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly TestDBContext _context;
        private readonly IConfiguration _config;

        public ProductsController(TestDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> GetStories(string APIKey)
        {
            try
            {
                if (_config["APIKey"] == APIKey)
                {
                    string apiEndPoint = _config["NYTimesAPIEndpoint"];
                    string urlParameters = "?api-key=" + _config["NYTimesAPIKey"].ToString();

                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Get, apiEndPoint + urlParameters);
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var obj = await response.Content.ReadAsStringAsync();

                    Root jsonObject = JsonConvert.DeserializeObject<Root>(obj);

                    /**************/
                    _context.Database.ExecuteSqlRaw("TRUNCATE TABLE StoryInfo");

                    // Loop through each result and map to StoryInfo entity
                    foreach (var result in jsonObject.results)
                    {
                        var storyInfo = new StoryInfo
                        {
                            Section = result.section,
                            Subsection = result.subsection,
                            Title = result.title,
                            Abstract = result.@abstract,
                            Url = result.url,
                            Uri = result.uri,
                            Byline = result.byline,
                            Item_Type = result.item_type,
                            Updated_Date = result.updated_date,
                            Created_Date = result.created_date,
                            Published_Date = result.published_date,
                            Material_Type_Facet = result.material_type_facet,
                            Kicker = result.kicker,
                            // Join lists into comma-separated strings
                            Des_Facet = string.Join(", ", result.des_facet),
                            Org_Facet = string.Join(", ", result.org_facet),
                            Per_Facet = string.Join(", ", result.per_facet),
                            Geo_Facet = string.Join(", ", result.geo_facet),
                            Multimedia = result.multimedia !=null ? string.Join(", ", result.multimedia.Select(m => m.url)) : string.Empty,
                            Short_Url = result.short_url
                        };

                        // Add StoryInfo to the database context
                        _context.StoryInfos.Add(storyInfo);

                        //var stories = await _context.StoryInfos.ToListAsync();
                        /********************/
                    }

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    return Ok(jsonObject);
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
