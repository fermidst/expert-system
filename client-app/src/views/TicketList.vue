<template>
  <MainLayout>
    <template v-slot:icon>
      <v-icon>mdi-airplane</v-icon>
    </template>
    <template v-slot:title>Список путевок</template>
  </MainLayout>
</template>

<script>
import MainLayout from "@/components/layout/MainLayout";
export default {
  name: "TicketList",
  components: { MainLayout }
};
</script>

<style scoped></style>
