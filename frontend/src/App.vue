<script setup>
  import VerticalNav from './components/Layout/VerticalNav.vue';
  import Home from './Views/Home.vue';
  import { RouterView } from 'vue-router';
</script>

<template>
  <div class="flex">
    <VerticalNav />
    <main class="flex-1 min-h-screen">
      <RouterView />
    </main>
  </div>
</template>
