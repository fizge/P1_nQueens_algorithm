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
        /// En este ejemplo, el coste es el número de reinas en conflicto.
        /// </summary>
        /// <param name="solucion">Solución actual.</param>
        /// <param name="nueva_solucion">Nueva solución generada.</param>
        /// <returns>Coste (número de conflictos).</returns>
        int calculo_coste(Solucion solucion, Solucion nueva_solucion)
        {
            // Solo comparo la última reina agregada con las anteriores
            (int,int) nueva_reina = nueva_solucion.coords[^1]; // Última reina agregada
            foreach ((int,int) otra_reina in solucion.coords)
            {
                // Conflicto en columna o diagonal
                if (nueva_reina.Item2 == otra_reina.Item2 || 
                    Math.Abs(nueva_reina.Item1 - otra_reina.Item1) == Math.Abs(nueva_reina.Item2 - otra_reina.Item2))
                {
                    return int.MaxValue / 2 ; // Penalización por conflicto
                }
            }
            
            return 1; // Coste normal de avanzar en la búsqueda
        }

        /// <summary>
        /// Calcula la heurística para una solución.
        /// En este ejemplo, la heurística es el número de reinas en conflicto.
        /// </summary>
        /// <param name="solucion">Solución actual.</param>
        /// <returns>Valor heurístico (número de conflictos).</returns>
        int calculo_heuristica(Solucion solucion)
        {
            
            return reinas - solucion.coords.Count;
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
                    if (nodo_j.Item2 == nodo_i.Item2 || Math.Abs(nodo_j.Item2 - nodo_i.Item2) == Math.Abs(j - i))
                        return false;
                }
            }
            return true;
        }

        // Listas para almacenar el número de nodos evaluados en cada búsqueda
        List<int> lista_evaluadosAstar = new List<int>();
        List<int> lista_evaluadosUCS = new List<int>();
        List<int> lista_evaluadosAvara = new List<int>();

        // Número de nodos evaluados en cada algoritmo
        int revisadosAstar = 0;
        int revisadosUCS = 0;
        int revisadosAvara = 0;

        // BÚSQUEDA A*
        while (revisadosAstar < 1500)
        {
            Console.WriteLine("\n\nBúsqueda por A*:");

            Stopwatch stopwatchAstar = new Stopwatch();
            stopwatchAstar.Start();

            AEstrella aestrella = new AEstrella();
            (Solucion solucionAstar, revisadosAstar) = aestrella.busqueda(solucion_inicial, criterio_parada, obtener_vecinos, calculo_coste, calculo_heuristica);

            stopwatchAstar.Stop();

            Console.WriteLine($"Tiempo transcurrido: {stopwatchAstar.ElapsedMilliseconds} ms");
            Console.WriteLine("Coordenadas:  " + solucionAstar.ToString());
            Console.WriteLine("Nodos evaluados:  " + revisadosAstar);

            lista_evaluadosAstar.Add(revisadosAstar);
            reinas++;
        }

        // BÚSQUEDA POR COSTE UNIFORME
        reinas = 4;  // Restablecemos el número de reinas
        while (revisadosUCS < 1500)
        {
            Console.WriteLine("\n\nBúsqueda por Coste Uniforme:");

            Stopwatch stopwatchUCS = new Stopwatch();
            stopwatchUCS.Start();

            CosteUniforme costeUniforme = new CosteUniforme();
            (Solucion solucionUCS, revisadosUCS) = costeUniforme.busqueda(solucion_inicial, criterio_parada, obtener_vecinos, calculo_coste, calculo_heuristica);

            stopwatchUCS.Stop();

            Console.WriteLine($"Tiempo transcurrido: {stopwatchUCS.ElapsedMilliseconds} ms");
            Console.WriteLine("Coordenadas:  " + solucionUCS.ToString());
            Console.WriteLine("Nodos evaluados:  " + revisadosUCS);

            lista_evaluadosUCS.Add(revisadosUCS);
            reinas++;
        }

        // BÚSQUEDA AVARA
        reinas = 4;  // Restablecemos el número de reinas
        while (revisadosAvara < 1500)
        {
            Console.WriteLine("\n\nBúsqueda Avara:");

            Stopwatch stopwatchAvara = new Stopwatch();
            stopwatchAvara.Start();

            BusquedaAvara busquedaAvara = new BusquedaAvara();
            (Solucion solucionAvara, revisadosAvara) = busquedaAvara.busqueda(solucion_inicial, criterio_parada, obtener_vecinos, calculo_coste, calculo_heuristica);

            stopwatchAvara.Stop();

            Console.WriteLine($"Tiempo transcurrido: {stopwatchAvara.ElapsedMilliseconds} ms");
            Console.WriteLine("Coordenadas:  " + solucionAvara.ToString());
            Console.WriteLine("Nodos evaluados:  " + revisadosAvara);

            lista_evaluadosAvara.Add(revisadosAvara);
            reinas++;
        }

        // Imprimir los resultados finales
        Console.WriteLine("\nResultados:");
        Console.WriteLine("A*: " + string.Join(", ", lista_evaluadosAstar));
        Console.WriteLine("Coste Uniforme: " + string.Join(", ", lista_evaluadosUCS));
        Console.WriteLine("Búsqueda Avara: " + string.Join(", ", lista_evaluadosAvara));
    }
}
