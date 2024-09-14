using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Models
{
    public class StoryInfo
    {
        public int Id { get; set; }  // Primary Key

        public string Section { get; set; }

        public string Subsection { get; set; }

        public string Title { get; set; }

        public string Abstract { get; set; }  // Maps to [Abstract] in the database

        public string Url { get; set; }

        public string Uri { get; set; }

        public string Byline { get; set; }

        public string Item_Type { get; set; }

        public DateTime? Updated_Date { get; set; }

        public DateTime? Created_Date { get; set; }

        public DateTime? Published_Date { get; set; }

        public string Material_Type_Facet { get; set; }

        public string Kicker { get; set; }

        public string Des_Facet { get; set; }  // Stored as nvarchar(max)

        public string Org_Facet { get; set; }  // Stored as nvarchar(max)

        public string Per_Facet { get; set; }  // Stored as nvarchar(max)

        public string Geo_Facet { get; set; }  // Stored as nvarchar(max)

        public string Multimedia { get; set; }  // Stored as nvarchar(max)

        public string Short_Url { get; set; }

        // Additional properties for handling lists from the API
        [NotMapped]
        public List<string> Des_FacetList { get; set; } = new List<string>();

        [NotMapped]
        public List<string> Org_FacetList { get; set; } = new List<string>();

        [NotMapped]
        public List<string> Per_FacetList { get; set; } = new List<string>();

        [NotMapped]
        public List<string> Geo_FacetList { get; set; } = new List<string>();

        [NotMapped]
        public List<Multimedium> MultimediaList { get; set; }
    }

    public class Result
    {
        public string section { get; set; }
        public string subsection { get; set; }
        public string title { get; set; }
        public string @abstract { get; set; }
        public string url { get; set; }
        public string uri { get; set; }
        public string byline { get; set; }
        public string item_type { get; set; }
        public DateTime updated_date { get; set; }
        public DateTime created_date { get; set; }
        public DateTime published_date { get; set; }
        public string material_type_facet { get; set; }
        public string kicker { get; set; }
        public List<string> des_facet { get; set; }
        public List<string> org_facet { get; set; }
        public List<string> per_facet { get; set; }
        public List<string> geo_facet { get; set; }
        public List<Multimedium> multimedia { get; set; }
        public string short_url { get; set; }
    }

    public class Multimedium
    {
        public string url { get; set; }
        public string format { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public string type { get; set; }
        public string subtype { get; set; }
        public string caption { get; set; }
        public string copyright { get; set; }
    }
    public class Root
    {
        public string status { get; set; }
        public string copyright { get; set; }
        public string section { get; set; }
        public DateTime last_updated { get; set; }
        public int num_results { get; set; }
        public List<Result> results { get; set; }
    }
}
