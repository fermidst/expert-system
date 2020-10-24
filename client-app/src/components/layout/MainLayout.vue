<template>
  <div>
    <v-app-bar app color="primary" dark>
      <div class="d-flex align-center">
        <slot name="icon">
          <v-icon>mdi-arrow-left</v-icon>
        </slot>
      </div>
      <v-spacer></v-spacer>
      <v-tabs>
        <v-tab to="/" v-if="loginStatus">Главная</v-tab>
        <v-tab to="/login" v-if="!loginStatus">Вход</v-tab>
        <v-tab to="/clientList" v-if="loginStatus">Список клиентов</v-tab>
        <v-tab to="/ticketList" v-if="loginStatus">Список путевок</v-tab>
      </v-tabs>
      <slot name="title">Title</slot>
      <v-btn v-if="loginStatus" class="ml-4" shaped rounded @click="logout"
        >Выход</v-btn
      >
    </v-app-bar>

    <v-main>
      <slot name="default" />
    </v-main>
  </div>
</template>

<script>
export default {
  name: "MainLayout",
  data() {
    return {
      tabs: [{}]
    };
  },
  computed: {
    loginStatus: {
      get() {
        return this.$store.getters.getLoginStatus;
      }
    }
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    }
  }
};
</script>

<style scoped></style>
