<script setup>
import { ref, onMounted, computed } from "vue";
import { useClienteStore } from "../stores/cliente.store";

const clienteStore = useClienteStore();

// Filtros de búsqueda locales
const searchNombre = ref("");
const searchApellido = ref("");

// Para el debounce de la búsqueda
let searchTimeout = null;

const handleSearch = () => {
  clearTimeout(searchTimeout);
  searchTimeout = setTimeout(() => {
    // Resetear la página a 1 cuando se aplica una nueva búsqueda
    clienteStore.page = 1;
    clienteStore.fetchClientes(searchNombre.value, searchApellido.value);
  }, 500); // Debounce de 500ms
};

// Acceder a la acción setPage del store
const goToPage = (pageNumber) => {
  clienteStore.setPage(pageNumber);
  clienteStore.fetchClientes(searchNombre.value, searchApellido.value); // Pasar filtros actuales
};

// Acceder a la acción setPageSize del store
const changePageSize = (event) => {
  clienteStore.setPageSize(parseInt(event.target.value));
  clienteStore.fetchClientes(searchNombre.value, searchApellido.value); // Pasar filtros actuales
};

// Computed para los números de página a mostrar en la paginación (interfaz)
const getPageNumbers = computed(() => {
  const pages = [];
  const startPage = Math.max(1, clienteStore.page - 2);
  const endPage = Math.min(clienteStore.totalPages, clienteStore.page + 2);

  for (let i = startPage; i <= endPage; i++) {
    pages.push(i);
  }
  return pages;
});

// Al montar el componente, cargar la primera página de clientes
onMounted(() => {
  clienteStore.fetchClientes();
});
</script>

<template>
  <div class="min-h-screen bg-gradient-to-b from-gray-800 to-gray-400 text-white">
    <div class="container mx-auto p-4 sm:p-6 lg:p-8">
      <h2 class="text-3xl font-bold text-gray-800 mb-6">Listado de Clientes</h2>

      <div class="mb-6 flex flex-col sm:flex-row gap-4">
        <input
          type="text"
          v-model="searchNombre"
          @input="handleSearch"
          placeholder="Buscar por Nombre"
          class="flex-1 p-3 border border-gray-300 rounded-lg focus:ring-blue-500 focus:border-blue-500"
        />
        <input
          type="text"
          v-model="searchApellido"
          @input="handleSearch"
          placeholder="Buscar por Apellido"
          class="flex-1 p-3 border border-gray-300 rounded-lg focus:ring-blue-500 focus:border-blue-500"
        />
      </div>

      <div
        v-if="clienteStore.loading"
        class="text-center py-8 text-blue-600 text-lg font-semibold"
      >
        Cargando clientes...
      </div>
      <div
        v-else-if="clienteStore.error"
        class="text-center py-8 text-red-600 text-lg font-bold"
      >
        Error al cargar clientes: {{ clienteStore.error.message }}
      </div>
      <div
        v-else-if="!clienteStore.clientes || clienteStore.clientes.length === 0"
        class="text-center py-8 text-gray-500 text-lg italic"
      >
        No hay clientes para mostrar.
      </div>

      <div v-else class="overflow-x-auto bg-white shadow-lg rounded-lg">
        <table class="min-w-full divide-y divide-gray-200">
          <thead class="bg-gray-50">
            <tr>
              <th
                scope="col"
                class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
              >
                ID
              </th>
              <th
                scope="col"
                class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
              >
                Nombre
              </th>
              <th
                scope="col"
                class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
              >
                Apellido
              </th>
              <th
                scope="col"
                class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
              >
                Dirección
              </th>
              <th
                scope="col"
                class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
              >
                Celular
              </th>
            </tr>
          </thead>
          <tbody class="bg-white divide-y divide-gray-200">
            <tr
              v-for="cliente in clienteStore.clientes"
              :key="cliente.idCliente"
              class="hover:bg-gray-100"
            >
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                {{ cliente.idCliente }}
              </td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                {{ cliente.nombre }}
              </td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                {{ cliente.apellido }}
              </td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                {{ cliente.direccion }}
              </td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                {{ cliente.celular }}
              </td>
            </tr>
          </tbody>
        </table>
      </div>

      <div
        class="mt-8 flex flex-col sm:flex-row items-center justify-between space-y-4 sm:space-y-0"
      >
        <div class="flex items-center space-x-2">
          <span class="text-gray-700">Ítems por página:</span>
          <select
            :value="clienteStore.pageSize"
            @change="changePageSize"
            class="block w-20 p-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500"
          >
            <option :value="5">5</option>
            <option :value="10">10</option>
            <option :value="20">20</option>
            <option :value="50">50</option>
          </select>
        </div>

        <div class="flex items-center space-x-2">
          <button
            @click="goToPage(clienteStore.page - 1)"
            :disabled="clienteStore.page === 1"
            class="px-4 py-2 bg-blue-600 text-white rounded-lg shadow-md hover:bg-blue-700 disabled:bg-gray-300 disabled:cursor-not-allowed transition-colors duration-200"
          >
            Anterior
          </button>

          <span
            v-for="page in getPageNumbers"
            :key="page"
            :class="{
              'px-3 py-1 rounded-lg cursor-pointer transition-colors duration-200': true,
              'bg-blue-600 text-white font-semibold':
                page === clienteStore.page,
              'bg-gray-200 text-gray-700 hover:bg-gray-300':
                page !== clienteStore.page,
            }"
            @click="goToPage(page)"
          >
            {{ page }}
          </span>

          <button
            @click="goToPage(clienteStore.page + 1)"
            :disabled="clienteStore.page === clienteStore.totalPages"
            class="px-4 py-2 bg-blue-600 text-white rounded-lg shadow-md hover:bg-blue-700 disabled:bg-gray-300 disabled:cursor-not-allowed transition-colors duration-200"
          >
            Siguiente
          </button>
        </div>

        <div class="text-gray-700">
          Página {{ clienteStore.page }} de
          {{ clienteStore.totalPages }} (Total:
          {{ clienteStore.totalItems }} clientes)
        </div>
      </div>
    </div>
  </div>
</template>
