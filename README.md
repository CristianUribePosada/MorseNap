[Taller_07_Estructura_de_Datos_2026_1.pdf](https://github.com/user-attachments/files/28247273/Taller_07_Estructura_de_Datos_2026_1.pdf)

# MorseNap - Estación de Telegrafía Digital

**MorseNap** es una aplicación web desarrollada en **ASP.NET Core Razor Pages** diseñada para la traducción bidireccional y precisa de mensajes de texto y código Morse internacional. El sistema cuenta con procesamiento en tiempo real, generación interactiva de señales acústicas y persistencia de datos mediante archivos planos.

Este proyecto fue desarrollado como aplicación práctica para el curso de **Estructuras de Datos** en el **ITM**, aplicando conceptos avanzados de estructuras de datos jerárquicas no lineales y flujos de entrada/salida.

---

## Características Principales

* **Traducción Bidireccional Exacta:** Conversión fluida de texto plano a código Morse y decodificación inversa de señales respetando los estándares mundiales.
* **Generador de Audio Interactiva (AudioContext):** Reproducción acústica en tiempo real de los puntos y rayas con las frecuencias y tiempos oficiales de telegrafía.
* **Gestión de Archivos Planos (.txt):** Herramientas integradas para la importación y exportación de transmisiones directamente desde documentos de texto.
* **Validación Estricta de Sintaxis:** Detección automática de caracteres inválidos o errores de espaciado en la señal, informando detalladamente las fallas en la transmisión.
* **Interfaz Moderna y Adaptativa:** Diseño limpio en modo oscuro, completamente responsive y estilizado con componentes de **Bootstrap** y la tipografía **Poppins**.

---

## Detalles de Ingeniería (Estructuras de Datos)

Para cumplir estrictamente con las directivas del taller académico, la lógica de decodificación evita por completo el uso de vectores, matrices o búsquedas indexadas tradicionales en memoria. En su lugar, el núcleo del software se fundamenta en una estructura jerárquica construida desde cero:

* **`ÁrbolBinarioDeBúsqueda (MorseTree)`:** Los caracteres del alfabeto y los dígitos numéricos se organizan en nodos enlazados en memoria RAM. 
  * Un recorrido hacia la **izquierda** representa un punto (`.`).
  * Un recorrido hacia la **derecha** representa una raya (`-`).
    
---

## INSTRUCCIONES DE USO DE MORSENAP

**1. Reglas del Estándar Internacional de Espaciado**

Para garantizar una decodificación exitosa en la estación, los mensajes en código Morse deben ingresarse respetando los tiempos del estándar:
* **Entre Letras:** Se deben dejar exactamente **3 espacios en blanco**.
* **Entre Palabras:** Se deben dejar exactamente **7 espacios en blanco**.

---

**2. Flujo de Operación Recomendado (Paso a Paso)**

* **PASO A: Transmisión de Texto Plano a Morse**
  1. En el panel izquierdo (**Texto Plano**), escriba el mensaje que desea transmitir (Ej: `HOLA MUNDO`). El sistema acepta minúsculas y mayúsculas, normalizándolas internamente mediante `.ToUpper()`.
  2. Presione el botón **"Generar Código Morse"**.
  3. El sistema procesará el texto a través del árbol y proyectará inmediatamente el resultado en el panel derecho, formateado con los espacios reglamentarios.

* **PASO B: Decodificación y Reproducción Acústica**
  1. En el panel derecho (**Señal Morse**), digite o conserve una cadena de símbolos (Ej: `....   ---   .-..   .-`).
  2. Presione el botón **"Decodificar Señal"** para visualizar su traducción en texto plano.
  3. Si la señal es válida, se activará el botón del parlante. Presiónelo para escuchar la transmisión en ráfagas de audio reales generadas por el navegador.

* **PASO C: Procesamiento Masivo con Archivos Planos**
  1. Diríjase a la sección inferior de **Gestión de Archivos**.
  2. Haga clic en "Seleccionar archivo" y cargue un documento `.txt` de su computadora que contenga un texto o código para traducir.
  3. Presione **"Importar"** para volcar el contenido a los paneles de control.
  4. Tras realizar cualquier traducción, presione el botón verde **"Exportar .txt"** para descargar el resultado procesado en un nuevo archivo plano.

---

**3. Comportamiento de Acciones Especiales y Errores**
* **Tratamiento de Espacios en Blanco:** Siguiendo la simplificación lógica del proyecto, los caracteres espaciales del texto original se procesan y representan directamente como silencios estructurales en la cadena Morse (7 espacios) en lugar de usar etiquetas de texto pesadas.
* **Control de Transmisión Fallida:** Si se ingresa un símbolo no admitido o una secuencia de puntos/rayas que no corresponde a ningún nodo del árbol, la aplicación interrumpe el flujo seguro y despliega una alerta roja superior indicando el error de sintaxis exacto detectado.
