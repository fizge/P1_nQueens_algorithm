# Semana 4: Búsqueda con restricciones

## 1. Cambios en `calculo_coste`, `calculo_heuristica` y `obtener_vecinos`

### Concepto de Heurística
Una heurística es una función que estima cuán cerca está un estado actual de una solución óptima. En algoritmos de búsqueda informada, la heurística **guía la exploración del espacio de búsqueda**, ayudando a reducir el número de nodos evaluados al priorizar aquellos que parecen más prometedores.

### `calculo_coste`

Se mantiene lo mismo que la semana anterior, la función `calculo_coste` incluye restricciones en soluciones incorrectas:
- **Si un estado genera un conflicto entre reinas, se le asigna un coste muy alto (`int.MaxValue / 2`)**. Esto penaliza los estados inválidos y reduce su prioridad en la búsqueda.
- **Si el estado es válido, se asigna un coste de `1`**, permitiendo que el algoritmo continúe explorando soluciones viables.

Esto mejora la eficiencia de la búsqueda al evitar la expansión de estados sin solución.

### `calculo_heuristica`
Antes, la heurística trataba de reflejar la estimación de cuán cerca está un estado de una solución completa, implementando la siguiente fórmula:
```csharp
int calculo_heuristica(Solucion solucion) {
    return reinas - solucion.coords.Count; // Número de reinas faltantes por colocar
}
```

Sin embargo esta era una heurística pobre ya que no incluía penalizaciones, por lo que la hemos mejorado a lo siguiente:
```csharp
int calculo_heuristica(Solucion solucion)
        {
            int reinasColocadas = solucion.coords.Count;
            int nuevaFila = reinasColocadas;
            
            // Si ya se han colocado todas, heurística es 0.
            if (reinasColocadas == reinas) return 0;

            // Calcular las columnas que estarían atacadas para la nueva fila.
            HashSet<int> columnasAtacadas = new HashSet<int>();
            foreach ((int fila, int col) in solucion.coords)
            {
                int d = nuevaFila - fila;
                columnasAtacadas.Add(col);
                if (col + d < reinas)
                    columnasAtacadas.Add(col + d);
                if (col - d >= 0)
                    columnasAtacadas.Add(col - d);
            }

            if (columnasAtacadas.Count == reinas)
            {
                return int.MaxValue/2;
            }
            return reinas - reinasColocadas;
        }
```

Si ya hemos colocado todas las reinas (`reinasColocadas == reinas`), significa que tenemos una solución completa, así que el valor heurístico es 0 (no queda nada por hacer). Pero en caso contrario, se recorren todas las reinas ya colocadas y se agregan a `columnasAtacadas` las columnas en las que están las reinas además de las diagonales. Si todas las columnas están atacadas, significa que no hay lugar seguro para colocar una nueva reina. Se devuelve un valor muy alto (`int.MaxValue/2`) como castigo heurístico para que el algoritmo evite esta solución. Si todavía hay espacio para colocar reinas, la heurística es simplemente el número de reinas que faltan (`reinas - reinasColocadas`). 

En el caso de que estemos con reinas fijas el cálculo de heurística cambia ligeramenente. Para calcular el índice de la `nuevaFila` se utiliza la función `primera_fila_sin_reina`. Por lo tanto, cuando se calcula la distancia entre la fila a tener en cuenta y la nueva fila se debe usar `Math.Abs()` para evitar que el resultado sea negativo.


### `obtener_vecinos`


Anteriormente la funcion `obtener_vecinos` no tenía en cuenta si una casilla vecina ya estaba amenzada por otra reina o no. Para mejorar eso se implementa una nueva función `es_prometedor` para ver si una casilla ya está siendo amenazada por una reina. 

```csharp
bool es_prometedor(Solucion solucion, (int, int) nuevo_nodo)
        {
            foreach ((int, int) nodo in solucion.coords)
            {
                if (nodo.Item2 == nuevo_nodo.Item2 || Math.Abs(nodo.Item2 - nuevo_nodo.Item2) == Math.Abs(nodo.Item1 - nuevo_nodo.Item1))
                {
                    return false; // Conflicto en la misma columna o diagonal
                }
            }
            return true;
        }
```


Esto se incorpora en la función `obtener_vecinos` de modo que antes de añadir un nuevo nodo al conjunto de vecinos se comprueba que este no tenga conflictos con otras reinas. De esta forma los nodos que ya tienen conflictos no se tienen en cuenta, reduciendo el número de nodos explorados para obtener la misma solución.



La función `obetener_vecinos` cuando hay reinas fijas además incluye la utilización de las funciones`fuera_de_tablero`, la cual comprueba si algún nodo de la solución dada está fuera de rango del tablero en cuenta.
```csharp
bool fuera_de_tablero(Solucion solucion)
        {
            foreach ((int, int) nodo in solucion.coords)
            {
                if (nodo.Item1 >= reinas || nodo.Item2 >= reinas)
                {
                    return true;
                }
            }
            return false;
        }
```

Y también incluye la función `primera_fila_sin_reina` que busca la primera fila disponible en el tablero que aún no tenga una reina colocada. Si todas las filas ya tienen reinas, devuelve reinas (lo cual indica que el tablero está lleno).

```csharp
int primera_fila_sin_reina(Solucion solucion)
        {
            HashSet<int> filasOcupadas = new HashSet<int>(solucion.coords.Select(coord => coord.Item1));
            for (int fila = 0; fila < reinas; fila++)
            {
                if (!filasOcupadas.Contains(fila))
                {
                    return fila;
                }
            }
            return reinas;
        }

```

## Nueva versión de `calculo_heuristica` para reinas fijas
El cálculo de la heurística ha sido ajustado para considerar la colocación de reinas fijas. En lugar de asumir que la siguiente fila vacía es `solucion.coords.Count`, se ha implementado la función `primera_fila_sin_reina` para identificar la primera fila disponible sin una reina colocada.
```csharp
int calculo_heuristica(Solucion solucion)
{
    int reinasColocadas = solucion.coords.Count;
    int nuevaFila = primera_fila_sin_reina(solucion);
    
    // Si ya se han colocado todas, heurística es 0.
    if (reinasColocadas == reinas) return 0;

    HashSet<int> columnasAtacadas = new HashSet<int>();
    foreach ((int fila, int col) in solucion.coords)
    {
        int d = Math.Abs(nuevaFila - fila);
        columnasAtacadas.Add(col);
        if (col + d < reinas)
            columnasAtacadas.Add(col + d);
        if (col - d >= 0)
            columnasAtacadas.Add(col - d);
    }

    if (columnasAtacadas.Count == reinas)
    {
        return int.MaxValue / 2;
    }
    return reinas - reinasColocadas;
}
```

## 2. Comparación de Nodos Evaluados con y sin reinas fijas


### Sin reinas fijas


| Número de Reinas | A*  | Coste Uniforme | Búsqueda Avara |
|------------------|----|---------------|----------------|
| 4  | 11  | 20  | 8  |
| 5  | 11  | 50  | 11 |
| 6  | 22  | 156 | 26 |
| 7  | 25  | 498 | 21 |
| 8  | 27  | 1940 | 126 |
| 9  | 55  | -   | 149   |
| 10 | 140 | -   | 210   |
| 11 | 240 | -   | 732   |
| 12 | 99  | -   | 3059   |
| 13 | 635 | -   | -   |
| 14 | 168 | -   | -   |
| 15 | 22235 | -   | -   |

Partiendo de 4 reinas, este es el número de nodos evaluados para cada uno con límite de 1500 nodos y sin reinas fijas.



### Con reinas fijas en (0, 3) y (2, 4)


| Número de Reinas | A*  | Coste Uniforme | Búsqueda Avara |
|------------------|-----|---------------|----------------|
| 4  |  -1 | -1  | -1  |
| 5  | 5  | 6  | 5 |
| 6  | 7  | 9 | 5 |
| 7  | 10  | 19 | 6 |
| 8  | 12  | 58 | 10 |
| 9  | 28  | 166   | 13   |
| 10 | 39 | 544   | 34   |
| 11 | 112 | 1931   | 65   |
| 12 | 58  | -   | 186   |
| 13 | 155 | -   | 66   |
| 14 | 271 | -   |1290   |
| 15 | 194 | -   | 956   |
| 16 | 49 | -   | 1940   |
| 17 | 743 | -   | -   |
| 18 | 690 | -   | -   |
| 19 | 2066 | -   | -   |


Con 4 reinas se marca `-1` al no encontrarse solución posible ya que en un tablero 4x4 que empieza en índice 0 `no existe` un nodo con coordenadas `(2, 4)`. 

## 3. Análisis de Rendimiento

- **A***: Logra un buen equilibrio entre eficiencia y solución óptima. Evalúa menos nodos en comparación con Coste Uniforme y encuentra soluciones óptimas.
- **Coste Uniforme**: Aunque garantiza soluciones óptimas, su eficiencia es menor porque no usa heurística. En problemas más grandes, su rendimiento empeora.
- **Búsqueda Avara**: A pesar de evaluar menos nodos en los primeros niveles, en problemas complejos explora demasiados nodos debido a su enfoque cortoplacista.

### Comparación reinas fijas

Al introducir 2 reinas fijas en las posiciones (0, 3) y (2, 4) se obtienen, obviando su imposibilidad con 4 reinas, soluciones en menor número de nodos explorados. Con el mismo número de reinas en el tablero, al haber dos fijas, hay menos reinas por colocar lo que permite que la heurística desde el principio tenga ya valor al poder evitar zonas atacadas. Además, al haber que colocar menos reinas se requieren menos tomas de decisión por lo que se disminuye el número de nodos explorados.

## Conclusión
Para resolver el problema de las N-Reinas, el algoritmo A* es la mejor opción tanto con reinas fijas como sin ellas ya que combina heurística con coste acumulado, reduciendo el número de nodos explorados mientras garantiza una solución óptima. Si se fijan reinas iniciales, el número de nodos explorados y la eficiencia de cada aloritmo disminuirá. 

