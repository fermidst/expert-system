<template>
  <MainLayout>
    <template v-slot:icon>
      <v-icon>mdi-card-account-details</v-icon>
    </template>
    <template v-slot:title>Список клиентов</template>
    <div v-if="!clientListInfo.isLoading">
      <div v-bind:key="client.id" v-for="client in clientListInfo.value">
        {{ client }}
      </div>
    </div>
    <div v-else>
      <v-progress-circular indeterminate size="72" />
    </div>
  </MainLayout>
</template>

<script>
import MainLayout from "@/components/layout/MainLayout";
export default {
  name: "ClientList",
  components: { MainLayout },
  created() {
    this.$store.dispatch("fetchClientListInfo");
  },
  computed: {
    clientListInfo: {
      get() {
        return this.$store.state.client.clientListInfo;
      }
    }
  }
};
</script>

<style scoped></style>
