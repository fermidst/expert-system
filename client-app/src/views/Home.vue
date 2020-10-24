<template>
  <MainLayout class="home">
    <template v-slot:icon>
      <v-icon>mdi-bank</v-icon>
    </template>
    <HelloWorld msg="Welcome to Your Vue.js App" />
  </MainLayout>
</template>

<script>
// @ is an alias to /src
import HelloWorld from "@/components/HelloWorld.vue";
import MainLayout from "@/components/layout/MainLayout";

export default {
  name: "Home",
  components: {
    MainLayout,
    HelloWorld
  }
};
</script>
