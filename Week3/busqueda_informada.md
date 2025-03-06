# Semana 3: Búsqueda Informada

## 1. Cambios en `calculo_coste` y `calculo_heuristica`

### Concepto de Heurística
Una heurística es una función que estima cuán cerca está un estado actual de una solución óptima. En algoritmos de búsqueda informada, la heurística **guía la exploración del espacio de búsqueda**, ayudando a reducir el número de nodos evaluados al priorizar aquellos que parecen más prometedores.

### `calculo_coste`
Antes, la función `calculo_coste` simplemente devolvía un valor fijo de `1`, lo que hacía que todos los estados fueran tratados de la misma manera en términos de coste. Sin embargo, este enfoque no penalizaba adecuadamente las soluciones incorrectas, lo que resultaba en la exploración innecesaria de caminos inválidos.

Ahora, la función `calculo_coste` se ha mejorado para incluir restricciones en soluciones incorrectas:
- **Si un estado genera un conflicto entre reinas, se le asigna un coste muy alto (`int.MaxValue / 2`)**. Esto penaliza los estados inválidos y reduce su prioridad en la búsqueda.
- **Si el estado es válido, se asigna un coste de `1`**, permitiendo que el algoritmo continúe explorando soluciones viables.

Este cambio mejora la eficiencia de la búsqueda al evitar la expansión de estados sin solución.

### `calculo_heuristica`
Antes, la función `calculo_heuristica` devolvía siempre `0`, lo que hacía que el algoritmo A* se comportara exactamente como una búsqueda de Coste Uniforme, ignorando completamente la heurística.

Ahora, la heurística se ha redefinido para reflejar mejor la estimación de cuán cerca está un estado de una solución completa. Se ha implementado la siguiente fórmula:
```csharp
int calculo_heuristica(Solucion solucion) {
    return reinas - solucion.coords.Count; // Número de reinas faltantes por colocar
}
```

Este cambio tiene varias ventajas:
- **Cuanto más cerca esté una solución completa, menor será su heurística**, lo que guía la búsqueda hacia soluciones más prometedoras.
- **Evita caminos sin futuro**, ya que los estados más prometedores son priorizados en A* y en Búsqueda Avara.
- **Hace que A* funcione correctamente**, ya que ahora combina heurística con el coste real.

## 2. Nuevas clases: `BusquedaPorCosteUniforme` y `BusquedaAvara`

### `BusquedaPorCosteUniforme`
Este algoritmo sigue un enfoque de "Costo Uniforme" (Uniform Cost Search). Se basa exclusivamente en el coste acumulado desde el inicio hasta el nodo actual, sin considerar heurísticas. Se implementa usando una cola de prioridad donde los nodos se extraen según su menor coste acumulado.

- Siempre encuentra la solución óptima.
- Puede expandir muchos nodos innecesarios si los costes no están bien definidos.

### `BusquedaAvara`
Este algoritmo sigue un enfoque de "Búsqueda Avara" (Greedy Best-First Search). Utiliza solo la heurística para determinar la prioridad de los nodos en la cola de prioridad. No toma en cuenta el coste acumulado.

- Tiende a expandir nodos que parecen prometedores a corto plazo.
- No garantiza una solución óptima.
- Puede quedarse atrapado en caminos incorrectos si la heurística no es precisa.

## 3. Comparación de Nodos Evaluados

| Número de Reinas | A*  | Coste Uniforme | Búsqueda Avara |
|------------------|----|---------------|----------------|
| 4  | 15  | 16  | 128 |
| 5  | 25  | 45  | 245 |
| 6  | 31  | 150 | 9880 |
| 7  | 66  | 513 | -   |
| 8  | 63  | 1966 | -  |
| 9  | 197 | -   | -   |
| 10 | 699 | -   | -   |
| 11 | 2508| -   | -   |

Partiendo de 4 reinas, este es el número de nodos evaluados para cada uno con límite de 1500 nodos

## 4. Análisis de Rendimiento

- **A***: Logra un buen equilibrio entre eficiencia y solución óptima. Evalúa menos nodos en comparación con Coste Uniforme y encuentra soluciones óptimas.
- **Coste Uniforme**: Aunque garantiza soluciones óptimas, su eficiencia es menor porque no usa heurística. En problemas más grandes, su rendimiento empeora.
- **Búsqueda Avara**: A pesar de evaluar menos nodos en los primeros niveles, en problemas complejos explora demasiados nodos debido a su enfoque cortoplacista.

### Conclusión
Para resolver el problema de las N-Reinas, el algoritmo A* es la mejor opción ya que combina heurística con coste acumulado, reduciendo el número de nodos explorados mientras garantiza una solución óptima.

