using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Teste.Models;

namespace Teste.Controllers
{
    public class PaisController : Controller
    {
        private readonly Context _context;

        public PaisController(Context context)
        {
            _context = context;
        }

        // GET: Pais
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pais.ToListAsync());
        }


        // GET: Pais/GetCountry
        [HttpGet]
        public async Task<IActionResult> GetCountry(string country, string abbr)
        {
            var _country = country;
            var _state = abbr;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://services.groupkt.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("state/get/" + _country + "/" + _state);
                if (!response.IsSuccessStatusCode)
                    return Json("Algo deu errado.");               

                var pais = response.Content.ReadAsStringAsync().Result;            

                var obj = JsonConvert.DeserializeObject<RootObject>(pais);

                var x = obj.RestResponse.result;

                if (x == null)
                    return Json("Não foram encontrados dados com essa pesquisa");

                var objResult = new Pais
                {
                    country = x.country,
                    name = x.name,
                    abbr = x.abbr,
                    area = x.area,
                    largest_city = x.largest_city,
                    capital = x.capital                  

                };
                return View(objResult);
            }
        }

        // GET: Pais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pais = await _context.Pais
                .FirstOrDefaultAsync(m => m.id == id);
            if (pais == null)
            {
                return NotFound();
            }

            return View(pais);
        }

        // GET: Pais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,country,name,abbr,area,largest_city,capital")] Pais pais)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pais);
                await _context.SaveChangesAsync();

                return View("~/Views/Pais/ThankYouPage.cshtml");
            }
            return View();
        }

        // GET: Pais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pais = await _context.Pais.FindAsync(id);
            if (pais == null)
            {
                return NotFound();
            }
            return View(pais);
        }

        // POST: Pais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,country,name,abbr,area,largest_city,capital")] Pais pais)
        {
            if (id != pais.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pais);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaisExists(pais.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pais);
        }

        // GET: Pais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pais = await _context.Pais
                .FirstOrDefaultAsync(m => m.id == id);
            if (pais == null)
            {
                return NotFound();
            }

            return View(pais);
        }

        // POST: Pais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pais = await _context.Pais.FindAsync(id);
            _context.Pais.Remove(pais);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaisExists(int id)
        {
            return _context.Pais.Any(e => e.id == id);
        }
    }
}
