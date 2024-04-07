using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiApi.Pages
{
    public class RegistroModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public RegistroModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public string Nombre { get; set; }

        [BindProperty]
        public int Edad { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var usuario = new { Nombre = this.Nombre, Edad = this.Edad, Email = this.Email };
            var usuarioJson = new StringContent(JsonSerializer.Serialize(usuario), Encoding.UTF8, "application/json");

            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.PostAsync("http://localhost:5000/api/Usuarios", usuarioJson);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Success");
            }
            else
            {
                // Manejar la respuesta fallida, por ejemplo, mostrando un mensaje de error
                ModelState.AddModelError(string.Empty, "Ocurrió un error al registrar el usuario");
                return Page();
            }
        }
    }
}
