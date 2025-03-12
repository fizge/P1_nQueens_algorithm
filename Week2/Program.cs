using System.Diagnostics;

public class Program
{
    public static void Main(string[] args)
    {
        // Estado inicial: lista vacía, sin ninguna reina colocada.
        List<(int, int)> solucion_inicial = new List<(int, int)>();
        // Número de reinas a colocar (tamaño del tablero).
        int reinas = 4;

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
                    if (nodo_j.Item2 == nodo_i.Item2 || Math.Abs(nodo_j.Item2 - nodo_i.Item2) == Math.Abs(nodo_j.Item1 - nodo_i.Item1))
                        return false;
                }
            }
            return true;
        }

        int revisadosAnch = 0;
        int revisadosProf = 0;

        List<int> lista_evaluadosAnch = new List<int>();
        List<int> lista_evaluadosProf = new List<int>();

        while (revisadosAnch < 1500)
        {
            Console.WriteLine("\n\nBúsqueda por anchura:");

            Stopwatch stopwatchAnch = new Stopwatch();
            stopwatchAnch.Start();

            BusquedaAnchura bAnchura = new BusquedaAnchura();
            (Solucion solucionAnch, int nodosEvaluados) = bAnchura.busqueda(
                solucion_inicial, criterio_parada, obtener_vecinos, calculo_coste, calculo_heuristica
            );

            stopwatchAnch.Stop();

            Console.WriteLine($"Tiempo transcurrido: {stopwatchAnch.ElapsedMilliseconds} ms");
            Console.WriteLine("Coordenadas:  " + solucionAnch.ToString());
            Console.WriteLine("Nodos evaluados:  " + nodosEvaluados);

            revisadosAnch = nodosEvaluados; // Actualizar la variable para la condición del while
            reinas++;
            lista_evaluadosAnch.Add(nodosEvaluados);
        }

        reinas = 4; // Resetear para la búsqueda en profundidad
        while (revisadosProf < 1500)
        {
            Console.WriteLine("\n\nBúsqueda en profundidad:");

            Stopwatch stopwatchProf = new Stopwatch();
            stopwatchProf.Start();

            BusquedaProfundidad bProfundidad = new BusquedaProfundidad();
            (Solucion solucionProf, int nodosEvaluados) = bProfundidad.busqueda(
                solucion_inicial, criterio_parada, obtener_vecinos, calculo_coste, calculo_heuristica
            );

            stopwatchProf.Stop();

            Console.WriteLine($"Tiempo transcurrido: {stopwatchProf.ElapsedMilliseconds} ms");
            Console.WriteLine("Coordenadas:  " + solucionProf.ToString());
            Console.WriteLine("Nodos evaluados:  " + nodosEvaluados);

            revisadosProf = nodosEvaluados; // Actualizar la variable para la condición del while
            reinas++;
            lista_evaluadosProf.Add(nodosEvaluados);
        }

        Console.WriteLine("\nResultados BFS:");
        Console.WriteLine(string.Join(", ", lista_evaluadosAnch));

        Console.WriteLine("\nResultados DFS:");
        Console.WriteLine(string.Join(", ", lista_evaluadosProf));


    }

}