using System.Diagnostics;
public class Program
{
    public static void Main(string[] args)
    {
        List<(int, int)> solucion_inicial = new List<(int, int)>();
        int reinas = 6;

        /// <summary>
        /// Calcula el coste entre dos soluciones.
        /// </summary>
        /// <param name="solucion">Solución actual.</param>
        /// <param name="nueva_solucion">Nueva solución.</param>
        /// <returns>Coste entre las soluciones.</returns>
        int calculo_coste(Solucion solucion, Solucion nueva_solucion)
        {
            return 1;
        }

        /// <summary>
        /// Calcula la heurística para una solución.
        /// </summary>
        /// <param name="solucion">Solución actual.</param>
        /// <returns>Valor heurístico.</returns>
        int calculo_heuristica(Solucion solucion)
        {
            return 0;
        }

        /// <summary>
        /// Obtiene los vecinos de una solución.
        /// </summary>
        /// <param name="solucion">Solución actual.</param>
        /// <returns>Lista de vecinos.</returns>
        List<(int, int)> obtener_vecinos(Solucion solucion)
        {
            int row = solucion.coords.Count == 0 ? -1 : solucion.coords[^1].Item1;
            List<(int, int)> vecinos = new List<(int, int)>();
            if (row + 1 < reinas)
            {
                for (int j = 0; j < reinas; j++)
                {
                    vecinos.Add((row + 1, j)); // Añade todas las posiciones posibles en la siguiente fila.
                }
            }
            return vecinos;
        }

        /// <summary>
        /// Verifica si una solución cumple con el criterio de parada.
        /// </summary>
        /// <param name="solucion">Solución actual.</param>
        /// <returns>True si cumple el criterio, de lo contrario false.</returns>
        bool criterio_parada(Solucion solucion)
        {
            if (solucion.coords.Count < reinas)
                return false; // Si no se han colocado todas las reinas, no cumple el criterio.

            for (int i = 0; i < solucion.coords.Count; i++)
            {
                (int, int) nodo_i = solucion.coords[i];
                for (int j = i + 1; j < solucion.coords.Count; j++)
                {
                    (int, int) nodo_j = solucion.coords[j];
                    if (nodo_j.Item2 == nodo_i.Item2 || Math.Abs(nodo_j.Item2 - nodo_i.Item2) == Math.Abs(j - i))
                        return false; // Si hay dos reinas en la misma columna o diagonal, no cumple el criterio.
                }
            }
            return true; // Si todas las reinas están en posiciones válidas, cumple el criterio.
        }

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        AEstrella astar = new AEstrella();
        (Solucion solucion, int revisados) = astar.busqueda(solucion_inicial, criterio_parada, obtener_vecinos, calculo_coste, calculo_heuristica);

        // Detener el cronómetro
        stopwatch.Stop();
        
        // Mostrar el tiempo transcurrido en milisegundos
        Console.WriteLine($"Tiempo transcurrido: {stopwatch.ElapsedMilliseconds} ms");

        Console.WriteLine("Coordenadas:  " + solucion.ToString());
        Console.WriteLine("Nodos evaluadas:  " + revisados);
    }
}
