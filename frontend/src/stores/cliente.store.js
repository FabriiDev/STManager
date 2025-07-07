import axios from "axios";
import { defineStore } from "pinia";

export const useClienteStore = defineStore("cliente", {
    // state datos a compratir entre componentes
    state: () => ({
        clientes: [], // array de clientes obtenidos
        totalItems: 0, // todo lo de paginacion
        page: 1,
        pageSize: 10,
        loading: false, // un loading que va a ser true mientra se espera respuesta
        error: null, // error para guardar errores si los hay
    }),
     getters: {
        totalPages(state) { // Los getters reciben el estado como primer parámetro
            return Math.ceil(state.totalItems / state.pageSize);
        }
    },
    actions: {
        async fetchClientes(nombre = '', apellido = ''){ // funcion para llamar a la api
            this.loading = true; //activamos el loading
            this.error = null; // reset del error
            try{
                // peticion get al endpoint de cliente 
                const res = await axios.get('http://localhost:5107/api/cliente', {
                    params: {
                        page: this.page,
                        pageSize: this.pageSize,
                        nombre, // filtros del back
                        apellido,
                    },
                });
                this.clientes = res.data.items; // guardamos el listado paginado
                this.totalItems = res.data.totalItems; // total de registros para mostrar la paginacion
            }catch(err){
                this.error = err; 
                console.log('error al traer clientes', err) // mostramos el error si hay 
            }finally {
                this.loading = false; // desactivamos siempre el loading una vez obtenida o no la res
            }
        },
         setPage(newPage) {
            // Usa 'this' para acceder a 'totalPages' si está en el mismo store,
            // pero si totalPages es un getter, Pinia se encarga de que sea accesible como propiedad.
            // Para ser explícito, puedes usar this.totalPages si lo definiste como un método,
            // pero con un getter es mejor accederlo directamente como propiedad.
            if (newPage > 0 && newPage <= this.totalPages) { 
                this.page = newPage;
                this.fetchClientes(); 
            }
        },
        setPageSize(newSize) {
            this.pageSize = newSize;
            this.page = 1; 
            this.fetchClientes(); 
        },
    
    },
});