<template>
  <aside class="w-60 h-screen bg-gray-800 text-white flex flex-col gap-10
        md:flex fixed md:static z-50 shadow-[5px_0_5px_rgba(0,0,0,0.3)]">

    <div class="logo flex justify-center">
      <router-link to="/">
        <img class="aspect-3/2 object-cover w-42 h-42 mt-5" src="https://cdn-icons-png.flaticon.com/512/5277/5277459.png" alt="">
      </router-link>
    </div>
    <div class="text-2xl font-bold flex justify-center">
      Nombre taller
    </div>

    <nav class="flex flex-col px-6 gap-4">
      <router-link class="hover:bg-gray-700 rounded px-2 py-2" to="/">
        Inicio
      </router-link>

      <div>
        <a @click="toggleSubMenu('proyectos')" class="cursor-pointer hover:bg-gray-700 px-2 py-2 flex justify-between items-center border-b-1 rounded-none">
          <span>Orden</span>     
          <span :class="{ 'rotate-90': showProyectos }" class="transform transition-transform duration-100">▶</span>
        </a>
        <div v-if="showProyectos" class="pl-6 mt-2 flex flex-col gap-2">
          <router-link class="hover:bg-gray-600 rounded px-2 py-1 text-sm" to="/orden">
            Nueva
          </router-link>
          <a href="#" class="hover:bg-gray-600 rounded px-2 py-1 text-sm">Historial</a>
          <a href="#" class="hover:bg-gray-600 rounded px-2 py-1 text-sm">Listado</a>
        </div>
      </div>

      <div>
        <a @click="toggleSubMenu('finanzas')" class="cursor-pointer hover:bg-gray-700 border-b-1 px-2 py-2 flex justify-between items-center">
          <span>Finanzas</span>
          <span :class="{ 'rotate-90': showFinanzas }" class="transform transition-transform duration-200">▶</span>
        </a>
        <div v-if="showFinanzas" class="pl-6 mt-2 flex flex-col gap-2">
          <a href="#" class="hover:bg-gray-600 rounded px-2 py-1 text-sm">Ventas</a>
          <a href="#" class="hover:bg-gray-600 rounded px-2 py-1 text-sm">Gastos</a>
        </div>
      </div>

      <div>
        <a @click="toggleSubMenu('impresiones')" class="cursor-pointer hover:bg-gray-700 border-b-1 px-2 py-2 flex justify-between items-center">
          <span>Impresion 3D</span>
          <span :class="{ 'rotate-90': showImpresiones }" class="transform transition-transform duration-200">▶</span>
        </a>
        <div v-if="showImpresiones" class="pl-6 mt-2 flex flex-col gap-2">
          <a href="#" class="hover:bg-gray-600 rounded px-2 py-1 text-sm">Materiales</a>
          <a href="#" class="hover:bg-gray-600 rounded px-2 py-1 text-sm">Lista de pedidos</a>
        </div>
      </div>

      <a href="#" class="hover:bg-gray-700 rounded px-2 py-2">Configuración</a>
    </nav>
    <div class="p-6">
      </div>
  </aside>
</template>

<script setup>
import { ref } from 'vue'
let imagenEjemplo = ref("https://cdn-icons-png.flaticon.com/512/5277/5277459.png")
// Estado para controlar la visibilidad de cada submenú
const showProyectos = ref(false)
const showFinanzas = ref(false)
const showImpresiones = ref(false)

// Función para alternar la visibilidad de un submenú específico
const toggleSubMenu = (menu) => {
  if (menu === 'proyectos') {
    showProyectos.value = !showProyectos.value
    // cerrar si se abre otro menu
    showFinanzas.value = false
    showImpresiones.value = false
  } else if (menu === 'finanzas') {
    showFinanzas.value = !showFinanzas.value
    // cerrar si se abre otro menu
    showProyectos.value = false
    showImpresiones.value = false
  } else if (menu === 'impresiones'){
    showImpresiones.value = !showImpresiones.value
    // cerrar otros
    showFinanzas.value = false
    showProyectos.value = false
  }
}
</script>


<style scoped>
/* Tu estilo existente, no hay cambios necesarios específicos para el toggle aquí */
</style>