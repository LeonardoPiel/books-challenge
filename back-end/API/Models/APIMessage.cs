using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Models
{
    public class APIMessage
    {
        public List<string>? Errors { get; set; }
        public object Data { get; set; }
        public APIMessage(List<string> errors, object data) 
        {
            Errors = errors;
            Data = data;    
        }
        public APIMessage()
        {
            Errors = new List<string>();
            Data = new object(); 
        }
        public static APIMessage NotFound(object obj)
        {
            var ret = new APIMessage();
            ret.Errors.Add("Nenhum registro encontrado");
            ret.Data = obj != null ? obj :"";
            return ret;
        }
        public static APIMessage Ok(object obj)
        {
            if(obj is  APIMessage) { return (APIMessage) obj; }
            var ret = new APIMessage();
            ret.Data = obj;
            return ret;
        }
    }
}
