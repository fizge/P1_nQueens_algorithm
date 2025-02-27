# Trabajo Realizado

## Introducción

Este documento detalla el proceso de traducción del código de Python para el problema de las N-Reinas a C#. Se han convertido tres módulos principales:
- **Algorithm.cs**
- **DataStructures.cs**
- **Program.cs**

La traducción implicó adaptar la sintaxis, aprovechar el tipado fuerte de C# y utilizar estructuras genéricas, manteniendo la lógica original del algoritmo A*.

## Principales Diferencias Técnicas

### Tipado y Seguridad en Tiempo de Ejecución
- **Python:**  
  - Lenguaje dinámico sin declaraciones de tipo explícitas.
  - Mayor flexibilidad pero con mayor riesgo de errores en tiempo de ejecución.
- **C#:**  
  - Lenguaje fuertemente tipado; se deben declarar tipos para variables, parámetros y valores de retorno.
  - Proporciona robustez y detección temprana de errores.

### Clases y Métodos Especiales
- **Python:**  
  - Uso de métodos especiales como `__init__`, `__eq__`, `__lt__` y `__str__` para inicialización, comparación y representación.
- **C#:**  
  - Uso de constructores y sobrescritura de métodos como `Equals` y `ToString`.
  - Implementación de `IComparable<T>` para definir el orden en estructuras como la cola de prioridad.

### Colecciones y Gestión de Datos
- **Python:**  
  - Uso de listas y diccionarios nativos.
  - La cola de prioridad se implementa con el módulo `heapq` y un diccionario auxiliar para evitar duplicados.
- **C#:**  
  - Empleo de colecciones genéricas como `List<T>`, `Dictionary<TKey, TValue>` y `PriorityQueue<TElement, int>`.
  - El uso de un diccionario auxiliar junto a la cola de prioridad optimiza la gestión de duplicados y mejora el rendimiento.

### Delegados, Lambdas y Manejo de Funciones
- **Python:**  
  - Las funciones son de primera clase y se pueden pasar como argumentos sin mayor formalidad.
- **C#:**  
  - Se utilizan delegados y expresiones lambda para pasar funciones como parámetros, lo que requiere definir explícitamente la firma de la función.

### Manejo de Errores y Excepciones
- **Python:**  
  - Manejo de errores mediante bloques `try/except`.
- **C#:**  
  - Uso de bloques `try/catch` que permiten un manejo estructurado y seguro de excepciones, importante en algoritmos con múltiples estados.

## Funcionamiento del Algoritmo y Funciones Importantes

El algoritmo A* (AEstrella) aplicado al problema de las N-Reinas utiliza una búsqueda informada que prioriza la expansión de nodos basándose en la suma del coste acumulado y una heurística que estima el coste restante hasta alcanzar la solución.

### Función `busqueda`
- **Descripción:**  
  - Es la función principal que ejecuta el proceso de búsqueda. Parte de una solución inicial vacía y, a través de un bucle, extrae el nodo con menor coste (más la heurística) de la cola de prioridad.
  - En cada iteración, se generan vecinos (nuevos estados) añadiendo una reina en la siguiente fila, y se verifica si alguno cumple el criterio de solución válida.
- **Dificultad de Traducción:**  
  - En Python, la flexibilidad de los tipos y la ausencia de declaraciones explícitas simplificaban el manejo de listas y diccionarios.
  - En C#, fue necesario definir de forma explícita los tipos y utilizar delegados para pasar funciones (como la heurística y el criterio de parada), lo que incrementó la complejidad del código.

### Función `calculo_de_prioridad`
- **Descripción:**  
  - Calcula la prioridad de cada nodo sumando el coste acumulado y el valor heurístico. En la versión A* se utiliza para determinar el orden de exploración de los nodos.
- **Dificultad de Traducción:**  
  - En Python, la operación es sencilla debido a la tipificación dinámica.
  - En C#, se tuvo que implementar una verificación para comprobar si la función heurística se ha proporcionado (evaluando si es `null`) y luego sumar el coste y la heurística. Además, se aprovechó el tipado fuerte para garantizar que los valores sean numéricos.

### Función `obtener_vecinos`
- **Descripción:**  
  - Genera nuevos estados (vecinos) a partir de la solución actual. Se calcula la siguiente fila disponible y se generan todas las posiciones posibles en esa fila.
- **Dificultad de Traducción:**  
  - La lógica es similar en ambos lenguajes, pero en C# fue necesario utilizar métodos de la clase `List<T>` (como `ToList()`) para crear copias de la lista de coordenadas y evitar modificaciones no deseadas en la solución original.
  - La manipulación de índices y la comprobación de límites también requirieron atención para asegurar la correcta traducción de la lógica.

### Funciones en la Clase `ColaDePrioridad`
- **Funciones Importantes:**  
  - **`anhadir`**: Inserta una solución en la cola de prioridad, controlando duplicados mediante un diccionario.
  - **`borrar`**: Marca una solución como eliminada, utilizando una constante (`REMOVED`) para ignorar elementos en la extracción.
  - **`obtener_siguiente`**: Extrae el siguiente nodo válido (con la menor prioridad) de la cola.
- **Dificultad de Traducción:**  
  - En Python se usaba `heapq` para la gestión de la cola, que requiere manipulación manual de listas.
  - En C#, se aprovechó la clase `PriorityQueue<TElement, TPriority>`, pero la integración con un diccionario auxiliar para evitar duplicados y gestionar elementos "eliminados" fue un reto, ya que se debieron adaptar los métodos de encolado y desencolado a la sintaxis y semántica de C#.

## Complejidades y Consideraciones Adicionales

- **Gestión de Memoria y Rendimiento:**  
  El uso de colecciones genéricas y el tipado fuerte en C# proporcionan un mejor manejo de la memoria y mayor rendimiento en comparación con Python.
  
- **Mantenimiento y Escalabilidad:**  
  La estructura del código en C# es más rígida, lo que facilita su mantenimiento y extensión en proyectos de mayor envergadura.

- **Flexibilidad en la Heurística:**  
  Aunque en el ejemplo la heurística es trivial (retornando 0), la arquitectura permite incorporar heurísticas más complejas para optimizar la búsqueda. La necesidad de definir delegados y comprobar su existencia fue una de las principales diferencias durante la traducción.

- **Integración con Estructuras de Datos Avanzadas:**  
  La traducción aprovechó nuevas estructuras como `PriorityQueue<TElement, int>`, disponible en versiones recientes de .NET, lo que representó una mejora respecto al manejo manual de la lista en Python, aunque implicó una reestructuración del código para adecuarse a esta API.

## Conclusión

La traducción del algoritmo A* para el problema de las N-Reinas de Python a C# implicó una adaptación detallada de la sintaxis y la lógica del algoritmo, manteniendo la funcionalidad original y aprovechando las ventajas del tipado fuerte y las colecciones genéricas. Las funciones clave —como `busqueda`, `calculo_de_prioridad`, `obtener_vecinos` y los métodos de la clase `ColaDePrioridad`— requirieron especial atención debido a las diferencias en la gestión de tipos, manejo de funciones y estructuras de datos entre ambos lenguajes.

Este trabajo demuestra cómo trasladar un algoritmo de un entorno dinámico a uno fuertemente tipado, optimizando aspectos técnicos y garantizando mayor robustez, rendimiento y mantenibilidad.

