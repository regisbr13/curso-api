using System.Threading.Tasks;
using curso_api.Data.VO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Tapioca.HATEOAS;

namespace curso_api.Hypermedia
{       
    public class PersonEricher : ObjectContentResponseEnricher<PersonVO>
    {
        protected override Task EnrichModel(PersonVO content, IUrlHelper urlHelper)
        {
            content.Links.Add(new HyperMediaLink() {            
                Action = HttpActionVerb.GET,                    
                Href = urlHelper.Link("GetPerson", new { id = content.Id }),   
                Rel = RelationType.self,                          
                Type = ResponseTypeFormat.DefaultGet            
            });       
            content.Links.Add(new HyperMediaLink() {            
                Action = HttpActionVerb.POST,                    
                Href = urlHelper.Link("CreatePerson", new { id = content.Id }),     
                Rel = RelationType.self,                          
                Type = ResponseTypeFormat.DefaultPost            
            });                               
            content.Links.Add(new HyperMediaLink() {            
                Action = HttpActionVerb.PUT,                    
                Href = urlHelper.Link("UpdatePerson", new { id = content.Id }),     
                Rel = RelationType.self,                          
                Type = ResponseTypeFormat.DefaultPost            
            }); 
            content.Links.Add(new HyperMediaLink() {            
                Action = HttpActionVerb.DELETE,                    
                Href = urlHelper.Link("DeletePerson", new { id = content.Id }),     
                Rel = RelationType.self,                          
                Type = "int"            
            }); 

            return null;
            
        }
    }
}