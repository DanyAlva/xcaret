using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using xcaret.Models;
using System.Text;
using System.Web;
using System.IO;
using System.Diagnostics;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace xcaret.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrincipalController : ControllerBase
    {

        ConnectionStrings opt1;
        public PrincipalController(IOptions <ConnectionStrings> opt) {
            opt1 = opt.Value;
        }

        // GET: api/<PrincipalController>
        [HttpGet()]
        public async Task<List<Entry>> Get()

        {
            HttpClient http = new HttpClient();


            string data = http.GetAsync(opt1.xcaretContext).Result.Content.ReadAsStringAsync().Result;

            Principal obj = JsonConvert.DeserializeObject<Principal>(data);

            List<Entry> filtro1 = obj.entries.ToList();


            return filtro1;

        }

        // GET: api/<PrincipalController>
        [HttpGet("/metodo1")]
        public async Task<List<Entry>> Get([FromQuery (Name = "param1")] bool param1 = true)
        
        {            
            try { 
            HttpClient http = new HttpClient();

            
            string data = http.GetAsync(opt1.xcaretContext).Result.Content.ReadAsStringAsync().Result;

            Principal obj = JsonConvert.DeserializeObject<Principal>(data);

             List<Entry> filtro1 = obj.entries.Where(x => x.HTTPS == param1).ToList();


            return filtro1;
        }
            catch (Exception e)
            {
                ELog.save(this, e);
                return null;
        }

    }

        // GET: api/<PrincipalController>
        [HttpGet("/metodo2")]
        public async Task<List<string>> Get2()

        {
            
            try { 
                HttpClient http = new HttpClient();

                string data = http.GetAsync(opt1.xcaretContext).Result.Content.ReadAsStringAsync().Result;

                Principal obj = JsonConvert.DeserializeObject<Principal>(data);

                List<string> filtro1 = obj.entries.Select(x => x.Category).Distinct().ToList();


                return filtro1;
            }
            catch (Exception e)
            {
                ELog.save(this, e);
                return null;
            }
            
        }


        public class ELog
        {
            public static void save(object obj, Exception ex)
            {
                string fecha = System.DateTime.Now.ToString("yyyyMMdd");
                string hora = System.DateTime.Now.ToString("HH:mm:ss");
                string path =  fecha + ".txt";

                StreamWriter sw = new StreamWriter(path, true);

                StackTrace stacktrace = new StackTrace();
                sw.WriteLine(obj.GetType().FullName + " " + hora);
                sw.WriteLine(stacktrace.GetFrame(1).GetMethod().Name + " - " + ex.Message);
                sw.WriteLine("");

                sw.Flush();
                sw.Close();
            }
        }

    }
}
