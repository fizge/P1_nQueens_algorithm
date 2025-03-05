using System.Diagnostics;

public class Program
{
    public static void Main(string[] args)
    {
        // Estado inicial: lista vacía, sin ninguna reina colocada.
        List<(int, int)> solucion_inicial = new List<(int, int)>();
        // Número de reinas a colocar (tamaño del tablero).
        int reinas = 7;

        /// <summary>
        /// Calcula el coste entre dos soluciones.
        /// En este ejemplo, cada movimiento tiene un coste fijo de 1.
        /// </summary>
        /// <param name="solucion">Solución actual.</param>
        /// <param name="nueva_solucion">Nueva solución generada.</param>
        /// <returns>Coste (1 en este caso).</returns>
        int calculo_coste(Solucion solucion, Solucion nueva_solucion)
        {
            return 1;
        }

        /// <summary>
        /// Calcula la heurística para una solución.
        /// En este ejemplo, la heurística retorna 0, por lo que se utiliza solo el coste acumulado.
        /// </summary>
        /// <param name="solucion">Solución actual.</param>
        /// <returns>Valor heurístico (0 en este caso).</returns>
        int calculo_heuristica(Solucion solucion)
        {
            return 0;
        }

        /// <summary>
        /// Obtiene los vecinos de una solución.
        /// A partir de la solución actual, se generan todos los posibles estados
        /// añadiendo una reina en cada posición de la siguiente fila.
        /// Solo se añaden vecinos prometedores (sin conflictos con las reinas ya colocadas).
        /// </summary>
        /// <param name="solucion">Solución actual.</param>
        /// <returns>Lista de vecinos (nuevas coordenadas posibles).</returns>
        List<(int, int)> obtener_vecinos(Solucion solucion)
        {
            int row = solucion.coords.Count == 0 ? -1 : solucion.coords[^1].Item1;
            List<(int, int)> vecinos = new List<(int, int)>();
            if (row + 1 < reinas)
            {
                for (int j = 0; j < reinas; j++)
                {
                    (int, int) nuevo_nodo = (row + 1, j);
                 
                    vecinos.Add(nuevo_nodo); // Añade la posición (siguiente fila, columna j)
                  
                }
            }
            return vecinos;
        }

        /// <summary>
        /// Verifica si la colocación de una nueva reina es prometedora (sin conflictos).
        /// </summary>
        /// <param name="solucion">Solución actual.</param>
        /// <param name="nuevo_nodo">Nueva coordenada de la reina a colocar.</param>
        /// <returns>True si es prometedor; de lo contrario, false.</returns>
   
        /// <summary>
        /// Verifica si una solución cumple con el criterio de parada.
        /// El algoritmo finaliza cuando:
        /// - Se han colocado todas las reinas (solución completa).
        /// - No existen conflictos: no hay dos reinas en la misma columna o en diagonales conflictivas.
        /// </summary>
        /// <param name="solucion">Solución actual.</param>
        /// <returns>True si la solución es completa y válida; de lo contrario, false.</returns>
        bool criterio_parada(Solucion solucion)
        {
            // Si aún no se han colocado todas las reinas, la solución no puede ser completa.
            if (solucion.coords.Count < reinas)
                return false;
            
            // Comprobar que no existan conflictos entre las reinas.
            for (int i = 0; i < solucion.coords.Count; i++)
            {
                (int, int) nodo_i = solucion.coords[i];
                for (int j = i + 1; j < solucion.coords.Count; j++)
                {
                    (int, int) nodo_j = solucion.coords[j];
                    // Se verifica que no estén en la misma columna o en la misma diagonal.
                    if (nodo_j.Item2 == nodo_i.Item2 || Math.Abs(nodo_j.Item2 - nodo_i.Item2) == Math.Abs(j - i))
                        return false;
                }
            }
            return true;
        }

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Se crea una instancia del algoritmo A* (AEstrella)
        AEstrella astar = new AEstrella();
        // Se inicia la búsqueda a partir del estado inicial (solución vacía)
        (Solucion solucion, int revisados) = astar.busqueda(solucion_inicial, criterio_parada, obtener_vecinos, calculo_coste, calculo_heuristica);

        stopwatch.Stop();
        // Mostrar el tiempo de ejecución
        Console.WriteLine($"Tiempo transcurrido: {stopwatch.ElapsedMilliseconds} ms");

        Console.WriteLine("Coordenadas:  " + solucion.ToString());
        Console.WriteLine("Nodos evaluados:  " + revisados);
    }
}
