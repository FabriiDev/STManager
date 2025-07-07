import { createRouter, createWebHistory } from "vue-router";
import Home from "../Views/Home.vue"
import Orden from "../Views/Orden.vue";
import Cliente from "../Views/Cliente.vue";

const router = createRouter({
    history: createWebHistory(), // para las url de toda la vida con las /orden
    routes: [
        {
            path: "/", // ruta
            name: "Home",
            component: Home // componente que se renderiza
        },
        {
            path: "/orden",
            name: "Orden",
            component: Orden
        },
        {
            path: "/cliente",
            name: "Cliente",
            component: Cliente
        }
    ]
})

export default router;