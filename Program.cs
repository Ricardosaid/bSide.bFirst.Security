using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace prueba
{
    internal class Program
    {
        /*
         * Creamos una instancia estática de HttpClient para controlar las solicitudes
         * y las respuestas
         
         */

        private static readonly HttpClient client = new HttpClient();


        /*
         * Si no le ponemos el async al método main, este se va a ejecutar sincronamente, cosa que no queremos, por lo tanto agregamos 
         * los operadores await mas adelante a medida que utilicemos el método
         * 
         * Hasta aquí, cambia la firma de Main al agregar el modificador async 
         * y cambia el tipo de valor devuelto a Task
         * 
         * Reemplaza la instrucción Console.WriteLine por la llamada al método ProcessRepositories que usa la palabra
         * clave await
         */
        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        /*
         * La clase que vamos autiliza (HttpClient) solo admite métodos asincronos para sus APIs de larga duración.
         * Por lo tanto, los pasos siguientes crean un método asincrono y lo llaman desde el método main
         * 
         * En este método llamamos al punto de conexión de nuestra api de query
         * que devuelve una lista de todos los respositorios del histoic access
         * 
         * Lo que hacemos es la condifuración de encabezados HTTP para todas las solicitudes:
         * Un encabezado Accept para aceptar respuestas JSON
         * Un encabezado User Agent que contiene una cadena caracteristica que permite identificar
         * el protocolo de red que ayuda a descubrir el tipo de aplicación, SO, proveedor del software
         * o la versión del software de la petición del User Agent.
         * 
         * Se llama a HttpClient.GetStringAsync(string) para realizar una solicitud web y recuperar
         * la respuesta. Este método inicia una tarea que realiza la solicutd web. Cuando la solictud 
         * se devuelve, la tarea lee el flujo de respuesta y extrae el contenido de la secuencia.
         * El cuerpo de la respuesta se devuelve como un elemento string, que está disponible cuando se complete la tarea.
         * Espera la tarea hasta que devuelve la cadena de respuesta e imprime la respuesta en la consola
         * 
         
         */
        private static async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Query Historic Repository");

            var stringTask = client.GetStringAsync("https://localhost:5001/api/HistoricAccess");

            var msg = await stringTask;
            Console.WriteLine(msg);

        }
    }
}