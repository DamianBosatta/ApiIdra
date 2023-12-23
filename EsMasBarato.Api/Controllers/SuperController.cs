using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EsMasBarato.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SuperController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public SuperController()
        {
            // Puedes configurar el HttpClient en el constructor o mediante DI
            _httpClient = new HttpClient();
        }

        [HttpGet("disco/")]
        public async Task<IActionResult> BuscarProductoDisco([FromQuery] string termino)
        {
            try
            {
                // Construir la URL del endpoint de disco.com.ar con el término proporcionado
                string discoEndpoint = "https://www.disco.com.ar/_v/segment/graphql/v1?workspace=master&maxAge=medium&appsEtag=remove&domain=store&locale=es-AR&operationName=productSuggestions&extensions={\"persistedQuery\":{\"version\":1,\"sha256Hash\":\"c6f3f04750f6176e275d0fe4baaaf295f9be9c7d6ee9b4bdee061d6bb4930fcb\",\"sender\":\"vtex.store-resources@0.x\",\"provider\":\"vtex.search-graphql@0.x\"}}&variables={\"productOriginVtex\":true,\"simulationBehavior\":\"default\",\"hideUnavailableItems\":true,\"fullText\":\"" + termino + "\",\"count\":100,\"shippingOptions\":[],\"variant\":null}";

                // Realizar la solicitud HTTP GET al endpoint de disco.com.ar
                HttpResponseMessage response = await _httpClient.GetAsync(discoEndpoint);

                // Verificar si la solicitud fue exitosa (código 2xx)
                if (response.IsSuccessStatusCode)
                {
                    // Leer y devolver la respuesta como string
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return Ok(responseBody);
                }
                else
                {
                    // Devolver el código de estado y la razón en caso de error
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la solicitud
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("carrefour/")]
        public async Task<IActionResult> BuscarProductoCarrefour([FromQuery] string termino)
        {
            try
            {
                // Construir la URL del endpoint de disco.com.ar con el término proporcionado
                string discoEndpoint = "https://www.carrefour.com.ar/_v/segment/graphql/v1?workspace=master&maxAge=medium&appsEtag=remove&domain=store&locale=es-AR&operationName=productSuggestions&extensions={\"persistedQuery\":{\"version\":1,\"sha256Hash\":\"c6f3f04750f6176e275d0fe4baaaf295f9be9c7d6ee9b4bdee061d6bb4930fcb\",\"sender\":\"vtex.store-resources@0.x\",\"provider\":\"vtex.search-graphql@0.x\"}}&variables={\"productOriginVtex\":true,\"simulationBehavior\":\"default\",\"hideUnavailableItems\":true,\"fullText\":\"" + termino + "\",\"count\":100,\"shippingOptions\":[],\"variant\":null}";

                // Realizar la solicitud HTTP GET al endpoint de disco.com.ar
                HttpResponseMessage response = await _httpClient.GetAsync(discoEndpoint);

                // Verificar si la solicitud fue exitosa (código 2xx)
                if (response.IsSuccessStatusCode)
                {
                    // Leer y devolver la respuesta como string
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return Ok(responseBody);
                }
                else
                {
                    // Devolver el código de estado y la razón en caso de error
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la solicitud
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("vea/")]
        public async Task<IActionResult> BuscarProductoVea([FromQuery] string termino)
        {
            try
            {
                // Construir la URL del endpoint de disco.com.ar con el término proporcionado
                string discoEndpoint = "https://www.vea.com.ar/_v/segment/graphql/v1?workspace=master&maxAge=medium&appsEtag=remove&domain=store&locale=es-AR&operationName=productSuggestions&extensions={\"persistedQuery\":{\"version\":1,\"sha256Hash\":\"c6f3f04750f6176e275d0fe4baaaf295f9be9c7d6ee9b4bdee061d6bb4930fcb\",\"sender\":\"vtex.store-resources@0.x\",\"provider\":\"vtex.search-graphql@0.x\"}}&variables={\"productOriginVtex\":true,\"simulationBehavior\":\"default\",\"hideUnavailableItems\":true,\"fullText\":\"" + termino + "\",\"count\":100,\"shippingOptions\":[],\"variant\":null}";

                // Realizar la solicitud HTTP GET al endpoint de disco.com.ar
                HttpResponseMessage response = await _httpClient.GetAsync(discoEndpoint);

                // Verificar si la solicitud fue exitosa (código 2xx)
                if (response.IsSuccessStatusCode)
                {
                    // Leer y devolver la respuesta como string
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return Ok(responseBody);
                }
                else
                {
                    // Devolver el código de estado y la razón en caso de error
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la solicitud
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("toledo/")]
        public async Task<IActionResult> BuscarProductoToledo([FromQuery] string termino)
        {
            try
            {
                // Construir la URL del endpoint de disco.com.ar con el término proporcionado
                string discoEndpoint = "https://toledodigital.com.ar/storeview_jara/mageworx_searchsuiteautocomplete/ajax/index/?q="+termino+"&_=1699617706313'";

                // Realizar la solicitud HTTP GET al endpoint de disco.com.ar
                HttpResponseMessage response = await _httpClient.GetAsync(discoEndpoint);

                // Verificar si la solicitud fue exitosa (código 2xx)
                if (response.IsSuccessStatusCode)
                {
                    // Leer y devolver la respuesta como string
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return Ok(responseBody);
                }
                else
                {
                    // Devolver el código de estado y la razón en caso de error
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la solicitud
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("dia/")]
        public async Task<IActionResult> BuscarProductoDia([FromQuery] string termino)
        {
            try
            {
                // Construir la URL del endpoint de disco.com.ar con el término proporcionado
                string discoEndpoint = "https://diaonline.supermercadosdia.com.ar/_v/segment/graphql/v1?workspace=master&maxAge=medium&appsEtag=remove&domain=store&locale=es-AR&operationName=productSuggestions&extensions={\"persistedQuery\":{\"version\":1,\"sha256Hash\":\"c6f3f04750f6176e275d0fe4baaaf295f9be9c7d6ee9b4bdee061d6bb4930fcb\",\"sender\":\"vtex.store-resources@0.x\",\"provider\":\"vtex.search-graphql@0.x\"}}&variables={\"productOriginVtex\":true,\"simulationBehavior\":\"default\",\"hideUnavailableItems\":true,\"fullText\":\"" + termino + "\",\"count\":100,\"shippingOptions\":[],\"variant\":null}";

                // Realizar la solicitud HTTP GET al endpoint de disco.com.ar
                HttpResponseMessage response = await _httpClient.GetAsync(discoEndpoint);

                // Verificar si la solicitud fue exitosa (código 2xx)
                if (response.IsSuccessStatusCode)
                {
                    // Leer y devolver la respuesta como string
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return Ok(responseBody);
                }
                else
                {
                    // Devolver el código de estado y la razón en caso de error
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la solicitud
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}

