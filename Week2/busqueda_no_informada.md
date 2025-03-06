# Semana 2: Búsqueda no informada

## Introducción
En esta práctica, se ha implementado la resolución del problema de las N-Reinas utilizando algoritmos de búsqueda no informada. Se han explorado dos enfoques distintos para resolver el problema:

1. **Uso de estructuras de Pilas y Colas**: Implementadas a través de las clases `BusquedaAnchura` y `BusquedaProfundidad`, que heredan de la clase `AlgoritmoDeBusqueda`.
2. **Modificación de las funciones de cálculo de coste y heurística**: Ajustando los valores de retorno en `calculo_coste` y `calculo_heuristica` para inducir un comportamiento similar al de una cola normal o una pila.

A continuación, se detalla cada uno de estos enfoques, sus ventajas y desventajas, así como una comparación de sus resultados en términos de eficiencia y correctitud.

---

## 1. Descripción de las dos soluciones posibles

### 1.1. Uso de estructuras de Pilas y Colas

Este enfoque implementa la búsqueda en anchura y en profundidad utilizando estructuras de datos específicas:

- **Búsqueda en anchura (BFS)**: Implementada en la clase `BusquedaAnchura`, la cual hereda de `AlgoritmoDeBusqueda` y utiliza una `Cola` para gestionar los nodos a explorar. En este caso, los nodos se extraen en orden **FIFO** (First In, First Out), garantizando que se exploren todos los estados a un nivel antes de pasar al siguiente.

- **Búsqueda en profundidad (DFS)**: Implementada en la clase `BusquedaProfundidad`, que también hereda de `AlgoritmoDeBusqueda` y utiliza una `Pila` para almacenar los nodos. Esto hace que los nodos se extraigan en orden **LIFO** (Last In, First Out), explorando primero los caminos más profundos antes de retroceder.

Este enfoque garantiza un comportamiento correcto y predecible, ya que las estructuras utilizadas son específicas para estos tipos de búsqueda.

### 1.2. Modificación de las funciones de cálculo de coste y heurística

En este segundo enfoque, en lugar de utilizar `BusquedaAnchura` y `BusquedaProfundidad`, se mantiene el uso de una **cola de prioridad** y se modifican las funciones `calculo_coste` y `calculo_heuristica` para inducir un comportamiento similar al de una cola o una pila:

- **Para imitar la búsqueda en profundidad**, `calculo_coste` retorna `-1`. Esto hace que cada nuevo nodo insertado tenga menor prioridad que su predecesor, provocando que la cola de prioridad lo extraiga antes que los nodos previamente insertados, imitando una pila.
- **Para imitar la búsqueda en anchura**, `calculo_coste` retorna `0`. En este caso, todos los nodos tienen la misma prioridad, por lo que la cola de prioridad los extrae en orden de inserción, imitando una cola FIFO.

Si bien esta solución puede aproximarse a los comportamientos de BFS y DFS, el uso de una **cola de prioridad basada en montículo** introduce una variabilidad en el orden de extracción, lo que impide una correspondencia exacta con los algoritmos tradicionales de búsqueda en anchura y profundidad.

---

## 2. Comparación y selección de la mejor solución

De las dos soluciones planteadas, la **primera opción** (uso de Pilas y Colas) es la más adecuada y la que se debe implementar. Esto se debe a que:

1. **Garantiza un comportamiento correcto**: Al utilizar estructuras de datos específicas (Cola para BFS y Pila para DFS), se asegura que la exploración de estados se realice de manera predecible y conforme a la definición de cada algoritmo.
2. **Evita inconsistencias del montículo**: En la segunda solución, la cola de prioridad basada en montículo no garantiza estabilidad en el orden de inserción para elementos con la misma prioridad, lo que puede hacer que la exploración de nodos varíe respecto a una cola o pila convencional.
3. **Mayor claridad y mantenibilidad**: La primera solución sigue los principios clásicos de implementación de BFS y DFS, facilitando su comprensión y evitando efectos secundarios inesperados.

Por lo tanto, la implementación mediante `BusquedaAnchura` y `BusquedaProfundidad` es la más recomendable.

---

## 3. Resultados de las ejecuciones

A continuación se presentan los resultados obtenidos para distintos valores de N en el problema de las N-Reinas. Se indica el número de nodos evaluados para cada algoritmo:

| N  | BFS - Nodos Evaluados | DFS - Nodos Evaluados |
|----|----------------------|----------------------|
| 4  | 200                  | 155                    |
| 5  | 1140                  | 451                   |
| 6  | 22092                  | 15316                   |


Estos valores reflejan una tendencia esperada: la búsqueda en anchura (BFS) evalúa un mayor número de nodos que la búsqueda en profundidad (DFS), debido a la exploración exhaustiva de todos los estados a cada nivel antes de continuar.

---

## 4. Elección del modelo de búsqueda no informada más adecuado

Para la resolución del problema de las N-Reinas, la **búsqueda en profundidad (DFS)** es generalmente más adecuada que la búsqueda en anchura (BFS). Esto se debe a que:

- **Menor consumo de memoria**: DFS mantiene un número de nodos en memoria significativamente menor que BFS, ya que explora un camino hasta el final antes de retroceder.
- **Mayor eficiencia en este problema**: En problemas como N-Reinas, DFS puede llegar rápidamente a una solución válida sin necesidad de evaluar tantos estados intermedios como BFS.

Sin embargo, en algunos casos, BFS puede ser preferible si se desea encontrar la solución más corta (por ejemplo, en problemas de grafos con caminos de diferente peso). En el contexto de N-Reinas, DFS es más eficiente y práctico.

