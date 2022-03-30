using bSide.bFirst.Security.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

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
            await ProcessXML();
            // await ProcessRepositories();
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

            /*Reemplazamos la llamada al metodo GetStringAsync*/

            var streamTask = client.GetStreamAsync("http://www.sat.gob.mx/sitio_internet/cfd/4/cfdv40.xsd"); // Usa como orgigen una secuenciaen lugar de una cadena
            /* Las expresiones await pueden aparecer prácticamente en cualquier
             * parte del código, aunque hasta ahora solo se han visto como parte
             * de una instrucción de asignación. 
             * 
             * DeserealizeAsync es genérico. Se va a deserializar en List<RepoDes> que es un objeto generico.
             * 
             * List<T> es una clase que administra ua colección de objetos.*** EL ARGUMENTO DE TIPO DE OBJETOS
             * ALAMCENADOS EN List<T>. EL ARGUMENTO DE TIPO ES LA CLASE RepoDes, PORQUE EL TEXTO JSON
             * REPRESENTA UNA COLECCIÓN DE OBKETOS DE REPOSITORIO.****
             */
            var repositories = await JsonSerializer.DeserializeAsync<List<RepoDes>>(await streamTask); //omitimos los parametros JsonSerializerOptions & cancellationToken (poner cursos en el método)

            foreach (var repo in repositories)
                Console.WriteLine($"**La persona con el numero de tarjeta {repo.cardNumber} tiene el siguiente ID {repo.idIoTDevice}** \n");

        }

        // Metodo para obtener valores de los nodos xml
        private static async Task ProcessXML()
        {
            string URLstring = "http://www.sat.gob.mx/sitio_internet/cfd/4/cfdv40.xsd";
            XmlTextReader reader = new XmlTextReader(URLstring);

            while (reader.Read())
            {
                var campo = reader.Name;
                if(campo == "xs:attribute")
                {
                    switch (reader.NodeType)
                    {
                    case XmlNodeType.Element:
                        // Console.Write("<" + reader.Name);

                        while(reader.MoveToNextAttribute())
                            if(reader.Name == "name")
                                Console.Write(" " + reader.Name + "='" + reader.Value + "'");
                        Console.Write(">");
                        Console.WriteLine(">");
                        break;

                    }

                }
                
                

                    /*Aqui leemos los nodos con su respectivo cierre así como su descripción*/
                    // case XmlNodeType.Element:
                    //     Console.Write("<" + reader.Name);
                    //     Console.WriteLine(">");
                    //     break;
                    // case XmlNodeType.Text:
                    //     Console.WriteLine(reader.Value);
                    //     break;
                    // case XmlNodeType.EndElement:
                    //     Console.Write("</" + reader.Name);
                    //     Console.WriteLine(">");
                    //     break;

            

            }

        }
    }
}